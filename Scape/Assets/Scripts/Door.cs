using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject doorKey;
    [SerializeField] Animator anim;
    public bool doorOpened = false;
    public bool canScaleOpenDoor = false;
    public bool canPositionOpenDoor = false;
    float dis = 2f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        doorKey.SetActive(false);
    }

    public void LocatePlayer()
    {
        LocateScalePlayer();
        LocatePositionPlayer();
    }

    public void LocateScalePlayer()
    {
        canScaleOpenDoor = Vector3.Distance(doorKey.transform.position, GameManager.instance.scalePlayer.transform.position) < dis
            && GameManager.instance.scalePlayer.GetComponent<PlayerManager>().PlayerHaveKey;

        GameManager.instance.scalePlayer.GetComponent<PlayerManager>().ShowHint(canScaleOpenDoor);
    }

    public void LocatePositionPlayer()
    {
        canPositionOpenDoor = Vector3.Distance(doorKey.transform.position, GameManager.instance.positionPlayer.transform.position) < dis
             && GameManager.instance.positionPlayer.GetComponent<PlayerManager>().PlayerHaveKey;

        GameManager.instance.positionPlayer.GetComponent<PlayerManager>().ShowHint(canPositionOpenDoor);
    }

    public void Open()
    {
        doorOpened = true;
        SoundManager.instance.PlaySound(SoundManager.instance.doorOpen);
        doorKey.SetActive(true);
        anim.SetBool("Open" , true);
    }

    public void Close()
    {
        doorOpened = false;
        SoundManager.instance.PlaySound(SoundManager.instance.doorClose);
        anim.SetBool("Open", false);
    }
}
