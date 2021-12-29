using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool isGameOver;
    private bool levelCompleted;
    private bool isGameStarted;
    private bool isContinued;
    private bool isRestarted;
    private int currentLevel;
    private int currentScore;
    private int platformPassed;

    public bool LevelCompleted { get => levelCompleted; set => levelCompleted = value; }
    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }

    // Start is called before the first frame update
    void Start()
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
        Debug.Log("haha");
        currentScore += 3;
    }

    private void CheckGameState() {
        if (isGameOver) 
        {
            UIManager.instance.ShowGameOverPanel(true);

            StartCoroutine(TapToContinue());
        }
        if (levelCompleted) 
        {
            UIManager.instance.ShowLevelCompletePanel(true);
            StartCoroutine(TapToContinue());
        }
            
    }

    IEnumerator TapToContinue() 
    {
        yield return new WaitForSeconds(0.2f);
         if (Input.GetMouseButton(0))
            RestartLevel();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
