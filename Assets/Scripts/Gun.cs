using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class Gun : MonoBehaviour
{

    public float speed = 100f;
    public float _aliveTime = 3f;
    public float maxRangeDrag = 5f;
    [SerializeField] private GameObject _explodeEffectPrefab, _bulletPrefab;


    Vector3 _initialPosition;
    Rigidbody2D rigidbody2d;
    LineRenderer lineRenderer;

    [HideInInspector]
    public GunState gunState;

    // Start is called before the first frame update
    void Start()
    {
        gunState = GunState.Inactive;
        _initialPosition = transform.position;
        rigidbody2d = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();

        rigidbody2d.isKinematic = true;
        lineRenderer.enabled = false;
    }

    public void reset()
    {
        gunState = GunState.Idle;
        _aliveTime = 2f;
        speed = 300;
        transform.position = _initialPosition;
        rigidbody2d.isKinematic = true;
        rigidbody2d.velocity = new Vector2(0, 0);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        CheckShoot();
        // Rotate gun according to the direction of movement.
        if (gunState == GunState.Thrown)
        {
            Vector3 move = Vector3.zero;
            move.x = rigidbody2d.velocity.x;
            move.y = rigidbody2d.velocity.y;
            if (transform.position.y > -1)
            {
                float angle = Mathf.Atan2(-move.x, move.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }

        lineRenderer.SetPosition(1, _initialPosition);
        lineRenderer.SetPosition(0, transform.position);
    }

    public bool isDone()
    {
        if (gunState == GunState.Thrown && rigidbody2d.velocity.magnitude <= 0.1)
        {
            _aliveTime -= Time.deltaTime;

        }
        if (_aliveTime <= 0)
        {
            return true;
        }
        return false;
    }

    public bool isMove()
    {
        return gunState == GunState.Thrown || gunState == GunState.BeforeThrown;
    }

    private void OnMouseDown()
    {
        if (gunState == GunState.Inactive) return;
        gunState = GunState.BeforeThrown;
        GetComponent<SpriteRenderer>().color = Color.red;
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = true;
    }
    private void OnMouseUp()
    {
        if (gunState == GunState.Thrown) return;
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        rigidbody2d.isKinematic = false;
        rigidbody2d.AddForce(directionToInitialPosition * speed);
        gunState = GunState.Thrown;
        lineRenderer.enabled = false;
        if (PlayerPrefs.GetInt ("LineUnlocked") > 0)
            PlayerPrefs.SetInt ("LineUnlocked", PlayerPrefs.GetInt ("LineUnlocked") - 1);
        GetComponent<AudioSource>().Play();
    }
    private void OnMouseDrag()
    {
        if (gunState == GunState.Thrown) return;
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

        if (PlayerPrefs.GetInt ("LineUnlocked") > 0) {
            Vector2[] trajactory = Plot(rigidbody2d, (Vector2)transform.position, diffDistance * speed, 500);
            lineRenderer.positionCount = trajactory.Length;
            Vector3[] positions = new Vector3[trajactory.Length];
            for (int i = 0; i < trajactory.Length; i++)
            {
            positions[i] = trajactory[i];
            }
            lineRenderer.SetPositions(positions);
        }
       

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Obstacle obstacle = collision.collider.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            Instantiate(_explodeEffectPrefab, transform.position, Quaternion.identity);
            return;
        }
    }

    void CheckShoot()
    {
        int bulletAmount = PlayerPrefs.GetInt("BulletAmount");
        if (bulletAmount > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_bulletPrefab, transform.position, transform.rotation);
            bulletAmount -= 1;
            PlayerPrefs.SetInt("BulletAmount", bulletAmount);
        }
    }

    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * 3500f * timestep * timestep;
        Debug.Log(Physics2D.gravity * 3500f);
        float drag = 1f - timestep * rigidbody.drag;

        Vector2 moveStep = velocity * timestep;
        for (int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }
        return results;
    }

}
