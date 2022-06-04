using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class DeathParticles : DeathEffect
{

    public ParticleSystem particlesOnDeath;
    public Explode explodeScale;
    public bool deParent = true;
    private float explosion;

    void Start()
    {  

        if(explodeScale != null){
            explosion = explodeScale.explosionRadius*1.5f;
        }
        
    }
    // Start is called before the first frame update
    public override void effect(){
        // Debug.Log("explode scale null " + (explodeScale==null));
        if (deParent)
            particlesOnDeath.transform.SetParent(null);
        particlesOnDeath.transform.position = transform.position;
        if(explodeScale != null){
            particlesOnDeath.gameObject.transform.localScale = new Vector3 (explosion,explosion,explosion);
        }
        particlesOnDeath.Play();
        Destroy(particlesOnDeath, 5);
    }
}
