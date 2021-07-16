using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _explodeEffectPrefab;

    private void OnCollisionEnter2D(Collision2D collision) {
        Gun gun = collision.collider.GetComponent<Gun>();
        if (gun != null) {
            Instantiate(_explodeEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if (enemy != null) {
            Instantiate(_explodeEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        if (collision.contacts[0].normal.y < -0.5) {
            Instantiate(_explodeEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
