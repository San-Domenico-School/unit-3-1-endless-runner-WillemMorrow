using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*****************************************
 * Script for constantly rotating objects.
 * Universally applicable, with provisions for rotating along all 3 axes.
 * 
 * Pacifica Morrow
 * 11.30.2024
 * Version1
 * ***************************************/

public class ObjectRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeedX;
    [SerializeField] private float rotationSpeedY;
    [SerializeField] private float rotationSpeedZ;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Time.deltaTime * Vector3.up * rotationSpeedY);
        transform.Rotate(Time.deltaTime * Vector3.left * rotationSpeedX);
        transform.Rotate(Time.deltaTime * Vector3.forward * rotationSpeedZ);
    }
}
