using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _explodeEffectPrefab;

    bool _hasDied;

    private void OnCollisionEnter2D(Collision2D collision) {
      if (ShouldDieFromCollision(collision))
      {
        Die();
      }
    }

    bool ShouldDieFromCollision(Collision2D collision) {
      if (_hasDied) {
        return false;
      }

      Gun gun = collision.collider.GetComponent<Gun>();
      if (gun != null) {
          return true;
      }

      Enemy enemy = collision.collider.GetComponent<Enemy>();
      if (enemy != null) {
          return true;
      }

      if (collision.contacts[0].normal.y < -0.5) {
          return true;
      }

      return false;
    }

    void Die()
    {
      _hasDied = true;
      Instantiate(_explodeEffectPrefab, transform.position, Quaternion.identity);
      gameObject.SetActive(false);
    }
}
