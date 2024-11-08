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
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityModifier;
    [SerializeField] private ParticleSystem dirtParticle, explosionParticle;
    [SerializeField] private AudioClip jumpSound, crashSound;
    private Animator playerAnimation;
    private AudioSource playerAudio;
    private Rigidbody playerRB;
    //private bool isOnGround, hasDoubleJump = true;
    public bool gameOver { get; private set; }
    private int jumpStatus;
    private Vector3 initialMomentum, doubleJumpForce, crouchScale;
    private BoxCollider playerCol;
    private bool isCrouched;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        initialMomentum = new Vector3(0, jumpForce, 0);
        playerCol = GetComponent<BoxCollider>();
        crouchScale = new Vector3(1, 0.5f, 1);
    }

    void OnJump(InputValue input)
    {
        if ((jumpStatus == 1) && (isCrouched == false))
        {
            Vector3 currentMomentum = playerRB.mass * playerRB.velocity;
            doubleJumpForce = (initialMomentum - currentMomentum);
            playerRB.AddForce(doubleJumpForce, ForceMode.Impulse);
            jumpStatus++;
        }

        if ((jumpStatus == 0) && (isCrouched == false))
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpStatus++;
        }
    }

    void OnCrouch(InputValue input)
    {
        playerCol.size = crouchScale;
        isCrouched = true;
        //HAVE THE BUTTON VALUE RETURN LIKE A VALUE, HAVE UPDATE CALL THE METHOD?
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //isOnGround = true;
            //hasDoubleJump = true;
            jumpStatus = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
