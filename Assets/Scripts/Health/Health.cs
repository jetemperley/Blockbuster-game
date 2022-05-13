using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Health : MonoBehaviour
{

    public string name;
    public int maxHealth;
    public int currentHealth;
    public float invulnerableTimeCooldown;

    private float invulnerableTimer;

    public Health shield;
    public AudioSource hitSFX;

    public DeathEffect[] effect;
    public DamageEffect damageEffect;
    public Explode explode;
   
    // Start is called before the first frame update
    void Start()
    {
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
                dam = shield.takeDamage(dam);
            }

            currentHealth -= dam;
            if (currentHealth <= 0){
                if(effect != null){
                    foreach (DeathEffect e in effect){
                        if (e != null)
                            e.effect();
                    }
                }
                if(gameObject.tag == "Player"){
                    PlayerStats.getInst().addStatAnalytic("kill Player", this.gameObject);
                }

                if(gameObject.tag == "Enemy"){
                    Debug.Log("Enemy Killed");
                    Analytics.CustomEvent(
                        "Enemy Killed",
                        new Dictionary<string, object>{
                            {"Enemy Type", gameObject.name},
                        }
                    );
                }
                if(explode != null){
                    Debug.Log("I exploded!");
                    explode.explode();
                }
                Destroy(gameObject);
                PlayerStats.getInst().addStat("kill "+ name);
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

    public void Reset(){
        currentHealth = maxHealth;
    }

}
