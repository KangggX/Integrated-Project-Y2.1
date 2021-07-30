using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public Canvas HealthPrefab;

    private float health;
    private Canvas theCanvas;
    private Slider healthBar;

    private void Start()
    {
        theCanvas = Instantiate(HealthPrefab, gameObject.transform);
        healthBar = theCanvas.GetComponentInChildren<Slider>();

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
