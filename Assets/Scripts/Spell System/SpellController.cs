using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    [SerializeField] private GameObject[] spellPrefabs;
    public event Action OnSpellCast;
    public event Action<SpellBehavior> OnSelection;
    private List<SpellBehavior> spellRack;
    private Dictionary<int,SpellBehavior> usedSpells;
    private Dictionary<int, bool> spellStatus;
    private SpellBehavior selected;
    private bool hasSpell = false;
    private CharacterController cc;
    [SerializeField] private bool isPlayer;

    private void Awake() {
        spellRack = new List<SpellBehavior>();
        cc = GetComponent<CharacterController>();
        usedSpells = new Dictionary<int, SpellBehavior>();
        spellStatus = new Dictionary<int, bool>();
        Initialize();
    }

    private void Initialize() {
        if (spellPrefabs.Length > 0) {
            foreach (GameObject go in spellPrefabs) {
                GameObject prefab = Instantiate(go, transform);
                SpellBehavior spell = prefab.GetComponent<SpellBehavior>();
                spellRack.Add(spell);
                usedSpells[spell.spellStats.spellID] = spell;
                spellStatus[spell.spellStats.spellID] = true;
            }
            hasSpell = true;
            selected = spellRack[0];
            OnSelection?.Invoke(selected);
        }
    }

    public void Cast() {
        if (hasSpell && cc.IsAlive) {
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
        if (!usedSpells.ContainsKey(spellID)) {
            GameObject spellPrefab = SpellManager.instance.createSpell(transform, spellID);
            SpellBehavior spell = spellPrefab.GetComponent<SpellBehavior>();
            spellRack.Add(spell);
            usedSpells[spellID] = spell;
            spellStatus[spellID] = true;
        } else { 
            if (!spellStatus[spellID]) {
                spellRack.Add(usedSpells[spellID]);
                spellStatus[spellID] = true;
            }
        }
        
        if (!hasSpell) {
            hasSpell = true;
            selected = spellRack[0];
            OnSelection?.Invoke(selected);
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
            if (spellStatus.ContainsKey(id)&& spellStatus[id]) {
                spellRack.Remove(usedSpells[id]);
                spellStatus[id] = false;
                if (selected.spellStats.spellID == id) {
                    selected = null;
                }
            }
            if (spellRack.Count < 1) {
                hasSpell = false;
                selected = null;
            }
            OnSelection?.Invoke(selected);
        }
    }

    public void SelectSpell(int index) {
        if (spellRack.Count > 0) {
            Debug.Log("scrolled");
            int realIndex = (index + spellRack.Count) % spellRack.Count;
            selected = spellRack[realIndex];
            OnSelection?.Invoke(selected);
        }
    }

    public void OffsetSlot(int offset) {
        if (spellRack.Count > 0) {
            int index = spellRack.IndexOf(selected) + offset;
            SelectSpell(index);
        }
    }

    public SpellBehavior GetCurrentSpell() {
        return selected;
    }

    private void OnEnable() {
        InputManager.OnMouseClickRight += Cast;
        InputManager.OnScroll += OffsetSlot;
        InputManager.OnMouseUpRight += StopCast;
    }

    private void OnDisable() {
        InputManager.OnMouseClickRight -= Cast;
        InputManager.OnScroll -= OffsetSlot;
        InputManager.OnMouseUpRight -= StopCast;
    }

    private void OnDestroy() {
        InputManager.OnMouseClickRight -= Cast;
        InputManager.OnScroll -= OffsetSlot;
        InputManager.OnMouseUpRight -= StopCast;
    }
}
