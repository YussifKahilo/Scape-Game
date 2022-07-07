using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject[] rooms;

    private Room currentRoom;
    private Room nextRoom;

    public static RoomManager instance;

    internal Room CurrentRoom { get => currentRoom; }

    private void Start()
    {
        instance = this;
    }

    public void SetRoom(int room)
    {
        if (currentRoom == null)
        {
            currentRoom = Instantiate(rooms[room - 1]).GetComponent<Room>();
        }
        else
        {
            currentRoom = nextRoom;
        }

        if (room < rooms.Length)
        {
            nextRoom = Instantiate(rooms[room]).GetComponent<Room>();
            DoorManager.instance.NextDoor = nextRoom.RoomDoor;
        }
        else
        {
            nextRoom = null;
        }
    }

    void LoadNewRoom()
    {
        DoorManager.instance.CloseDoor();
        GameManager.instance.CurrentRoom++;
        GameManager.instance.positionPlayer.GetComponent<PositionPower>().CancelPower();
        SetRoom(GameManager.instance.CurrentRoom);
    }

    private void Update()
    {
        if (nextRoom != null && nextRoom.NumberOfPlayersInRoom == 2)
        {
            LoadNewRoom();
        }
    }

}
