using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

/************************
 * Script managing the game
 * 
 * 
 * Pacifica Morrow
 * 11.17.2024
 * Version1
 ************************/

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TextMeshProUGUI scoreboardText, timeRemainingTextTMP;
    [SerializeField] private float scoreAdded;
    [SerializeField] private GameObject toggleGroup;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject enableManager;
    [SerializeField] private GameObject TimeRemainingTextGaOb;
    public static bool gameOver;
    private static float score;
    private bool gameStarted;
    private AudioSource audioSource;
    private int timeRemaining = 60;
    private bool timedGame;
    private bool gameEnd;
    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(this);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /*private void Update()
    {
        DisplayUI();
    }

    private void DisplayUI()
    {

    } */

    private void TimeCountdown()
    {
        timeRemaining--;
        timeRemainingTextTMP.text = ("" + timeRemaining);
        if (timeRemaining < 0)
        {
            EndGame();
        }
        Debug.Log("Counting Down");
    }

    private void TimeCountup()
    {
        timeRemaining++;
        timeRemainingTextTMP.text = ("" + timeRemaining);
    }

    public void StartGame()
    {
        audioSource.Play();
        startButton.SetActive(false);
        toggleGroup.SetActive(false);
        enableManager.SetActive(true);
        if (timedGame == true)
        {
            TimeRemainingTextGaOb.SetActive(true);
            InvokeRepeating("TimeCountdown", 0.0f, 1.0f);
        }

        else
        {
            timeRemaining = 0;
            TimeRemainingTextGaOb.SetActive(true);
            InvokeRepeating("TimeCountup", 0.0f, 1.0f);
        }

    }

    public void EndGame()
    {
        gameOver = true;
        audioSource.Stop();
        CancelInvoke("TimeCountdown");
        CancelInvoke("TimeCountup");
    }

    public void SetTimed(bool timed)
    {
        //Toggle[] gameModeToggle = toggleGroup.GetComponentsInChildren<Toggle>();

        timedGame = timed;
        Debug.Log("SetTimed triggered! " + timedGame);
        //Debug.Log("gameModeToggle1 = " + gameModeToggle[1].name);
        /*if (timedGame == false)
        {
            gameModeToggle[1].value = true;
        }*/
    }

    public void SetTimeCounting(bool timed)
    {

    }

    public void AddToScore()
    {
        score += scoreAdded;
        
        scoreboardText.text = ("score: " + score);
    }
}
