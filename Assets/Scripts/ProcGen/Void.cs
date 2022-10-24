using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour
{
    ConductorV2 conductor;
    public float eventHorizon; //Distance between the fallaway and the void
    GameObject player;
    public static float voidPosition;

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

        voidPosition = transform.position.z;
    }

    //CONSUME
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.parent != null)
        {        
            if(other.gameObject.transform.parent.name.Contains("PlayerContainer"))
            {
                GameObject.Find("Player").GetComponent<Health>().takeDamage(1000);
            }else{
                Destroy(other.gameObject.transform.parent.gameObject);
            }    
            
        }
        
    }
}
