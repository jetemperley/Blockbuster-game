using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Health : MonoBehaviour
{
    public bool shieldOrNot;

    public string name;
    public int maxHealth;
    public int currentHealth;
    public float invulnerableTimeCooldown;

    private float invulnerableTimer;

    public Health shield;
    public AudioSource hitSFX;

    public DeathEffect[] effect;
    public DamageEffect damageEffect;

   
    // Start is called before the first frame update
    void Start()
    {
        if (!shieldOrNot)
            currentHealth = maxHealth;
        invulnerableTimer = 0;
        //hitSFX = GetComponent<AudioSource>();
        if(hitSFX != null)
            {
                hitSFX.Stop();
            }
    }

    // Update is called once per frame
    void Update()
    {
        if(invulnerableTimer>0){
            invulnerableTimer -= Time.deltaTime;
        }else{
            invulnerableTimer = 0;
        }

        if (transform.position.y < -100 && gameObject.layer == 9) {
            takeDamage(1000);
            PlayerStats.getInst().addStat("fall");
        }
    }

    public int takeDamage(int dam){
        if(invulnerableTimer<=0)
        {
            if(hitSFX != null && hitSFX.enabled)
            {
                hitSFX.Play(0);
            }
            
            if(gameObject.layer == 9)
            {
                invulnerableTimer = invulnerableTimeCooldown;
            }
            if (shield != null) {

                if (shield.takeDamage(dam)<=0 && !shield.shieldOrNot)
                    shield = null;
                
                if (shield.currentHealth > 0 && shield.shieldOrNot)
                {
                    dam = shield.takeDamage(dam);
                }
                else if (shield.currentHealth <= 0 && shield.shieldOrNot)
                {
                    currentHealth -= dam;
                }

            } else {
                currentHealth -= dam;
            }


             Debug.Log("took dam");
            
            if (currentHealth <= 0 && !shieldOrNot){
                if(effect != null){
                    foreach (DeathEffect e in effect){
                        if (e != null)
                            e.effect();
                    }
                }
                //Analytics
                if(gameObject.tag == "Player"){
                    PlayerStats.getInst().addStatAnalytic("kill Player", this.gameObject);
                }

                if(gameObject.tag == "Enemy"){
                    // Debug.Log("Enemy Killed");
                    Analytics.CustomEvent(
                        "Enemy Killed",
                        new Dictionary<string, object>{
                            {"Enemy Type", gameObject.name},
                        }
                    );
                }
                Debug.Log("destroying");
                Destroy(gameObject);
                PlayerStats.getInst().addStat("kill "+ name);
                ScoreManager.Inst.AddScore(50);
            }

            if (currentHealth > 0)
            {
                if (damageEffect != null)
                    damageEffect.DamageFlash();
                return 0;
            }
            
        } 
        return currentHealth;
    }

    public int getHealth(){
        return currentHealth;
    }

    public void kill(){
        if(effect != null){
            foreach (DeathEffect e in effect){
                if (e != null)
                    e.effect();
            }
        }
        Destroy(gameObject);
    }

    public void Reset(){
        currentHealth = maxHealth;
    }

}
