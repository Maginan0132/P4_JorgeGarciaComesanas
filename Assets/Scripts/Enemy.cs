using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Entity
{
    protected NavMeshAgent agent;
    protected Transform playerPos;
    protected float aggro = 15;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected override void Move()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        float distance = Vector3.Distance(playerPos.position, this.transform.position);

        if(distance < aggro && distance > 1)
            agent.SetDestination(playerPos.position);
        else if(distance <= 1)
        {
            agent.ResetPath();
        }
    }

    protected override void Death()
    {
        Debug.Log("Enemy has died.");
        Destroy(this.gameObject);
        GameManager.INSTANCE.EnemyKilled();
    }
}
