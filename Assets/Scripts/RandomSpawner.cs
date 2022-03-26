using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{

    public GameObject spawn;
    public Vector3 moveDirection = new Vector3(0, 0, 10);
    public float chancePerSec = 50;

    public Vector3 spawnArea = new Vector3(10, 10, 10);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float chance = Random.Range(0f, 100f);
        if (chance < chancePerSec*100*Time.deltaTime){
            float x = Random.Range(transform.position.x-spawnArea.x/2, transform.position.x+spawnArea.x/2);
            float y = Random.Range(transform.position.y-spawnArea.y/2, transform.position.y+spawnArea.y/2);
            float z = Random.Range(transform.position.z-spawnArea.z/2, transform.position.z+spawnArea.z/2);
            GameObject g = Instantiate(spawn);
            spawn.transform.position = new Vector3(x, y, z);
            spawn.transform.rotation = Quaternion.identity;
            g.GetComponent<Rigidbody>().AddForce(moveDirection,ForceMode.VelocityChange);

        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(transform.position, spawnArea);
        Gizmos.DrawLine(transform.position, transform.position + moveDirection);
    
    }
}
