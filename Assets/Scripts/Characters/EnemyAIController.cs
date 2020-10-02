using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour {

    [SerializeField] private CharacterController _characterController = null;
    [SerializeField] private CombatController _combatController = null;
    [SerializeField] private SpriteRenderer _spriteRenderer = null;

    private Vector2 _targetPosition;
    private bool _seesTarget = false;

    void Awake() {
        if (_characterController == null) _characterController = GetComponent<CharacterController>();
        if (_combatController == null) _combatController = GetComponent<CombatController>();
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start() {
        _targetPosition = transform.position;
        StartCoroutine(FindPlayer());
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
        StopCoroutine(FindPlayer());
    }

    void Update() {

        if(_characterController.IsAlive && _combatController.currentWeapon != null) {

            if(_seesTarget) {
                _targetPosition = PlayerController.instance.transform.position;
            }

            Vector2 enemyDir = _targetPosition - (Vector2) transform.position;

            if(Vector2.Distance(transform.position, _targetPosition) >= _combatController.currentWeapon.range) {
                _characterController.Move(enemyDir);
                _characterController.SetSpriteRotation(enemyDir);
            } else {
                _characterController.Move(Vector2.zero);
                if(_seesTarget) {
                    _characterController.SetSpriteRotation(enemyDir);
                    _combatController.Attack(enemyDir);
                }
            }
        }
    }

    IEnumerator FindPlayer() {

        while(true) {
            if(PlayerController.instance == null) {
                yield return null;
            } else {
                RaycastHit2D hit = Physics2D.Linecast(transform.position, PlayerController.instance.transform.position);
                _seesTarget = hit.collider != null && hit.collider.gameObject == PlayerController.instance.gameObject;
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}
