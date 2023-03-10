using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.Skripts.Management;
using static UnityEngine.InputSystem.InputAction;
using Assets.Skripts;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem.XInput;
using Unity.VisualScripting;

namespace Assets.Skripts.Player
{
    public partial class PlayerController : MonoBehaviour, IDamageable
    {
        #region Attributes
        public Canvas winScreen;
        public Canvas deathScreen;
        private bool doubleJumpAvailable = false;
        internal const string RunningParameter = "IsRunning";
        internal const string JumpingParameter = "IsJumping";
        internal const string AttackParameter = "Attack";
        internal const RigidbodyConstraints2D dontMove = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        internal const RigidbodyConstraints2D move = RigidbodyConstraints2D.FreezeRotation;

        [Header("Variable Forces"), SerializeField, Range(0, 1000)] private int Jump_Force = 0;
        [SerializeField, Range(0, 1000)] private int Variable_Speed = 1;

        [Space(10), Header("Combat"), SerializeField] private LayerMask enemyLayers;
        [SerializeField] private Transform attackPoint;
        [SerializeField, Range(0f, 5f)] private float attackRange = 0.5f;
        [SerializeField] private int attackDamage;
        [SerializeField] private int criticalDamage;

        [SerializeField] private Light2D lightSource; 

        private Inputs inputAction;
        private Animator animator;
        private Rigidbody2D rigidBody;

        [SerializeField] private GameObject healthBarObject;

        private HealthBar healthBar;

        private Vector2 jumpForce = Vector2.up;

        private volatile float horizontalSpeed = 0f;

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

        private volatile bool isGrounded = false;
        private volatile bool canAttack = true;

        private byte jumps = 2;
        #endregion

        #region Monobehaviour Methods    
        void Awake()
        {
            inputAction = FindObjectOfType<GameController>().inputControlls;

            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody2D>();

            inputAction.PlayerBasics.Run.performed += MovePerform;
            inputAction.PlayerBasics.Run.canceled += MoveEnd;

            inputAction.PlayerBasics.Jump.performed += Jump;

            inputAction.PlayerBasics.Fastfall.started += FastFallStart;
            inputAction.PlayerBasics.Fastfall.canceled += FastFallEnd;

            inputAction.PlayerBasics.Attack.started += AttackPerform;
        }

        void Start()
        {
            healthBar = healthBarObject.GetComponent<HealthBar>();

            healthBar.SetMaxHealth(100);

            inputAction.PlayerBasics.Enable();

            rigidBody.sharedMaterial = new PhysicsMaterial2D("Verbuggter kekw")
            {
                friction = 0
            };

            Time.timeScale= 1;
        }

        void FixedUpdate()
        {
            if (horizontalSpeed is not 0)
            {
                UpdateMovementMetrics();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.isTrigger)
            {
                jumps = 2;

                FastFallEnd(default);

                animator.SetBool(JumpingParameter, false);

                if (horizontalSpeed is 0)
                {
                    rigidBody.constraints = dontMove;
                }

                isGrounded = true;
            }

            if (collision.tag == "Victory")
            {
                StartCoroutine(TriggerVictoryScreen());

            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
        }

        #endregion

        #region Internal Methods
        public void Die()
        {
            //GameController.SplitSprite(gameObject, 100, Vector2.zero);

            //lightSource.transform.parent = null;
            inputAction.PlayerBasics.Disable();

            animator.SetTrigger("Die");

            gameObject.tag = "Untagged";
            healthBarObject.active = false;

            StartCoroutine(TriggerDeathScreen());

            //MonoBehaviour.Destroy(gameObject);



        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void TakeDamage(float damage)
        {
            var currentHealth = healthBar.GetHealth();

            FindObjectOfType<GameController>().HitPopUp(damage, gameObject, Vector2.zero);

            healthBar.SetHealth(currentHealth - damage);

            if (healthBar.GetHealth() <= 0)
            {
                Die();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateMovementMetrics()
        {
            rigidBody.velocity = new Vector2(horizontalSpeed * Time.fixedDeltaTime, rigidBody.velocity.y);
        }

        private IEnumerator TriggerVictoryScreen() 
        { 
            yield return new WaitForSeconds(1.5f);
            winScreen.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        private IEnumerator TriggerDeathScreen()
        {
            yield return new WaitForSeconds(1.5f);
            deathScreen.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        #endregion

        private IEnumerator AttackCooldown()
        {
            canAttack = false;

            for (int i = 0; i < 32; i++)
            {
                yield return new WaitForFixedUpdate();
            }

            canAttack = true;
        }
    }
}