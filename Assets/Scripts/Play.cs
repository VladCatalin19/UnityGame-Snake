using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    public AudioClip clip;
    public void OnPlayButtonClick()
    {
        int soundStatus = PlayerPrefs.GetInt("sound");
        if (soundStatus == 1)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(clip, 1f);
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("game");
    }
}
