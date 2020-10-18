using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType {
    RANGED,
    PROJECTILE,
    SELF
}

abstract public class BaseSkill {
    public SkillType Type {get; private set;}
    public float Cooldown {get; private set;}
    public float Range {get; private set;} 

    public GameObject projectile;
    public Sprite slotSprite;

    public BaseSkill(SkillType type, float cooldown, float range, Sprite slotSprite = null, GameObject projectile = null) {
        Type = type;
        Cooldown = cooldown;
        Range = range;
        this.projectile = projectile;
        this.slotSprite = slotSprite;
    }

    
}
