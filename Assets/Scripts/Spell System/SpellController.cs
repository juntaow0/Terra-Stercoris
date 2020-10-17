using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    [SerializeField] private GameObject[] spellPrefabs;
    public event Action OnSpellCast;
    private List<SpellBehavior> spellRack;
    private SpellBehavior selected;
    private bool hasSpell = false;
    private CharacterController cc;
    [SerializeField] private bool isPlayer;

    private void Awake() {
        spellRack = new List<SpellBehavior>();
        cc = GetComponent<CharacterController>();
        Initialize();
    }

    private void Initialize() {
        if (spellPrefabs.Length > 0) {
            foreach (GameObject go in spellPrefabs) {
                GameObject prefab = Instantiate(go, transform);
                spellRack.Add(prefab.GetComponent<SpellBehavior>());
            }
            hasSpell = true;
            selected = spellRack[0];
        }
    }

    public void Cast() {
        if (hasSpell) {
            selected.Cast(cc);
            OnSpellCast?.Invoke();
        }
    }

    public void StopCast() {
        if (hasSpell) {
            selected.StopCast();
        }
    }

    public void SetSpell(int spellID) {
        GameObject spellPrefab = SpellManager.instance.createSpell(transform, spellID);
        spellRack.Add(spellPrefab.GetComponent<SpellBehavior>());
        if (!hasSpell) {
            hasSpell = true;
            selected = spellRack[0];
        }
    }

    public void SetSpells(int[] spellIDs) {
        for (int i = 0; i < spellIDs.Length; i++) {
            SetSpell(spellIDs[i]);
        }
    }

    public List<int> GetSpellID() {
        List<int> ids = new List<int>();
        foreach (SpellBehavior sb in spellRack) {
            ids.Add(sb.spellStats.spellID);
        }
        return ids;
    }

    public void RemoveSpell(int id) {
        if (hasSpell) {
            for (int i = 0; i < spellRack.Count; i++) {
                if (spellRack[i].spellStats.spellID == id) {
                    spellRack.RemoveAt(i);
                    break;
                }
            }
            if (spellRack.Count < 1) {
                hasSpell = false;
            }
        }
    }

    public void SelectSpell(int index) {
        int realIndex = index % spellRack.Count;
        selected = spellRack[realIndex];
    }

    private void OnEnable() {
        InputManager.OnMouseClickRight += Cast;
        InputManager.OnScroll += SelectSpell;
        InputManager.OnMouseUpRight += StopCast;
    }

    private void OnDisable() {
        InputManager.OnMouseClickRight -= Cast;
        InputManager.OnScroll -= SelectSpell;
        InputManager.OnMouseUpRight -= StopCast;
    }

    private void OnDestroy() {
        InputManager.OnMouseClickRight -= Cast;
        InputManager.OnScroll -= SelectSpell;
        InputManager.OnMouseUpRight -= StopCast;
    }
}
