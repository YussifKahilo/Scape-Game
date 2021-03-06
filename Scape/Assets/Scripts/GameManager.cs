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

    private int currentRoom = 1;

    public bool isTutorial = true;

    public static GameManager instance;

    internal int CurrentRoom { get => currentRoom; set => currentRoom = value; }

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

        positionPlayer.GetComponent<PlayerAnimation>().Anim = positionPlayer.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        Destroy(positionPlayer.transform.GetChild(0).GetChild(1).gameObject);

        positionPlayer.GetComponent<PlayerController>().Power = positionPlayer.GetComponent<PositionPower>();

        positionPlayer.GetComponent<PlayerController>().CameraHolder.transform.GetChild(0).GetChild(0).GetChild(0)
            .GetComponent<UnityEngine.UI.Image>().color = positionColor;

        positionPlayer.GetComponent<PlayerController>().CameraHolder.transform.GetChild(0).GetChild(0).SetParent(null);


        Destroy(positionPlayer.transform.GetChild(positionPlayer.transform.childCount - 1).gameObject);
        Destroy(positionPlayer.GetComponent<ScalePower>());
    }

    void SetScalePlayer()
    {
        scalePlayer = Instantiate(SettingsManager.instance.isPositionPlayerKeyboard ?
            joystickPlayerPrefab : keyboardPlayerPrefab, SpawnManager.instance.GetRoomPoint2(currentRoom).position,
            SpawnManager.instance.GetRoomPoint2(currentRoom).rotation);

        scalePlayer.GetComponent<PlayerAnimation>().Anim = scalePlayer.transform.GetChild(0).GetChild(1).GetComponent<Animator>();
        Destroy(scalePlayer.transform.GetChild(0).GetChild(0).gameObject);

        scalePlayer.GetComponent<PlayerController>().Power = scalePlayer.GetComponent<ScalePower>();

        scalePlayer.GetComponent<PlayerController>().CameraHolder.transform.GetChild(0).GetChild(0).GetChild(0)
            .GetComponent<UnityEngine.UI.Image>().color = scaleColor;

        scalePlayer.GetComponent<PlayerController>().CameraHolder.transform.GetChild(0).GetChild(0).SetParent(null);


        Destroy(scalePlayer.GetComponent<UnityEngine.AI.NavMeshAgent>());

        Destroy(scalePlayer.GetComponent<PositionPower>());

    }

    IEnumerator SetTutorial()
    {
        yield return new WaitForSeconds(3f);
        scalePlayer.GetComponent<Tutorial>().positionTurotial.SetActive(false);
        positionPlayer.GetComponent<Tutorial>().scaleTutorial.SetActive(false);

        positionPlayer.GetComponent<Tutorial>().positionTurotial.transform.parent.gameObject.SetActive(true);
        scalePlayer.GetComponent<Tutorial>().scaleTutorial.transform.parent.gameObject.SetActive(true);

    }

    void SetHintImages()
    {
        scalePlayer.GetComponent<PlayerManager>().SetHintImage();
        positionPlayer.GetComponent<PlayerManager>().SetHintImage();
    }

    void StartGame()
    {
        SetPositionPlayer();
        SetScalePlayer();

        Invoke("SetHintImages" , 0.5f);

        SetRoom();
        SetSpitView();

        StartCoroutine(SetTutorial());
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
    }

    public void EndTutorial()
    {
        positionPlayer.GetComponent<PositionPower>().CancelPower();

        Destroy(positionPlayer.GetComponent<Tutorial>().positionTurotial.transform.parent.gameObject);
        Destroy(positionPlayer.GetComponent<Tutorial>());

        Destroy(scalePlayer.GetComponent<Tutorial>().scaleTutorial.transform.parent.gameObject);
        Destroy(scalePlayer.GetComponent<Tutorial>());

        TutorialManager.instance.firstRoomKey.SetActive(true);
        Destroy(TutorialManager.instance);

        isTutorial = false;
    }
}

