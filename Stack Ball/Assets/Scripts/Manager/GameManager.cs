using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rand = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] List<GameObject> platformsPassed = new List<GameObject>();
    private bool isGameOver;
    private bool levelCompleted;
    private bool isGameStarted;
    private bool isContinued;
    private bool isRestarted;
    private int numberOfPlatforms;
    private int currentLevel;
    private int currentScore;
    private int platformPassed;

    public bool LevelCompleted { get => levelCompleted; set => levelCompleted = value; }
    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    public List<GameObject> PlatformsPassed { get => platformsPassed; set => platformsPassed = value; }
    public int NumberOfPlatforms { get => numberOfPlatforms; set => numberOfPlatforms = value; }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this) 
            Destroy(this.gameObject);
        else
            instance = this;
        StartTask();
        //PlayerPrefs.DeleteAll();
    }

    private void StartTask()
    {
        isGameStarted = false;
        isGameOver = false;
        isRestarted = false;
        currentScore = 0;
        platformPassed = 0;
        numberOfPlatforms = PlayerPrefs.GetInt("NumberOfPlatforms", 25);
        currentLevel = PlayerPrefs.GetInt("Level", 0);
        //LevelSpawner.instance.GenerateTheStack();
    }

    // Update is called once per frame
    void Update()
    {
        StartGame();
        CheckGameState();
        GameProgress();
    }

    private void GameProgress()
    {
        UIManager.instance.CurrentLevel.text = PlayerPrefs.GetInt("Level").ToString();
        UIManager.instance.NextLevel.text = (PlayerPrefs.GetInt("Level") + 1).ToString();

        if (currentScore > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
        }
        UIManager.instance.CurrentScoreText.text = currentScore.ToString();
        UIManager.instance.BestScoreText.text = "BEST: " + PlayerPrefs.GetInt("BestScore", 0).ToString();
    }

    private void StartGame()
    {
        if (Input.GetMouseButton(0) && !isGameStarted)
        {
            isGameStarted = true;
        }
    }

    public void IncreaseScore() {
        currentScore += 3;
    }

    private void CheckGameState() {
        if (isGameOver) 
        {
            UIManager.instance.ShowGameOverPanel(true);

            StartCoroutine(TapToRestart());
        }
        if (levelCompleted) 
        {
            UIManager.instance.ShowLevelCompletePanel(true);
            StartCoroutine(TapToContinue());
        }         
    }

    IEnumerator TapToRestart() 
    {
        yield return new WaitForSeconds(0.2f);
         if (Input.GetMouseButton(0))
            RestartLevel();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator TapToContinue() 
    {
        yield return new WaitForSeconds(0.2f);
         if (Input.GetMouseButton(0))
            NextLevel();
    }

    private void NextLevel()
    {
        PlayerPrefs.SetInt("Level", currentLevel + 1);
        PlayerPrefs.SetInt("NumberOfPlatforms",  numberOfPlatforms + Rand.Range(5, 11));
        SceneManager.LoadScene(0);
    }
}
