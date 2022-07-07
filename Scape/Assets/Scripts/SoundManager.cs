using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip footSound , buttonClick , playerSelect , playerReady,
        scalePower , positionPowerEnd , positionPowerCancel , positionPowerTrace ,positionPowerElectric,
        positionPowerStill, doorClose , doorOpen, putKey ,takeKey , jump ,footStep;

    public static SoundManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        GetComponent<AudioSource>().mute = !SettingsManager.instance.soundNotMuted;
        GetComponent<AudioSource>().volume = (float)SettingsManager.instance.soundVolume / 10;

        transform.GetChild(0).GetComponent<AudioSource>().mute = !SettingsManager.instance.soundNotMuted;
        transform.GetChild(0).GetComponent<AudioSource>().volume = (float)SettingsManager.instance.soundVolume / 10;
    }

    public void PlayFootSound()
    {
        PlaySound(footSound);
    }

    public void PlaySound(AudioClip sound)
    {
        if (!SettingsManager.instance.soundNotMuted)
        {
            return;
        }
        audioSource.PlayOneShot(sound);
    }
}
