using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NavMeshFollower : MonoBehaviour
{
    public float maxDistance;
    public float minDistance;
    public float chaseSpeed;
    public float wanderSpeed;
    
    private GameObject target;
    private NavMeshAgent agent;
    private Animator animator;
    
    private float changeDirectionCooldown = 2f;
    private double angleChange;
    private float distance;
    private float wanderSpeedActual;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = chaseSpeed;
        wanderSpeedActual = wanderSpeed;

        target = GameObject.Find("Player");
    }

    void Update()
    {
        GetRandomDirectionChange();
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance < maxDistance && distance > minDistance)
        {
            agent.SetDestination(target.transform.position);
        }
        else if (distance > maxDistance)
        {
            Vector3 toMove = new Vector3((float)System.Math.Cos(angleChange), (float)System.Math.Sin(angleChange), 0);
            if (toMove.x > 0)
                transform.eulerAngles = new Vector3(0, 0, 0);
            else
                transform.eulerAngles = new Vector3(0, 180, 0);
            
            transform.position =
                Vector2.MoveTowards(transform.position, transform.position + toMove * 5, wanderSpeedActual * Time.deltaTime);
        }
        else
        {
            agent.ResetPath();
        }
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
        
        animator.SetFloat("WalkSpeed", wanderSpeedActual);
        changeDirectionCooldown = Random.Range(1f, 5f);
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        changeDirectionCooldown = 0;
    }
}