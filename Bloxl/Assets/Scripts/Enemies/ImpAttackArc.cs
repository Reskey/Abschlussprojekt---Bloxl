using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Skripts.Enemies
{
    internal class ImpAttackArc : MonoBehaviour
    {
        internal GameObject target = null!;

        private Vector3 targetPos = Vector3.zero;

        private void Start()
        {
            targetPos = target.transform.position;

            targetPos.x += Random.Range(-1.3f, 1.3f);

            //float angle = Mathf.Acos(Vector2.Distance(targetPos, Vector2.zero) / Vector2.Distance(transform.position, Vector2.zero)) * Mathf.Rad2Deg;

            float angle = Vector3.SignedAngle(targetPos, transform.position, Vector3.forward);

            transform.Rotate(Vector3.forward, angle);
        }

        private void FixedUpdate()
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, .15f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<IDamageable>().TakeDamage(5);

                Destroy(gameObject, .2f);
            }
        }
    }
}
