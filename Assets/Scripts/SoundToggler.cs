using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundToggler : MonoBehaviour
{
    int soundStatus;

    public void OnSoundButtonClick ()
    {
        soundStatus = PlayerPrefs.GetInt("sound",1);
        if (soundStatus == 1)
        {
            soundStatus = 0;
        }
        else if (soundStatus == 0)
        {
            soundStatus = 1;
        }
        PlayerPrefs.SetInt("sound", soundStatus);
        Debug.Log(soundStatus);
    }
}
