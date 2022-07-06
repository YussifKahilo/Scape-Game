using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject[] rooms;

    private Room currentRoom;

    public static RoomManager instance;

    internal Room CurrentRoom { get => currentRoom; }

    private void Start()
    {
        instance = this;
    }

    public void SetRoom(int room)
    {
        currentRoom = rooms[room - 1].GetComponent<Room>();
        Instantiate(currentRoom.gameObject);
        if (room < rooms.Length)
        {
            Instantiate(rooms[room]);
        }
    }

}
