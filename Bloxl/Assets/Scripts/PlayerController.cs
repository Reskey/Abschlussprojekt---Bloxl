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
        [Header("Variable Forces"), SerializeField, Range(0, 1000)] private int Jump_Force = 0;
        [SerializeField, Range(1000, 10000)] private int Variable_Speed = 1;

        private PlayerInput inputAction;
        private Animator animator;
        private Rigidbody2D rigidBody;

        [Space(10), Header("Neccesary Objects"), SerializeField] private Transform groundCheck;

        private Vector3 defaultVelocity = Vector3.zero;
        private Vector2 jumpForce = Vector2.up;

        private volatile float currentSpeed = 0f;

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

        private bool isGrounded => Physics2D.OverlapCircleAll(groundCheck.position, 0.15f).Any(x => x.gameObject != this.gameObject);
        #endregion

        #region Monobehaviour Methods
        void Awake()
        {
            inputAction = new PlayerInput();
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody2D>();

            inputAction.Enable();

            inputAction.PlayerBasic.Run.performed += MovePerform;
            inputAction.PlayerBasic.Run.canceled += MoveEnd;

            inputAction.PlayerBasic.Jump.performed += Jump;

            inputAction.PlayerBasic.Fastfall.started += FastFallStart;
            inputAction.PlayerBasic.Fastfall.canceled += FastFallEnd;
        }

        void FixedUpdate()
        {
            if (currentSpeed is not 0)
            {
                UpdateMovementMetrics();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider is TilemapCollider2D or CompositeCollider2D)
            {
                FastFallEnd(default);

                animator.SetBool("IsJumping", false);

                if (currentSpeed is 0)
                {
                    rigidBody.velocity = Vector2.zero;
                }
            }
        }
        #endregion

        #region Internal Methods
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateMovementMetrics()
        {
            rigidBody.velocity = new Vector2(currentSpeed * Time.fixedDeltaTime, rigidBody.velocity.y);
        }

        private void FlipSprite()
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
        #endregion
    }
}