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

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
}
