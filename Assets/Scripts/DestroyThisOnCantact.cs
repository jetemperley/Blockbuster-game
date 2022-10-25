using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThisOnCantact : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
    }
}
