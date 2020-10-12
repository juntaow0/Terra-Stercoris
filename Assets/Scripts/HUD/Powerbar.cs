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
        this.power = power;
        slider.maxValue = power.max;
        slider.value = power.quantity;
        power.OnResourceUpdated += updatePowerBarUI;
    }

    void updatePowerBarUI(int amount)
    {
        slider.value = amount;
        hudFader?.SwapSprite(amount);
    }

    private void OnDisable() {
        power.OnResourceUpdated -= updatePowerBarUI;
    }

    private void OnDestroy() {
        OnDisable();
    }
}
