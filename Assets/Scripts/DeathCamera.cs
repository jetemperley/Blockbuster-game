using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCamera : MonoBehaviour
{
    public GameObject camera;
    
    private void OnDestroy() {
        GameObject cam = Instantiate(camera);
    }
}
