using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    Transform target;
    Rigidbody rb;
    public float moveSpeed = 0;
    public float maxLookDist = 10;
    public string targetTag = "Player";

    public float lockOnTime = 3;
    private float lockOnTimer = 0;

    public float shootDelayTime = 0.4f;
    private float shootDelayTimer = 0;

    public int damage = 1;

    private Vector3[] points;
    LineRenderer laser;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
        laser = GetComponent<LineRenderer>();
        laser.startWidth = 0.1f;
        laser.endWidth = 0.1f;
        points = new Vector3[2];
    }

    // Update is called once per frame
    void Update()
    {
        if(lockOnTimer<lockOnTime && isInRange()){
            LookAtPlayer();
            LaserLine();
            lockOnTimer += Time.deltaTime;
        }else if(lockOnTimer>lockOnTime && isInRange()){
            shootDelayTimer += Time.deltaTime;
            if(shootDelayTimer>shootDelayTime){
                Shoot();
                shootDelayTimer = 0;
                lockOnTimer= 0;
            }
        }else{
            laser.enabled = false;
            lockOnTimer= 0;
            shootDelayTimer = 0;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, maxLookDist);
    }

    private void LookAtPlayer(){
            Vector3 direction = target.position-transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
            rb.MovePosition( rb.position +(target.position - rb.position).normalized*moveSpeed*Time.fixedDeltaTime);
    }

    private void LaserLine(){
        laser.enabled=true;
        points[1] = rb.position;
        points[0] = target.position;
        laser.SetPositions(points);
    }

    private bool isInRange(){
        if(target == null || (target.position - transform.position).magnitude > maxLookDist || target.position.z > rb.position.z){
            return false;
        }else{
            return true;
        }
    }

    private void Shoot(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit)){
            if(hit.transform.name == targetTag){
            Health h = target.gameObject.GetComponent<Health>();
            if (h != null)
                h.takeDamage(damage);
            }
        }
    }

    private void Flicker(){
        
    }
    
}
