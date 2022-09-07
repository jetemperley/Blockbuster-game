using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Laserfire : MonoBehaviour
{
    private PlayerInput controls;
    public float fireCooldown; //seconds
    public int damage = 1;

    private float fireTimer; //seconds
    public int laserRange;
    public LineRenderer lineRenderer;
    private Vector3[] points;

    private float ticRate;
    public float ticRateCap = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        controls = PlayerInputLoader.Instance.gameObject.GetComponent<PlayerInput>();
        fireTimer = 0f;     
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        points = new Vector3[2];
    }

    // Update is called once per frame
    void Update(){
        if (controls.actions["Fire"].ReadValue<float>() == 1 && !PauseMenu.gameIsPaused)
        {
            Shoot();
        }else{
            lineRenderer.enabled = false;
            ticRate = 0;
        }
        Debug.Log(ticRate);
    }

    private void Shoot(){
        RaycastHit hit;
        lineRenderer.enabled=true;
        if (Physics.Raycast(transform.position, transform.forward*laserRange, out hit)){
                if(hit.collider.attachedRigidbody.gameObject.layer == 11){
                    ticRate+=Time.deltaTime;
                    if(ticRate>=ticRateCap){
                        Health health = hit.collider.attachedRigidbody.gameObject.GetComponent<Health>();
                        if (health != null){
                            health.takeDamage(damage); 
                            ticRate=0;
                        }
                    }
                }
            
            
            points[0] = transform.position; 
            points[1] = hit.point;
            lineRenderer.SetPositions(points);
        }else{
            points[0] = transform.position; 
            points[1] = transform.position+(transform.forward*laserRange);
            lineRenderer.SetPositions(points);
        }
    }
}
