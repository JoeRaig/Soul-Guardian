using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeTest : MonoBehaviour
{
    public ParticleSystem smoke1VFX;
    public ParticleSystem smoke2VFX;
    public ParticleSystem smoke3VFX;
    public ParticleSystem darkVFX;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            smoke1VFX.Play();
            smoke2VFX.Play();
            smoke3VFX.Play();
            darkVFX.Play();
        }
    }
}
