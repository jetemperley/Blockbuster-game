using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBack : MonoBehaviour
{
    public GameObject start;

    void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.parent.position =
         new Vector3(start.transform.parent.position.x,other.gameObject.transform.position.y,other.gameObject.transform.position.z);
    }
}
