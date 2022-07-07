using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.name != transform.parent.name && !collider.CompareTag("Room Trigger"))
        {
            transform.parent.GetComponent<PlayerController>().SetGroundedState(true);
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.name != transform.parent.name && !collider.CompareTag("Room Trigger"))
        {
            transform.parent.GetComponent<PlayerController>().SetGroundedState(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.name != transform.parent.name && !collider.CompareTag("Room Trigger"))
        {
            transform.parent.GetComponent<PlayerController>().SetGroundedState(false);
        }
    }
}
