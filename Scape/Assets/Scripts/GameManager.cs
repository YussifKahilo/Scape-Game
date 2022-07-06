using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject joystickPlayerPrefab;
    [SerializeField] GameObject keyboardPlayerPrefab;
    [SerializeField] Transform[] points;

    public GameObject scalePlayer;
    public GameObject positionPlayer;

    SettingsManager settingsManager;


    private void Start()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].gameObject.SetActive(false);
        }
        settingsManager = FindObjectOfType<SettingsManager>();
        StartGame();
    }

    public void SetSpitView()
    {
        scalePlayer.GetComponent<PlayerController>().cameraHolder.transform.GetChild(0).GetComponent<Camera>().rect = settingsManager.GetPlayer1SplitView();
        positionPlayer.GetComponent<PlayerController>().cameraHolder.transform.GetChild(0).GetComponent<Camera>().rect = settingsManager.GetPlayer2SplitView();
    }

    void StartGame()
    {
        positionPlayer = Instantiate(settingsManager.isPositionPlayerKeyboard ? keyboardPlayerPrefab : joystickPlayerPrefab
            , points[0].position , points[0].rotation);

        positionPlayer.GetComponent<PlayerController>().anim = positionPlayer.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        Destroy(positionPlayer.transform.GetChild(0).GetChild(1).gameObject);



        scalePlayer = Instantiate(settingsManager.isPositionPlayerKeyboard ? joystickPlayerPrefab : keyboardPlayerPrefab
            , points[1].position, points[1].rotation);

        scalePlayer.GetComponent<PlayerController>().anim = scalePlayer.transform.GetChild(0).GetChild(1).GetComponent<Animator>();
        Destroy(scalePlayer.transform.GetChild(0).GetChild(0).gameObject);

        SetSpitView();
    }

}
