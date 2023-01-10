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
        #endregion
    }
}