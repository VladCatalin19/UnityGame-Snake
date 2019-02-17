using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    public AudioClip clip;
    public void OnHighScoresButtonClick()
    {
        int soundStatus = PlayerPrefs.GetInt("sound");
        if (soundStatus == 1)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(clip, 1f);
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("high scores");
    }
}
