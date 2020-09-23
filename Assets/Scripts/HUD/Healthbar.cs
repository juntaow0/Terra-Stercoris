using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Useful functions:
 * gainHealth(int gain) - increases health by gain
 * loseHealth(int loss) - decreases health by loss
 * setHealth(int setVal) - set health to a specific integer value
 * getHealth() - returns health as an integer value
 */ 

public class Healthbar : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void gainHealth(int gain)
    //Call to gain health
    {
        currentHealth += gain;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void loseHealth(int loss)
    //Call to lose health
    {
        currentHealth -= loss;
        //eventually, we should call a function here that triggers player death, or something similar 
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
    }

    public int getCurrentHealth()
    //return the player's current health as an integer
    {
        return currentHealth;
    }
}
