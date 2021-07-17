using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {

public GameController gameController;

void OnTriggerEnter2D(Collider2D collider)
{
        string tag = collider.gameObject.tag;
        if(tag == "Gun")
        {
                gameController.EndTurn();
        }

        if(tag == "Enemy")
        {
                collider.gameObject.GetComponent<Enemy>().Die();
        }
}
}
