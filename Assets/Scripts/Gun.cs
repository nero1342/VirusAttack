using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{

    public float speed = 100f;
    public float _aliveTime = 30f;
    public float maxRangeDrag = 5f;
    [SerializeField] private GameObject _explodeEffectPrefab, _bulletPrefab;


    Vector3 _initialPosition; 
    bool _makeAShoot = false;
    Rigidbody2D rigidbody2d;
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.position;
        rigidbody2d = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        // lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckOutOfScreen();
        CheckDead();
        CheckShoot();
        // Rotate gun according to the direction of movement.
        if (_makeAShoot) {
            Vector3 move = Vector3.zero ;
            move.x= rigidbody2d.velocity.x;
            move.y = rigidbody2d.velocity.y;
            if (transform.position.y > -1) {
                float angle = Mathf.Atan2(-move.x, move.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }

        lineRenderer.SetPosition(1, _initialPosition);
        lineRenderer.SetPosition(0, transform.position);

    }

    void CheckDead() {
        if (_makeAShoot && rigidbody2d.velocity.magnitude <= 0.1) {
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
        rigidbody2d.gravityScale = 0.5f;
        _makeAShoot = true;
        lineRenderer.enabled = false;
    }
    private void OnMouseDrag() {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 diffDistance = _initialPosition - newPosition;

        // Adjust maximum range of dragging 
        float currRangeDrag = Mathf.Min(maxRangeDrag, diffDistance.magnitude);
        diffDistance = diffDistance / diffDistance.magnitude * currRangeDrag;
        newPosition.x = _initialPosition.x - diffDistance.x;
        newPosition.y = _initialPosition.y - diffDistance.y;
        transform.position = new Vector3(newPosition.x, newPosition.y, 0);

        // Rotate direction of gun 
        float angle = Mathf.Atan2(-diffDistance.x, diffDistance.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Obstacle obstacle = collision.collider.GetComponent<Obstacle>();
        if (obstacle != null) {
            Instantiate(_explodeEffectPrefab, transform.position, Quaternion.identity);
            // yield WaitForSeconds (3.0f);
            // ReloadScene();
            WaitForReloadScene(3);
            return;
        }
    }

    IEnumerator WaitForReloadScene(float time) {
        yield return new WaitForSeconds (time);
        ReloadScene();
    }

    void CheckShoot() {
        if (PlayerPrefs.GetInt ("IsBulletUnlocked") == 1 && Input.GetKeyDown(KeyCode.Space)) {
            Instantiate(_bulletPrefab, transform.position, transform.rotation);
        }
    }
}
