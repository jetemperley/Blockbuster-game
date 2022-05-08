
using UnityEngine;
using System.Collections;
 
[ExecuteInEditMode]
public class ScaleParticles : MonoBehaviour {


    void Awake() {
        ParticleSystem.MainModule main = gameObject.GetComponent<ParticleSystem>().main;
        main.scalingMode = ParticleSystemScalingMode.Hierarchy;
    }
}