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
        bool openDoor = false;

        if (GameManager.instance.isTutorial)
        {
            if (GetComponent<PositionPower>() != null)
            {
                openDoor = TutorialManager.instance.positionDoor.canPositionOpenDoor;
            }
            else
            {
                openDoor = TutorialManager.instance.scaleDoor.canScaleOpenDoor;
            }
        }
        else
        {
            openDoor = (DoorManager.instance.NextDoor.canPositionOpenDoor && GetComponent<PositionPower>() != null) ||
            (DoorManager.instance.NextDoor.canScaleOpenDoor && GetComponent<ScalePower>() != null);
        }

        if (openDoor)
        {
            SoundManager.instance.PlaySound(SoundManager.instance.putKey);
            playerHaveKey = false;
            cameraKey.SetActive(false);
            if (GameManager.instance.isTutorial)
            {
                if (GetComponent<PositionPower>() != null)
                {
                    TutorialManager.instance.positionDoor.Open();
                }
                else
                {
                    TutorialManager.instance.scaleDoor.Open();
                }
            }
            else
            {
                DoorManager.instance.OpenDoor();
            }
        }
    }
}
