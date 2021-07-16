using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{
    Vector3 _initialPosition; 
    bool _makeAShot = false;
    public float speed = 100f;
    public float _aliveTime = 3;

    Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckOutOfScreen();
        CheckDead();
    }

    void CheckDead() {
        if (_makeAShot && rigidbody.velocity.magnitude <= 0.1) {
            _aliveTime -= Time.deltaTime;
        }
        if (_aliveTime <= 0) {
            ReloadScene();
        }
    }
    void ReloadScene() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    void CheckOutOfScreen() {
        if (transform.position.x > 10 ||
            transform.position.y > 10 || 
            transform.position.x < -10||
            transform.position.y < -10) {
                ReloadScene();
            }
    }
    private void OnMouseDown() {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
    private void OnMouseUp() {
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        rigidbody.AddForce(directionToInitialPosition * speed);
        rigidbody.gravityScale = 1;
        _makeAShot = true;
    }
    private void OnMouseDrag() {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y, 0);
    }
}
