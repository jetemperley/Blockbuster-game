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
    public int scoreValue;

    private float invulnerableTimer;

    public Health shield;
    public AudioClip hitSFX;

    public DeathEffect[] effect;
    public DamageEffect damageEffect;

    private float fallLimit;

   
    // Start is called before the first frame update
    void Start()
    {
        if (!shieldOrNot)
            currentHealth = maxHealth;
        invulnerableTimer = 0;
        fallLimit = TerrainGen.yOffset-25;
    }

    // Update is called once per frame
    void Update()
    {
        if(invulnerableTimer>0){
            invulnerableTimer -= Time.deltaTime;
        }else{
            invulnerableTimer = 0;
        }
        if(fallLimit > TerrainGen.yOffset-10)
        {
            fallLimit = TerrainGen.yOffset-10;
        }
        if (transform.position.y < fallLimit && gameObject.layer == 9) {
            takeDamage(1000);
            PlayerStats.getInst().addStat("fall");
        }
    }

    public int takeDamage(int dam){
        if(invulnerableTimer<=0)
        {
            if(hitSFX != null)
            {
                AudioSource audio = AudioPool.GetAudioSource();
                audio.gameObject.transform.position = this.gameObject.transform.position;
                audio.clip = hitSFX;
                audio.volume = PlayerPrefs.GetFloat("sfxSound",1f) * PlayerPrefs.GetFloat("masterSound",1f);
                audio.Play(0);
            }
            
            if(gameObject.layer == 9)
            {
                invulnerableTimer = invulnerableTimeCooldown;
            }

            if (shield != null) {
                if (shield.takeDamage(dam)<=0 && !shield.shieldOrNot){
                    shield = null;
                    return currentHealth;
                }
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
                    FindObjectOfType<GameManager>().EndGame(); 
                }

                if(gameObject.tag == "Enemy"){
                    Analytics.CustomEvent(
                        "Enemy Killed",
                        new Dictionary<string, object>{
                            {"Enemy Type", gameObject.name},
                        }
                    );
                }
                Destroy(gameObject);

                PlayerStats.getInst().addStat("kill "+ name);
                ScoreManager.Inst.AddScore(scoreValue);
            }

            if (currentHealth > 0)
            {
                if (damageEffect != null)
                    damageEffect.DamageFlash();
                return currentHealth;
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
