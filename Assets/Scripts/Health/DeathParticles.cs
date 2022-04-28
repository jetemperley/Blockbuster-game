using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class DeathParticles : DeathEffect
{

    public ParticleSystem particlesOnDeath;
    // Start is called before the first frame update
    public override void effect(){
        particlesOnDeath.Play();
    }
}
