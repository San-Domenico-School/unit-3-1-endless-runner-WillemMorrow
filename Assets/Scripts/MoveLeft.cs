using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**************************
 * Moves objects to the left at a given speed, to a given boundry,
 * until game end.
 * 
 * Pacifica Morrow
 * 10.24.2024
 * Version1
 * ***********************/

public class MoveLeft : MonoBehaviour
{
    public static float speed = 10;
    private static float slowDownRate = 6;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameOver == false)
        {
            MoveToLeft();
        }

        if ((GameManager.Instance.gameEnd == true) && (speed > 0))
        {
            speed -= (Time.deltaTime * slowDownRate);
            
            if (speed < 0)
            {
                speed = 0;
            }
        }
    }

    private void MoveToLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }    
}
