using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityModifier;
    [SerializeField] private ParticleSystem dirtParticle, explosionParticle;
    [SerializeField] private AudioClip jumpSound, crashSound;
    private Animator playerAnimation;
    private AudioSource playerAudio;
    private Rigidbody playerRB;
    private bool isOnGround, hasDoubleJump = true;
    public bool gameOver { get; private set; }
    private float jumpStatus;
    private Vector3 initialMomentum;
    private Vector3 doubleJumpForce;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    private void Update()
    {
        //playerRB.velocity

    }

    void OnJump(InputValue input) 
    {
        if (jumpStatus == 1.0f)
        {
            playerRB.AddForce(doubleJumpForce, ForceMode.Impulse);
            jumpStatus++;
            Vector3 currentMomentum = playerRB.mass * playerRB.velocity;
            Debug.Log("currentMomentum MV: " + currentMomentum);
            doubleJumpForce = initialMomentum -= currentMomentum;
        }

        if (jumpStatus == 0.0f)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpStatus++;
            initialMomentum = playerRB.mass * playerRB.velocity;
            Debug.Log("initialMomentum MV: " + initialMomentum);
        }

        /*if (isOnGround)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
        if (!isOnGround && hasDoubleJump)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            hasDoubleJump = false;
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            hasDoubleJump = true;
            jumpStatus = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
