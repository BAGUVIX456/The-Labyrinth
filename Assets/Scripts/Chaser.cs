using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Chaser : MonoBehaviour
{
    public GameObject player;

    public float speed;
    public float maximumDistance;
    public float minimumDistance;
    public float wanderSpeed;
    
    private double angleChange;
    private float distance;
    private float changeDirectionCooldown = 2f;
    
    void Update()
    {
        GetRandomDirectionChange();
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < maximumDistance && distance > minimumDistance)
        {
            transform.position =
                Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else if (distance > maximumDistance)
        {
            Vector3 toMove = new Vector3((float)System.Math.Cos(angleChange), (float)System.Math.Sin(angleChange), 0);

            transform.position =
                Vector2.MoveTowards(transform.position, transform.position + toMove * 5, wanderSpeed * Time.deltaTime);
        }
    }

    private void GetRandomDirectionChange()
    {
        changeDirectionCooldown -= Time.deltaTime;

        if (changeDirectionCooldown <= 0)
        {
            angleChange = Random.Range(-180f, 180f);
            
            changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }

    private void OnCollisionStay2D()
    {
        changeDirectionCooldown = 0;
    }
}
