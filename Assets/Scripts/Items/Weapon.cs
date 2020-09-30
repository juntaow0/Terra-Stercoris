using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon {

    public string name;
    public WeaponType type;
    public int damage;
    public float range;
    public float cooldown;
    public float speed;
    public Sprite projectileSprite;

    private int _nextShot = 0;
    private List<Projectile> projectiles;

    public Weapon(WeaponBase weapon) {
        name = weapon.name;
        type = weapon.type;
        damage = weapon.damage;
        range = weapon.range;
        cooldown = weapon.cooldown;
        speed = weapon.speed;
        projectileSprite = weapon.projectileSprite;

        projectiles = new List<Projectile>();
        projectiles.Add(CreateProjectile());
    }

    public Projectile GetProjectile() {

        if(projectiles[_nextShot].gameObject.activeSelf) {
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
}