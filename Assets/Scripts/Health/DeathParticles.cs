using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class DeathParticles : DeathEffect
{

    public ParticleSystem particlesOnDeath;
    public Explode explodeScale;
    private float explosion;

    void Start()
    {  
        if(explodeScale != null){
            explosion = explodeScale.explosionRadius*1.5f;
        }
        
    }
    // Start is called before the first frame update
    public override void effect(){
        particlesOnDeath.transform.SetParent(null);
        if(explodeScale != null){
            particlesOnDeath.gameObject.transform.localScale = new Vector3 (explosion,explosion,explosion);
        }
        particlesOnDeath.Play();
        Destroy(particlesOnDeath, 5);
    }
}
