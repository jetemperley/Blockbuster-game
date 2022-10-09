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
    Vector3 inFrontPos = new Vector3 (0,0,6);

    public float countdownTimer = 5;
    private float countdowner;

    private int damage = 1000;
    public LayerMask ground;

    public AudioSource bombBeep;
    private float beepRate;
    private float beepRate2;
    private float volume;
    private float distance;
    public float maxSoundDist;
    private float multiplier;


    // Start is called before the first frame update
    void Start()
    {
        beepRate = 4;
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
        startTime = Time.time;
        startPos = transform.position;
        endPos = target.position + inFrontPos;
        countdowner=countdownTimer;
    }

    // Update is called once per frame
    void Update()
    {
        moveAtPlayerArc();
        countdowner-=Time.deltaTime;
        Beep();
        if(countdowner<=0){
            if (health != null){
                health.takeDamage(damage);
            }
        }
        
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


    private void Beep(){
        multiplier = -countdowner+countdownTimer;
        beepRate += Time.deltaTime*multiplier;
        distance = (target.position - transform.position).magnitude;
        volume=(-1/maxSoundDist)*distance+0.2f; 
        
        if(beepRate>=0.27f){
            bombBeep.PlayOneShot(bombBeep.clip, volume);
            beepRate = 0;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, maxSoundDist);
    }
}
