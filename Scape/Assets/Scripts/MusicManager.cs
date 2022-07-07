using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public static MusicManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<AudioSource>().mute = !SettingsManager.instance.musicNotMuted;
        GetComponent<AudioSource>().volume = (float)SettingsManager.instance.musicVolume / 10;
    }
}
