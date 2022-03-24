using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{

    public GameObject spawn;
    public Vector3 moveDirection;
    public float chancePerSec = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float r = Random.Range(0f, 100f);
        if (r < chancePerSec*Time.deltaTime){
            GameObject g = Instantiate(spawn);
            spawn.transform.position = new Vector3(Random.Range(-10, 10), 1, 10);
            spawn.transform.rotation = Quaternion.identity;
            g.GetComponent<Rigidbody>().AddForce(moveDirection,ForceMode.VelocityChange);

        }
    }
}
