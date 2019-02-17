using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    // Current score
    public Text lastScoreText;
    int score;

    // High scores
    public Text highScoreText;
    int[] highscore = new int[5];

    // Difficulties
    public Text difficultiesLevelText;
    int[] diffs = new int[5];

    public AudioClip audioClip;
    // Play sound
    void PlaySound()
    {
        int soundStatus = PlayerPrefs.GetInt("sound");
        if (soundStatus == 1)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioClip);
        }

    }

    void Start()
    {
        PlaySound();
        // Display current score
        score = PlayerPrefs.GetInt("last_score");
        //Debug.Log(PlayerPrefs.GetInt("Last Score"));
        lastScoreText.text = score.ToString();

        // Display high scores
        for (int i = 0; i < highscore.GetLength(0); i++)
        {
            string name = "high_score_" + (i+1).ToString();
            highscore[i] = PlayerPrefs.GetInt(name );
            //Debug.Log(highscore[i]);
        }
        highScoreText.text = highscore[0].ToString() + "\n" + highscore[1].ToString() + "\n" + highscore[2].ToString() + "\n" + highscore[3].ToString() + "\n" + highscore[4].ToString();

        // Display difficulties
        for (int i=0; i<diffs.GetLength(0); i++)
        {
            string name = "diff_" + (i+1).ToString();
            diffs[i] = PlayerPrefs.GetInt(name);
        }


        difficultiesLevelText.text = "";
        for (int i = 0; i < diffs.GetLength(0); i++)
        {
            switch (diffs[i])
            {
                case 0:
                    {
                        if (highscore[i] != 0)
                            difficultiesLevelText.text += "Easy";
                        break;
                    }
                case 1:
                    {
                        difficultiesLevelText.text += "Medium";
                        break;
                    }

                case 2:
                    {
                        difficultiesLevelText.text += "Hard";
                        break;
                    }

                case 3:
                    {
                        difficultiesLevelText.text += "Very Hard";
                        break;
                    }
            }
            difficultiesLevelText.text += "\n";
        }
    }
}
