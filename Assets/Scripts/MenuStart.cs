using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStart : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        string name;

		// Set difficulties high scores
        for (int i=1;i<=5;i++)
        {
            name = "diff_"+i.ToString();
            if (PlayerPrefs.HasKey(name) == false)
            {
                PlayerPrefs.SetInt(name,0);
            }
        }

        // Set high scores
        for (int i = 1; i <= 5; i++)
        {
            name = "high_score_" + i.ToString();
            if (PlayerPrefs.HasKey(name) == false)
            {
                PlayerPrefs.SetInt(name, 0);
            }
        }

        // Set sound
        if (PlayerPrefs.HasKey("sound") == false)
        {
            PlayerPrefs.SetInt("sound", 1);
        }

        // Set game difficulty
        if (PlayerPrefs.HasKey("difficulty") == false)
        {
            PlayerPrefs.SetInt("difficulty", 1);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
