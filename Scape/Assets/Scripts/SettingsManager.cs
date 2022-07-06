using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public bool isVerticalSplitScreen;

    public bool isPositionPlayerKeyboard;

    [SerializeField] Rect p1V, p2V, p1H, p2H;

    const string pp_spliteMode = "SPLITE_MODE";

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        isVerticalSplitScreen = PlayerPrefs.GetInt(pp_spliteMode, 1) == 1; //if split mode returns 1 it will be vertical
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

}
