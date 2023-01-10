using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine;
using System.Threading;

namespace Assets.Skripts
{
    public partial class PlayerController
    {
        private void MovePerform(CallbackContext context)
        {
            animator.SetBool(RunningParameter, true);
            
            float direction = context.ReadValue<float>();

            facingRight = direction > 0;
            
            Interlocked.Exchange(ref horizontalSpeed, direction * Variable_Speed);
        }

        private void MoveEnd(CallbackContext context)
        {
            animator.SetBool(RunningParameter, false);

            Interlocked.Exchange(ref horizontalSpeed, 0f);

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
                isGrounded = false;

                animator.SetBool(JumpingParameter, true);

                rigidBody.AddForce(jumpForce * Jump_Force);

                slippery.friction = 0f;
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
