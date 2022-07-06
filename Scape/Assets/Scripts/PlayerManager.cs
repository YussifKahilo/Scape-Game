using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private bool playerHaveKey = false;
    [SerializeField] GameObject cameraKey;

    [SerializeField] GameObject putKeyHint;

    internal bool PlayerHaveKey { get => playerHaveKey; }

    public void PlayerTookKey()
    {
        playerHaveKey = true;
        //cameraKey.SetActive(true);
    }

    public void ShowHint(bool show)
    {
        //putKeyHint.SetActive(show);
    }

    public void UseKey()
    {
        playerHaveKey = false;
        //cameraKey.SetActive(false);
    }
}
