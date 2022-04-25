using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    LineRenderer line;
    float time = 1;
    float speed = 700;
    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0){
            time -= Time.deltaTime;
            line.SetPosition(0, line.GetPosition(0) + dir.normalized*speed);
        } else {
            gameObject.SetActive(false);
        }
    }

    public void fire(Vector3 pos, Vector3 dir, float time){
        fire(pos, dir, time, speed);
    }

    public void fire(Vector3 pos, Vector3 dir, float time, float speed){
        this.dir = dir;
        this.speed = speed*Time.deltaTime;
        this.time = time;
        RaycastHit hit;
        if (Physics.Raycast (pos, dir, out hit, 1000)){
            try{
                Health health = hit.collider.attachedRigidbody.gameObject.GetComponent<Health>();
                if (health != null){
                health.takeDamage(1);
                }
            } catch{}
            
            Vector3[] arr = {pos, hit.point};
            line.SetPositions(arr);
        }else{
            Vector3[] arr = {pos,pos+(dir*1000)};
            line.SetPositions(arr);
            // Debug.DrawLine(spawnPoint.transform.position, spawnPoint.transform.position*1000);
        }
    }

}
