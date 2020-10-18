using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneDataLoader
{
    // save player data, but we only have wrench for now
    // maybe save some other states
    static private int[] playerWeapons = null;
    static private List<int> playerSpells = null;
    static private int playerHealth;
    static private int playerEnergy;

    public static void SaveStates() {
        SavePlayerStates();
    }

    public static void LoadStates() {
        LoadPlayerStates();
    }

    public static void Initialize() {
        playerHealth = 100;
        playerEnergy = 100;
    }

    private static void SavePlayerStates() {
        if (PlayerController.instance != null) {
            playerWeapons = PlayerController.instance.weaponController.GetWeapons();
            playerHealth = PlayerController.instance.characterController.GetHealth();
            playerEnergy = PlayerController.instance.characterController.GetEnergy();
            playerSpells = PlayerController.instance.transform.GetComponent<SpellController>().GetSpellID();
        }
        
    }

    private static void LoadPlayerStates() {
        if (PlayerController.instance != null) {
            if (playerWeapons != null) {
                PlayerController.instance.weaponController.SetWeapons(playerWeapons);
            }
            PlayerController.instance.characterController.SetHealth(playerHealth);
            PlayerController.instance.characterController.SetEnergy(playerEnergy);
            if (playerSpells != null) {
                PlayerController.instance.gameObject.GetComponent<SpellController>().SetSpells(playerSpells.ToArray());
            } 
        }
    }
}
