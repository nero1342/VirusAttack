using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _explodeEffectPrefab;

    bool _hasDied;
    protected Vector3 _initialPosition;
    public int rewardMoney = 1;
    protected void Start() {
        Debug.Log("Emeny start");
        _initialPosition = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (ShouldDieFromCollision(collision)) {
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
            return false;
        }

        if (collision.contacts[0].normal.y < -0.5) {
            return true;
        }

        return false;
    }

    public void Die() {
        // Debug.Log(GetComponent<AudioSource>());
        // GetComponent<AudioSource>().Play(0);
        _hasDied = true;
        Instantiate(_explodeEffectPrefab, transform.position, Quaternion.identity);
        PlayerPrefs.SetInt("MoneyAmount", PlayerPrefs.GetInt("MoneyAmount") + rewardMoney);
        // Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
