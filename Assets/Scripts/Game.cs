using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class Game : MonoBehaviour
{
    Transform snakeTransform;
    float lastMove;
    public float timeInBetweenMoves;
    Vector3 direction;

    int[,] grid = new int[20, 10];
    int snakeScore = 1;
    int snakeX = 0;
    int snakeY = 4;
    bool hasLost;
    bool isPaused;

    public Text scoreText;
    public Text diffText;
    public Text pauseText;

    public GameObject applePrefab;

    public AudioClip audioClip;

    // Play sound
    void PlaySound()
    {
        int soundStatus = PlayerPrefs.GetInt("sound");
        if (soundStatus == 1)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioClip, 1f);
        }
    }

    // Load High Scores
    int[] highscore = new int[5];
    void LoadHighScores()
    {
        for (int i = 0; i < highscore.GetLength(0); i++)
        {
            string name = "high_score_" + (i+1).ToString();
            highscore[i] = PlayerPrefs.GetInt(name);
        }
    }

    // Load Difficulties
    int[] diffs = new int[5];
    void LoadDifficulties ()
    {
        for (int i=0; i<diffs.GetLength(0); i++)
        {
            string name = "diff_" + (i + 1).ToString();
            diffs[i] = PlayerPrefs.GetInt(name);
        }
    }

    // Insert scores
    void insertScore (int score, int diff)
    {
        // Puts 1st score
        if (score >= highscore[0])
        {
            highscore[4] = highscore[3];
            highscore[3] = highscore[2];
            highscore[2] = highscore[1];
            highscore[1] = highscore[0];
            highscore[0] = score;

            diffs[4] = diffs[3];
            diffs[3] = diffs[2];
            diffs[2] = diffs[1];
            diffs[1] = diffs[0];
            diffs[0] = diff;
            return;
        }

        // Puts 2nd score
        if (score >= highscore[1] && score < highscore[0])
        {
            highscore[4] = highscore[3];
            highscore[3] = highscore[2];
            highscore[2] = highscore[1];
            highscore[1] = score;

            diffs[4] = diffs[3];
            diffs[3] = diffs[2];
            diffs[2] = diffs[1];
            diffs[1] = diff;
            return;
        }

        // Puts 3rd score
        if (score >= highscore[2] && score < highscore[1])
        {
            highscore[4] = highscore[3];
            highscore[3] = highscore[2];
            highscore[2] = score;

            diffs[4] = diffs[3];
            diffs[3] = diffs[2];
            diffs[2] = diff;
            return;
        }

        // Puts 4th score
        if (score >= highscore[3] && score < highscore[2])
        {
            highscore[4] = highscore[3];
            highscore[3] = score;

            diffs[4] = diffs[3];
            diffs[3] = diff;
            return;
        }

        // Puts 5th score
        if (score >= highscore[4] && score < highscore[3])
        {
            highscore[4] = score;

            diffs[4] = diff;
            return;
        }
    }

    // Get difficulty
    void GetDifficulty ()
    {
        int diff = PlayerPrefs.GetInt("difficulty",1);
        switch (diff)
        {
            case 0:
            {
                timeInBetweenMoves = 0.3f;
                diffText.text = "Difficulty: Easy";
                break;
            }
            case 1:
            {
                timeInBetweenMoves = 0.1f;
                diffText.text = "Difficulty: Medium";
                break;
            }

            case 2:
            {
                timeInBetweenMoves = 0.075f;
                diffText.text = "Difficulty: Hard";
                break;
            }

            case 3:
            {
                timeInBetweenMoves = 0.05f;
                diffText.text = "Difficulty: Very Hard";
                break;
            }

        }
    }

    // Pause the game
    bool SetPaused (bool isPaused)
    {
        isPaused = !isPaused;
        if (isPaused == true)
        {
            pauseText.text = "Paused";
        }
        else
        {
            pauseText.text = "";
        }

        return isPaused;
    }

    // Use this for initialization
    void Start () {
        // Get difficulty
        GetDifficulty();
        //Debug.Log(PlayerPrefs.GetInt("difficulty"));
        //Debug.Log(timeInBetweenMoves);

        // Creates snake
        snakeTransform = GetComponent<Transform>();
        direction = Vector3.right;
        grid[snakeX, snakeY] = snakeScore;

        // Displays score
        scoreText.text = "Score: " + snakeScore.ToString();

        // Display when paused
        pauseText.text = "";

        // Sets first apple
        int x = UnityEngine.Random.Range(0, grid.GetLength(0));
        int y = UnityEngine.Random.Range(0, grid.GetLength(1));
        grid[x, y] = -1;
        GameObject apple = Instantiate(applePrefab) as GameObject;
        apple.transform.position = new Vector3(x, y, 0);
        apple.name = "Apple";

        // Load high scores
        LoadHighScores();

        // Load difficulties
        LoadDifficulties();

    }

	// Update is called once per frame
	void Update ()
    {
        // Check
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = SetPaused(isPaused);
        }

        if (isPaused)
        {
            return;
        }

        // Check if game ended
        if (hasLost || (snakeScore == grid.GetLength(0) * grid.GetLength(1)))
        {
            // Add scores to high score board
            insertScore(snakeScore, PlayerPrefs.GetInt("difficulty"));

            for (int i=0; i<highscore.GetLength(0); i++)
            {
                string name = "high_score_" + (i+1).ToString();
                PlayerPrefs.SetInt(name, highscore[i]);

                name = "diff_" + (i + 1).ToString();
                PlayerPrefs.SetInt(name, diffs[i]);
            }
            PlayerPrefs.SetInt("last_score", snakeScore);

            // Load next scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("lose");
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && direction != Vector3.down)
        {
            //Debug.Log("Move Up");
            direction = Vector3.up;
        }

        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && direction != Vector3.up)
        {
            //Debug.Log("Move Down");
            direction = Vector3.down;
        }

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && direction != Vector3.right)
        {
            //Debug.Log("Move Left");
            direction = Vector3.left;
        }

        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && direction != Vector3.left)
        {
            //Debug.Log("Move Right");
            direction = Vector3.right;
        }
        //Debug.Log("-------------------------");

        // Wait for time between moves
        if (Time.time - lastMove > timeInBetweenMoves)
        {
            for (int i = 0; i < grid.GetLength(0); ++i)
            {
                for (int j = 0; j < grid.GetLength(1); ++j)
                {
                    if (grid[i, j] > 0)
                    {
                        grid[i, j]--;
                    }

                }
            }
            lastMove = Time.time;

            // Create parts of snake that did not move
            if (!(snakeX >= grid.GetLength(0) || snakeX < 0 || snakeY >= grid.GetLength(1) || snakeY < 0))
            {
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.position = new Vector3(snakeX, snakeY, 0);
                go.name = snakeX.ToString() + snakeY.ToString();
                //Debug.Log("Creating " + go.name);
            }

            // Find where to place next snake part
            if (direction.x == 1)
            {
                snakeX++;
            }

            if (direction.x == -1)
            {
                snakeX--;
            }

            if (direction.y == 1)
            {
                snakeY++;
            }

            if (direction.y == -1)
            {
                snakeY--;
            }

            // If out of bounds
            if (snakeX >= grid.GetLength(0) || snakeX < 0 || snakeY >= grid.GetLength(1) || snakeY < 0)
            {
                Debug.Log("Out of bounds!");
                hasLost = true;
                return;
            }
            else
            {
                // If apple is eaten
                if (grid[snakeX, snakeY] == -1)
                {
                    // Increase snake size
                    snakeScore++;
                    scoreText.text = "Score: " + snakeScore.ToString();
                    //Debug.Log("Apple eaten!");
                    GameObject toDestroy = GameObject.Find("Apple");
                    Destroy(toDestroy);
                    for (int i = 0; i < grid.GetLength(0); ++i)
                    {
                        for (int j = 0; j < grid.GetLength(1); ++j)
                        {
                            if (grid[i,j] > 0)
                                grid[i, j]++;
                        }
                    }

                    // Create new apple
                    int x, y;
                    do
                    {
                        x = UnityEngine.Random.Range(0, grid.GetLength(0));
                        y = UnityEngine.Random.Range(0, grid.GetLength(1));
                    }
                    while (grid[x, y] != 0);

                    grid[x, y] = -1;
                    GameObject appleRand = Instantiate(applePrefab) as GameObject;
                    appleRand.transform.position = new Vector3(x, y, 0);
                    appleRand.name = "Apple";

                    PlaySound();
                }

                // If snake eats itself
                else if (grid[snakeX, snakeY] != 0)
                {
                    Debug.Log("Game lost!");
                    hasLost = true;
                    return;
                }

                // Move snake
                snakeTransform.position += direction;
                grid[snakeX, snakeY] = snakeScore;
            }
            for (int i = 0; i < grid.GetLength(0); ++i)
            {
                for (int j = 0; j < grid.GetLength(1); ++j)
                {
                    if (grid[i, j] == 0)
                    {
                        // Destroy last snake part
                        string find = i.ToString() + j.ToString();
                        //Debug.Log("Searching " + find);
                        GameObject toDestroy = GameObject.Find(find);
                        if (toDestroy != null)
                        {
                            Destroy(toDestroy);
                            //Debug.Log("Destroying " + find);
                        }
                        else
                        {
                            //Debug.Log("No object " + find + " found");
                        }

                    }
                }
            }
        }
    }

    public AudioClip clip;
    public void OnBackButtonClick()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
        UnityEngine.SceneManagement.SceneManager.LoadScene("menu");
    }

    public void OnPausedButtonClick()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
        isPaused = SetPaused(isPaused);
    }
}
