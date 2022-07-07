using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject winPanel;

    [SerializeField] GameObject pausePanel, settingsPanel , controlListPanel;
    [SerializeField] Toggle verticalToggle , horizontalToggle;

    [SerializeField] Slider mouseSens, stickSens;
    [SerializeField] Text mouseSensText, stickSensText;

    [Header("Sound Menu")]
    [SerializeField] Text soundText;
    [SerializeField] Text musicText;
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Toggle soundToggle;
    [SerializeField] Toggle musicToggle;

    bool isGamePaused = false;
    bool isSettingsPanelOpen = false;
    bool isControlListOpen = false;

    public static PauseManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        verticalToggle.isOn = SettingsManager.instance.isVerticalSplitScreen;
        horizontalToggle.isOn = !SettingsManager.instance.isVerticalSplitScreen;

        SetSens(true , SettingsManager.instance.mouseSens);
        SetSens(false, SettingsManager.instance.stickSens);

        MusicToggleChanged(SettingsManager.instance.musicNotMuted);
        SoundToggleChanged(SettingsManager.instance.soundNotMuted);

        MusicVolumeChanged(SettingsManager.instance.musicVolume);
        SoundVolumeChanged(SettingsManager.instance.soundVolume);
    }

    private void Update()
    {
        Cursor.lockState = isGamePaused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void ToggleSound()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
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
        Time.timeScale = 1;
        SoundManager.instance.GetComponent<AudioSource>().Stop();
        SoundManager.instance.GetComponent<AudioSource>().clip = null;

        SoundManager.instance.transform.GetChild(0).GetComponent<AudioSource>().Stop();
        SoundManager.instance.transform.GetChild(0).GetComponent<AudioSource>().clip = null;
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void PausePanelView()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0 : 1;
        pausePanel.SetActive(isGamePaused);
        GameManager.instance.positionPlayer.GetComponent<PlayerController>().enabled = !isGamePaused;
        GameManager.instance.scalePlayer.GetComponent<PlayerController>().enabled = !isGamePaused;
    }

    public void ControlsListView()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
        isControlListOpen = !isControlListOpen;
        controlListPanel.SetActive(isControlListOpen);
    }

    public void SettingsPanelView()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
        if (isSettingsPanelOpen)
            SaveSettings();
        isSettingsPanelOpen = !isSettingsPanelOpen;
        settingsPanel.SetActive(isSettingsPanelOpen);
    }

    public void SaveSettings()
    {
        SettingsManager.instance.SetView(verticalToggle.isOn);
        SettingsManager.instance.SaveSound((int)musicSlider.value, (int)soundSlider.value, musicToggle.isOn, soundToggle.isOn);
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
        Time.timeScale = 1;
        SoundManager.instance.GetComponent<AudioSource>().Stop();
        SoundManager.instance.GetComponent<AudioSource>().clip = null;

        SoundManager.instance.transform.GetChild(0).GetComponent<AudioSource>().Stop();
        SoundManager.instance.transform.GetChild(0).GetComponent<AudioSource>().clip = null;
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void MusicVolumeChanged(float value)
    {
        SettingsManager.instance.musicVolume = (int)value;
        musicSlider.value = (int)value;
        musicText.text = (int)value + "";
    }

    public void SoundVolumeChanged(float value)
    {
        SettingsManager.instance.soundVolume = (int)value;
        soundSlider.value = (int)value;
        soundText.text = (int)value + "";
    }

    public void MusicToggleChanged(bool value)
    {
        SettingsManager.instance.musicNotMuted = value;
    }

    public void SoundToggleChanged(bool value)
    {
        SettingsManager.instance.soundNotMuted = value;
    }

}
