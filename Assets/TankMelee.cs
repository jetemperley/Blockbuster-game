using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMelee : MonoBehaviour
{
    private PlayerMove player;
    public ParticleSystem meleeCharge;
    public GameObject ChargingCircleUI;
    public GameObject AoEIndicatorCanvas;

    public float maxMeleeRadius;
    public float startMeleeDistance;
    public int meleeDamage;
    public float meleeChargeTime;
    public float attackCooldown;
    private float attackTimer;
    private bool isAttacking;
    private bool growChargeCircle;
    private float chargeCircleTimer;
    

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMove>();
        AoEIndicatorCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if(dist <= startMeleeDistance && attackTimer <= 0 && !isAttacking)
        {
            StartCoroutine(AttackSequence());
            isAttacking = true;

        }

        if(growChargeCircle && Mathf.Abs(chargeCircleTimer/meleeChargeTime)<1)
        {
            chargeCircleTimer += Time.deltaTime;
            ChargingCircleUI.transform.localScale = 
                new Vector3(chargeCircleTimer/meleeChargeTime,chargeCircleTimer/meleeChargeTime,chargeCircleTimer/meleeChargeTime);

        }

        attackTimer -= Time.deltaTime;
            
    }

    private IEnumerator AttackSequence()
    {
        //do charge up particles
        ChargeMelee();
        yield return new WaitForSeconds(meleeChargeTime);
        meleeCharge.Stop();
        AttackPlayer();
        ResetCooldown();
    }
    
    private void AttackPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, maxMeleeRadius);
        foreach(Collider hc in hitColliders){

            Rigidbody rb = hc.GetComponent<Collider>().attachedRigidbody;
            if(rb != null){
                Health h = rb.gameObject.GetComponent<Health>();
                if (h != null && rb.gameObject.layer == 9){
                  h.takeDamage(meleeDamage);
                } 
            }
                        
        } 
        ResetMelee();
    }

    private void ResetMelee()
    {
        growChargeCircle = false;
        chargeCircleTimer = 0;
        attackTimer = attackCooldown;
        isAttacking = false;
        ChargingCircleUI.transform.localScale = new Vector3(0,0,0);
        AoEIndicatorCanvas.SetActive(false);
    }

    void ChargeMelee()
    {
        AoEIndicatorCanvas.SetActive(true);
        meleeCharge.Play();
        growChargeCircle = true;
    }

    void ResetCooldown()
    {

    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxMeleeRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, startMeleeDistance);
    }


}
