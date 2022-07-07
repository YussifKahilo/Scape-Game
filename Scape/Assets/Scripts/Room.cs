using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] Door roomDoor;
    int numberOfPlayersInRoom = 0;

    internal Door RoomDoor { get => roomDoor; }

    internal int NumberOfPlayersInRoom { get => numberOfPlayersInRoom; set => numberOfPlayersInRoom = value; }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            numberOfPlayersInRoom++;
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
