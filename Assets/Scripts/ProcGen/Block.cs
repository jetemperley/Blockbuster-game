using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float length; //Length of block
    public float heightOffset; //Change in height (if its flat its 0, if it goes up its positive, down its negative)

    private float distToDestroy = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < Void.voidPosition-distToDestroy)
            Destroy(this.gameObject);
    }
}
