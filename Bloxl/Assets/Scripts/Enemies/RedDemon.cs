using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RedDemon : MonoBehaviour
{
    private const float normalSpeed = .01f;
    private const float aggroSpeed = .011f;
    private const float patrolDistance = 8f;

    private WaitUntil idleWalkWait;
    private WaitWhile stalkWait;

    private Vector2 moveDirection = Vector2.right;
    private Vector2 rootPos = Vector2.zero;
    private Vector2 target = Vector2.zero;

    GameObject targetRef = null!;

    private volatile float speed = normalSpeed;

    private volatile bool stalkTarget = false;

    private Vector2 pos => (Vector2)transform.position;

    void Start()
    {
        rootPos = pos;

        idleWalkWait = new WaitUntil(() => Vector2.Distance(pos, target) < .3f);

        stalkWait = new WaitWhile(() =>
        {
            target = (Vector2)targetRef?.transform.position;

            return stalkTarget;
        });

        GetComponent<Rigidbody2D>().sharedMaterial = new PhysicsMaterial2D()
        {
            friction = 1
        };

        StartCoroutine(MovementBehaviour()); 
    }

    void Update()
    {
        transform.position = (Vector3)Vector2.MoveTowards(pos, target, speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !stalkTarget)
        {
            targetRef = collision.gameObject;

            stalkTarget = true;

            StopAllCoroutines();

            StartCoroutine(FollowBehaviour(collision.gameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && stalkTarget)
        {
            stalkTarget = false;

            speed = normalSpeed;

            targetRef = null!;

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

        GameController.FlipSprite(gameObject);

        StartCoroutine(MovementBehaviour());
    }

    private IEnumerator FollowBehaviour(GameObject foundPos)
    {
        speed = aggroSpeed;

        targetRef = foundPos;

        yield return stalkWait;
    }
}
