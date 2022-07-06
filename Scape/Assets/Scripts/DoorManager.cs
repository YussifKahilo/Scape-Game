using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private Door nextDoor;

    public static DoorManager instance;

    internal Door NextDoor { get => nextDoor; set => nextDoor = value; }

    private void Start()
    {
        instance = this;
    }

    public void OpenDoor()
    {
        nextDoor.Open();
    }

}
