using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**************************
 * Moves objects to the left at a given speed, to a given boundry,
 * until game end.
 * 
 * Pacifica Morrow
 * 10.08.2024
 * Version1
 * ***********************/

public class MoveLeft : MonoBehaviour
{
    private float speed = 8;
    private float leftBound = -15;
    private PlayerController playerControllerScript;

    private void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            MoveToLeft();
        }
    }

    private void MoveToLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }    
}
