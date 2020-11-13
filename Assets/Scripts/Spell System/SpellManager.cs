using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spellPrefabs;
    public static SpellManager instance { get; private set; }

    private void Awake() {
        instance = this;
    }

    public GameObject createSpell(Transform parent, int spellId) {
        Debug.Log("spell id: " +spellId);
        return Instantiate(spellPrefabs[spellId], parent);
    }
}
