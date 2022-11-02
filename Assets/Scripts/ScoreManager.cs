using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int additiveScore = 0;
    public static int scoreToAdd = 0;
    public static int currentScore = 0;
    public static int highscore = 0;
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

    private PlayerMovement2 player;
    private Vector3 savePoint;
    private float distanceScore;
    bool addingScore;

    void Awake() {
        if (inst == null)
            inst = this;
        else 
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        player = FindObjectOfType<PlayerMovement2>();
        timer = timeToAdd;
        scoreMultiplier = baseMultiplier;
    }

    // Update is called once per frame
    void Update()
    {

        if(timer <= 0)
        {
            distanceScore = 0;
        }else{
            distanceScore = (Vector3.Distance(savePoint, player.transform.position)*scoreMultiplier) - Vector3.Distance(savePoint, player.transform.position);
        }

        scoreToAdd += (int)distanceScore;
        addRate = scoreToAdd/baseAddRate;
        if (scoreToAdd > 0)
            timer -= Time.deltaTime;

        if (timer <= 0 && scoreToAdd > addRate)
        {
            additiveScore += addRate;
            scoreToAdd -= addRate;
        }

        if (timer <= 0 && scoreToAdd <= baseAddRate)
        {
            additiveScore += scoreToAdd;
            scoreToAdd = 0;
            timer = timeToAdd;            
            scoreMultiplier = baseMultiplier;
        }

        if(player)
            currentScore = additiveScore + (int)player.Distance;
            if(currentScore>highscore)
            {
                highscore = currentScore;
                PlayerPrefs.SetInt("highscore",highscore);
                PlayerPrefs.Save();
            }
    }

    public void AddScore(int score)
    {
        float floatScore;
        floatScore = (float)score*scoreMultiplier;
        scoreToAdd += (int)floatScore;
        timer = timeToAdd;
        scoreToAdd += (int)distanceScore;
        if (scoreMultiplier < maxMultiplier)            
            savePoint = player.transform.position;
            scoreMultiplier += multiplierRate;
    }

    public static int SetScore()
    {
        return additiveScore += scoreToAdd;
    }

    public static void ResetScores()
    {
        additiveScore = 0;
        scoreToAdd = 0;
    }
}
