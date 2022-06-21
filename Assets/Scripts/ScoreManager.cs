using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int currentScore = 0;
    public static int scoreToAdd = 0;
    public int baseAddRate;
    private int addRate;
    public float timeToAdd;
    private float timer;

    public float scoreMultiplier;
    public float baseMultiplier;
    public float multiplierRate;
    public float maxMultiplier;

    private static ScoreManager inst;
    public static ScoreManager Inst 
    {
        get 
        {
            return inst;
        }
    }

    void Awake() {
        if (inst == null)
            inst = this;
        else 
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = timeToAdd;
        scoreMultiplier = baseMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        addRate = scoreToAdd/baseAddRate;

        if (scoreToAdd > 0)
            timer -= Time.deltaTime;

        if (timer <= 0 && scoreToAdd > addRate)
        {
            currentScore += addRate;
            scoreToAdd -= addRate;
        }

        if (timer <= 0 && scoreToAdd <= baseAddRate)
        {
            currentScore += scoreToAdd;
            scoreToAdd = 0;
            timer = timeToAdd;
            scoreMultiplier = baseMultiplier;
        }
    }

    public void AddScore(int score)
    {
        float floatScore;

        floatScore = (float)score*scoreMultiplier;
        scoreToAdd += (int)floatScore;
        timer = timeToAdd;

        if (scoreMultiplier < maxMultiplier)
            scoreMultiplier += multiplierRate;
    }

    public static int SetScore()
    {
        return currentScore += scoreToAdd;
    }

    public static void ResetScores()
    {
        currentScore = 0;
        scoreToAdd = 0;
    }
}
