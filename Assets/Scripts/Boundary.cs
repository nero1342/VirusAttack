using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;
        if(tag == "Gun")
        {
            Destroy(collider.gameObject);
        }
    }
}
