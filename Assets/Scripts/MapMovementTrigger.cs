using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**************************************
 * Script which moves GameObjects upon the player entering a trigger.
 * 
 * Component of the trigger in question.
 * 
 * Pacifica Morrow
 * 11.25.2024
 * Version 1
 * ***********************************/

public class MapMovementTrigger : MonoBehaviour
{
    [SerializeField] private GameObject moveable;
    [SerializeField] private float moveAmount;
    [SerializeField] private float objectMoveSpeed;
    private Vector3 objectInitialPosition;
    private bool triggered;
    

    // Start is called before the first frame update
    void Start()
    {
        objectInitialPosition = moveable.transform.localPosition;
        //FIX MOVEABLES STARTING WAY OFF THE MAP!!!!!!!
    }

    // Update is called once per frame
    void Update()
    {
        if ((triggered == true) && (moveable.transform.position.y <= (objectInitialPosition.y + moveAmount)))
        {
            moveable.transform.Translate(Time.deltaTime * Vector3.up * objectMoveSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            triggered = true;
        }
    }

    private void OnDisable()
    {
        triggered = false;
        moveable.transform.localPosition = objectInitialPosition;
        //SOMETHING HERE MAYBE?????? LOCAL W/ PARENT VS. GLOBAL POSITIONS?
        //parent is moving, but so is this. switch to only modifying movement along Y axis.
    }
}
