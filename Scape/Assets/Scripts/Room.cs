using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] Door roomDoor;
    int numberOfPlayersInRoom = 0;
    [SerializeField] bool isLast= false;

    //public bool lastRoom = false;

    internal Door RoomDoor { get => roomDoor; }

    internal int NumberOfPlayersInRoom { get => numberOfPlayersInRoom; set => numberOfPlayersInRoom = value; }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            numberOfPlayersInRoom++;
            if (numberOfPlayersInRoom >= 2)
            {
                if (isLast) {
                    PauseManager.instance.winPanel.SetActive(true);
                }
                if (GameManager.instance.isTutorial)
                {
                    GameManager.instance.EndTutorial();
                }
                else
                {
                    RoomManager.instance.LoadNewRoom();
                }
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            numberOfPlayersInRoom--;
        }
    }
}
