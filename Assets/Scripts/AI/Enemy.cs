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
    private Animator animator;

    private void Awake()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Find the player object
        player = FindObjectOfType<Player>();

        theCanvas = Instantiate(HealthPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + healthBarPosition, gameObject.transform.position.z), transform.rotation, gameObject.transform);
        healthBar = theCanvas.GetComponentInChildren<Slider>();
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
        animator.SetBool("isAttacking", true);
        canAttack = false;

        yield return new WaitForSeconds(2.1f);

        player.TakeDamage(damage);
        canAttack = true;
    }

    IEnumerator Die()
    {
        gameObject.GetComponent<Animator>().SetTrigger("isDead");

        yield return new WaitForSeconds(4);

        Destroy(gameObject);
    }
}
