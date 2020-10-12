using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Powerbar powerBar;
    [SerializeField] private Healthbar healthBar;
    [SerializeField] private Notification notification;
    [SerializeField] private Tooltip toolTip;

    private void Start() {
        CharacterController playerInfo = PlayerController.instance.characterController;
        if (powerBar != null) {
            powerBar.bindPowerBar(playerInfo.energy);
        }
        if (healthBar != null) {
            healthBar.bindHealthBar(playerInfo.health);
        }
    }
}
