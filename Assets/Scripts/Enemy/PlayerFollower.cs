using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshFollower : MonoBehaviour
{
    public float maxDistance;
    public float minDistance;
    public float chaseSpeed;
    public float wanderSpeed;
    
    [SerializeField] private Transform target;

    private NavMeshAgent agent;
    
    private float changeDirectionCooldown = 2f;
    private double angleChange;
    [SerializeField]
    private float distance;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = chaseSpeed;
    }

    void Update()
    {
        GetRandomDirectionChange();
        distance = Vector2.Distance(transform.position, target.position);

        if (distance < maxDistance && distance > minDistance)
        {
            agent.SetDestination(target.position);
        }
        else if (distance > maxDistance)
        {
            Vector3 toMove = new Vector3((float)System.Math.Cos(angleChange), (float)System.Math.Sin(angleChange), 0);

            transform.position =
                Vector2.MoveTowards(transform.position, transform.position + toMove * 5, wanderSpeed * Time.deltaTime);
        }
        else
        {
            agent.ResetPath();
        }
    }
    
    private void GetRandomDirectionChange()
    {
        changeDirectionCooldown -= Time.deltaTime;

        if (changeDirectionCooldown <= 0)
        {
            angleChange = UnityEngine.Random.Range(-180f, 180f);
            
            changeDirectionCooldown = UnityEngine.Random.Range(1f, 5f);
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        changeDirectionCooldown = 0;
    }
}
