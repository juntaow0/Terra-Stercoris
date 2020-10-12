using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//THE ONLY FUNCTION USED IS UPDATE GUI, EVERYTHING ELSE IS ALREADY ACCOUNTED FOR BY NUMERICALRESOURCE

/* Useful functions:
 * gainHealth(int gain) - increases health by gain
 * loseHealth(int loss) - decreases health by loss
 * setHealth(int setVal) - set health to a specific integer value
 * getHealth() - returns health as an integer value
 */ 

public class Healthbar : MonoBehaviour
{
    private Slider slider;
    private NumericalResource health;

    private void Awake() {
        slider = GetComponent<Slider>();
    }

    public void bindHealthBar(NumericalResource health) {
        if (this.health != null) {
            this.health.OnResourceUpdated -= updateHealthBarUI;
        }
        this.health = health;
        if (this.health != null) {
            this.health.OnResourceUpdated += updateHealthBarUI;
            slider.maxValue = health.max;
            slider.value = health.quantity;
        } else {
            slider.value = 0;
        }
    }

    void updateHealthBarUI(int amount)
    {
        slider.value = amount;
    }

    private void OnDisable() {
        PlayerController.instance.characterController.health.OnResourceUpdated -= updateHealthBarUI;
    }

    private void OnDestroy() {
        OnDisable();
    }
}
