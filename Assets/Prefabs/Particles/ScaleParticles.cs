
using UnityEngine;
using System.Collections;
 
[ExecuteInEditMode]
[RequireComponent(typeof(ParticleSystem))]
public class ScaleParticles : MonoBehaviour {


    void Awake() {
        ParticleSystem.MainModule main = gameObject.GetComponent<ParticleSystem>().main;
        main.scalingMode = ParticleSystemScalingMode.Hierarchy;
    }
}