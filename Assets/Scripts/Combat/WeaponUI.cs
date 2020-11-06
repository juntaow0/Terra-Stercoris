using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Image weaponIcon;
    private WeaponController playerWC;
    [SerializeField] private Sprite defaultIcon;

    public void bindWeaponSlot(WeaponController playerWeaponController) {
        if (playerWC != null) {
            playerWC.OnSelection -= updateWeaponIcon;
        }
        playerWC = playerWeaponController;
        if (playerWC != null) {
            playerWC.OnSelection += updateWeaponIcon;
            WeaponBehavior selected = playerWC.GetWeapon();
            updateWeaponIcon(selected);
            // get cooldown progress here
        } else {
            weaponIcon.sprite = defaultIcon;
        }
    }
    void updateWeaponIcon(WeaponBehavior wb) {
        if (wb != null) {
            Debug.Log("Change Icon");
            weaponIcon.sprite = wb.weaponStats.icon;
        } else {
            weaponIcon.sprite = defaultIcon;
        }
    }
    private void OnDestroy() {
        if (playerWC != null) {
            playerWC.OnSelection -= updateWeaponIcon;
        }
    }
}
