using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    private int levelSpeed = 1;
    private int gameSpeed = 1;

    //Singleton stuff
    public static Conductor conductor;
    
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
        }
    }

    public int getLevelSpeed()
    {
        return levelSpeed;
    }

    public int getGameSpeed()
    {
        return gameSpeed;
    }

    private void setSpeeds(int lspeed, int gspeed)
    {
        levelSpeed = lspeed;
        gameSpeed = gspeed;
    }
}
