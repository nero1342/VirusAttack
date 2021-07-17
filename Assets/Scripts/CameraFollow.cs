using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
[HideInInspector]
public Vector3 StartingPosition;


private const float minCameraX = -2;
private const float maxCameraX = 13;
[HideInInspector]
public bool IsFollowing;
public GameObject gunObject;
Gun gun;

void Start()
{
        gun = gunObject.GetComponent<Gun>();
        StartingPosition = transform.position;
}

void Update()
{
        if (gun.isMove())
        {
                var targetPosition = gunObject.transform.position;
                float x = Mathf.Clamp(targetPosition.x, minCameraX, maxCameraX);
                //camera follows bird's x position
                transform.position = new Vector3(x, StartingPosition.y, StartingPosition.z);
        }
}
}
