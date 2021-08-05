using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public Canvas HealthPrefab;
    public float healthBarPosition;
    public int damage;

    private float health;
    private bool playerInRange;
    private bool canAttack = true;
    private Canvas theCanvas;
    private Slider healthBar;
    private Player player;

    private void Start()
    {
        theCanvas = Instantiate(HealthPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + healthBarPosition, gameObject.transform.position.z), transform.rotation, gameObject.transform);
        healthBar = theCanvas.GetComponentInChildren<Slider>();
        player = FindObjectOfType<Player>();

        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    private void Update()
    {
        healthBar.value = health;

        if (health <= 0)
        {
            StartCoroutine(Die());
        }

        if (playerInRange && canAttack)
        {
            player.TakeDamage(damage);
            StartCoroutine(AttackCooldown());
        }
    }

    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;

        yield return new WaitForSeconds(3);

        canAttack = true;
    }

    IEnumerator Die()
    {
        gameObject.GetComponent<Animator>().SetTrigger("isDead");

        yield return new WaitForSeconds(4);

        Destroy(gameObject);
    }
}
