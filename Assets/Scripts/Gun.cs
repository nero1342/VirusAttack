using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{
    Vector3 _initialPosition; 
    bool _makeAShot = false;
    public float speed = 100f;
    public float _aliveTime = 30;
    Rigidbody2D rigidbody2d;
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.position;
        rigidbody2d = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckOutOfScreen();
        CheckDead();
        // Rotate gun according to the direction of movement.
        if (_makeAShot) {
            Vector3 move = Vector3.zero ;
            move.x= rigidbody2d.velocity.x;
            move.y = rigidbody2d.velocity.y;
            if (transform.position.y > -1) {
                float angle = Mathf.Atan2(-move.x, move.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }

        lineRenderer.SetPosition(0, _initialPosition);
        lineRenderer.SetPosition(1, transform.position);
        


    }

    void CheckDead() {
        if (_makeAShot && rigidbody2d.velocity.magnitude <= 0.1) {
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
        lineRenderer.enabled = true;
        
    }
    private void OnMouseUp() {
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        rigidbody2d.AddForce(directionToInitialPosition * speed);
        rigidbody2d.gravityScale = 1;
        _makeAShot = true;
        lineRenderer.enabled = false;
    }
    private void OnMouseDrag() {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y, 0);
        
        Vector2 diffDistance = _initialPosition - transform.position;
        
        float angle = Mathf.Atan2(-diffDistance.x, diffDistance.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }
}
