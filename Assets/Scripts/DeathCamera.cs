using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCamera : MonoBehaviour
{
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy() {
        GameObject cam = Instantiate(camera);
    }
}
