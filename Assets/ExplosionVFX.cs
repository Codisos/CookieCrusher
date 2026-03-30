using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionVFX : MonoBehaviour
{
    ParticleSystem particles;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        source = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (!particles.IsAlive() && !source.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
