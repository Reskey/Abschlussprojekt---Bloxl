using Assets.Skripts;
using Assets.Skripts.Enemies;
using Assets.Skripts.Management;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Imp : MonoBehaviour, IDamageable
{
    #region Attributes
    [SerializeField] GameObject attackPrefab;

    private float health = 50;

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
    #endregion

    #region Monobehaviour Methods
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();

        rootArea = pos;

        followCurrentTargetWait = new WaitUntil(delegate
        {
            pos = Vector2.MoveTowards(pos, target, speed);

            return Vector2.Distance(pos, target) < .3f || !outOfRange;
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Map"))
        {
            StopCoroutine(MovementBehaviour());
            StartCoroutine(MovementBehaviour());
        }
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
    #endregion

    #region Internal Mehtods
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Vector2 GetNewPositionInsideRootArea()
    {
        var xOffset = Random.Range(-4f, 4f);
        var yOffset = Random.Range(-4f, 4f);

        return rootArea + new Vector2(xOffset, yOffset);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        FindObjectOfType<GameController>().HitPopUp(amount, gameObject, GameController.PlayerDirection);

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameController.SplitSprite(gameObject, 50, GameController.PlayerDirection);

        Destroy(gameObject);
    }

    #endregion

    #region Coroutines
    private IEnumerator MovementBehaviour()
    {
        animator.SetBool(RunningParameter, true);

        target = targetAquired switch
        {
            false => GetNewPositionInsideRootArea(),
            true => (Vector2)targetRef.transform.position
        };

        facingRight = target.x < pos.x;

        yield return followCurrentTargetWait;

        animator.SetBool(RunningParameter, false);

        if (!outOfRange)
        {
            StartCoroutine(AttackBehaviour());
            yield break;
        }

        yield return idleWait;

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
        yield return idleWait;

        for (int i = 0; i < 10; i++)
        {
            if (outOfRange)
            {
                StartCoroutine(MovementBehaviour());
                yield break;
            }

            var x = Instantiate(attackPrefab, transform.position, Quaternion.identity);

            var y = x.AddComponent<ImpAttackArc>();

            y.target = targetRef;

            Destroy(x, 3);

            yield return new WaitForSeconds(.07f);
        }

        StartCoroutine(AttackCooldown());

        yield return new WaitUntil(() => canAttack);

        StartCoroutine(AttackBehaviour());
    } 
    #endregion
}
