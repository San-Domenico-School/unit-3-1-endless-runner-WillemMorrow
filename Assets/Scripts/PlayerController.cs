using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/****************************************************
 * PlayerControllerScript
 * controls player movement
 * Component of: Player
 * 
 * 11.07.2024
 * Pacifica Morrow
 * Version 1
 * *************************************************/


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce;                                           // The force of the player's jump
    [SerializeField] private float gravityModifier;                                     // How much gravity there is, compared to 9.8 N/kg
    [SerializeField] private ParticleSystem dirtParticle, explosionParticle;            // Particles related to the player
    [SerializeField] private AudioClip jumpSound, crashSound;                           // Audio clips relating to the player
    private Animator playerAnimation;                                                   // Player's animator
    private AudioSource playerAudio;                                                    // Player's audiosource
    private Rigidbody playerRB;                                                         // Player's rigidbody
    //private bool isOnGround, hasDoubleJump = true;                                    // (unused) booleans relating to whether the player is on the ground, and if they have Double Jump
    public bool gameOver { get; private set; }                                          // Boolean determining if the game is over.
    private int jumpStatus;                                                             // An integer used to determine what jump state the player is in; on ground == 0, in the air with double jump available == 1, in the air w/o double jump == >1
    private Vector3 initialMomentum, doubleJumpForce, crouchScale, initialScale;        // The player's initial momentum out of a jump, the force that should be applied on double jump, how small (relative to previous size) the player's hitbox becomes.
    private BoxCollider playerCol;                                                      // A reference to the player's BoxCollider
    private bool isCrouched;                                                            // Whether the player is crouched or not.
    private float crouchInput;                                                          // float value from the Input, either 0 or 1, which determines whether the player is crouched


    // Initializes many fields before the first frame.
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        initialMomentum = new Vector3(0, jumpForce, 0);
        playerCol = GetComponent<BoxCollider>();
        crouchScale = new Vector3(1, 0.5f, 1);
        initialScale = playerCol.size;
    }

    private void Update()
    {
        ChangeCrouch();
    }

    // Jump Mechanics
    void OnJump(InputValue input)
    {
        // Second Jump; activates if player isn't crouched and if jumpStatis is 1.
        if ((jumpStatus == 1) && (isCrouched == false))
        {
            Vector3 currentMomentum = playerRB.mass * playerRB.velocity;
            doubleJumpForce = (initialMomentum - currentMomentum);
            playerRB.AddForce(doubleJumpForce, ForceMode.Impulse);
            jumpStatus++;
        }

        // Initial jump; activates if player isn't crouched and if jumpStatis is 0.
        if ((jumpStatus == 0) && (isCrouched == false))
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCrouch(InputValue input)
    {
        float inputCrouch = input.Get<float>();
        crouchInput = inputCrouch;
    }

    void ChangeCrouch()
    {
        if (crouchInput == 1)
        {
            playerCol.size = crouchScale;
            isCrouched = true;
        }
        
        else
        {
            playerCol.size = initialScale;
            isCrouched = false;
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //isOnGround = true;
            //hasDoubleJump = true;
            jumpStatus = 0;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        jumpStatus = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
