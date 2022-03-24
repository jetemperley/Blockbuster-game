using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour
{

    public float zLimit = -10;
    public bool lessThan = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lessThan == transform.position.z < -10)
            Destroy(gameObject);
    }
}
