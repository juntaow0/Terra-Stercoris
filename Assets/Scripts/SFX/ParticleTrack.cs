using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrack : MonoBehaviour {

    public GameObject particleSource = null;
    public ParticleSystem particleSystem;
    private ParticleSystem.MainModule _mainModule;
    private ParticleSystem.EmissionModule _emissionModule;

    private ParticleSystem.Particle[] particles;

    void Start() {
        particleSystem = GetComponent<ParticleSystem>();

        _emissionModule = particleSystem.emission;
        _mainModule = particleSystem.main;

        particles = new ParticleSystem.Particle[_mainModule.maxParticles];
    }

    void Update() {
        if(particleSource != null) {
            transform.position = particleSource.transform.position + new Vector3(0,0,-1);
            //transform.rotation = Quaternion.AngleAxis(
            //        Vector2.SignedAngle(Vector2.right, transform.position - transform.parent.position), Vector3.forward) * Quaternion.Euler(0, 90, 0);

            particleSystem.startLifetime = Vector2.Distance(transform.position, transform.parent.position) / Mathf.Abs(_mainModule.startSpeed.constant);

            int numParticlesAlive = particleSystem.GetParticles(particles);

            float velocity;
            float distance = 0;

            // Change only the particles that are alive
            for (int i = 0; i < numParticlesAlive; ++i) {
                velocity = particles[i].velocity.magnitude;
                particles[i].velocity = velocity * (transform.parent.position - particles[i].position).normalized;
                distance = Vector2.Distance(transform.parent.position, particles[i].position);
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
