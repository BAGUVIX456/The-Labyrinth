using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public float speed = 5f;

    private Vector2 _movement;
    private Vector2 facingLeft;
    private bool isFacingLeft = false;
    
    public PlayerControls playerControl;
    private InputAction move, quit;

    private void Awake()
    {
        playerControl = new PlayerControls();
    }

    private void OnEnable()
    {
        move = playerControl.Player.Move;
        quit = playerControl.Player.Quit;
        
        move.Enable();
        quit.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        quit.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
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
        _movement = move.ReadValue<Vector2>();

        if (_movement == Vector2.zero)
        {
            animator.SetFloat("Speed", 0);
        }
        else if (_movement == Vector2.right && isFacingLeft)
        {
            isFacingLeft = false;
            Flip();
            animator.SetFloat("Speed", 5);
        }
        else if (_movement == Vector2.left && !isFacingLeft)
        {
            isFacingLeft = true;
            Flip();
            animator.SetFloat("Speed", 5);
        }
        else
        {
            animator.SetFloat("Speed", 5);
        }
        
        if (quit.ReadValue<float>() == 1)
            Application.Quit();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + _movement * (speed * Time.fixedDeltaTime));
    }
}