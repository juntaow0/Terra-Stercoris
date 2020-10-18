using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellBehavior:MonoBehaviour
{
    public Spell spellStats;
    public bool inCooldown { get; private set; } = false;
    public void Cast(CharacterController user) {
        if (!inCooldown) {
            CoreBehavior(user);
            StartCoroutine(Cooldown());
            Debug.Log(spellStats.name + " fired");
        } else {
            Debug.Log(spellStats.name + " in cooldown");
        }
    }

    public void StopCast() {
        StoppingMechanics();
    }

    protected abstract void CoreBehavior(CharacterController user);
    protected abstract void StoppingMechanics();

    protected SpellStates spellState = SpellStates.Ready;

    public SpellStates GetState() {
        return spellState;
    }

    IEnumerator Cooldown() {
        inCooldown = true;
        yield return new WaitForSeconds(spellStats.cooldownTime);
        inCooldown = false;
    }
}
