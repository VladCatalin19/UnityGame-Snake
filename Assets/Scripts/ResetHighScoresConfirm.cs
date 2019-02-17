using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetHighScoresConfirm : MonoBehaviour
{
    float time = 0;
	// Use this for initialization
	void Start ()
    {
        time = 1.5f;

        // Reset highscores
        for (int i=1; i<=5; i++)
        {
            name = "high_score_" + i.ToString();
            PlayerPrefs.SetInt(name, 0);
        }

        // Reset difficulties
        for (int i = 1; i <= 5; i++)
        {
            name = "diff_" + i.ToString();
            PlayerPrefs.SetInt(name, 0);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("settings");
        }
	}
}
