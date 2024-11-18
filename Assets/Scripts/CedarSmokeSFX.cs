using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************
 * script parenting just the transform position of the smokeSFX
 * to the smokestack
 * 
 * Pacifica Morrow
 * 11.18.2024
 * **************************/


public class CedarSmokeSFX : MonoBehaviour
{
    [SerializeField] private Transform smokeStackAnchor;

    // Update is called once per frame
    void Update()
    {
        transform.position = smokeStackAnchor.position;
    }
}
