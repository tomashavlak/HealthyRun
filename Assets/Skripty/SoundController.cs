using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class SoundController : MonoBehaviour
{
    public GameObject sound;
    void Start()
    {
        InitSettings();
        
    }

    public static void InitSettings()
    {
        if (PlayerPrefs.GetString("sound") == "OFF")
        {
            GameSync.sound = false;
        }
        else
        {
            GameSync.sound = true;
        }
    }

    void Update()
    {
        if (GameSync.sound)
        {
            sound.GetComponent<AudioSource>().volume = 1;
        }
        else
        {
            sound.GetComponent<AudioSource>().volume = 0;
        }
    }
}
