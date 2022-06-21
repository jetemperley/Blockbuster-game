using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBack : MonoBehaviour
{
    public GameObject start;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.parent.position =
         new Vector3(start.transform.parent.position.x,other.gameObject.transform.position.y,other.gameObject.transform.position.z);
    }
}
