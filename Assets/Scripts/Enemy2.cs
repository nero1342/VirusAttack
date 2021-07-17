using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    // Enemy with linear move from _initPos -> _endPos;
    public float _maxRange = 3f;
    public Vector3 _randomDirection, _endPosition;
    public bool fixedDirection = true;
    
    int _direction;
    public float _speed = 1f;
    protected void Start() {
        Debug.Log("Emeny2 start");
        _randomDirection = new Vector3(Random.value, Random.value, 0).normalized;
        _direction = 1;
    }
    void Update() {
        Vector3 newPosition = transform.position + _randomDirection * _speed * Time.deltaTime;
        transform.position = newPosition;
        
        if ((newPosition - _initialPosition).magnitude >= _maxRange) {
            _direction *= -1;
            _randomDirection *= -1;
            Vector3 temp = _initialPosition;
            _initialPosition = newPosition;
            newPosition = temp;
            if (_direction == 1 && !fixedDirection) {
                _randomDirection = new Vector3(Random.value, Random.value, 0).normalized;
            }
        }
    }
}
