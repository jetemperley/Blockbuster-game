using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGen : MonoBehaviour
{
    private float zOffset = 0.0f;
    private float xOffset = 0.0f;

    public Building[] buildings; //Array of the different types of buildings to spawn
    public GameObject parent;

    public Vector2 randX;
    public Vector2 randY;
    public Vector2 randZ;

    private float randomX;
    private float randomY;
    private float randomZ;

    // Start is called before the first frame update
    void Start()
    {
        zOffset = 0.0f;
        xOffset = 80f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnBuilding()
    {
        while(zOffset<=TerrainGen.zOffset){
            System.Random random = new System.Random();
            int randomNumber = random.Next(0, buildings.Length);


            RandXYZ();
            Instantiate(buildings[randomNumber].gameObject, new Vector3(xOffset+randomX, TerrainGen.yOffset+randomY, zOffset+randomZ), buildings[randomNumber].gameObject.transform.rotation, parent.transform);
            xOffset *= -1;

            RandXYZ();
            Instantiate(buildings[randomNumber].gameObject, new Vector3(xOffset+randomX, TerrainGen.yOffset+randomY, zOffset+randomZ), buildings[randomNumber].gameObject.transform.rotation, parent.transform);
            zOffset += 20;
        }
    }

    void RandXYZ(){
        System.Random random = new System.Random();
        randomX = random.Next((int) randX.x, (int) randX.y);
        randomY = random.Next((int) randY.x, (int) randY.y);
        randomZ = random.Next((int) randZ.x, (int) randZ.y);
    }
}
