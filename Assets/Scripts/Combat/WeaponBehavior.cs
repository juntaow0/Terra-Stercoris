using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[ExecuteAlways]
public class WeaponBehavior : MonoBehaviour, IInteractable {

    [SerializeField] private Weapon _weaponStats;
    public int equippedLayer = 4;
    public int groundLayer = 2;
    private Collider2D _collider;
    private SpriteRenderer _renderer;
    private SpriteRenderer _realRenderer = null;
    public Weapon weaponStats {get {return _weaponStats;} private set {_weaponStats = value;}}
    public bool inCooldown {get; private set;} = false;

    private GameObject _weaponHolder = null;

    void Awake() {
        Initialize();
        if(_realRenderer != null) {
            _realRenderer.enabled = false;
        }
    }

    void OnValidate() {
        Initialize();
    }

    void Initialize() {
        if(_collider == null) _collider = GetComponent<Collider2D>();
        if(_renderer == null) _renderer = GetComponent<SpriteRenderer>();
        if(transform.childCount > 0) _realRenderer = transform.GetChild(0).GetComponentInChildren<SpriteRenderer>();
        _collider.isTrigger = true;
        if(weaponStats != null) {
            _renderer.sprite = weaponStats.icon;
            _renderer.sortingOrder = groundLayer;
            if(_realRenderer != null) {
                _realRenderer.sprite = weaponStats.icon;
                _realRenderer.sortingOrder = equippedLayer;
            }
        }
    }

    public void SetHolder(GameObject holder) {
        _weaponHolder = holder;
        if(holder == null) {
            transform.SetParent(null);
            _collider.enabled = _renderer.enabled = true;
        } else {
            Transform holderPivot = holder.transform.GetChild(0);
            transform.SetParent(holderPivot);
            transform.localPosition = Vector3.zero;
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
        _realRenderer.enabled = true;
        yield return new WaitForSeconds(0.166f);
        _realRenderer.enabled = false;
        yield return new WaitForSeconds(weaponStats.cooldownTime- 0.166f);
        inCooldown = false;
        
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