using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public SoundManager soundManager;
    public Slider slider;
    public AudioMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        slider = GetComponent<Slider>();
        mixer = soundManager.mixer;
        slider.value = 0;
        MasterAudioControl();
        BGsoundAudioControl();
        SFXAudioControl();
    }


    public void MasterAudioControl()
    {

        float sound = slider.value;

        if (sound == -40f) mixer.SetFloat("Master", -80);
        else mixer.SetFloat("Master", sound);
    }

    public void BGsoundAudioControl()
    {
        float sound = slider.value;

        if (sound == -40f) mixer.SetFloat("BGsound", -80);
        else mixer.SetFloat("BGsound", sound);
    }
    public void SFXAudioControl()
    {
        float sound = slider.value;

        if (sound == -40f) mixer.SetFloat("SFX", -80);
        else mixer.SetFloat("SFX", sound);
    }
}
