using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel, settingsPanel;
    [SerializeField] Toggle verticalToggle , horizontalToggle;

    [SerializeField] Slider mouseSens, stickSens;
    [SerializeField] Text mouseSensText, stickSensText;

    bool isGamePaused = false;
    bool isSettingsPanelOpen = false;

    const string pp_MouseSens = "KSens" , pp_StickSens = "JSens";

    SettingsManager settingsManager;
    [SerializeField] GameManager gameManager;

    public static PauseManager instance;

    private void Awake()
    {
        settingsManager = FindObjectOfType<SettingsManager>();
        instance = this;
    }

    private void Start()
    {
        verticalToggle.isOn = settingsManager.isVerticalSplitScreen;
        horizontalToggle.isOn = !settingsManager.isVerticalSplitScreen;

        SetSens(true,  PlayerPrefs.GetInt(pp_MouseSens,5));
        SetSens(false, PlayerPrefs.GetInt(pp_StickSens, 5));
    }

    public void Pause()
    {
        if (isSettingsPanelOpen)
        {
            SettingsPanelView();
            return;
        }

        PausePanelView();
    }

    void PausePanelView()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0 : 1;
        pausePanel.SetActive(isGamePaused);
        gameManager.positionPlayer.GetComponent<PlayerController>().enabled = !isGamePaused;
        gameManager.scalePlayer.GetComponent<PlayerController>().enabled = !isGamePaused;
    }

    public void SettingsPanelView()
    {
        if (isSettingsPanelOpen)
            SaveSettings();
        isSettingsPanelOpen = !isSettingsPanelOpen;
        settingsPanel.SetActive(isSettingsPanelOpen);
    }

    public void SaveSettings()
    {
        settingsManager.SetView(verticalToggle.isOn);
        gameManager.SetSpitView();
    }

    public void MouseSensChanged(float value)
    {
        SetSens(true,(int) value);
    }

    public void StickSensChanged(float value)
    {
        SetSens(false , (int)value);
    }

    void SetSens(bool isKeyboard , int value)
    {
        PlayerPrefs.SetInt(isKeyboard? pp_MouseSens : pp_StickSens, value);
        if (isKeyboard)
        {
            if (settingsManager.isPositionPlayerKeyboard)
            {
                gameManager.positionPlayer.GetComponent<PlayerController>().senstivity = value;
            }
            else
            {
                gameManager.scalePlayer.GetComponent<PlayerController>().senstivity = value;
            }
            mouseSens.value = value;
            mouseSensText.text = "" + value ;
        }
        else
        {
            if (settingsManager.isPositionPlayerKeyboard)
            {
                gameManager.scalePlayer.GetComponent<PlayerController>().senstivity = value;
            }
            else
            {
                gameManager.positionPlayer.GetComponent<PlayerController>().senstivity = value;
            }
            stickSens.value = value;
            stickSensText.text = "" + value;
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
