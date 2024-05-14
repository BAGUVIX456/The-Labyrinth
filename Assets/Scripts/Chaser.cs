using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    public GameObject player;

    public float speed;
    public float distanceBetween;
    public float minimumDistance;

    private float distance;
    private float changeDirectionCooldown;
    void Start()
    {
        
    }
    
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        
        if (distance < distanceBetween && distance > minimumDistance)
        {
            transform.position =
                Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }    
    }

    private void HandleRandomDirectionChange()
    {
        changeDirectionCooldown -= Time.deltaTime;

        if (changeDirectionCooldown <= 0)
        {
            float angleChange = Random.Range(-90f, 90f);

            changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }
}
