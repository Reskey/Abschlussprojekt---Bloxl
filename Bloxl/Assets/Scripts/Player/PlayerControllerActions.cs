using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine;
using System.Threading;
using System.Collections;
using Assets.Scripts;

namespace Assets.Skripts.Player
{
    public partial class PlayerController
    {
        private void MovePerform(CallbackContext context)
        {
            animator.SetBool(RunningParameter, true);
            
            float direction = context.ReadValue<float>();

            facingRight = direction > 0;

            Interlocked.Exchange(ref horizontalSpeed, direction * Variable_Speed);

            rigidBody.constraints = move;
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

            rigidBody.constraints = dontMove;
        }

        private void Jump(CallbackContext context)
        {
            if (jumps > 0)
            {
                jumps--;

                animator.SetBool(JumpingParameter, true);

                Vector2 x = rigidBody.velocity;
                x.y = 0;
                rigidBody.velocity = x;

                rigidBody.AddForce(jumpForce * Jump_Force);

                isGrounded = false;
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

        private void AttackPerform(CallbackContext context)
        {
            if (canAttack)
            {
                StartCoroutine(AttackCooldown());

                animator.SetTrigger(AttackParameter);

                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

                foreach (Collider2D hitTarget in hitEnemies.Where(x => !x.isTrigger))
                {
                    IDamageable enemy = hitTarget.GetComponent<IDamageable>();

                    if (isGrounded)
                    {
                        enemy.TakeDamage(attackDamage);
                    }
                    else
                    {
                        enemy.TakeDamage(criticalDamage);
                    }
                }
            }
        }
    }
}
