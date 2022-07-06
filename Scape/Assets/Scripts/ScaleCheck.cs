using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.name != transform.parent.name && collider.CompareTag("Ground"))
        {
            transform.parent.GetComponent<ScalePower>().SetCanScaleState(false);
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.name != transform.parent.name && collider.CompareTag("Ground"))
        {
            transform.parent.GetComponent<ScalePower>().SetCanScaleState(false);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.name != transform.parent.name && collider.CompareTag("Ground"))
        {
            transform.parent.GetComponent<ScalePower>().SetCanScaleState(true);
        }
    }
}
