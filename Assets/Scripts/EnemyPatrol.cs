using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Enemigo que patrulla entre varios puntos predefinidos.
/// Persigue al jugador si este entra en su rango de detección.
/// Vuelve a patrullar cuando el jugador sale del rango.
/// Hereda del enemigo base y sobrescribe el movimiento.
/// </summary>
public class EnemyPatrol : Enemy
{
    // Array con los puntos de patrulla en la escena
    private Transform[] patrolPoints;
    
    // Índice del punto de patrulla actual
    private int currentPoint = 0;

    /// <summary>
    /// Inicializa el enemigo patrullero.
    /// Busca todos los puntos de patrulla etiquetados como "PatrolPoint".
    /// </summary>
    public new void Start()
    {
        base.Start();
        
        // Buscar todos los GameObjects con tag "PatrolPoint"
        GameObject[] points = GameObject.FindGameObjectsWithTag("PatrolPoint");
        patrolPoints = new Transform[points.Length];
        
        // Copiar los transforms de los puntos de patrulla
        for(int i = 0; i < points.Length; i++)
        {
            patrolPoints[i] = points[i].transform;
        }
    }
    
    /// <summary>
    /// Sobrescribe el movimiento del enemigo base.
    /// Si el jugador está en rango, persigue. Si no, patrulla.
    /// </summary>
    protected override void Move()
    {
        // Obtener referencia al jugador
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        // Calcular distancia hasta el jugador
        float distance = Vector3.Distance(playerPos.position, this.transform.position);

        // Si el jugador está en rango de agro, perseguir
        if(distance < aggro && distance > 1)
            agent.SetDestination(playerPos.position);
        // Si el jugador está muy cerca, detener
        else if(distance <= 1)
        {
            agent.ResetPath();
        }
        // Si el jugador no está en rango, patrullar
        else
        {
            Patrol();
        }
    }

    /// <summary>
    /// Controla la patrulla del enemigo entre los puntos definidos.
    /// Una vez alcanza un punto, se mueve al siguiente (circular).
    /// </summary>
    private void Patrol()
    {
        // Si no hay puntos de patrulla, no hacer nada
        if (patrolPoints.Length == 0) return;

        // Establecer el destino al punto de patrulla actual
        agent.SetDestination(patrolPoints[currentPoint].position);
        
        // Cuando está lo suficientemente cerca del punto, pasar al siguiente
        if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < 1f) 
        {
            // Incrementar índice y volver al primero si llegamos al final (circular)
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }
}
