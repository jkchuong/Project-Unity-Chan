using System;
using System.Collections;
using System.Collections.Generic;
using UnityChan;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    [SerializeField] private ScoresScriptableObject scoresScriptableObject;
    
    public ScrollingBackground plane; // get a plane for speed information so no magic numbers
    public UnityChanControlScript unityChan; // get unity-chan alive status    
    public float secondsRunning;

    public float Distance { get; set; }
    public int Points { get; set; }

    public void ResetSeconds() => secondsRunning = 0;

    private void Awake()
    {
        Distance = 0;
        Points = 0;
        secondsRunning = 0;
    }

    private void Start()
    {
       unityChan = FindObjectOfType<UnityChanControlScript>();

        if (unityChan)
        {
            unityChan.OnDeath += CalculatePoints;
            unityChan.OnDeath += ResetSeconds;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // keep updating till death
        secondsRunning += Time.deltaTime;
    }

    // calculates the distance ran base on plane speed and seconds alive
    private float CalculateDistanceRan()
    {
        Distance = secondsRunning * plane.scrollSpeed; // assuming plane speed is in m/s (also remember unitychan doesn't move only the plane)

        #if UNITY_EDITOR
        Debug.Log($"Distance ran: {Distance}");
        #endif

        return Distance; 
    }

    private void OnDisable()
    {
        unityChan.OnDeath -= CalculatePoints;
    }

    // Handles point calculation
    private void CalculatePoints()
    {
        // TO DO: define how to award/calculate points (as we attack too, and maybe in the future we have consumables mixed along with obstacles)
        float obstaclesKilled = 5;
        float obstaclesConsumed = 9;
        float scalar = 0.8f;
        //---------------------------------------------------------------------------------------------------------------------------------------

        // come up with a cool equation and round up for an int score
        Points = Mathf.FloorToInt(scalar * (secondsRunning + obstaclesConsumed + obstaclesKilled));

        #if UNITY_EDITOR
        Debug.Log($"Points obtained on this run: {Points}");
        #endif

        scoresScriptableObject.playerScore.Add(Points);
    }
}
