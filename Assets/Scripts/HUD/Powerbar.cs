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
    private HUDFader hudFader;
    public int maxPower;

    private void Awake() {
        slider = GetComponent<Slider>();
        hudFader = GetComponent<HUDFader>();
    }

    public void bindPowerBar(int maxPower, int currentPower) {
        slider.maxValue = maxPower;
        slider.value = currentPower;
    }

    public void updatePowerBarUI(int amount)
    //changes the graphical appearance of the powerbar
    {
        slider.value = amount;
        hudFader.SwapSprite(amount);
    }

    /*
    public void gainPower(int gain)
    //Call to gain power
    {
        currentPower += gain;
        if (currentPower > maxPower)
        {
            currentPower = maxPower;
        }
        updatePowerBarUI();
    }

    public void losePower(int loss)
    //Call to lose power
    {
        currentPower -= loss;
        if (currentPower < 0)
        {
            currentPower = 0;
        }
        updatePowerBarUI();
    }

    public void setPower(int setVal)
    //set power to a specific value
    {
        currentPower = setVal;
        if (currentPower > maxPower)
        {
            currentPower = maxPower;
        }
        else if (currentPower < 0)
        {
            currentPower = 0;
        }
        updatePowerBarUI();
    }

    public int getCurrentPower()
    //return the player's current power as an integer
    {
        return currentPower;
    }
    */
}
