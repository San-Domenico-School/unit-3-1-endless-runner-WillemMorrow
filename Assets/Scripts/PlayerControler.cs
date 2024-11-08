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
    private int jumpStatus;
    private Vector3 initialMomentum, doubleJumpForce, crouchScale;
    private BoxCollider playerCol;

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
        if (jumpStatus == 1)
        {
            Vector3 currentMomentum = playerRB.mass * playerRB.velocity;
            doubleJumpForce = (initialMomentum -= currentMomentum);
            playerRB.AddForce(doubleJumpForce, ForceMode.Impulse);
            jumpStatus++;
            initialMomentum = new Vector3(0, jumpForce, 0);
            Debug.Log("DoubleJumpForce: " + doubleJumpForce);
            Debug.Log("currentMomentum MV: " + currentMomentum);
            Debug.Log("initialMomentum = " + initialMomentum);
        }

        if (jumpStatus == 0)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpStatus++;
            Debug.Log("initialMomentum MV: " + initialMomentum);
        }
    }

    void OnCrouch(InputValue input)
    {
        
        playerCol.size = crouchScale;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            hasDoubleJump = true;
            jumpStatus = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
