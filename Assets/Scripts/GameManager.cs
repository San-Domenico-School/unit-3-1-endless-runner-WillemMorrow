using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

/************************
 * Script managing the game
 * 
 * 
 * Pacifica Morrow
 * 11.24.2024
 * Version1
 ************************/

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;                                         // reference to the GameManager

    [SerializeField] private TextMeshProUGUI scoreboardText, timeRemainingText; // scoreboard and time remaining text TMPs.
    [SerializeField] private TextMeshProUGUI gameOverText;                      // reference to the game over TMP.
    [SerializeField] private float scoreAdded;                                  // how much score is added uppon the player getting a Scoreable
    [SerializeField] private GameObject ToggleObjectGroup, enableManager;       // several obvious gameObjects. 
    [SerializeField] private ParticleSystem smokeParticles, dirtParticles;      // smoke and dirt particles
    [SerializeField] private AudioClip scoreSFX, backgroundMusic;               // reference to the SFX for gaining score.
    public static bool gameOver;                                                // game end bool activated if the player dies
    private static float score;                                                 // the player's score
    private AudioSource audioSource;                                            // reference to the source of the music audio.
    public int Time { get; private set; } = 10;                                 // the time of the game. if the gamemode is 1 minute, it will count down from 60. if the gamemode is infinate, it will count up until the player dies.
    public bool oneMinuteGame;                                                  // whether the gamemode is 1 minute or infinate.
    public bool gameEnd {  get; private set; }                                  // game end bool activated if the timer runs out.
    private Animator playerAnimator;                                            // reference to the player's animator.


    // sets Instance to reference this specific object. If there are other instances of this object, they will be deleted.
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(this);
    }

    // initialises audioSource and playerAnimator.
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerAnimator = GameObject.Find("Player").GetComponentInChildren<Animator>();
    }

    // sets the playerAnimator's "speed_f" perameter to MoveLeft.speed.
    void Update()
    {
        playerAnimator.SetFloat("Speed_f", MoveLeft.speed);
    }

    // counts down the timer every second.
    private void TimeCountdown()
    {
        Time--;
        timeRemainingText.text = ("" + Time);
        if (Time < 0)
        {
            EndGame();
        }
    }

    // counts up on the timer every second.
    private void TimeCountup()
    {
        Time++;
        timeRemainingText.text = ("" + Time);
    }

    // method for starting the game; sets up the UI, music, and obstacle spawning.
    public void StartGame()
    {
        audioSource.PlayOneShot(backgroundMusic, 0.75f);
        enableManager.SetActive(true);
        ToggleObjectGroup.SetActive(false);
        if (oneMinuteGame == true)
        {
            timeRemainingText.enabled = true;
            InvokeRepeating("TimeCountdown", 0.0f, 1.0f);
        }

        else
        {
            Time = 0;
            timeRemainingText.enabled = true;
            InvokeRepeating("TimeCountup", 0.0f, 1.0f);
        }

    }

    // ends the game; called upon timer reaching 0, and on collision with an object (via the PlayerController)
    public void EndGame()
    {
        var shapeModule = smokeParticles.shape;

        audioSource.Stop();
        CancelInvoke("TimeCountdown");
        CancelInvoke("TimeCountup");

        dirtParticles.Stop();
        shapeModule.rotation = new Vector3 (-85, 0, 0);
        gameOverText.enabled = true;

        // if the time remaining is above zero, IE the game ended as a result of the player colliding with an obstacle,
        // gameOver will be true, and the timing text will display the final time.
        if (Time > 0)
        {
            gameOver = true;
            timeRemainingText.text = ("Final time: " + Time);
        }

        if (Time <= 0)
        {
            Time = 0;
            gameEnd = true;
            playerAnimator.SetBool("GameEnd_bool", true);

            timeRemainingText.text = ("Time's up");

        }
    }

    // sets whether the game clock is counting down or up.
    public void SetTimed(bool timed)
    {
        oneMinuteGame = timed;

        StartGame();
    }

    // adds to the score when the player colides with a scorable object; called by the PlayerController.
    public void AddToScore()
    {
        score += scoreAdded;
        
        audioSource.PlayOneShot(scoreSFX, 4.0f);

        scoreboardText.text = ("Score: " + score);
    }

    // manages the raycast when choosing the gamemode in the beginning of the game.
    public void OnM1()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name == "InfinateGame")
            {
                SetTimed(false);
            }

            if (hit.collider.gameObject.name == "1MinuteGame")
            {
                SetTimed(true);
            }
        }
    }
}
