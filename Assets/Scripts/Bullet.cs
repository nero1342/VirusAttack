using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;

    [SerializeField] private GameObject _explodeEffectPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.rotation);
        Debug.Log(speed);
        rb.velocity = transform.forward.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        // if (rb.velocity.magnitude <= 0.1) {
        //     Instantiate(_explodeEffectPrefab, transform.position, Quaternion.identity);
        //     Destroy(gameObject);
        // }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Instantiate(_explodeEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
