using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage : MonoBehaviour
{
    // FIGURE OUT HOW TO PRESERVE TIME ACROSS SCENE LOADING
    
    
    public static DataStorage Instance;

    public float timeStorage;
    public bool keepTimeStorage;
   
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(this);

        DontDestroyOnLoad(gameObject);
    }

    public void timeStorageSet(bool keepTime)
    {
        timeStorage = GameManager.time;
        keepTimeStorage = keepTime;
}
}
