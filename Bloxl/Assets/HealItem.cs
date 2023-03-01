using Assets.Skripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject target = collision.gameObject;

        if (target.tag == "Player")
        {
            if (FindObjectOfType<HealthBar>().GetHealth() < 100)
            {

                target.GetComponent<PlayerController>().TakeDamage(-20, Vector2.down);

                Destroy(gameObject);
            } 
        }
        if (target.layer == 9)
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }
    }
}
