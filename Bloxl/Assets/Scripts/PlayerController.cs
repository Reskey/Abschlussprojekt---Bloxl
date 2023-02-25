using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.InputSystem.InputAction;


namespace Assets.Skripts
{
    public partial class PlayerController : MonoBehaviour
    {
        #region Attributes
        internal const string RunningParameter = "IsRunning";
        internal const string JumpingParameter = "IsJumping";
        internal const RigidbodyConstraints2D dontMove = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        internal const RigidbodyConstraints2D move = RigidbodyConstraints2D.FreezeRotation;

        [Header("Variable Forces"), SerializeField, Range(0, 1000)] private int Jump_Force = 0;
        [SerializeField, Range(0, 1000)] private int Variable_Speed = 1;

        [Space(10), Header("Neccesary Objects"), SerializeField] private Transform groundCheck;
        [SerializeField] private BoxCollider2D capsuleCollider2D;
        [SerializeField, Range(0f, 5f)] private float groundCheckHeight = 0.2f;

        [Space(10), Header("Combat"), SerializeField] private int playerMaxHealth;
        private int playerHealth;
        [SerializeField] private LayerMask enemyLayers;
        [SerializeField] private Transform attackPoint;
        [SerializeField, Range(0f, 5f)] private float attackRange = 0.5f;
        [SerializeField] private int attackDamage;
        [SerializeField] private int criticalDamage;
        [SerializeField] private int maxHealth;

        [Space(10), Header("Sounds"), SerializeField] private AudioSource jumpSound;
        [SerializeField] private AudioSource runSound;

        private Inputs inputAction;
        private Animator animator;
        private Rigidbody2D rigidBody;
        [SerializeField] private HealthBar healthBar;

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

            inputAction.PlayerBasics.Attack.performed += Attack;
        }

        void Start()
        {
            inputAction.PlayerBasics.Enable();

            rigidBody.sharedMaterial = new PhysicsMaterial2D("Verbuggter kekw")
            {
                friction = 0
            };

            healthBar.SetMaxHealth(100);

            playerHealth = maxHealth;
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
            if (collision is TilemapCollider2D or CompositeCollider2D)
            {
                FastFallEnd(default);

                animator.SetBool(JumpingParameter, false);

                if (horizontalSpeed is 0)
                {
                    rigidBody.constraints = dontMove;
                }

                isGrounded = true;
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawRay(capsuleCollider2D.bounds.center + new Vector3(capsuleCollider2D.bounds.extents.x, -capsuleCollider2D.size.y), Vector2.down * (groundCheckHeight));
            Gizmos.DrawRay(capsuleCollider2D.bounds.center - new Vector3(capsuleCollider2D.bounds.extents.x, capsuleCollider2D.size.y), Vector2.down * (groundCheckHeight));
            Gizmos.DrawRay(capsuleCollider2D.bounds.center - new Vector3(capsuleCollider2D.bounds.extents.x, capsuleCollider2D.size.y + groundCheckHeight), Vector2.right * (capsuleCollider2D.size.x * 2));

            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        #endregion

        #region Internal Methods
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateMovementMetrics()
        {
            rigidBody.velocity = new Vector2(horizontalSpeed * Time.fixedDeltaTime, rigidBody.velocity.y);
        }

        public void TakeDamage(int hp)
        {
            playerHealth -= hp;

            if (playerHealth <= 0)
            {

            }

            healthBar.SetHealth(playerHealth);
        }
        #endregion
    }
}