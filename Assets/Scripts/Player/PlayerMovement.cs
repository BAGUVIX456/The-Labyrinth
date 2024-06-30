using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    
    public float speed = 5f;
    public float attackCooldown = 0.1f;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage;
    private float attackCooldownActual;

    private Vector2 _movement;
    private Vector2 facingLeft;
    private bool isFacingLeft;
    private bool isAttacking;
    private bool attackPressed;
    
    public PlayerControls playerControl;
    private InputAction move, attack;

    private void Awake()
    {
        playerControl = new PlayerControls();
    }

    private void OnEnable()
    {
        move = playerControl.Player.Move;
        attack = playerControl.Player.Fire;
        
        move.Enable();
        attack.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        attack.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
        attackCooldownActual = attackCooldown;
        attackPressed = false;
    }

    private void Flip()
    {
        if (isFacingLeft)
        {
            transform.localScale = facingLeft;
        }
        else
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    private void Update()
    {
        if (isAttacking)
            attackCooldownActual -= Time.deltaTime;
        
        _movement = move.ReadValue<Vector2>();

        if (_movement == Vector2.zero)
        {
            animator.SetFloat("Speed", 0);
        }
        else if (_movement.x > 0 && isFacingLeft)
        {
            isFacingLeft = false;
            Flip();
            if(!isAttacking)
                animator.SetFloat("Speed", 5);
        }
        else if (_movement.x < 0 && !isFacingLeft)
        {
            isFacingLeft = true;
            Flip();
            if(!isAttacking)
                animator.SetFloat("Speed", 5);
        }
        else
        {
            if(!isAttacking)
                animator.SetFloat("Speed", 5);
        }

        if (attack.ReadValue<float>() == 1 && !attackPressed)
        {
            Attack();
            isAttacking = true;
            attackCooldownActual = attackCooldown;
            attackPressed = true;
        }
        else if (attack.ReadValue<float>() != 1)
        {
            if(attackCooldownActual <= 0)
                isAttacking = false;
            attackPressed = false;
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void FixedUpdate()
    {
        if(!isAttacking)
            rb.MovePosition(rb.position + _movement * (speed * Time.fixedDeltaTime));
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
            
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}