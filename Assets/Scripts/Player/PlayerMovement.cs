using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;

    private Vector2 _movement;
    
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

    private void Update()
    {
        _movement = move.ReadValue<Vector2>();
        
        if (quit.ReadValue<float>() == 1)
        {
            Application.Quit();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + _movement * (speed * Time.fixedDeltaTime));
    }
}
