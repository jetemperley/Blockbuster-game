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
    private int checkpointNumber; //The total number of checkpoints spawned
    private int checkpointCount;
    public int checkpointsToNextLvl; //The number of checkpoints to be reached until a level increase

    public static int level = 0; //The current level (determines difficulty)
    public int maxLevel; //Maximum level
    public int lengthIncrement; //The increase in a level length
    public float levelSpeedIncrement; //Increase in fall away speed after increasing level

    public bool checkpointSpawned;

    public static TerrainGen instance;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        zOffset = 0.0f;
        yOffset = 0.0f;
        level = 0;

        player =  FindObjectOfType<PlayerMovement2>().gameObject;

        for (int i = 0; i < blocksToSpawn; i++) //Create initial starting area
        {
            Instantiate(emptyBlock.gameObject, new Vector3(0.0f, yOffset, zOffset), emptyBlock.gameObject.transform.rotation);
            zOffset += emptyBlock.length;
            yOffset += emptyBlock.heightOffset;
        }

        checkpointSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            /*if (player.transform.position.z > (zOffset - distanceToSpawn))
            {
                SpawnTerrain();
            }*/

            /*if (player.transform.position.z > (zOffset - checkpointBlock.length))
            {
                checkpointNumber++;
                checkpointCount++;
                checkpointSpawned = false;
            }*/
        }

        //Increase level after reaching set number of checkpoints
        /*if (checkpointCount >= checkpointsToNextLvl && level < maxLevel)
        {
            level += 1;
            distToCheckpoint += lengthIncrement;
            checkpointCount = 0;
            ConductorV2.getConductor().levelSpeed += levelSpeedIncrement;
        }*/

        while(zOffset < distToCheckpoint*(checkpointNumber+1))
        {
            SpawnTerrain();
        }

        //Spawn a checkpoint
        if (zOffset >= distToCheckpoint*(checkpointNumber+1) && !checkpointSpawned)
        {
            checkpointNumber++;
            checkpointCount++;
            //Increase level after reaching set number of checkpoints
            if (checkpointCount >= checkpointsToNextLvl && level < maxLevel)
            {
                level += 1;
                distToCheckpoint += lengthIncrement;
                checkpointCount = 0;
                ConductorV2.getConductor().levelSpeed += levelSpeedIncrement;
            }
            Instantiate(checkpointBlock.gameObject, new Vector3(0.0f, yOffset, zOffset), checkpointBlock.gameObject.transform.rotation);
            zOffset += checkpointBlock.length;
            yOffset += checkpointBlock.heightOffset;
            checkpointSpawned = true;
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

    public void ProgressCheckpoint()
    {
        if (checkpointSpawned)
        {
            //checkpointNumber++;
            //checkpointCount++;
            checkpointSpawned = false;
        }
    }
}
