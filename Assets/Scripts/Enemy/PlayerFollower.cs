using UnityEngine;
using UnityEngine.AI;

public class NavMeshFollower : MonoBehaviour
{
    public float maxDistance;
    public float minDistance;
    public float chaseSpeed;
    public float wanderSpeed;
    
    private GameObject target;

    private NavMeshAgent agent;
    
    private float changeDirectionCooldown = 2f;
    private double angleChange;
    private float distance;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = chaseSpeed;

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
    
    private void OnTriggerStay2D(Collider2D other)
    {
        changeDirectionCooldown = 0;
    }
}
