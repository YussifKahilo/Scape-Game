using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSound : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SoundManager.instance.PlayFootSound();
    }
}
