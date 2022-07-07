using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public Door scaleDoor , positionDoor;

    public GameObject firstRoomKey;

    public static TutorialManager instance;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Invoke("HideKey" , 1);
    }

    void HideKey()
    {
        firstRoomKey = RoomManager.instance.CurrentRoom.transform.GetChild(0).gameObject;
        firstRoomKey.SetActive(false);
    }

    private void Update()
    {
        scaleDoor.LocateScalePlayer();
        positionDoor.LocatePositionPlayer();
    }
}
