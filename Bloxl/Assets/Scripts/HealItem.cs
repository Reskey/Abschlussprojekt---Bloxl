using Assets.Skripts;
using Assets.Skripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Skripts
{
    public class HealItem : MonoBehaviour
    {
        private void FixedUpdate()
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Healitem"), FindObjectOfType<HealthBar>().GetHealth() == 100);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            GameObject target = collision.gameObject;

            if (target.tag == "Player")
            {
                if (FindObjectOfType<HealthBar>().GetHealth() < 100)
                {

                    target.GetComponent<IDamageable>().TakeDamage(-20);

                    Destroy(gameObject);
                }
            }
            if (target.layer == 9)
            {
                Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
            }
        }
    }
}
