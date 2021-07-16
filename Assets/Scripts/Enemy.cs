using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _explodeEffectPrefab;

    private void destroy() {
            Instantiate(_explodeEffectPrefab, transform.position, Quaternion.identity);
            PlayerPrefs.SetInt("MoneyAmount", PlayerPrefs.GetInt("MoneyAmount") + 1);
            Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        Gun gun = collision.collider.GetComponent<Gun>();
        if (gun != null) {
            destroy();
            return;
        }

        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if (enemy != null) {
            destroy();
            return;
        }

        if (collision.contacts[0].normal.y < -0.5) {
            destroy();
            return;
        }
    }
}
