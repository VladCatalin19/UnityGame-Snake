using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButtonClickSoundPlay : MonoBehaviour
{
    public AudioClip clip;
    public void OnButtonClickSoundPlays()
    {
        int soundStatus = PlayerPrefs.GetInt("sound");
        if (soundStatus == 1)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(clip, 1f);
        }

    }
}
