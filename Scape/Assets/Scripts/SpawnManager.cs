using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] roomPoints;

    public static SpawnManager instance;

    private void Start()
    {
        instance = this;
        for (int i = 0; i < roomPoints.Length; i++)
        {
            roomPoints[i].transform.GetChild(0).gameObject.SetActive(false);
            roomPoints[i].transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public Transform GetRoomPoint1(int room)
    {
        return roomPoints[room - 1].transform.GetChild(0);
    }

    public Transform GetRoomPoint2(int room)
    {
        return roomPoints[room - 1].transform.GetChild(1);
    }
}

