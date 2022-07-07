using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(new Vector3(0,20,0) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(SoundManager.instance.takeKey);
            collider.GetComponent<PlayerManager>().PlayerTookKey();
            Destroy(gameObject);
        }
    }
}
