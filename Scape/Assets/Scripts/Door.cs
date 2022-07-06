using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject doorKey;

    // Update is called once per frame
    void Update()
    {
        bool scalePlayerHint = Vector3.Distance(doorKey.transform.position, GameManager.instance.scalePlayer.transform.position) < 1
            && GameManager.instance.scalePlayer.GetComponent<PlayerManager>().PlayerHaveKey;
        bool positionPlayerHint = Vector3.Distance(doorKey.transform.position, GameManager.instance.positionPlayer.transform.position) < 1
             && GameManager.instance.positionPlayer.GetComponent<PlayerManager>().PlayerHaveKey;

        GameManager.instance.scalePlayer.GetComponent<PlayerManager>().ShowHint(scalePlayerHint);
        GameManager.instance.positionPlayer.GetComponent<PlayerManager>().ShowHint(positionPlayerHint);
    }

    public void Open()
    {
        doorKey.SetActive(true);
        Destroy(gameObject);
    }
}
