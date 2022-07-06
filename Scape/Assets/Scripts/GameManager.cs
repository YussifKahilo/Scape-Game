using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject joystickPlayerPrefab;
    [SerializeField] GameObject keyboardPlayerPrefab;

    [SerializeField] Color positionColor;
    [SerializeField] Color scaleColor;

    public GameObject scalePlayer;
    public GameObject positionPlayer;

    int currentRoom = 1;

    public static GameManager instance;

    private void Start()
    {
        instance = this;
        StartGame();
    }

    public void SetSpitView()
    {
        scalePlayer.GetComponent<PlayerController>().CameraHolder.transform
            .GetChild(0).GetComponent<Camera>().rect = SettingsManager.instance.GetPlayer1SplitView();
        positionPlayer.GetComponent<PlayerController>().CameraHolder.transform
            .GetChild(0).GetComponent<Camera>().rect = SettingsManager.instance.GetPlayer2SplitView();
    }

    void SetPositionPlayer()
    {
        positionPlayer = Instantiate(SettingsManager.instance.isPositionPlayerKeyboard ?
            keyboardPlayerPrefab : joystickPlayerPrefab, SpawnManager.instance.GetRoomPoint1(currentRoom).position,
            SpawnManager.instance.GetRoomPoint1(currentRoom).rotation);

        positionPlayer.GetComponent<PlayerController>().Anim = positionPlayer.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        Destroy(positionPlayer.transform.GetChild(0).GetChild(1).gameObject);

        positionPlayer.GetComponent<PlayerController>().Power = positionPlayer.GetComponent<PositionPower>();

        positionPlayer.GetComponent<PlayerController>().CameraHolder.transform.GetChild(0).GetChild(0).GetChild(0)
            .GetComponent<UnityEngine.UI.Image>().color = positionColor;

        Destroy(positionPlayer.GetComponent<ScalePower>());
    }

    void SetScalePlayer()
    {
        scalePlayer = Instantiate(SettingsManager.instance.isPositionPlayerKeyboard ?
            joystickPlayerPrefab : keyboardPlayerPrefab, SpawnManager.instance.GetRoomPoint2(currentRoom).position,
            SpawnManager.instance.GetRoomPoint2(currentRoom).rotation);

        scalePlayer.GetComponent<PlayerController>().Anim = scalePlayer.transform.GetChild(0).GetChild(1).GetComponent<Animator>();
        Destroy(scalePlayer.transform.GetChild(0).GetChild(0).gameObject);

        scalePlayer.GetComponent<PlayerController>().Power = scalePlayer.GetComponent<ScalePower>();

        scalePlayer.GetComponent<PlayerController>().CameraHolder.transform.GetChild(0).GetChild(0).GetChild(0)
            .GetComponent<UnityEngine.UI.Image>().color = scaleColor;

        Destroy(GetComponent<UnityEngine.AI.NavMeshAgent>());
        Destroy(scalePlayer.GetComponent<PositionPower>());
    }

    void StartGame()
    {
        SetPositionPlayer();
        SetScalePlayer();
        SetRoom();
        SetSpitView();
    }

    private void Update()
    {
        if (SettingsManager.instance.isPositionPlayerKeyboard)
        {
            scalePlayer.GetComponent<PlayerController>().Senstivity = SettingsManager.instance.stickSens;
            positionPlayer.GetComponent<PlayerController>().Senstivity = SettingsManager.instance.mouseSens;
        }
        else
        {
            scalePlayer.GetComponent<PlayerController>().Senstivity = SettingsManager.instance.mouseSens;
            positionPlayer.GetComponent<PlayerController>().Senstivity = SettingsManager.instance.stickSens;
        }
    }

    void SetRoom()
    {
        RoomManager.instance.SetRoom(currentRoom);
        DoorManager.instance.NextDoor = RoomManager.instance.CurrentRoom.RoomDoor;
    }
}

