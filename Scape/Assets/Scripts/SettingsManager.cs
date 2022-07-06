using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public bool isVerticalSplitScreen;

    public bool isPositionPlayerKeyboard;

    [SerializeField] Rect p1V, p2V, p1H, p2H;

    const string pp_spliteMode = "SPLITE_MODE", pp_MouseSens = "KSens", pp_StickSens = "JSens";

    public int stickSens = 5;

    public int mouseSens = 5;

    public static SettingsManager instance;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        isVerticalSplitScreen = PlayerPrefs.GetInt(pp_spliteMode, 1) == 1; //if split mode returns 1 it will be vertical

        SetSens(true, PlayerPrefs.GetInt(pp_MouseSens, 5));
        SetSens(false, PlayerPrefs.GetInt(pp_StickSens, 5));
    }

    public void SetView(bool value)
    {
        isVerticalSplitScreen = value;
        PlayerPrefs.SetInt(pp_spliteMode, isVerticalSplitScreen ? 1 : 0);
    }

    public Rect GetPlayer1SplitView()
    {
        return isVerticalSplitScreen ? p1V : p1H;
    }

    public Rect GetPlayer2SplitView()
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
