using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneDataLoader
{
    // save player data, but we only have wrench for now
    // maybe save some other states
    static private WeaponBase playerWeapon = null;
    static private int playerHealth;
    static private int playerEnergy;

    public static void SaveStates() {
        SavePlayerStates();
    }

    public static void LoadStates() {
        LoadPlayerStates();
    }

    public static void Initialize() {
        playerWeapon = null;
        playerHealth = 100;
        playerEnergy = 100;
    }

    private static void SavePlayerStates() {
        if (PlayerController.instance != null) {
            if (PlayerController.instance.combatController.currentWeapon != null) {
                playerWeapon = PlayerController.instance.combatController.currentWeapon.weaponBase;
            }
            playerHealth = PlayerController.instance.characterController.GetHealth();
            playerEnergy = PlayerController.instance.characterController.GetEnergy();
        }
        
    }

    private static void LoadPlayerStates() {
        if (PlayerController.instance != null) {
            if (playerWeapon != null) {
                PlayerController.instance.combatController.SetWeapon(playerWeapon);
            }
            PlayerController.instance.characterController.SetHealth(playerHealth);
            PlayerController.instance.characterController.SetEnergy(playerEnergy);
        }
    }
}
