using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    LineRenderer line;
    float time = 1;

    // Start is called before the first frame update
    void Start()
    {
        line = gameObject.AddComponent<LineRenderer>();
         line.startWidth = 0.1f;
         line.endWidth = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0){
            time -= Time.deltaTime;
        } else {
            gameObject.SetActive(false);
        }
    }

    public void fire(Vector3 pos, Vector3 dir, float time){
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
            Vector3[] arr = {pos,dir};
            line.SetPositions(arr);
            // Debug.DrawLine(spawnPoint.transform.position, spawnPoint.transform.position*1000);
        }
    }

}
