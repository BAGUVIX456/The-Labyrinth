using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public float maxDistance;
    public float minDistance;
    public float chaseSpeed;
    public float wanderSpeed;
    public float attackCooldown = 5f;
    public int maxHealth;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask playerLayer;
    public int attackDamage;
    public AudioManager audiomanager;
    
    private GameObject target;
    private NavMeshAgent agent;
    private Animator animator;
    private HealthController playerHealthController;
    
    private float changeDirectionCooldown = 2f;
    private double angleChange;
    private float distance;
    private float wanderSpeedActual;
    private float initialPosition;
    private bool attackBlocked;
    private int currentHealth;

    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = chaseSpeed;
        wanderSpeedActual = wanderSpeed;
        initialPosition = transform.position.x;
        currentHealth = maxHealth;

        target = GameObject.Find("Player");
        playerHealthController = target.GetComponent<HealthController>();
    }

    void Update()
    {
        if (playerHealthController.currentHealth <= 0)
            this.enabled = false;
        
        GetRandomDirectionChange();
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance < maxDistance && distance > minDistance)
        {
            animator.SetFloat("WalkSpeed", 101);
            agent.SetDestination(target.transform.position);
        }
        else if (distance > maxDistance)
        {
            animator.SetFloat("WalkSpeed", wanderSpeedActual);
            Vector3 toMove = new Vector3((float)System.Math.Cos(angleChange), (float)System.Math.Sin(angleChange), 0);
            
            transform.position =
                Vector2.MoveTowards(transform.position, transform.position + toMove * 5, wanderSpeedActual * Time.deltaTime);
        }
        else
        {
            animator.SetFloat("WalkSpeed", 0);
            agent.ResetPath();
            AttackPlayer();
        }
        
        if (transform.position.x < initialPosition)
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        else if (transform.position.x > initialPosition)
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        
        initialPosition = transform.position.x;
    }
    
    private void GetRandomDirectionChange()
    {
        changeDirectionCooldown -= Time.deltaTime;

        if (!(changeDirectionCooldown <= 0)) return;
        
        if (Random.Range(0,10) >= 5)
        {
            wanderSpeedActual = wanderSpeed;
            angleChange = Random.Range(-180f, 180f);
        }
        else
            wanderSpeedActual = 0;
        
        changeDirectionCooldown = Random.Range(1f, 5f);
    }

    private void AttackPlayer()
    {
        if (attackBlocked)
            return;
        
        animator.SetTrigger("Attack");
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }

    private void HurtPlayer()
    {
        Collider2D hitPlayer =
            Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (hitPlayer != null)
            playerHealthController.TakeDamage(attackDamage);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        audiomanager.PlaySFX(audiomanager.skeletonDeath);
        animator.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;

        this.enabled = false;
    }
    
    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(attackCooldown);
        attackBlocked = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        changeDirectionCooldown = 0;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}