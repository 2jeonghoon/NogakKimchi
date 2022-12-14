using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // ����� �ͼ�
    public AudioMixer mixer;
    // �������
    public AudioSource bgSound;
    public AudioClip[] bglist;

    // ��ư ����
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
            //Debug.Log("����");
            instance = this;
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
                Debug.Log("bgm ���");
                BgSoundPlay(bglist[i]);
            }
        }
        //SceneLoad�� �� slider���� �����;� ��.
        mixer.SetFloat("Master", -10);
        mixer.SetFloat("BGsound", -10);
        mixer.SetFloat("SFX", -10);
        MasteraudioSlider.value = -10;
        BGSoundaudioSlider.value = -10;
        SFXaudioSlider.value = -10;
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

    // ��ư ���� ���
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
        float sound = MasteraudioSlider.value;

        if (sound == -40f) mixer.SetFloat("Master", -80);
        else mixer.SetFloat("Master", sound);
    }

    public void BGsoundAudioControl()
    {
        float sound = BGSoundaudioSlider.value;

        if (sound == -40f) mixer.SetFloat("BGsound", -80);
        else mixer.SetFloat("BGsound", sound);
    }
    public void SFXAudioControl()
    {
        float sound = SFXaudioSlider.value;

        if (sound == -40f) mixer.SetFloat("SFX", -80);
        else mixer.SetFloat("SFX", sound);
    }


    public void ToggledAudioVolum()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }

}