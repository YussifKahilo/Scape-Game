using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRoom : MonoBehaviour
{
    [SerializeField] Door door;

    private void OnTriggerEnter(Collider collider)
    {
        if (door.doorOpened)
        {
            door.Close();
        }
    }
}
