using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/****************************************************
 * PlayerControllerScript
 * controls player movement
 * Component of: Player
 * 
 * 11.24.2024
 * Pacifica Morrow
 * Version 1
 * *************************************************/


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce;                                               // The force of the player's jump
    [SerializeField] private float gravityModifier;                                         // How much gravity there is, compared to 9.8 N/kg
    [SerializeField] private ParticleSystem dirtParticle, explosionParticle, smokeParticle; // Particles related to the player
    [SerializeField] private AudioClip jumpSound, slideSound, landSound;                    // Audio clip for jumping, sliding, and for when the player lands.
    [SerializeField] private AudioClip[] crashSounds;                                       // Audio clips for when the player crashes.
    private Animator playerAnimator;                                                        // Player's animator
    private AudioSource playerAudio;                                                        // Player's audiosource
    private Rigidbody playerRB;                                                             // Player's rigidbody
    private int jumpStatus;                                                                 // An integer used to determine what jump state the player is in; on ground == 0, in the air with double jump available == 1, in the air w/o double jump == >1
    private Vector3 initialMomentum, doubleJumpForce, crouchScale, initialScale;            // The player's initial momentum out of a jump, the force that should be applied on double jump, how small (relative to previous size) the player's hitbox becomes.
    private BoxCollider playerCol;                                                          // A reference to the player's BoxCollider
    private bool isCrouched;                                                                // Whether the player is crouched or not.
    private float crouchInput;                                                              // float value from the Input, either 0 or 1, which determines whether the player is crouched


    // Initializes many fields before the first frame.
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        initialMomentum = new Vector3(0, jumpForce, 0);
        playerCol = GetComponent<BoxCollider>();
        crouchScale = new Vector3(1, 0.5f, 1);
        initialScale = playerCol.size;
        playerAnimator = GetComponentInChildren<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // calls every frame; Sets the crouch state and perameters for the Player's animator.
    private void Update()
    {
        ChangeCrouch();
        playerAnimator.SetInteger("JumpStatus_int", jumpStatus); 
        playerAnimator.SetBool("Crouch_bool", isCrouched);
    }

    // Jump Mechanics
    void OnJump(InputValue input)
    {
        // Second Jump; activates if player isn't crouched and if jumpStatis is 1.
        if ((jumpStatus == 1) && (isCrouched == false) && (GameManager.gameOver == false) && (GameManager.Instance.gameEnd == false))
        {
            Vector3 currentMomentum = playerRB.mass * playerRB.velocity;
            doubleJumpForce = (initialMomentum - currentMomentum);
            playerRB.AddForce(doubleJumpForce, ForceMode.Impulse);
            jumpStatus++;
            playerAnimator.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 0.25f);
        }

        // Initial jump; activates if player isn't crouched and if jumpStatis is 0.
        if ((jumpStatus == 0) && (isCrouched == false) && (GameManager.gameOver == false) && (GameManager.Instance.gameEnd == false))
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnimator.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 0.25f);
        }
    }

    // Gets the input from the crouch and sets it to a float value.
    void OnCrouch(InputValue input)
    {
        float inputCrouch = input.Get<float>();
        crouchInput = inputCrouch;
    }

    // Sets the player to a crouched state when the crouch button is pressed.
    void ChangeCrouch()
    {
        if ((GameManager.gameOver == false) && (GameManager.Instance.gameEnd == false))
        {
            if (crouchInput == 1)
            {
                playerCol.size = crouchScale;
                isCrouched = true;
            }

            else
            {
                //playerColY = Mathf.Lerp(playerCol.size.y, initialScale.y, 3);
                //playerCol.size = new Vector3(playerCol.size.x, playerColY, playerCol.size.z);
                playerCol.size = initialScale;
                isCrouched = false;
            }
        }

        else
        {
            playerCol.size = initialScale;
        }
    }
    
    // Method handling the player's collisions
    private void OnCollisionEnter(Collision collision)
    {
        
        // If it is a standable object, it will restore the player's jump abilities.
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpStatus = 0;
            //playerAudio.PlayOneShot(landSound, 0.1f);
            if ((GameManager.gameOver == false) && (GameManager.Instance.gameEnd == false))
            {
                dirtParticle.Play();
            }

        }

        // If it is an obstacle object, it will end the game.
        // Does all the end-game functions
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            int crashSFXIndex = Random.Range(0, crashSounds.Length);

            GameManager.Instance.EndGame();
            playerAnimator.SetBool("GameOver_bool", true);

            dirtParticle.Stop();
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSounds[crashSFXIndex], 0.5f);
            smokeParticle.Stop();

            playerCol.size = initialScale;
        }
    }

    // Sets the player to an airborne state upon leaving a collider.
    private void OnCollisionExit(Collision collision)
    {
        jumpStatus = 1;
        dirtParticle.Stop();
    }

    // Adds to the player's score when coliding with a scoreable object.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scoreable"))
        {
            GameManager.Instance.AddToScore();
            other.gameObject.SetActive(false);
        }
    }

    private void OnClick()
    {
        GameManager.Instance.OnM1();
    }


    /***********  UNUSED METHOD ENABLING THE PLAYER'S RAGDOLL ***********************
    private void EnableRagdoll()
    {
        Collider[] RagdollColliders = this.gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider collider in RagdollColliders)
        {
            collider.enabled = true;
        }

        playerCol.enabled = false;
        playerAnimator.enabled = false;
    }*/
}
