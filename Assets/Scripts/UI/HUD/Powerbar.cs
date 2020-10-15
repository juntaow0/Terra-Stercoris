using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Useful functions:
 * gainPower(int gain) - increases power by gain
 * losePower(int loss) - decreases power by loss
 * setPower(int setVal) - set power to a specific integer value
 * getPower() - returns power as an integer value
 */

public class Powerbar : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private HUDFader hudFader;
    private NumericalResource power;

    private void Awake() {
        slider = GetComponent<Slider>();
    }

    public void bindPowerBar(NumericalResource power) {
        if (this.power != null) {
            this.power.OnResourceUpdated -= updatePowerBarUI;
        }
        this.power = power;
        if (this.power != null) {
            this.power.OnResourceUpdated += updatePowerBarUI;
            slider.maxValue = power.max;
            slider.value = power.quantity;
        } else {
            slider.value = 0;
        }
    }

    void updatePowerBarUI(int amount)
    {
        slider.value = amount;
        hudFader?.SwapSprite(amount);
    }

    private void OnDestroy() {
        if (PlayerController.instance != null) {
            PlayerController.instance.characterController.energy.OnResourceUpdated -= updatePowerBarUI;
        }
    }
}
