using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class CameraMove : MonoBehaviour
{

private float dragSpeed = 0.02f;
private float timeDragStarted;
private Vector3 previousPosition = Vector3.zero;
public Gun gun;

void Update()
{
        //drag start
        // return;
        // Debug.Log(gun.gunState);
        if (gun.gunState == GunState.Idle || gun.gunState == GunState.Inactive) {

                if (Input.GetMouseButtonDown(0))
                {
                        timeDragStarted = Time.time;
                        previousPosition = Input.mousePosition;
                }
                else if (Input.GetMouseButton(0) && Time.time - timeDragStarted > 0.05f)
                {
                        Vector3 input = Input.mousePosition;
                        float deltaX = (previousPosition.x - input.x)  * dragSpeed;
                        float deltaY = (previousPosition.y - input.y) * dragSpeed;

                        float newX = Mathf.Clamp(transform.position.x + deltaX, 0, 13.36336f);
                        float newY = Mathf.Clamp(transform.position.y + deltaY, 0, 2.715f);

                        //move camera
                        transform.position = new Vector3(
                                newX,
                                newY,
                                transform.position.z);

                        previousPosition = input;
                }
        }
}
}