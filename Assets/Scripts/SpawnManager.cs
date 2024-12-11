using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/***************************************************
 * Custom spawn manager
 * instead of instantiating the new obstacles, it enables random,
 * prefabricated obstacles.
 * 
 * Pacifica Morrow
 * 11.24.2024
 * version 1
 * ************************************************/

public class SpawnManager : MonoBehaviour
{
    
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private float spawnDelay;
    private int prefabIndex;

    public static SpawnManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(this);
    }
    
    // starts the game; called upon the first frame, calls EnablePrefab for the first time.
    void Start()
    {
        StartCoroutine("ObstacleInitiator");
    }
    
    //Enables a random prefabricated obstacle course from those available in the scene.
    public void EnablePrefab()
    {
        if ((GameManager.Instance.time > 3) || (GameManager.Instance.oneMinuteGame == false))
        {
            if ((obstacles.Length > 0) && (obstacles[0] != null))
            {
                int prefabRestrict = prefabIndex;
                while (prefabIndex == prefabRestrict)
                {
                    prefabIndex = Random.Range(0, obstacles.Length);
                }

                GameObject prefab = obstacles[prefabIndex];
                prefab.SetActive(true);
                foreach (Transform child in prefab.transform)
                {
                    child.gameObject.SetActive(true);
                }
            }

            else
            {
                Debug.LogWarning("there are no Obsticles to enable!");
            }
        }
    }

    // Enables the next prefab after a delay of 1 frame.
    public void EnablePrefabDelay()
    {
        StartCoroutine("ObstacleDelayer");
    }

    // Waits for SpawnDelay (in seconds) before it calls EnablePrefab().
    IEnumerator ObstacleInitiator()
    {
        yield return new WaitForSeconds(spawnDelay);

        EnablePrefab();
    }

    // waits until the next frame before calling EnablePrefab(). 
    // Coroutine is started by ObstacleManager.OnDisable; enables the next prefab
    // once the player has moved through the current prefab. 
    IEnumerator ObstacleDelayer()
    {
        yield return new WaitForFixedUpdate();

        EnablePrefab();
    }
}






//hawktua (as requested by Tyce Wa Moreson)
