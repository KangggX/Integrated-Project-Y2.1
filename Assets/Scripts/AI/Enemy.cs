using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Slider healthBar;
    public float maxHealth;

    private float health;

    private void Start()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    private void Update()
    {
        healthBar.value = health;

        if (health <= 0)
        {
            Dead();
        }
    }

    public void TakeDamage()
    {
        if (health > 0)
        {
            health -= 10;
        }
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
}
