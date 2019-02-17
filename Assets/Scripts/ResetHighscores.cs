using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetHighscores : MonoBehaviour
{
    public AudioClip clip;
    public void OnResetHighScoresButtonClick ()
    {
        int soundStatus = PlayerPrefs.GetInt("sound");
        if (soundStatus == 1)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(clip, 1f);
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("reset highscores");
    }
}
