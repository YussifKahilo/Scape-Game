using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject startGameBtn;

    [Header("Player Selecting")]
    [SerializeField] GameObject[] keyboard;
    [SerializeField] GameObject[] joystick;

    [Header("Ready Panels")]
    [SerializeField] GameObject player1ReadyPanel;
    [SerializeField] GameObject player2ReadyPanel;

    [Header("Ready Toggles")]
    [SerializeField] Toggle p1Toggle;
    [SerializeField] Toggle p2Toggle;

    [Header("Ready Hint Images")]
    [SerializeField] GameObject[] p1Images;
    [SerializeField] GameObject[] p2Images;

    [Header("Ready Text")]
    [SerializeField] Text p1ReadyText;
    [SerializeField] Text p2ReadyText;

    [Header("Panels")]
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject controlListPanel;

    [Header("Split Screen Mode Toggles")]
    [SerializeField] Toggle verticalToggle;
    [SerializeField] Toggle horizontalToggle;

    [Header("Players Sens")]
    [SerializeField] Slider mouseSens;
    [SerializeField] Slider stickSens;
    [SerializeField] Text mouseSensText, stickSensText;

    [Header("Sound Menu")]
    [SerializeField] Text soundText;
    [SerializeField] Text musicText;
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Toggle soundToggle;
    [SerializeField] Toggle musicToggle;

    [Header("Managers")]
    [SerializeField] GameObject settingsManager;
    [SerializeField] GameObject musicManager;
    [SerializeField] GameObject soundManager;

    private int keyboardCurrentPosition= 1 , joystickCurrentPosition =1;

    private bool isSettingsPanelOpen = false , isGamePanelOpen = false , isControlListOpen = false;

    public void OnBack(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            if (isGamePanelOpen)
            {
                GamePanelView();
            }
            else if (isSettingsPanelOpen)
            {
                SettingsPanelView();
            }else if (isControlListOpen)
            {
                ControlsListView();
            }
        }
    }

    public void OnReady(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            SetPlayerReady(value.control.device.ToString().StartsWith("K"));
        }        
    }

    public void OnLeftPlayerSelect(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            SwitchDevicePosition(value.control.device.ToString().StartsWith("K"), -1);
        }
    }

    public void OnRightPlayerSelect(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            SwitchDevicePosition(value.control.device.ToString().StartsWith("K"), 1);
        }
    }

    void Start()
    {
        CreateManagers();

        verticalToggle.isOn = SettingsManager.instance.isVerticalSplitScreen;
        horizontalToggle.isOn = !verticalToggle.isOn;

        SetSens(true, SettingsManager.instance.mouseSens);
        SetSens(false, SettingsManager.instance.stickSens);

        MusicToggleChanged(SettingsManager.instance.musicNotMuted);
        SoundToggleChanged(SettingsManager.instance.soundNotMuted);

        MusicVolumeChanged(SettingsManager.instance.musicVolume);
        SoundVolumeChanged(SettingsManager.instance.soundVolume);
    }

    void CreateManagers()
    {
        Instantiate(settingsManager);
        Instantiate(soundManager);
        Instantiate(musicManager);
    }

    private void Update()
    {
        SetReadyToggels();

        startGameBtn.SetActive(p1Toggle.isOn && p2Toggle.isOn);
    }

    public void PlayToggleSound()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
    }

    void SetPlayerReady(bool isKeyboard)
    {
        if (isKeyboard)
        {
            switch (keyboardCurrentPosition)
            {
                case 0:
                    if (p1Toggle.isOn)
                    {
                        p1Toggle.isOn = false;
                    }
                    else
                    {
                        p1Toggle.isOn = true;
                    }
                    break;
                case 1:
                    return;
                case 2:
                    if (p2Toggle.isOn)
                    {
                        p2Toggle.isOn = false;
                    }
                    else
                    {
                        p2Toggle.isOn = true;
                    }
                    break;
            }
        }
        else
        {
            switch (joystickCurrentPosition)
            {
                case 0:
                    if (p1Toggle.isOn)
                    {
                        p1Toggle.isOn = false;
                    }
                    else
                    {
                        p1Toggle.isOn = true;
                    }
                    break;
                case 1:
                    return;
                case 2:
                    if (p2Toggle.isOn)
                    {
                        p2Toggle.isOn = false;
                    }
                    else
                    {
                        p2Toggle.isOn = true;
                    }
                    break;
            }
        }
        SoundManager.instance.PlaySound(SoundManager.instance.playerReady);
    }

    void SetReadyToggels()
    {
        bool p1CanBeReady = joystickCurrentPosition == 0 ||
            keyboardCurrentPosition == 0;
        bool p2CanBeReady = joystickCurrentPosition == 2 ||
            keyboardCurrentPosition == 2;

        player1ReadyPanel.SetActive(p1CanBeReady);
        player2ReadyPanel.SetActive(p2CanBeReady);

        if (!p1CanBeReady)
        {
            p1Toggle.isOn = false;
        }

        if (!p2CanBeReady)
        {
            p2Toggle.isOn = false;
        }

        if (p1Toggle.isOn)
        {
            p1ReadyText.text = "Ready";
        }
        else
        {
            p1ReadyText.text = "Unready";
        }

        if (p2Toggle.isOn)
        {
            p2ReadyText.text = "Ready";
        }
        else
        {
            p2ReadyText.text = "Unready";
        }
    }

    public void StartGame()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
        SettingsManager.instance.isPositionPlayerKeyboard = keyboardCurrentPosition == 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GamePanelView()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
        ResetSelecting();
        isGamePanelOpen = !isGamePanelOpen;
        gamePanel.SetActive(isGamePanelOpen);
    }

    public void ControlsListView()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
        isControlListOpen = !isControlListOpen;
        controlListPanel.SetActive(isControlListOpen);
    }

    public void ResetSelecting()
    {
        joystickCurrentPosition = 1;
        keyboardCurrentPosition = 1;
        SwitchDevicePosition(true , 0);
        SwitchDevicePosition(false, 0);
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
        SettingsManager.instance.SaveSound((int)musicSlider.value , (int)soundSlider.value , musicToggle.isOn , soundToggle.isOn);
    }

    public void QuitGame()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
        Application.Quit();
    }

    void SwitchDevicePosition(bool isKeyboard, int position)
    {
        if (!isGamePanelOpen)
            return;

        SetDeviceImage(isKeyboard ,position);

        SetHintImage();
        
    }

    void SetDeviceImage(bool isKeyboard , int position)
    {
        if (isKeyboard)
        {
            if (keyboardCurrentPosition + position == joystickCurrentPosition && joystickCurrentPosition != 1
                || keyboardCurrentPosition + position < 0 || keyboardCurrentPosition + position > 2)
            {
                return;
            }

            keyboardCurrentPosition += position;

            for (int i = 0; i < 3; i++)
            {
                keyboard[i].SetActive(false);
            }
            keyboard[keyboardCurrentPosition].SetActive(true);
        }
        else
        {
            if (joystickCurrentPosition + position == keyboardCurrentPosition && keyboardCurrentPosition != 1
                || joystickCurrentPosition + position < 0 || joystickCurrentPosition + position > 2)
            {
                return;
            }

            joystickCurrentPosition += position;

            for (int i = 0; i < 3; i++)
            {
                joystick[i].SetActive(false);
            }
            joystick[joystickCurrentPosition].SetActive(true);
        }
        SoundManager.instance.PlaySound(SoundManager.instance.playerSelect);
    }

    void SetHintImage()
    {
        int imageIndex;
        if (Gamepad.current != null && (Gamepad.current.ToString().StartsWith("X") || Gamepad.current.ToString().StartsWith("x")))
        {
            imageIndex = 1;
        }
        else
        {
            imageIndex = 2;
        }

        for (int i = 0; i < 3; i++)
        {
            p1Images[i].SetActive(false);
            p2Images[i].SetActive(false);
        }

        if (keyboardCurrentPosition == 2)
        {
            p2Images[0].SetActive(true);
        }
        else if (keyboardCurrentPosition == 0)
        {
            p1Images[0].SetActive(true);
        }

        if (joystickCurrentPosition == 2)
        {
            p2Images[imageIndex].SetActive(true);
        }
        else if (joystickCurrentPosition == 0)
        {
            p1Images[imageIndex].SetActive(true);
        }

    }

    public void MouseSensChanged(float value)
    {
        SetSens(true, (int)value);
    }

    public void StickSensChanged(float value)
    {
        SetSens(false, (int)value);
    }

    void SetSens(bool isKeyboard, int value)
    {
        SettingsManager.instance.SetSens(isKeyboard ,value);
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

    public void MusicVolumeChanged(float value)
    {
        SettingsManager.instance.musicVolume = (int)value;
        musicText.text = (int)value + "";
    }

    public void SoundVolumeChanged(float value)
    {
        SettingsManager.instance.soundVolume = (int)value;
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
