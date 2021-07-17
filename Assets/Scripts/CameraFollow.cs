using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
  [HideInInspector]
  public Vector3 StartingPosition;


  private const float minCameraX = 0;
  private const float maxCameraX = 13;
  [HideInInspector]
  public bool IsFollowing;
  [HideInInspector]
  public Transform TargetToFollow;

  void Start()
  {
    StartingPosition = transform.position;
  }

  void Update()
  {
    if (IsFollowing)
    {
      if (TargetToFollow != null)
      {
        var targetPosition = TargetToFollow.transform.position;
        float x = Mathf.Clamp(targetPosition.x, minCameraX, maxCameraX);
        //camera follows bird's x position
        transform.position = new Vector3(x, StartingPosition.y, StartingPosition.z);
      }
      else
        IsFollowing = false;
    }
  }
}
