using Assets.Skripts.Management;
using Assets.Skripts.Player;
using Assets.Skripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Skripts.Enemies
{
    public class RedDemon : MonoBehaviour, IDamageable
    {
        
        private const string AttackParameter = "IsAttacking";
        private const float normalSpeed = .07f;
        private const float aggroSpeed = .15f;
        private const float patrolDistance = 8f;

        private float health = 300;

        private Animator animator;
        private Rigidbody2D rigidbody;

        private WaitUntil idleWalkWait;
        private WaitWhile stalkWait;

        private Vector2 moveDirection = Vector2.right;
        private Vector2 rootPos = Vector2.zero;
        private Vector2 target = Vector2.zero;

        GameObject targetRef = null!;

        private bool canAttack = true;

        private volatile bool _facingRight = true;
        private bool facingRight
        {
            get => _facingRight;
            set
            {
                if (value != _facingRight)
                {
                    GameController.FlipSprite(gameObject);
                }

                _facingRight = value;
            }
        }

        private volatile float speed = normalSpeed;

        private volatile bool stalkTarget = false;

        private Vector2 pos => (Vector2)transform.position;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            rootPos = pos;

            idleWalkWait = new WaitUntil(() => Vector2.Distance(pos, target) < .3f);

            stalkWait = new WaitWhile(() =>
            {
                target = (Vector2)targetRef?.transform.position;

                return stalkTarget;
            });

            rigidbody.sharedMaterial = new PhysicsMaterial2D()
            {
                friction = 2
            };

            StartCoroutine(MovementBehaviour());
        }

        void FixedUpdate()
        {
            facingRight = (target - pos).x > 0;

            var x = Vector2.MoveTowards(pos, target, speed);

            var y = transform.position;

            y.x = x.x;

            transform.position = y;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player") && canAttack)
            {
                collision.collider.GetComponent<IDamageable>().TakeDamage(20);

                StartCoroutine(AttackCooldown());
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !stalkTarget)
            {
                canAttack = true;

                targetRef = collision.gameObject;

                stalkTarget = true;

                StopAllCoroutines();

                StartCoroutine(FollowBehaviour());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.isTrigger && collision.CompareTag("Player") && stalkTarget)
            {
                stalkTarget = false;

                speed = normalSpeed;

                targetRef = null!;

                animator.SetBool(AttackParameter, false);

                StopAllCoroutines();

                StartCoroutine(MovementBehaviour());
            }
        }

        public void TakeDamage(float amount)
        {
            health -= amount;

            gameObject.GetComponent<Rigidbody2D>().AddForce(GameController.PlayerDirection * 400f + Vector2.up * 100f);

            FindObjectOfType<GameController>().HitPopUp(amount, gameObject, GameController.PlayerDirection);

            if (health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            GameController.SplitSprite(gameObject, 100, GameController.PlayerDirection);

            int rndNum = Random.Range(1, 5);

            if (rndNum == 3)
            {
                GameObject item = GameController.HealItem;

                MonoBehaviour.Instantiate(item, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }

        private IEnumerator MovementBehaviour()
        {
            rootPos = pos;

            target = rootPos + moveDirection * patrolDistance;

            yield return idleWalkWait;

            moveDirection = moveDirection.x switch
            {
                -1 => Vector2.right,
                1 => Vector2.left,
                _ => Vector2.zero
            };

            StartCoroutine(MovementBehaviour());
        }

        private IEnumerator FollowBehaviour()
        {
            animator.SetBool(AttackParameter, true);

            speed = aggroSpeed;

            yield return stalkWait;
        }

        private IEnumerator AttackCooldown()
        {
            canAttack = false;

            for (int i = 0; i < 30; i++)
            {
                yield return new WaitForFixedUpdate();
            }

            canAttack = true;
        }
    } 
}
