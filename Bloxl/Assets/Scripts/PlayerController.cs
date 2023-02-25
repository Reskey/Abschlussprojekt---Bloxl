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
        public Canvas winScreen;
        private bool doubleJumpAvailable = false;
        internal const string RunningParameter = "IsRunning";
        internal const string JumpingParameter = "IsJumping";
        internal const RigidbodyConstraints2D dontMove = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        internal const RigidbodyConstraints2D move = RigidbodyConstraints2D.FreezeRotation;

        [Header("Variable Forces"), SerializeField, Range(0, 1000)] private int Jump_Force = 0;
        [SerializeField, Range(0, 1000)] private int Variable_Speed = 1;

        [Space(10), Header("Neccesary Objects"), SerializeField] private Transform groundCheck;
        [SerializeField] private BoxCollider2D capsuleCollider2D;
        [SerializeField, Range(0f, 5f)] private float groundCheckHeight = 0.2f;
        [SerializeField] private LayerMask enemyLayers;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackRange = 0.5f;


        [Space(10), Header("Sounds"), SerializeField] private AudioSource jumpSound;
        [SerializeField] private AudioSource runSound;

        [Space(10), Header("Debug Options"), SerializeField] private bool slopeCheckVisuals = true;
        [SerializeField] private bool groundCheckVisuals = true;

        private Inputs inputAction;
        private Animator animator;
        private Rigidbody2D rigidBody;

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
                    FlipSprite();
                }
                
                _facingRight = value;
            }
        }

        private bool isGrounded => Physics2D.OverlapBoxAll(capsuleCollider2D.bounds.center + new Vector3(0f, -capsuleCollider2D.size.y, 0f), new Vector2(capsuleCollider2D.bounds.size.x - 0.2f, groundCheckHeight), 0f).Any(x => x.gameObject != this.gameObject);
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
        }

        void FixedUpdate()
        {
            if (horizontalSpeed is not 0)
            {
                UpdateMovementMetrics();
            }

            if (isGrounded)
            {
                FastFallEnd(default);

                animator.SetBool(JumpingParameter, false);

                if (horizontalSpeed is 0)
                {
                    rigidBody.constraints = dontMove;
                }
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawRay(capsuleCollider2D.bounds.center + new Vector3(capsuleCollider2D.bounds.extents.x, -capsuleCollider2D.size.y), Vector2.down * (groundCheckHeight));
            Gizmos.DrawRay(capsuleCollider2D.bounds.center - new Vector3(capsuleCollider2D.bounds.extents.x, capsuleCollider2D.size.y), Vector2.down * (groundCheckHeight));
            Gizmos.DrawRay(capsuleCollider2D.bounds.center - new Vector3(capsuleCollider2D.bounds.extents.x, capsuleCollider2D.size.y + groundCheckHeight), Vector2.right * (capsuleCollider2D.size.x * 2));

            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        /*private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision is TilemapCollider2D or CompositeCollider2D)
            {
                
            }
        }*/
        #endregion

        #region Internal Methods
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateMovementMetrics()
        {
            rigidBody.velocity = new Vector2(horizontalSpeed * Time.fixedDeltaTime, rigidBody.velocity.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void FlipSprite()
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Victory")
            {
                winScreen.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
        #endregion
    }
}