using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject[] rooms;

    private Room currentRoom;
    private Room nextRoom;
    private Room afterNextRoom;

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

        if (afterNextRoom != null)
        {
            nextRoom = afterNextRoom;
            DoorManager.instance.NextDoor = nextRoom.RoomDoor;
        }
        else
        {
            if (room < rooms.Length)
            {
                nextRoom = Instantiate(rooms[room]).GetComponent<Room>();
                DoorManager.instance.NextDoor = nextRoom.RoomDoor;
            }
            else
            {
                nextRoom = null;
                PauseManager.instance.winPanel.SetActive(true);
            }
        }

        if (room + 1 < rooms.Length)
        {
            afterNextRoom = Instantiate(rooms[room + 1]).GetComponent<Room>();
        }
        else
        {
            afterNextRoom = null;
        }
    }

    public void LoadNewRoom()
    {
        DoorManager.instance.CloseDoor();
        GameManager.instance.CurrentRoom++;
        GameManager.instance.positionPlayer.GetComponent<PositionPower>().CancelPower();
        if (afterNextRoom == null)
        {
            return;
        }
        SetRoom(GameManager.instance.CurrentRoom);
    }

}
