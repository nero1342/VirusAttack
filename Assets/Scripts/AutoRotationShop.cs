using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotationShop : MonoBehaviour
{
    // Start is called before the first frame update
    private static float angle = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angle += 3;
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }
}
