using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon {

    public WeaponBase weaponBase {get; private set;}

    public string name;
    public WeaponType type;
    public int damage;
    public float range;
    public float cooldown;
    public float speed;
    public Sprite projectileSprite;
    public Sprite weaponSprite;

    private int _nextShot = 0;
    private List<Projectile> projectiles;

    public Weapon(WeaponBase weapon) {
        weaponBase = weapon;

        name = weapon.name;
        type = weapon.type;
        damage = weapon.damage;
        range = weapon.range;
        cooldown = weapon.cooldown;
        speed = weapon.speed;
        projectileSprite = weapon.projectileSprite;
        weaponSprite = weapon.weaponSprite;

        if(type == WeaponType.RANGED) {
            projectiles = new List<Projectile>();
        }
    }

    public Projectile GetProjectile() {

        if(projectiles.Count == 0) {
            projectiles.Add(CreateProjectile());
            _nextShot = 0;
        } else if(projectiles[_nextShot].gameObject.activeSelf) {
            _nextShot = (_nextShot + 1) % projectiles.Count;
            if(projectiles[_nextShot].gameObject.activeSelf) {
                _nextShot = projectiles.Count;
                projectiles.Add(CreateProjectile());
            }
        }

        return projectiles[_nextShot];
    }

    public Projectile CreateProjectile() {
        GameObject projectile = new GameObject(name + " Projectile");
        projectile.SetActive(false);
        
        SpriteRenderer renderer = projectile.AddComponent<SpriteRenderer>();
        renderer.sprite = projectileSprite;

        Rigidbody2D rigidbody = projectile.AddComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;

        CircleCollider2D collider = projectile.AddComponent<CircleCollider2D>();
        collider.radius = 0.1f;
        collider.isTrigger = true;

        Projectile projectileComponent = projectile.AddComponent<Projectile>();
        projectileComponent.Setup(speed, range, damage, rigidbody);

        return projectileComponent;
    }

    public void ClearProjectiles() {
        foreach(Projectile proj in projectiles) {
            Object.Destroy(proj.gameObject);
        }
    }
}