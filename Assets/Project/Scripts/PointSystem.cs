using System.Collections;
using System.Collections.Generic;
using UnityChan;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    [SerializeField] public MovingPlane plane; // get a plane for speed information so no magic numbers
    [SerializeField] public UnityChanControlScript unityChan; // get unity-chan alive status    
    public float secondsRunning;

    public float Distance { get; set; }
    public int Points { get; set; }

    private void ResetSeconds() => secondsRunning = 0;

    private void Awake()
    {
        Distance = 0;
        Points = 0;
        secondsRunning = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // keep updating till death
        secondsRunning += Time.deltaTime;

        // calculate points and distance when dead
        if (unityChan.IsDead)
        {
            CalculateDistanceRan();
            CalculatePoints();
            // ResetSeconds();
        }
    }

    // calculates the distance ran base on plane speed and seconds alive
    private float CalculateDistanceRan()
    {
        Distance = secondsRunning * plane.speed; // assuming plane speed is in m/s (also remember unitychan doesn't move only the plane)

        #if UNITY_EDITOR
        Debug.Log($"Distance ran: {Distance}");
        #endif

        return Distance; 
    }

    // Handles point calculation
    private float CalculatePoints()
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

        return Points;
    }
}
