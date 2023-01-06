using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Skripts
{
    public partial class PlayerController : MonoBehaviour
    {
        #region Attributes
        private PlayerInput inputAction;
        private Animator animator;
        private Rigidbody2D rigidBody;
        [SerializeField] private Transform groundCheck;

        private Vector3 defaultVelocity = Vector3.zero;
        private readonly Vector2 jumpForce = new Vector2(0, 350);
        private readonly Vector2 dashForce = new Vector2(180, 0);

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

        private volatile bool dashCooldown = false;
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

                rigidBody.position = Vector3.MoveTowards(rigidBody.position, defaultVelocity, currentSpeed);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider is TilemapCollider2D)
            {
                FastFallEnd(default);
            }
        }
        #endregion

        #region Internal Methods
        private void UpdateMovementMetrics()
        {
            Vector3 velocity = new Vector2(currentSpeed * Time.fixedDeltaTime, rigidBody.velocity.y);

            rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, velocity, ref defaultVelocity, 0.5f);
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
