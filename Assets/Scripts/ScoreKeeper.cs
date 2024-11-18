using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/************************
 * Script keeping the player's score
 * component of the UI canvas
 * 
 * Pacifica Morrow
 * 11.17.2024
 * Version1
 ************************/ 

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper Instance;

    private float score = 1;
    [SerializeField] private TextMeshProUGUI scoreboardText;
    [SerializeField] private float scoreAdded;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(this);
    }

    public void AddToScore()
    {
        score += scoreAdded;
        
        scoreboardText.text = ("score = " + score);
    }
}
