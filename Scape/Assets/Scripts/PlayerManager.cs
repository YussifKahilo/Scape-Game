using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private bool playerHaveKey = false;
    [SerializeField] GameObject cameraKey;
    [SerializeField] GameObject[] hintImages;
    [SerializeField] GameObject putKeyHint;


    internal bool PlayerHaveKey { get => playerHaveKey; }

    public void SetHintImage()
    {
        int imageIndex = 0;

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
            hintImages[i].SetActive(false);
        }

        if (SettingsManager.instance.isPositionPlayerKeyboard)
        {
            if (GetComponent<PositionPower>() != null)
            {
                hintImages[0].SetActive(true);
            }
            else
            {
                hintImages[imageIndex].SetActive(true);
            }
        }
        else
        {
            if (GetComponent<PositionPower>() != null)
            {
                hintImages[imageIndex].SetActive(true);
            }
            else
            {
                hintImages[0].SetActive(true);
            }
        }
    }

    public void PlayerTookKey()
    {
        playerHaveKey = true;
        cameraKey.SetActive(true);
    }

    public void ShowHint(bool show)
    {
        putKeyHint.SetActive(show);
    }

    public void UseKey()
    {
        if (!playerHaveKey)
            return;
        playerHaveKey = false;
        cameraKey.SetActive(false);
        DoorManager.instance.OpenDoor();
    }
}
