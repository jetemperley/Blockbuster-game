using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGen : MonoBehaviour
{
    public GameObject[][] groups = new GameObject[2][];
    public GameObject[] lvl1Groups; //Array of the different types of enemy groups to spawn
    public GameObject[] lvl2Groups;

    void Awake()
    {
        groups[0] = lvl1Groups;
        groups[1] = lvl2Groups;
    }

    void Start()
    {
        for (int i = 0; i < groups.Length; i++)
        {
            for (int j = 0; i < groups[i].Length; i++)
            {
                groups[i][j].SetActive(false);
            }
        }
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, groups[0].Length);
        groups[0][randomNumber].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
