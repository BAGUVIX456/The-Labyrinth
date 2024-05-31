using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    
    public float speed = 5f;
    public float attackCooldown = 0.1f;
    private float attackCooldownActual;

    private Vector2 _movement;
    private Vector2 facingLeft;
    private bool isFacingLeft;
    private bool isAttacking;
    
    public PlayerControls playerControl;
    private InputAction move, quit, attack;

    private void Awake()
    {
        playerControl = new PlayerControls();
    }

    private void OnEnable()
    {
        move = playerControl.Player.Move;
        quit = playerControl.Player.Quit;
        attack = playerControl.Player.Fire;
        
        move.Enable();
        quit.Enable();
        attack.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        quit.Disable();
        attack.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
        attackCooldownActual = attackCooldown;
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

        if (attack.ReadValue<float>() == 1)
        {
            animator.SetBool("Attack", true);
            isAttacking = true;
            attackCooldownActual = attackCooldown;
        }
        else
        {
            animator.SetBool("Attack", false);
            if(attackCooldownActual <= 0)
                isAttacking = false;
        }

        if (quit.ReadValue<float>() == 1)
            Application.Quit();
    }

    private void FixedUpdate()
    {
        if(!isAttacking)
            rb.MovePosition(rb.position + _movement * (speed * Time.fixedDeltaTime));
    }
}