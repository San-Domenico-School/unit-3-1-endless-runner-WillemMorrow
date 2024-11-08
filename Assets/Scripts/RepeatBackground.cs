using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************
 * seamlessly repeats the background of the scene
 * Component of background
 * 
 * Pacifica Morrow
 * 10.08.2024
 * Version1
 * *************/

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    private BoxCollider bgBoxColider;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        bgBoxColider = GetComponent<BoxCollider>();
        repeatWidth = (bgBoxColider.size.x / 2);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(repeatWidth);
        if (transform.position.x < (startPos.x - repeatWidth))
        {
            transform.position = startPos;
        }
    }
}
