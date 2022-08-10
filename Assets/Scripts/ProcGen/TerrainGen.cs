using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGen : MonoBehaviour
{
    public static float zOffset = 0.0f; //Next z position to spawn blocks
    public float distanceToSpawn; //Distance to player to spawn new blocks
    public int blocksToSpawn; //Initial amount of blocks to spawn

    public GameObject[] blocks; //Array of the different types of blocks to spawn
    public float blockLength; //Length of a block

    public static TerrainGen instance;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        player =  FindObjectOfType<PlayerMove>().gameObject;

        for (int i = 0; i < blocksToSpawn; i++)
        {
            Instantiate(blocks[0], new Vector3(0.0f, 0.0f, zOffset), blocks[0].transform.rotation);
            zOffset += blockLength;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z > (zOffset - distanceToSpawn))
        {
            SpawnTerrain();
        }
    }

    void SpawnTerrain()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, blocks.Length);
        Instantiate(blocks[randomNumber], new Vector3(0.0f, 0.0f, zOffset), blocks[randomNumber].transform.rotation);

        zOffset += blockLength;
    }
}
