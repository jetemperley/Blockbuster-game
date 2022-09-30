using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGen : MonoBehaviour
{
    public GameObject[][] groups = new GameObject[4][];
    public GameObject[] lvl0Groups; //Array of the different types of enemy groups to spawn
    public GameObject[] lvl1Groups;
    public GameObject[] lvl2Groups;
    public GameObject[] lvl3Groups;

    private int distanceToLoad = 100;
    private int randomNumber;
    private bool loaded;

    private GameObject player;

    void Awake()
    {
        groups[0] = lvl0Groups;
        groups[1] = lvl1Groups;
        groups[2] = lvl2Groups;
        groups[3] = lvl3Groups;

        player =  FindObjectOfType<PlayerMovement2>().gameObject;
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
        randomNumber = random.Next(0, groups[TerrainGen.level].Length);
        //groups[TerrainGen.level][randomNumber].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!loaded && player != null)
        {
            if (player.transform.position.z >= this.transform.position.z - distanceToLoad)
            {
                loaded = true;
                groups[TerrainGen.level][randomNumber].SetActive(true);
            }
        }
    }
}
