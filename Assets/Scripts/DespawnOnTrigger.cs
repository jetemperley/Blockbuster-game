using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnOnTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);
        //Debug.Log("despawned " + other.gameObject.name);
    }
}
