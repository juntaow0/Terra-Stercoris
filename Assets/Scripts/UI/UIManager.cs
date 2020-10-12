using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Powerbar powerBar;
    [SerializeField] private Healthbar healthBar;
    [SerializeField] private Notification notification;
    [SerializeField] private Tooltip toolTip;
    public static UIManager instance { get; private set; }

    private void Awake() {
        instance = this;
    }

    private void Start() {
        CharacterController playerInfo = PlayerController.instance.characterController;
        if (powerBar != null) {
            powerBar.bindPowerBar(playerInfo.energy);
        }
        if (healthBar != null) {
            healthBar.bindHealthBar(playerInfo.health);
        }
    }

    public void showTooltip(InteractableObject obj) {
        toolTip.Show(obj);
    }

    public void hdieTooltip() {
        toolTip.Hide();
    }
}
