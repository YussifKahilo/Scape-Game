using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel, settingsPanel , controlListPanel;
    [SerializeField] Toggle verticalToggle , horizontalToggle;

    [SerializeField] Slider mouseSens, stickSens;
    [SerializeField] Text mouseSensText, stickSensText;

    bool isGamePaused = false;
    bool isSettingsPanelOpen = false;
    bool isControlListOpen = false;

    public static PauseManager instance;

    private void Awake()
    {
        SettingsManager.instance = FindObjectOfType<SettingsManager>();
        instance = this;
    }

    private void Start()
    {
        verticalToggle.isOn = SettingsManager.instance.isVerticalSplitScreen;
        horizontalToggle.isOn = !SettingsManager.instance.isVerticalSplitScreen;

        SetSens(true , SettingsManager.instance.mouseSens);
        SetSens(false, SettingsManager.instance.stickSens);
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

    public void Restart()
    {

    }

    void PausePanelView()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0 : 1;
        pausePanel.SetActive(isGamePaused);
        GameManager.instance.positionPlayer.GetComponent<PlayerController>().enabled = !isGamePaused;
        GameManager.instance.scalePlayer.GetComponent<PlayerController>().enabled = !isGamePaused;
    }

    public void ControlsListView()
    {
        isControlListOpen = !isControlListOpen;
        controlListPanel.SetActive(isControlListOpen);
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
        SettingsManager.instance.SetView(verticalToggle.isOn);
        GameManager.instance.SetSpitView();
    }

    public void MouseSensChanged(float value)
    {
        SetSens(true,(int) value);
    }

    public void StickSensChanged(float value)
    {
        SetSens(false , (int)value);
    }

    void SetSens(bool isKeyboard, int value)
    {
        SettingsManager.instance.SetSens(isKeyboard , value); 
        if (isKeyboard)
        {
            mouseSens.value = value;
            mouseSensText.text = "" + value;
        }
        else
        {
            stickSens.value = value;
            stickSensText.text = "" + value;
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
