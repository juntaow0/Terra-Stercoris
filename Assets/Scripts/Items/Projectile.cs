using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] float speed;
    [SerializeField] float lifetime;
    [SerializeField] int damage;
    [SerializeField] Rigidbody2D body;

    GameObject source;

    public void Setup(float speed, float range, int damage, Rigidbody2D rigidbody) {
        this.speed = speed;
        this.lifetime = range / speed;
        this.damage = damage;
        this.body = rigidbody;
    }

    public void Fire(Vector2 startLocation, Vector2 direction, Vector2 inheritVelocity, GameObject source) {
        transform.position = startLocation;
        body.velocity = direction.normalized * speed + inheritVelocity;
        this.source = source;
        StartCoroutine(waitForLifetime());
    }

    IEnumerator waitForLifetime() {
        yield return new WaitForSeconds(lifetime);

        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject != source) {
            col.gameObject.GetComponent<CharacterController>()?.Damage(damage);
            gameObject.SetActive(false);
        }
    }
}