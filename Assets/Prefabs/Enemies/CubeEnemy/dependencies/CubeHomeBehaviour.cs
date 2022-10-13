using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FieldOfView))]
public class CubeHomeBehaviour : MonoBehaviour
{
    FieldOfView fov;
    Transform target;
    Rigidbody rb;
    public float moveSpeed = 3;
    public float maxLookDist = 10;
    public string targetTag = "Player";

    private AudioSource followsfx;
    // Start is called before the first frame update
    void Start()
    {
        fov = GetComponent<FieldOfView>();
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
        followsfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseMenu.gameIsPaused)
        {
            followsfx.Stop();
        }
    }

    void FixedUpdate() {

        if (target == null || fov.visibleTargets.Count < 1)
        {
            followsfx.Stop();
            return;
        }
            

        if(!followsfx.isPlaying)
        {
            followsfx.volume = PlayerPrefs.GetFloat("sfxSound",1f) * PlayerPrefs.GetFloat("masterSound",1f);
            followsfx.Play();
        }
        
        Vector3 direction = target.position-transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        rb.MovePosition( rb.position +(target.position - rb.position).normalized*moveSpeed*Time.fixedDeltaTime);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, maxLookDist);
    }
}
