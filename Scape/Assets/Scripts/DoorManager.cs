using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] Door nextDoor;

    public static DoorManager instance;

    internal Door NextDoor { get => nextDoor; set => nextDoor = value; }

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (!GameManager.instance.isTutorial)
        {
            nextDoor.LocatePlayer();
        }
    }

    public void OpenDoor()
    {
        nextDoor.Open();
    }

    public void CloseDoor()
    {
        nextDoor.Close();
    }

}
