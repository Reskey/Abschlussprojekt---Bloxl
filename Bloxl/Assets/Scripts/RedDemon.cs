using Assets.Skripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RedDemon : MonoBehaviour
{
    private const string AttackParameter = "IsAttacking";
    private const float normalSpeed = .07f;
    private const float aggroSpeed = .15f;
    private const float patrolDistance = 8f;

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
            var player = collision.collider.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(20);
            StartCoroutine(AttackCooldown());

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !stalkTarget)
        {
            targetRef = collision.gameObject;

            stalkTarget = true;

            StopAllCoroutines();

            StartCoroutine(FollowBehaviour());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && stalkTarget)
        {
            stalkTarget = false;

            speed = normalSpeed;

            targetRef = null!;

            animator.SetBool(AttackParameter, false);

            StopAllCoroutines();

            StartCoroutine(MovementBehaviour());
        }
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
