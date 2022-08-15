using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGen : MonoBehaviour
{
    public static float zOffset = 0.0f; //Next z position to spawn blocks
    public static float yOffset = 0.0f; //Next y position to spawn blocks
    public float distanceToSpawn; //Distance to player to spawn new blocks
    public int blocksToSpawn; //Initial amount of blocks to spawn

    public Block[] blocks; //Array of the different types of blocks to spawn
    public Block checkpointBlock;
    public Block emptyBlock; //Empty block for the start of each level

    public float distToCheckpoint; //Total distance needed to spawn the next checkpoint
    private int checkpointNumber = 1; //The number of checkpoints spawned, starting at 1

    public static int level = 1; //The current level (determines difficulty)

    public static TerrainGen instance;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        player =  FindObjectOfType<PlayerMove>().gameObject;

        for (int i = 0; i < blocksToSpawn; i++) //Create initial starting area
        {
            Instantiate(emptyBlock.gameObject, new Vector3(0.0f, yOffset, zOffset), emptyBlock.gameObject.transform.rotation);
            zOffset += emptyBlock.length;
            yOffset += emptyBlock.heightOffset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z > (zOffset - distanceToSpawn))
        {
            SpawnTerrain();
        }

        //Spawn a checkpoint
        if (zOffset >= distToCheckpoint*checkpointNumber)
        {
            Instantiate(checkpointBlock.gameObject, new Vector3(0.0f, yOffset, zOffset), checkpointBlock.gameObject.transform.rotation);
            zOffset += checkpointBlock.length;
            yOffset += checkpointBlock.heightOffset;
            checkpointNumber++;
        }
    }

    void SpawnTerrain()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, blocks.Length);
        Instantiate(blocks[randomNumber].gameObject, new Vector3(0.0f, yOffset, zOffset), blocks[randomNumber].gameObject.transform.rotation);

        zOffset += blocks[randomNumber].length;
        yOffset += blocks[randomNumber].heightOffset;
    }
}
