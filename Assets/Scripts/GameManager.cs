using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

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

    [SerializeField] private TextMeshProUGUI scoreboardText, timeRemainingText;
    [SerializeField] private float scoreAdded;
    [SerializeField] private GameObject toggleGroup;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject enableManager;
    public static bool gameOver;
    private static float score;
    private bool gameStarted;
    private AudioSource audioSource;
    private int timeRemaining = 60;
    private bool timedGame;
    

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

    private void Update()
    {
        DisplayUI();
    }

    private void DisplayUI()
    {

    }

    private void TimeCountdown()
    {
        timeRemaining--;
    }

    public void StartGame()
    {
        audioSource.Play();
        startButton.SetActive(false);
        toggleGroup.SetActive(false);
        InvokeRepeating("TimeCountdown", 0.0f, 1.0f);
        enableManager.SetActive(false);

    }

    public void EndGame()
    {
        gameOver = true;
    }

    public void SetTimed(bool timed)
    {
        
    }

    public static void ChangeScore(int change)
    {

    }

    public void AddToScore()
    {
        score += scoreAdded;
        
        scoreboardText.text = ("score: " + score);
    }
}
