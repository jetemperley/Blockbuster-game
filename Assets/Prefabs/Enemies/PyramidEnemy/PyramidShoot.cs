using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FieldOfView))]
public class PyramidShoot : MonoBehaviour
{    
    private FieldOfView fov;
    public float maxLookDist = 10;
    public string targetTag = "Player";

    public float fireRate = 1;
    public GameObject projectile;
    public float projSpawnDist = 1;
    public float projSpeed = 5;
    

    private Transform target;
    private Rigidbody rb;
    
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        fov = GetComponent<FieldOfView>();
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        
        if (target == null || fov.visibleTargets.Count < 1)
            {    
                return;
            }
        
        if (timer > fireRate){
            // shoot
            GameObject g = Instantiate(projectile);
            g.transform.SetParent(transform);
            Vector3 dir = (target.position - transform.position).normalized * projSpawnDist;

            g.transform.position = transform.position + dir;
            g.transform.SetParent(null);
            Rigidbody prb = g.GetComponent<Rigidbody>();
            prb.AddForce(dir.normalized*projSpeed, ForceMode.VelocityChange);

            // alter timer
            timer = 0;
        }

    }

    private void OnDrawGizmos() {
        
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, maxLookDist);
        Gizmos.DrawWireSphere(transform.position, projSpawnDist);
    }
}