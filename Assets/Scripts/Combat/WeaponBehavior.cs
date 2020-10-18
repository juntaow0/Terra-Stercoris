using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour, IInteractable {

    [SerializeField] private Weapon _weaponStats;
    private Collider2D _collider;
    private SpriteRenderer _renderer;
    public Weapon weaponStats {get {return _weaponStats;} private set {_weaponStats = value;}}
    public bool inCooldown {get; private set;} = false;

    private GameObject _weaponHolder = null;

    void Awake() {
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void SetHolder(GameObject holder) {
        _weaponHolder = holder;
        if(holder == null) {
            transform.SetParent(null);
            _collider.enabled = _renderer.enabled = true;
        } else {
            transform.position = holder.transform.position;
            transform.SetParent(holder.transform);
            _collider.enabled = _renderer.enabled = false;
        }
    }

    public void Attack(CharacterController user) {
        if (!inCooldown && !InteractEnabled) {
            StartCoroutine(Cooldown());
            RaycastHit2D hit = Physics2D.Raycast(user.transform.position, user.rotation, _weaponStats.range);
            if(hit.collider != null) {
                hit.transform.GetComponent<IDamagable>()?.Damage(weaponStats.damage);
            }
            Debug.Log(user.name + " attacked using " + weaponStats.name);
        }
    }

    IEnumerator Cooldown() {
        inCooldown = true;
        _renderer.enabled = true;
        yield return new WaitForSeconds(weaponStats.cooldownTime);
        inCooldown = false;
        _renderer.enabled = false;
    }

    public string message {get {return "Pick Up";} set {}}
    public bool InteractEnabled {get {return _weaponHolder == null;} set {}}

    public void Interact() {
        PlayerController.instance.weaponController.SetWeapon(this);
    }

    // Forced to be added by IInteractable
    public void StopInteract() {}

    /*private int _nextShot = 0;
    private List<Projectile> projectiles;*/

    /*public Projectile GetProjectile() {

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
    }*/

    /*public Projectile CreateProjectile() {
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
    }*/

    /*public void ClearProjectiles() {
        foreach(Projectile proj in projectiles) {
            Object.Destroy(proj.gameObject);
        }
    }*/
}