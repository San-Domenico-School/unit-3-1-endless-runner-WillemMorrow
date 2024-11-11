using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/********************************************
 * Component of Obstacle Prefabs
 * Disables itself when hitting the trigger and/or when transform.position is below a threshhold
 * On disable, it's transform will be reset. 
 * 
 * Pacifica Morrow
 * Version1
 * 11.08.2024
 * *****************************************/

//MAKE CALLING THE NEXT PREFAB AND DISABLING THE OBJECT DIFFERENT METHODS(?)
//potential problem: it can enable the same prefab -- ask Mr.G if one can exclude a value from Random.Range or an equivalent 

public class ObstacleManager : MonoBehaviour
{
    private Vector3 startPosition;
    private SpawnManager spawnManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObstacleReset"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        transform.position = startPosition;
        spawnManagerScript.StartCoroutine("ObstacleDelayer");
        /*
        if (SpawnManager.Instance != null)
        {
            SpawnManager.Instance.EnablePrefab();
        }
        else
        {
            Debug.LogWarning("SpawnManager.Instance is null!");
        }
        */
    }
}
