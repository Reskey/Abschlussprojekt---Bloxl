using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine;

namespace Assets.Skripts
{
    public partial class PlayerController
    {
        private void MovePerform(CallbackContext context)
        {
            animator.SetBool("IsRunning", true);

            float direction = context.ReadValue<float>();

            facingRight = direction > 0;

            currentSpeed = (direction / 20) * Variable_Speed;
        }

        private void MoveEnd(CallbackContext context)
        {
            animator.SetBool("IsRunning", false);

            currentSpeed = 0f;

            if (!isGrounded)
            {
                rigidBody.velocity /= 1.5f;

                return;
            }

            rigidBody.velocity = Vector2.zero;
        }

        private void Jump(CallbackContext context)
        {
            if (isGrounded)
            {
                animator.SetBool("IsJumping", true);

                rigidBody.AddForce(jumpForce * Jump_Force);
            }
        }

        private void FastFallStart(CallbackContext context)
        {
            if (!isGrounded)
            {
                rigidBody.gravityScale = 8;
            }
        }

        private void FastFallEnd(CallbackContext context)
        {
            rigidBody.gravityScale = 4;
        }
    }
}