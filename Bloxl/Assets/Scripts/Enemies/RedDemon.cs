using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RedDemon : MonoBehaviour
{
    private WaitUntil idleWalkWait;

    private Vector2 moveDirection = Vector2.right;
    private Vector2 rootPos = Vector2.zero;
    private Vector2 target = Vector2.zero;

    private volatile float speed = .015f;

    private volatile bool stalkTarget = false;

    private Vector2 pos => (Vector2)transform.position;

    void Start()
    {
        rootPos = pos;

        idleWalkWait = new WaitUntil(() => Vector2.Distance(rootPos, pos) > 6f);

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
        if (collision.CompareTag("Player"))
        {
            stalkTarget = true;

            StopCoroutine(MovementBehaviour());

            StartCoroutine(FollowBehaviour(collision.gameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            stalkTarget = false;
        }
    }

    private IEnumerator MovementBehaviour()
    {
        print("Move");

        rootPos = pos;

        moveDirection = moveDirection.x switch
        {
            -1 => Vector2.right,
            1 => Vector2.left,
            _ => Vector2.zero
        };

        target = pos + moveDirection;

        yield return idleWalkWait;

        GameController.FlipSprite(gameObject);

        StartCoroutine(MovementBehaviour());

        yield break;
    }

    private IEnumerator FollowBehaviour(GameObject foundPos)
    {
        speed *= 1.3f;

        yield return new WaitWhile(() =>
        {
            target = (Vector2)foundPos.transform.position;
            
            return stalkTarget;
        });

        speed /= 1.3f;

        StartCoroutine(MovementBehaviour());

        yield break;
    }
}
