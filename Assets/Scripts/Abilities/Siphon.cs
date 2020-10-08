using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siphon : MonoBehaviour {

    private float _range = 5.0f;
    private int _healthPerTick = 1;
    private float _stealPercentage = 1.0f;
    private float _tickRate = 0.2f;
    private int _energyCostPerTick = 1;

    public bool Active {get; private set;}

    [SerializeField] private SpriteRenderer _originSprite;

    public void SetActive(bool value) {
        Active = value;
        if(Active) {
            StartCoroutine(siphon());
        }
    }
    
    [SerializeField] private ParticleTrack particles;

    IEnumerator siphon() {
        
        while(Active && (!DialogueManager.InConversation && !TimelineController.InCutscene)) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, PlayerController.instance.characterRotation, _range);
            if(hit.transform != null) {
                CharacterController target = hit.transform.gameObject.GetComponent<CharacterController>();
                if(target != null && target.health.quantity > 0) {
                    target.Damage(_healthPerTick);
                    PlayerController.instance.characterController.AddHealth((int) (_healthPerTick * _stealPercentage));
                    PlayerController.instance.characterController.AddEnergy(-_energyCostPerTick);
                    particles.particleSource = hit.transform.gameObject;
                    particles.particleSystem.Play();
                    _originSprite.enabled = true;
                } else {
                    _originSprite.enabled = false;
                    particles.particleSystem.Stop();
                }
            } else {
                _originSprite.enabled = false;
                particles.particleSystem.Stop();
            }
            yield return new WaitForSeconds(_tickRate);
        }
        _originSprite.enabled = false;
        particles.particleSystem.Stop();
    }
}
