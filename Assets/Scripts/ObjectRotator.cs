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
    [SerializeField] private float maxRotationX;

    // Update is called once per frame
    void Update()
    {
        float rotationXDegrees = Mathf.Rad2Deg * transform.rotation.x;
        if (maxRotationX >= 0)
        {
            if ((rotationXDegrees < maxRotationX) || (maxRotationX == 0))
            {
                transform.Rotate(Time.deltaTime * Vector3.up * rotationSpeedY);
                transform.Rotate(Time.deltaTime * Vector3.left * rotationSpeedX);
                transform.Rotate(Time.deltaTime * Vector3.forward * rotationSpeedZ);
            }
        }

        else
        {
            if (rotationXDegrees > maxRotationX)
            {
                transform.Rotate(Time.deltaTime * Vector3.up * rotationSpeedY);
                transform.Rotate(Time.deltaTime * Vector3.left * rotationSpeedX);
                transform.Rotate(Time.deltaTime * Vector3.forward * rotationSpeedZ);
            }
        }
    }
}
