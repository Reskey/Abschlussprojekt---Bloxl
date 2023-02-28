using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    internal class ImpAttackArc : MonoBehaviour
    {
        internal GameObject target = null!;

        private Vector2 targetPos = Vector2.zero;

        private void Start()
        {
            float angle = Vector2.Angle(target.transform.position, transform.position);

            transform.Rotate(Vector3.forward, angle);

            targetPos = target.transform.position;

            targetPos.y += Random.Range(-1.3f, 1.3f);
            targetPos.x += Random.Range(-1.3f, 1.3f);
        }

        private void FixedUpdate()
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, .1f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
