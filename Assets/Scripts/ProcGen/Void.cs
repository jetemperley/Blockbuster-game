using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour
{
    ConductorV2 conductor;
    public float eventHorizon; //Distance between the fallaway and the void
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement2>().gameObject;
        conductor = ConductorV2.getConductor();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
            transform.position = new Vector3(0, player.transform.position.y, conductor.getPosition()-eventHorizon);
    }

    //CONSUME
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.parent != null)
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }
        
    }
}
