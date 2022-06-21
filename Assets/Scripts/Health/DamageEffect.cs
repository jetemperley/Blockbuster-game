using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    public float flashTime;
    Color originalColor;
    public Color flashColor;
    public MeshRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        originalColor = renderer.material.color;
    }

    // Update is called once per frame
    public void DamageFlash()
    {
        Debug.Log("flash");
        renderer.material.color = flashColor;
        Invoke("ResetColor", flashTime);
    }

    void ResetColor()
    {
        renderer.material.color = originalColor;
    }
}
