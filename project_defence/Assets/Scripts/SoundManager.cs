using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

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
            if (arg0.name + "BGM" == bglist[i].name)
            {
                Debug.Log("bgm ���");
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

}