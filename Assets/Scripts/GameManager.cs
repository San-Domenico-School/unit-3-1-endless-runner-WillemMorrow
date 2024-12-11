using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/************************
 * Script managing the game; score, time, starting the game, ending the game, etc.
 * 
 * 
 * Pacifica Morrow
 * 12.05.2024
 * Version1
 ************************/
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;                                         // reference to the GameManager Instance.

    [Header("TextMeshes")]
    [SerializeField] private TextMesh GmOvrPnlScoreTxt;
    [SerializeField] private TextMesh GmOvrPnlTimeTxt;                          // references to the text on the gameEnd Panel.
    [SerializeField] private TextMesh physicalScoreTxt, chronometerTxt;         // reference to the score text on the scoreometer panel.
    [Header ("Score?????")]
    [SerializeField] private float scoreAdded;                                  // how much score is added uppon the player getting a Scoreable
    [Header("GameObjects")]
    [SerializeField] private GameObject ToggleObjectGroup;                      // several obvious gameObjects. 
    [SerializeField] private GameObject enableManager;
    [Header("ParticleSystems")]
    [SerializeField] private ParticleSystem smokeParticles;                     // smoke and dirt particles
    [SerializeField] private ParticleSystem dirtParticles;
    [Header("AudioClips")]
    [SerializeField] private AudioClip scoreSFX;                                // reference to the SFX for gaining score.
    [SerializeField] private AudioClip backgroundMusic;
    [Header("ObjectRotators")]
    [SerializeField] private ObjectRotator startButtonsRotator;                 // references to the ObjectRotator component of the starting buttons and end game panel.
    [SerializeField] private ObjectRotator endGameRotator;
    [SerializeField] private ObjectRotator scoreometerRotator;
    public static bool gameOver;                                                // game end bool activated if the player dies
    private static int score;                                                   // the player's score
    private AudioSource audioSource;                                            // reference to the source of the music audio.
    public float time { get; private set; } = 60;                               // the time of the game. if the gamemode is 1 minute, it will count down from 60. if the gamemode is infinite, it will count up until the player dies.
    public float gameTime { get; private set; }
    public bool oneMinuteGame;                                                  // whether the gamemode is 1 minute or infinite.
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
        if (time < 0)
        {
            time = 0;
        }
    }

    // counts down the timer every second.
    private void timeCountDown()
    {
        time--;
        chronometerTxt.text = ($"{time}");
        gameTime++;
        if (time < 0)
        {
            EndGame();
        }
    }

    // counts up on the timer every second.
    private void timeCountUp()
    {
        time++;
        chronometerTxt.text = ($"{time}");
        gameTime++;
    }

    // method for starting the game; sets up the UI, music, and obstacle spawning.
    public void StartGame()
    {
        audioSource.PlayOneShot(backgroundMusic, 0.75f);
        enableManager.SetActive(true);
        //ToggleObjectGroup.SetActive(false);
        startButtonsRotator.enabled = true;
        if (oneMinuteGame == true)
        {
            InvokeRepeating("timeCountDown", 0.0f, 1.0f);
        }

        else
        {
            time = 0;
            InvokeRepeating("timeCountUp", 0.0f, 1.0f);
        }
    }

    // ends the game; called upon timer reaching 0, and on collision with an object (via the PlayerController)
    public void EndGame()
    {   
        var smokeParticleShape = smokeParticles.shape;
        smokeParticleShape.rotation = new Vector3(-85, 0, 0);

        audioSource.Stop();
        dirtParticles.Stop();
        CancelInvoke("timeCountdown");
        CancelInvoke("timeCountup");
        scoreometerRotator.enabled = true;

        StartCoroutine("GmOvrPanelRotator");
        
        GmOvrPnlScoreTxt.text = ($"{score}");
        GmOvrPnlTimeTxt.text = ($"{time}");

        // if the time remaining is above zero, IE the game ended as a result of the player colliding with an obstacle,
        // gameOver will be true, and the timing text will display the final time.
        if (time > 0)
        {
            gameOver = true;
            //timeRemainingText.text = ("Final time: " + time);
        }

        if (time <= 0)
        {
            time = 0;
            GmOvrPnlTimeTxt.text = ($"{time}");
            gameEnd = true;
            playerAnimator.SetBool("GameEnd_bool", true);
            //timeRemainingText.text = ("time's up");
        }
    }

    // sets whether the game clock is counting down or up.
    public void Settimed(bool timed)
    {
        oneMinuteGame = timed;

        StartGame();
    }

    // adds to the score when the player colides with a scorable object; called by the PlayerController.
    public void AddToScore()
    {
        float localTime = gameTime;
        if (time < 1)
        {
            localTime = 1;
        }

        float scoreByTime = ((scoreAdded * (0.25f * gameTime)) * 10);
        
        score += (int)scoreByTime;

        audioSource.PlayOneShot(scoreSFX, 4.0f);

        physicalScoreTxt.text = ($"{score}");
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(0);
        gameOver = false;
        gameEnd = false;
        time = 60;
        MoveLeft.speed = 10;
        score = 0;
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
                Settimed(false);
            }

            if (hit.collider.gameObject.name == "1MinuteGame")
            {
                Settimed(true);
            }

            if (hit.collider.gameObject.name == "RestartGame")
            {
                RestartGame();
            }
        }
    }

    // Waits for a few seconds before enabling the GameEnd panel's rotator.
    IEnumerator GmOvrPanelRotator()
    {
        yield return new WaitForSeconds(2);

        endGameRotator.enabled = true;
    }
}