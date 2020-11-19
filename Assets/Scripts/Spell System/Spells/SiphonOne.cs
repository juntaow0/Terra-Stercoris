using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiphonOne : SpellBehavior {
    [SerializeField] private GameObject particlesPrefab;
    [SerializeField] private GameObject userEffectPrefab;
    private ParticleTrack particles;
    private SpriteRenderer originSprite;
    private Camera mainCamera;
    private int healthPerTick = 1;
    private float stealPercentage = 1.0f;
    private float tickRate = 0.2f;
    private int energyCostPerTick = 1;
    private bool active;

    private void Start() {
        active = false;
        mainCamera = Camera.main;
        particles = Instantiate(particlesPrefab, transform.parent).GetComponent<ParticleTrack>();
        originSprite = Instantiate(userEffectPrefab, transform.parent).GetComponentInChildren<SpriteRenderer>();
        originSprite.enabled = false;
    }

    protected override void CoreBehavior(CharacterController user) {
        StartCoroutine(siphon(user));
    }

    protected override void StoppingMechanics() {
        active = false;
    }

    IEnumerator siphon(CharacterController user) {
        if (!DialogueManager.InConversation && !TimelineController.InCutscene) {
            Vector3 dir = Vector3.Normalize(mainCamera.ScreenToWorldPoint(Input.mousePosition) - user.transform.position);
            RaycastHit2D hit = Physics2D.Raycast(user.transform.position, dir, spellStats.range);
            if (hit.collider != null) {
                ISiphonable target = hit.transform.GetComponent<ISiphonable>();
                AudioManager.instance.Play("Siphon");
                if (target != null && target.IsSiphonable && user.energy.quantity >= energyCostPerTick) {
                    particles.particleSource = hit.transform;
                    active = true;
                    originSprite.enabled = true;
                    particles.Play();
                    Vector3 hitOffset = target.transform.position - (Vector3) hit.point;
                    while (active&&(hitOffset + user.transform.position-target.transform.position).magnitude<=spellStats.range && target.IsSiphonable && user.energy.quantity >= energyCostPerTick && user.IsAlive) {
                        target.Siphon(healthPerTick);
                        user.AddHealth((int)(healthPerTick * stealPercentage));
                        user.AddEnergy(-energyCostPerTick);
                        yield return new WaitForSeconds(tickRate);
                    }
                    active = false;
                    originSprite.enabled = false;
                    particles.Stop();
                }
            }
        }
    }
}
