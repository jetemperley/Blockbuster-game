using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    LineRenderer line;
    float time = 1;
    float speed = 700;
    Vector3 dir;

    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        damage = 0;
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
            line.SetPosition(0, line.GetPosition(0) + dir.normalized*speed*Time.deltaTime);
        } else {
            gameObject.SetActive(false);
        }
    }

    public void fire(Vector3 pos, Vector3 dir, float time, string weap){
        fire(pos, dir, time, 150, weap);
    }

    public void fire(Vector3 pos, Vector3 dir, float time, float speed, string weap){
        this.dir = dir;
        this.speed = speed;
        this.time = time;
        RaycastHit hit;
        if (Physics.Raycast (pos, dir, out hit, 1000)){
            try{
                if (hit.collider.attachedRigidbody.gameObject.layer == 9)
                    return;
                Health health = hit.collider.attachedRigidbody.gameObject.GetComponent<Health>();
                if (health != null){
                    health.takeDamage(damage); 
                    if (health.getHealth() <= 0){
                        // Debug.Log(weap);
                        PlayerStats.getInst().addStat(weap);
                    }
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

    public void firePierce(Vector3 pos, Vector3 dir, float time, float speed, string weap, int pierceNum)
    {
        this.dir = dir;
        this.speed = speed;
        this.time = time;
        RaycastHit[] hits;
        hits = Physics.RaycastAll(pos, dir, 1000);

        Vector3[] arr = {pos,pos+(dir*1000)};
        RaycastHit hit;

        int numPierced = 0;

        for (int i = 0; i < hits.Length; i++)
        {
            if (numPierced > pierceNum)
            {
                line.SetPositions(arr);
                return;
            }

            hit = hits[i];

            try 
            {
                if (hit.collider.attachedRigidbody.gameObject.layer == 9)
                    return;
        
                Health health = hit.collider.attachedRigidbody.gameObject.GetComponent<Health>();
                if (health != null){
                    health.takeDamage(damage); 
                    numPierced++;
                    if (health.getHealth() <= 0){
                        // Debug.Log(weap);
                        PlayerStats.getInst().addStat(weap);
                    }
                }
            } catch {}
        }

        if (hits.Length > 0)
        {
            hit = hits[hits.Length-1];

            Vector3[] arr1 = {pos, hit.point};
            line.SetPositions(arr1);
        }
        else 
        {
            line.SetPositions(arr);
        }
    }

    public void SetDamage(int dmg){
        damage = dmg;
    }

}
