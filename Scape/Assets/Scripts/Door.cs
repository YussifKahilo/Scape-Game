using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject doorKey;
    [SerializeField] Animator anim;
    float dis = 2f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        doorKey.SetActive(false);
    }

    public void LocatePlayer()
    {
        bool scalePlayerHint = Vector3.Distance(doorKey.transform.position, GameManager.instance.scalePlayer.transform.position) < dis
            && GameManager.instance.scalePlayer.GetComponent<PlayerManager>().PlayerHaveKey;
        bool positionPlayerHint = Vector3.Distance(doorKey.transform.position, GameManager.instance.positionPlayer.transform.position) < dis
             && GameManager.instance.positionPlayer.GetComponent<PlayerManager>().PlayerHaveKey;

        GameManager.instance.scalePlayer.GetComponent<PlayerManager>().ShowHint(scalePlayerHint);
        GameManager.instance.positionPlayer.GetComponent<PlayerManager>().ShowHint(positionPlayerHint);
    }

    public void Open()
    {
        doorKey.SetActive(true);
        anim.SetBool("Open" , true);
    }

    public void Close()
    {
        anim.SetBool("Open", false);
    }
}
