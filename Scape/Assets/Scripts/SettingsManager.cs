using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public bool isVerticalSplitScreen;

    public bool isPositionPlayerKeyboard;

    [SerializeField] Rect p1V, p2V, p1H, p2H;

    const string pp_spliteMode = "SPLITE_MODE", pp_MouseSens = "KSens", pp_StickSens = "JSens",
        pp_SoundMute = "SMute" , pp_MusicMute = "MMute" , pp_SoundVolume = "SVolume" , 
        pp_MusicVolume = "MVolume";

    public int stickSens = 5;

    public int mouseSens = 5;

    public int musicVolume;

    public int soundVolume;

    public bool musicNotMuted;

    public bool soundNotMuted;

    public static SettingsManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        isVerticalSplitScreen = PlayerPrefs.GetInt(pp_spliteMode, 1) == 1; //if split mode returns 1 it will be vertical

        SetSens(true, PlayerPrefs.GetInt(pp_MouseSens, 5));
        SetSens(false, PlayerPrefs.GetInt(pp_StickSens, 5));

        GetSounds();
    }

    void GetSounds()
    {
        musicVolume = PlayerPrefs.GetInt(pp_MusicVolume,10);
        soundVolume = PlayerPrefs.GetInt(pp_SoundVolume, 10);

        musicNotMuted = PlayerPrefs.GetInt(pp_MusicMute, 1) == 1;
        soundNotMuted = PlayerPrefs.GetInt(pp_SoundMute, 1) == 1;
    }

    public void SaveSound(int mV , int sV , bool mM , bool sM)
    {
        musicVolume = mV;
        soundVolume = sV;

        musicNotMuted = mM;
        soundNotMuted = sM;

        PlayerPrefs.SetInt(pp_MusicVolume, mV);
        PlayerPrefs.SetInt(pp_SoundVolume, sV);

        PlayerPrefs.SetInt(pp_MusicMute,mM ? 1 : 0);
        PlayerPrefs.SetInt(pp_SoundMute,sM ? 1 : 0);
    }

    public void SetView(bool value)
    {
        isVerticalSplitScreen = value;
        PlayerPrefs.SetInt(pp_spliteMode, isVerticalSplitScreen ? 1 : 0);
    }

    public Rect GetPlayer2SplitView()
    {
        return isVerticalSplitScreen ? p1V : p1H;
    }

    public Rect GetPlayer1SplitView()
    {
        return isVerticalSplitScreen ? p2V : p2H;
    }

    public void SetSens(bool isKeyboard , int value)
    {
        PlayerPrefs.SetInt(isKeyboard ? pp_MouseSens : pp_StickSens, value);
        if (isKeyboard)
        {
            mouseSens = value;
        }
        else
        {
            stickSens = value;
        }
    }

}
