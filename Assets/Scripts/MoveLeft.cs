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
    private float speed = 10;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameOver == false)
        {
            MoveToLeft();
        }
    }

    private void MoveToLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }    
}
