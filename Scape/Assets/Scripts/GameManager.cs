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

    public GameObject player1;
    public GameObject player2;

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
        player1.GetComponent<PlayerController>().cameraHolder.transform.GetChild(0).GetComponent<Camera>().rect = settingsManager.GetPlayer1SplitView();
        player2.GetComponent<PlayerController>().cameraHolder.transform.GetChild(0).GetComponent<Camera>().rect = settingsManager.GetPlayer2SplitView();
    }

    void StartGame()
    {
        player1 = Instantiate(settingsManager.isPlayer1Keyboard ? keyboardPlayerPrefab : joystickPlayerPrefab
            , points[0].position , points[0].rotation);

        player2 = Instantiate(settingsManager.isPlayer1Keyboard ? joystickPlayerPrefab : keyboardPlayerPrefab
            , points[1].position, points[1].rotation);

        SetSpitView();
    }

}
