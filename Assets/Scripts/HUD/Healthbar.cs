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
    public int maxHealth;

    private void Awake() {
        slider = GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void bindHealthBar(int maxHealth, int currentHealth) {
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
    }

    public void updateHealthBarUI(int amount)
    //changes the graphical appearance of the healthbar
    {
        slider.value = amount;
    }

    /*
    public void gainHealth(int gain)
    //Call to gain health
    {
        currentHealth += gain;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        updateHealthBarUI();
    }

    public void loseHealth(int loss)
    //Call to lose health
    {
        currentHealth -= loss;
        //eventually, we should call a function here that triggers player death, or something similar
        updateHealthBarUI();
    }

    public void setHealth(int setVal)
    //set health to a specific value (does not allow going over max and can trigger death)
    {
        currentHealth = setVal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            //eventually call a function to trigger death or something similar
            ;
        }
        updateHealthBarUI();
    }

    public int getCurrentHealth()
    //return the player's current health as an integer
    {
        return currentHealth;
    }
    */
}
