using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAway : MonoBehaviour
{
    static Vector3 fallSpeed = new Vector3(0, -1, 0);

    public float backWall = -10;

    private bool fall = false;

    public Renderer rend;

    Color startColor;
    Color endColor = Color.red;
    float duration = 1.0f;
    float distanceToFlash = 30.0f;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        startColor = rend.material.color;
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (ConductorV2.getConductor().getTotalSpeed() != 0 && !fall && transform.position.z < ConductorV2.conductor.getPosition()){
            
            if (rb == null){
                rb = gameObject.AddComponent<Rigidbody>();
            }
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.mass = 1000;
            // rb.drag = 0.1f;
            rb.angularDrag = 1;
            fall = true;
            
            // rb.AddForce(randX, randY, randZ, ForceMode.VelocityChange);
            rb.AddForce(
                Random.Range(-30f, 30f), 
                Random.Range(1f, 20f), // Random.Range(0f, 1f), 
                0, 
                ForceMode.VelocityChange);
            // rb.MovePosition(rb.position + fallSpeed*Time.deltaTime);
            
        }
        if (transform.position.y < TerrainGen.yOffset-100)
            Destroy(gameObject);

        //Flash red when close to falling away
        if (transform.position.z < ConductorV2.conductor.getPosition()+distanceToFlash)
        {
            float lerp = Mathf.PingPong(Time.time, duration) / duration;
            rend.material.color = Color.Lerp(startColor, endColor, lerp);
        }
    }

}

