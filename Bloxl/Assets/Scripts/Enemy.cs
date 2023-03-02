using Assets.Skripts.Management;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject healItem;

    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int dmg, Vector2 direction)
    {
        currentHealth -= dmg;

        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * 400f + Vector2.up * 100f);


        if (currentHealth <= 0)
        {
            Die(direction);
            return;
        }

        FindObjectOfType<GameController>().HitPopUp(dmg, gameObject, direction);

    }

    void Die(Vector2 direction)
    {
        GameController.SplitSprite(gameObject, 100, direction);

        int rndNum = Random.Range(1, 5);

        if (rndNum == 4)
        {
            GameObject item = healItem;

            GameObject drop = MonoBehaviour.Instantiate(item, transform.position, Quaternion.identity);

            MonoBehaviour.Destroy(drop, 10);
        }
    }
}
