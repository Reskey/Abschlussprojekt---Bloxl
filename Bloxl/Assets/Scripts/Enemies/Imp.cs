using Assets.Scripts.Enemies;
using Assets.Skripts.Management;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Imp : MonoBehaviour
{
    [SerializeField] GameObject attackPrefab;

    private const string RunningParameter = "IsRunning";
    private const float speed = .02f;

    private Animator animator;
    private Rigidbody2D rigidbody;

    private GameObject targetRef = null!;

    private Vector2 target = Vector2.zero;
    private Vector2 rootArea = Vector2.zero;
    
    private Vector2 pos
    {
        get => (Vector2)transform.position;
        set => transform.position = (Vector3)value;
    }

    private WaitForSeconds idleWait = new WaitForSeconds(2f);
    private WaitUntil followCurrentTargetWait;

    private volatile bool targetAquired = false;
    private volatile bool canAttack = true;

    int state = 0;

    private bool outOfRange
    {
        get
        {
            if (targetRef is null) return true;

            return Vector2.Distance(pos, targetRef.transform.position) > 5;
        }
    }

    private volatile bool _facingRight = false;
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

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();

        rootArea = pos;

        followCurrentTargetWait = new WaitUntil(delegate
        {
            pos = Vector2.MoveTowards(pos, target, speed);
            
            return Vector2.Distance(pos, target) < .3f;
        });

        StartCoroutine(MovementBehaviour());
    }

    void FixedUpdate()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(rootArea, rootArea + new Vector2(4, 0));
        Gizmos.DrawLine(rootArea, rootArea + new Vector2(-4, 0));
        Gizmos.DrawLine(rootArea, rootArea + new Vector2(0, 4));
        Gizmos.DrawLine(rootArea, rootArea + new Vector2(0, -4));

        //Gizmos.DrawSphere(rootArea, 4);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !targetAquired)
        {
            targetAquired = true;

            targetRef = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && targetAquired)
        {
            targetAquired = false;

            targetRef = null!;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") & !outOfRange & canAttack)
        {
            canAttack = false;

            if (state is not 2) StopAllCoroutines();
            StartCoroutine(AttackBehaviour());

            return;
        }

        if (outOfRange && state is not 1)
        {
            StopAllCoroutines();
            StartCoroutine(MovementBehaviour());
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Vector2 GetNewPositionInsideRootArea()
    {
        var xOffset = Random.Range(-4f, 4f);
        var yOffset = Random.Range(-4f, 4f);

        return rootArea + new Vector2(xOffset, yOffset);
    }

    private IEnumerator MovementBehaviour()
    {
        state = 1;

        animator.SetBool(RunningParameter, true);

        target = targetAquired switch
        {
            false => GetNewPositionInsideRootArea(),
            true => (Vector2)targetRef.transform.position
        };

        facingRight = target.x < pos.x;

        yield return followCurrentTargetWait;

        animator.SetBool(RunningParameter, false);

        yield return idleWait;
        //print(Vector2.Distance(rootArea, pos) > System.Math.Sqrt(pos.x * pos.x + pos.y * pos.y));
        StartCoroutine(MovementBehaviour());
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        
        for (int i = 0; i < 150; i++)
        {
            yield return new WaitForFixedUpdate();
        }

        canAttack = true;
    }

    private IEnumerator AttackBehaviour()
    {
        state = 2;

        yield return idleWait;

        StartCoroutine(AttackCooldown());

        for (int i = 0; i < 25; i++)
        {
            var x = Instantiate(attackPrefab, transform.position, Quaternion.identity);

            var y = x.AddComponent<ImpAttackArc>();

            y.target = targetRef;

            Destroy(x, 3);

            yield return new WaitForSeconds(.07f);
        }
    }
}
