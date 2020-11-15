using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrackR : MonoBehaviour {

    public Transform particleTarget = null;
    private Transform particleSource;
    private ParticleSystem particleSystem;
    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.Particle[] particles;
    

    private void Awake() {
        particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem != null) {
            mainModule = particleSystem.main;
            particles = new ParticleSystem.Particle[mainModule.maxParticles];
        } else {
            enabled = false;
        }
        particleSource = transform.parent.transform;
        transform.position = particleSource.position + new Vector3(0,0,-1);
    }

    public void Play() {
        particleSystem.Play();
    }

    public void Stop() {
        particleSystem.Stop();
    }

    void Update() {
        if(particleTarget != null) {

            particleSystem.startLifetime = Vector2.Distance(particleSource.position, particleTarget.position) / Mathf.Abs(mainModule.startSpeed.constant);

            int numParticlesAlive = particleSystem.GetParticles(particles);

            float velocity;
            float distance;

            // Change only the particles that are alive
            for (int i = 0; i < numParticlesAlive; ++i) {
                velocity = particles[i].velocity.magnitude;
                particles[i].velocity = velocity * (particleTarget.position - particles[i].position).normalized;
                distance = Vector2.Distance(particleTarget.position, particles[i].position);
                if(distance > 0.01f) {
                    particles[i].remainingLifetime = distance / velocity;
                } else {
                    particles[i].remainingLifetime = -1;
                }
            }
            particleSystem.SetParticles(particles, numParticlesAlive);
        }
    }
}
