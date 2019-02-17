using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundCheckmarkUpdate : MonoBehaviour
{
    int soundStatus;
    public Image image;

	// Use this for initialization
	void Start ()
    {
        soundStatus = PlayerPrefs.GetInt("sound",1);

    }
	
	// Update is called once per frame
	void Update ()
    {
        soundStatus = PlayerPrefs.GetInt("sound", 1);
        if (soundStatus == 1)
        {
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
        }
    }
}
