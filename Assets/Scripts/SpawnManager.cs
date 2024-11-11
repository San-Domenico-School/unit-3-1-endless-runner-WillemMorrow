using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/***************************************************
 * Custom spawn manager
 * 
 * Pacifica Morrow
 * 11.08.2024
 * version 1
 * ************************************************/

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private float spawnDelay, spawnInterval;
    public static SpawnManager Instance;

    void OnAwake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }
    
    // starts the game; called upon the first frame, calls EnablePrefab for the first time.
    void Start()
    {
        StartCoroutine("ObstacleInitiator");
    }
    
    //Enables a random prefabricated obstacle course from those available in the scene.
    public void EnablePrefab()
    {
        if (obstacles.Length > 0 && (obstacles[0] != null)) 
        {
            int prefabIndex = Random.Range(0, obstacles.Length);
            GameObject prefab = obstacles[prefabIndex];

            prefab.SetActive(true);
        }

        else
        {
            Debug.LogWarning("there are no Obsticles to enable!");
        }
        Debug.Log("SpawnManager.EnablePrefab() activated");
    }

    // Waits for SpawnDelay (in seconds) before it calls EnablePrefab().
    IEnumerator ObstacleInitiator()
    {
        yield return new WaitForSeconds(spawnDelay);

        EnablePrefab();
    }

    // waits until the next frame before calling EnablePrefab(). 
    // Coroutine is started by ObstacleManager.OnDisable; enables the next prefab
    //      once the player has moved through the current prefab. 
    IEnumerator ObstacleDelayer()
    {
        yield return new WaitForFixedUpdate();

        EnablePrefab();
    }
}






//hawktua (as requested by Tyce Wa Moreson)
