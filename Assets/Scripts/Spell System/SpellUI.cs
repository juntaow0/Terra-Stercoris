using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellUI : MonoBehaviour
{
    [SerializeField] private Image spellIcon;
    private SpellController playerSC;
    [SerializeField] private Sprite defaultIcon;

    public void bindSpellSlot(SpellController playerSpellController) {
        if (playerSC != null) {
            playerSC.OnSelection -= updateSpellIcon;
        }
        playerSC = playerSpellController;
        if (playerSC != null) {
            Debug.Log("Spell UI bind");
            playerSC.OnSelection += updateSpellIcon;
            SpellBehavior selected = playerSC.GetCurrentSpell();
            updateSpellIcon(selected);
            // get cooldown progress here
        } else {
            spellIcon.sprite = defaultIcon;
        }
    }
    void updateSpellIcon(SpellBehavior sb) {
        if (sb != null) {
            Debug.Log("Change Icon");
            spellIcon.sprite = sb.spellStats.icon;
        } else {
            spellIcon.sprite = defaultIcon;
        }
    }
    private void OnDestroy() {
        if (playerSC != null) {
            playerSC.OnSelection -= updateSpellIcon;
        }
    }
}
