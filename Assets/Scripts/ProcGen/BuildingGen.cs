using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGen : MonoBehaviour
{
    public float zOffset = 0.0f; //Next z position to spawn buildings
    public float yOffset = 0.0f; //Next y position to spawn buildings
    public float xOffset = 0.0f; //Next x position to spawn buildings

    public Building[] buildings; //Array of the different types of buildings to spawn
    
    public int numOfBuildings;


    // Start is called before the first frame update
    void Start()
    {
        zOffset = 0.0f;
        xOffset = 80f;
    }

    // Update is called once per frame
    void Update()
    {
        while(zOffset < numOfBuildings)
        {
            SpawnBuilding();
            numOfBuildings++;
        }
    }

    void SpawnBuilding()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, buildings.Length);

        float randomX = random.Next(-20,20);
        float randomY = random.Next(-40,40);
        float randomZ = random.Next(-10,10);

        Instantiate(buildings[randomNumber].gameObject, new Vector3(xOffset+randomX, randomY, zOffset+randomZ), buildings[randomNumber].gameObject.transform.rotation);
        xOffset *= -1;
        Instantiate(buildings[randomNumber].gameObject, new Vector3(xOffset+randomX, randomY, zOffset+randomZ), buildings[randomNumber].gameObject.transform.rotation);
        zOffset += 20;
    }

}
