using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyChoser : MonoBehaviour
{

    public Dropdown difficulty;
    int val;
    int difficultyLevel;

    List<string> levels = new List<string> { "Easy", "Medium", "Hard", "Very Hard" };

    public void DifficultyIndex (int index)
    {
        Debug.Log(difficulty.value);
        PlayerPrefs.SetInt("difficulty", difficulty.value);
    }

    // Use this for initialization
    void Start ()
    {
        difficulty.AddOptions(levels);

        difficultyLevel = PlayerPrefs.GetInt("difficulty");
        difficulty.value = difficultyLevel;

        Button[] allButton = FindObjectsOfType<Button>();
        foreach (Button button in allButton)
        {
            button.onClick.AddListener(delegate {
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null, null);
            });
        }
    }
}
