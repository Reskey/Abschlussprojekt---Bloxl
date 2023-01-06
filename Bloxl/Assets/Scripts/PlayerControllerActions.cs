using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Skripts
{
    public partial class PlayerController
    {
        private void MovePerform(CallbackContext context)
        {
            animator.SetBool("IsRunning", true);

            float direction = context.ReadValue<float>();

            facingRight = direction > 0;

            currentSpeed = direction / 10;
        }

        private void MoveEnd(CallbackContext context)
        {
            animator.SetBool("IsRunning", false);

            currentSpeed = 0f;
        }

        private void Jump(CallbackContext context)
        {
            if (isGrounded)
            {
                rigidBody.AddForce(jumpForce);
            }
        }

        private void FastFallStart(CallbackContext context)
        {
            if (!isGrounded)
            {
                rigidBody.gravityScale = 4;
            }
        }

        private void FastFallEnd(CallbackContext context)
        {
            rigidBody.gravityScale = 1;
        }
    }
}
