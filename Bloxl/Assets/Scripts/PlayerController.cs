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

        private Inputs inputAction;
        private Animator animator;
        private Rigidbody2D rigidBody;

        [Space(10), Header("Neccesary Objects"), SerializeField] private Transform groundCheck;

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

        private bool isGrounded => Physics2D.OverlapCircleAll(groundCheck.position, 0.25f).Any(x => x.gameObject != this.gameObject);
        #endregion

        #region Monobehaviour Methods
        void Awake()
        {
            inputAction = FindObjectOfType<GameController>().inputControlls;
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody2D>();

            inputAction.PlayerBasics.Enable();
            
            inputAction.PlayerBasics.Run.performed += MovePerform;
            inputAction.PlayerBasics.Run.canceled += MoveEnd;

            inputAction.PlayerBasics.Jump.performed += Jump;

            inputAction.PlayerBasics.Fastfall.started += FastFallStart;
            inputAction.PlayerBasics.Fastfall.canceled += FastFallEnd;
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