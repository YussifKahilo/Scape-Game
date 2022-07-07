using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCube : MonoBehaviour
{
    [SerializeField] GameObject[] platForms;

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject platForm in platForms)
        {
            platForm.GetComponent<Rigidbody>().isKinematic = false;
            platForm.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
