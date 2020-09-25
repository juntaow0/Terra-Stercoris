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
    public int currentPower;
    public int maxPower;

    private void Awake() {
        slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        maxPower = 100;
        currentPower = maxPower;
        slider.maxValue = maxPower;
        slider.value = currentPower;
    }

    private void updatePowerBarUI()
    //changes the graphical appearance of the powerbar
    {
        slider.value = currentPower;
    }

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
}
