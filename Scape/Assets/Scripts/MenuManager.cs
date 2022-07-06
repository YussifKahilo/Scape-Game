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
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject GamePanel;
    [SerializeField] GameObject settingsPanel;

    [Header("Split Screen Mode Toggles")]
    [SerializeField] Toggle verticalToggle;
    [SerializeField] Toggle horizontalToggle;

    private int keyboardCurrentPosition= 1 , joystickCurrentPosition =1;

    private SettingsManager settingsManager;

    private bool isSettingsPanelOpen = false , isGamePanelOpen = false;

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
        settingsManager = FindObjectOfType<SettingsManager>();
        verticalToggle.isOn = settingsManager.isVerticalSplitScreen;
        horizontalToggle.isOn = !settingsManager.isVerticalSplitScreen;
    }

    private void Update()
    {
        SetReadyToggels();

        startGameBtn.SetActive(p1Toggle.isOn && p2Toggle.isOn);
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
        settingsManager.isPositionPlayerKeyboard = keyboardCurrentPosition == 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GamePanelView()
    {
        ResetSelecting();
        isGamePanelOpen = !isGamePanelOpen;
        GamePanel.SetActive(isGamePanelOpen);
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
        if (isSettingsPanelOpen)
            SaveSettings();
        isSettingsPanelOpen = !isSettingsPanelOpen;
        settingsPanel.SetActive(isSettingsPanelOpen);
    }

    public void SaveSettings()
    {
        settingsManager.SetView(verticalToggle.isOn);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void SwitchDevicePosition(bool isKeyboard, int position)
    {
        if (!isGamePanelOpen)
            return;

        SetDeviceImage(isKeyboard ,position);

        SetHintImage(isKeyboard);
        
    }

    void SetDeviceImage(bool isKeyboard , int position)
    {
        if (isKeyboard)
        {
            if (keyboardCurrentPosition + position == joystickCurrentPosition && joystickCurrentPosition != 1)
            {
                return;
            }

            keyboardCurrentPosition += position;

            if (keyboardCurrentPosition <= 0)
            {
                keyboardCurrentPosition = 0;
            }
            else if (keyboardCurrentPosition >= 2)
            {
                keyboardCurrentPosition = 2;
            }
            for (int i = 0; i < 3; i++)
            {
                keyboard[i].SetActive(false);
            }
            keyboard[keyboardCurrentPosition].SetActive(true);
        }
        else
        {
            if (joystickCurrentPosition + position == keyboardCurrentPosition && keyboardCurrentPosition != 1)
            {
                return;
            }

            joystickCurrentPosition += position;

            if (joystickCurrentPosition <= 0)
            {
                joystickCurrentPosition = 0;
            }
            else if (joystickCurrentPosition >= 2)
            {
                joystickCurrentPosition = 2;
            }
            for (int i = 0; i < 3; i++)
            {
                joystick[i].SetActive(false);
            }
            joystick[joystickCurrentPosition].SetActive(true);
        }
    }

    void SetHintImage(bool isKeyboard)
    {
        int imageIndex;
        if (Gamepad.current.ToString().StartsWith("X") || Gamepad.current.ToString().StartsWith("x"))
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
}
