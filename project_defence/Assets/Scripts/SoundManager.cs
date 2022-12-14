using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // 오디오 믹서
    public AudioMixer mixer;
    // 배경음악
    public AudioSource bgSound;
    public AudioClip[] bglist;

    // 버튼 사운드
    [SerializeField]
    public AudioClip buttonSoundClip;

    public static SoundManager instance;

    public Slider MasteraudioSlider;
    public Slider BGSoundaudioSlider;
    public Slider SFXaudioSlider;

    private void Awake()
    {
        if (instance == null)
        {
            //Debug.Log("생성");
            instance = this;
            string name = "";
            Scene scene = SceneManager.GetActiveScene();

            Debug.Log(scene.name);
            DontDestroyOnLoad(instance);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bglist.Length; i++)
        {
            //if (arg0.name + "BGM" == bglist[i].name)
            if ("Map_1" == bglist[i].name)
            {
                Debug.Log("bgm 재생");
                BgSoundPlay(bglist[i]);
            }
        }
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audiosource.clip = clip;
        audiosource.Play();

        Destroy(go, clip.length);
    }

    // 버튼 사운드 출력
    public void onClickButton()
    {
        Debug.Log("button click");
        GameObject go = new GameObject("ButtonSound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audiosource.clip = buttonSoundClip;
        audiosource.Play();
        Destroy(go, buttonSoundClip.length);
    }

    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGsound")[0];
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 0.1f;
        bgSound.Play();
    }


    public void MasterAudioControl()
    {
        MasteraudioSlider = GameObject.FindGameObjectWithTag("MasterSlider").GetComponent<Slider>();
        
        float sound = MasteraudioSlider.value;

        if (sound == -40f) mixer.SetFloat("Master", -80);
        else mixer.SetFloat("Master", sound);
    }

    public void BGsoundAudioControl()
    {
        BGSoundaudioSlider = GameObject.FindGameObjectWithTag("BGSoundSlider").GetComponent<Slider>();
        float sound = BGSoundaudioSlider.value;

        if (sound == -40f) mixer.SetFloat("BGsound", -80);
        else mixer.SetFloat("BGsound", sound);
    }
    public void SFXAudioControl()
    {
        SFXaudioSlider = GameObject.FindGameObjectWithTag("SFXSlider").GetComponent<Slider>();
        float sound = SFXaudioSlider.value;

        if (sound == -40f) mixer.SetFloat("SFX", -80);
        else mixer.SetFloat("SFX", sound);
    }


    public void ToggledAudioVolum()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }

}