using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a singleton script that manages the speed of the terrain and enemies
public class ConductorV2 : MonoBehaviour
{
    public float levelSpeed = 10; //Level speed is the speed at which the level progresses (Flat value)
    public float gameSpeed = 1; //Game speed is a multiplier for how things move overall (including actions)
    public float boundary = -30; 
    public float startBoundary = 30;

    private float currentPosition = 0; 
    //public GameObject keepUpWith;
    public GameObject player;
    public PlayerBounds playerBounds;
    public float maxDistAway = 50;

    //Singleton stuff
    public static ConductorV2 conductor;

    private static GameObject instance;
    
    private void Awake()
    {
        if (conductor != null && conductor != this)
        {
            Debug.Log("Conductor Destroyed");
            Destroy(this);
        }
        else
        {
            conductor = this;
            currentPosition = boundary;
        }

        player = FindObjectOfType<PlayerMove>().gameObject;
        playerBounds = player.GetComponent<PlayerBounds>();
    }

    private void Update() 
    {
        if (!playerBounds.inBounds) 
            currentPosition += Time.deltaTime*levelSpeed*gameSpeed;
        // Debug.Log("position " + currentPosition);
        // if ( keepUpWith.transform.position.z - currentPosition > maxDistAway){
        //     // Debug.Log("keeping up");
        //     float dist = keepUpWith.transform.position.z - currentPosition;
        //     dist -= maxDistAway;
        //     currentPosition += dist;
        // }
        if (player.transform.position.z - currentPosition > maxDistAway) 
        {
            currentPosition = player.transform.position.z - maxDistAway;
        }
    }

    public float getPosition(){
        return currentPosition;
    }
    
    public static ConductorV2 getConductor()
    {
        if (conductor == null)
        {
            instance = new GameObject("Conductor");
            instance.AddComponent<ConductorV2>();
            conductor = instance.GetComponent<ConductorV2>();
        }

        return conductor;
    }

    public float getLevelSpeed()
    {
        return levelSpeed;
    }

    public float getGameSpeed()
    {
        return gameSpeed;
    }

    public float getBoundary()
    {
        return boundary;
    }

    private void setSpeeds(float lspeed, float gspeed)
    {
        levelSpeed = lspeed;
        gameSpeed = gspeed;
    }

    public float getTotalSpeed(){
        return gameSpeed*levelSpeed;
    }
}