using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Enemigo base que persigue al jugador utilizando NavMesh.
/// Heredada de Entity para reutilizar el sistema de vida.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Entity
{
    // Componente NavMesh para movimiento automático
    protected NavMeshAgent agent;
    
    // Referencia a la posición del jugador
    protected Transform playerPos;
    
    // Distancia a la que el enemigo comienza a perseguir al jugador
    protected float aggro = 15;

    /// <summary>
    /// Inicializa el componente NavMeshAgent al comenzar.
    /// </summary>
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// Controla el movimiento del enemigo. 
    /// Persigue al jugador si está dentro del rango de detección (aggro).
    /// </summary>
    protected override void Move()
    {
        // Obtener referencia al jugador
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        // Calcular distancia entre el enemigo y el jugador
        float distance = Vector3.Distance(playerPos.position, this.transform.position);

        // Si el jugador está dentro del rango de agro y lejos del enemigo, perseguir
        if(distance < aggro && distance > 1)
            agent.SetDestination(playerPos.position);
        // Si el jugador está muy cerca, detener el movimiento
        else if(distance <= 1)
        {
            agent.ResetPath();
        }
    }

    /// <summary>
    /// Se ejecuta cuando el enemigo muere.
    /// Destruye el GameObject y notifica al GameManager.
    /// </summary>
    protected override void Death()
    {
        Debug.Log("Enemy has died.");
        Destroy(this.gameObject);
        // Informar al GameManager que un enemigo ha sido eliminado
        GameManager.INSTANCE.EnemyKilled();
    }
}
