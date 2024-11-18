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
 * 11.11.2024
 * *****************************************/

public class ObstacleManager : MonoBehaviour
{
    private Vector3 startPosition;
    //private SpawnManager spawnManagerScript;

    // Start is called before the first frame update; sets the StartPosition to the object's starting position
    void Start()
    {
        startPosition = transform.position;
        //spawnManagerScript = SpawnManager.Instance;
        //spawnManagerScript = GameObject.Find("EnableManager").GetComponent<SpawnManager>();
    }

    // either enables the next projectile (via SpawnManager class) or disables the object, depending on which trigger it colides with.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "ObstacleInitiator")
        {
            SpawnManager.Instance.EnablePrefabDelay();
        }

        if (other.gameObject.name == "ObstacleReset")
        {
            gameObject.SetActive(false);
        }
    }

    //upon the object's disabling, it resets its position.
    private void OnDisable()
    {
        transform.position = startPosition;
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
