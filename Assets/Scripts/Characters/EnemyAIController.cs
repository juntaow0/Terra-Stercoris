using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour {

    [SerializeField] private CharacterController _characterController = null;
    [SerializeField] private WeaponController _weaponController = null;
    [SerializeField] private SpriteRenderer _spriteRenderer = null;
    [SerializeField] private GameObject _target = null;

    private Vector2 _targetPosition;
    private bool _seesTarget = false;

    void Awake() {
        if (_characterController == null) _characterController = GetComponent<CharacterController>();
        if (_weaponController == null) _weaponController = GetComponent<WeaponController>();
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start() {
        _targetPosition = transform.position;
        StartCoroutine(FindTarget());
    }

    void OnEnable() {
        _characterController.OnDeath += Die;
    }

    void OnDisable() {
        _characterController.OnDeath -= Die;
    }

    void OnDestroy() {
        OnDisable();
    }

    void Die() {
        _characterController.Move(Vector2.zero);
        StopCoroutine(FindTarget());
    }

    public void SetTarget(GameObject target) {
        _target = target;
        _seesTarget = false;
        _targetPosition = transform.position;
    }

    public void UnsetTarget() {
        SetTarget(null);
    }

    void Update() {

        if(_characterController.IsAlive && _weaponController.selected != null) {

            if(_seesTarget) {
                _targetPosition = _target.transform.position;
            }

            Vector2 enemyDir = _targetPosition - (Vector2) transform.position;

            if(Vector2.Distance(transform.position, _targetPosition) >= _weaponController.selected.weaponStats.range) {
                _characterController.rotation = enemyDir;
                _characterController.Move(enemyDir);
            } else {
                _characterController.Move(Vector2.zero);
                if(_seesTarget) {
                    _characterController.rotation = enemyDir;
                    _weaponController.Attack();
                }
            }
        } else {
            _characterController.Move(Vector2.zero);
        }
    }

    IEnumerator FindTarget() {

        while(true) {
            if(_target == null) {
                yield return null;
            } else {
                RaycastHit2D hit = Physics2D.Linecast(transform.position, _target.transform.position);
                _seesTarget = hit.collider != null && hit.collider.gameObject == _target;
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}
