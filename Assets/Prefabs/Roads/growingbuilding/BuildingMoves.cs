using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMoves : MonoBehaviour
{
    public BuildingFloor buildingPrefab;
    public GameObject spawnLoc;
    // Start is called before the first frame update
    void Start()
    {
         BuildingFloor building = Instantiate(buildingPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
