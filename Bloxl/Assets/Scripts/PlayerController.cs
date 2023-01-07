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
        [SerializeField] private CapsuleCollider2D capsuleCollider2D;
        [SerializeField, Range(0f, 5f)] private float groundCheckHeight = 0.2f;

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

        //private bool isGrounded => Physics2D.OverlapBoxAll(groundCheck.position, new Vector3(capsuleCollider2D.bounds.extents.x, 2f, 0f), 0f).Any(x => x.gameObject != this.gameObject);

        private bool isGrounded
        {
            get
            {
                return Physics2D.OverlapBoxAll(capsuleCollider2D.bounds.center + new Vector3(0f, capsuleCollider2D.size.y), new Vector3(capsuleCollider2D.bounds.extents.x, capsuleCollider2D.bounds.extents.y + groundCheckHeight * 2.5f, 0f), 0f).Any(x => x.gameObject != this.gameObject);
            }
        }

        #endregion

        #region Monobehaviour Methods    
        void Awake()
        {
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody2D>();           
        }

        void Start()
        {
            inputAction = FindObjectOfType<GameController>().inputControlls;

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
            Color rayColor = Color.red;
            if (isGrounded)
            {
                rayColor = Color.green;
            }
            Debug.DrawRay(capsuleCollider2D.bounds.center + new Vector3(capsuleCollider2D.bounds.extents.x, -capsuleCollider2D.size.y), Vector2.down * (-capsuleCollider2D.size.y * 3 + groundCheckHeight * 2.5f), rayColor);
            Debug.DrawRay(capsuleCollider2D.bounds.center - new Vector3(capsuleCollider2D.bounds.extents.x, capsuleCollider2D.size.y), Vector2.down * (-capsuleCollider2D.size.y * 3 + groundCheckHeight * 2.5f), rayColor);
            Debug.DrawRay(capsuleCollider2D.bounds.center - new Vector3(capsuleCollider2D.bounds.extents.x, -capsuleCollider2D.size.y * 2 + groundCheckHeight * 2.5f), Vector2.right * (capsuleCollider2D.bounds.extents.y + 0.5f), rayColor);
 
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