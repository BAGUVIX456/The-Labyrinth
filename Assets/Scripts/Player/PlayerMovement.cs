using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PowerupCircleController powerupCircleController;
    private Animator animator;
    
    public float speed = 5f;
    public float attackCooldown = 0.1f;
    public float powerUpAttackCooldown = 15.0f;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackRangeScale;
    public float powerUpAttackRadiusRate = 1f;
    public LayerMask enemyLayers;
    public int attackDamage;
    public int powerUpDamage;
    private float attackCooldownActual;
    private float powerUpAttackCooldownActual;
    public AudioSource audioSource;

    private Vector2 _movement;
    private Vector2 facingLeft;
    private bool isFacingLeft;
    private bool isAttacking;
    private bool attackPressed;
    private bool spaceHeld;
    private float spaceHeldTime = 0.0f;
    private float maxPowerupRadius = 35.0f;
    private bool isPoweredUp = true;

    public PlayerControls playerControl;
    private InputAction move, attack;

    private GameObject player;
    private PowerupController powerupController;

    private void Awake()
    {
        playerControl = new PlayerControls();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        powerupController = player.GetComponent<PowerupController>();
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
        powerupCircleController = GameObject.FindGameObjectWithTag("Powerup Attack").GetComponent<PowerupCircleController>();
        facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
        attackCooldownActual = attackCooldown;
        attackPressed = false;
        spaceHeld = false;
        isPoweredUp = false;
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
        if(isPoweredUp)
        {
            powerUpAttackCooldownActual -= Time.deltaTime;
            if(powerUpAttackCooldownActual <= 0)
            {
                isPoweredUp = false;
                powerupController.ShowPoweredUp();
            } else {
                powerupController.UpdateCooldown(powerUpAttackCooldownActual);
            }
            
        }
        

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

        if(Input.GetKey(KeyCode.Space))
        {
            if(!isPoweredUp) 
            {
                if(GetRadius(spaceHeldTime) <= maxPowerupRadius)
                {
                    spaceHeldTime += Time.deltaTime;
                }
                spaceHeld = true;
                powerupCircleController.setRadius(GetRadius(spaceHeldTime));
            }
        }
        else if(spaceHeld)
        {
            PowerUpAttack();
            powerupController.HidePoweredUp();
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

    void PowerUpAttack()
    {
        float radius = GetRadius(spaceHeldTime);
        animator.SetTrigger("Powerup Attack");
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(powerupCircleController.transform.position, radius*attackRangeScale, enemyLayers);
        powerUpAttackCooldownActual = powerUpAttackCooldown;
        powerupCircleController.setRadius(attackRange);
        spaceHeld = false;
        isPoweredUp = true;
        spaceHeldTime = 0.0f;
 
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(powerUpDamage);
        }
    }

    private void FixedUpdate()
    {
        if (isAttacking)
            return;
                
        rb.MovePosition(rb.position + _movement * (speed * Time.fixedDeltaTime));
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

        if(spaceHeld)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(powerupCircleController.transform.position, GetRadius(spaceHeldTime)*attackRangeScale);
        }
    }

    private float GetRadius(float timeHeld)
    {
        return attackRange/attackRangeScale + (timeHeld * powerUpAttackRadiusRate);
    }

    public void PlaySFX(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.PlayOneShot(clip);
    }
}