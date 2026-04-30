using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrol : Enemy
{
    private Transform[] patrolPoints;
    private int currentPoint = 0;

    public new void Start()
    {
        base.Start();
        GameObject[] points = GameObject.FindGameObjectsWithTag("PatrolPoint");
        patrolPoints = new Transform[points.Length];
        for(int i = 0; i < points.Length; i++)
        {
            patrolPoints[i] = points[i].transform;
        }
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
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        agent.SetDestination(patrolPoints[currentPoint].position);
        
        if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < 1f) 
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }
}
