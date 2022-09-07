using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    Transform target;
    public string targetTag = "Player";

    public float speed;

    public float journeyTime = 1.0f;
    float startTime;
    Vector3 centerPoint;
    Vector3 startRelCenter;
    Vector3 endRelCenter;

    public Health health;
    Vector3 startPos;
    Vector3 endPos;

    public float countdownTimer = 5;
    private int damage = 1000;
    private int takeDmgCount=0;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
        startTime = Time.time;
        startPos = transform.position;
        endPos = target.position + new Vector3 (0,0,6);
    }

    // Update is called once per frame
    void Update()
    {
        moveAtPlayerArc();
        countdownTimer-=Time.deltaTime;
        if(countdownTimer<=0){
            if (health != null){
                health.takeDamage(damage);
            }
        }

        Debug.Log(countdownTimer);
    }


    private void moveAtPlayerArc(){
        centerPoint = (startPos + endPos) * 0.5f;

        centerPoint -= new Vector3(0,1,0);

        startRelCenter = startPos - centerPoint;
        endRelCenter = endPos - centerPoint;

        float fracComplete = (Time.time - startTime) / journeyTime;

        transform.position = Vector3.Slerp(startRelCenter, endRelCenter, fracComplete);
        transform.position += centerPoint;
    }

}
