using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;


        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        FindObjectOfType<GameController>().HitPopUp(dmg, gameObject);

    }

    void Die()
    {
        BreakObj.BreakObjectIntoPieces(gameObject, 100, 5.8f, 0, 6);
    }
}
