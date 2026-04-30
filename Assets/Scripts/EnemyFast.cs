using UnityEngine;

/// <summary>
/// Variante del enemigo base con mayores capacidades.
/// Se mueve más rápido y detecta al jugador desde mayor distancia.
/// Hereda el comportamiento principal del enemigo base.
/// </summary>
public class EnemyFast : Enemy
{
    /// <summary>
    /// Inicializa el enemigo rápido.
    /// Aumenta el rango de detección (aggro) al doble del enemigo base.
    /// </summary>
    public new void Start()
    {
        base.Start();
        // Duplicar el rango de detección respecto al enemigo base
        this.aggro = this.aggro * 2f;
        // El NavMeshAgent se configurará con mayor velocidad en el inspector
    }

    /// <summary>
    /// Utiliza el movimiento del enemigo base.
    /// El comportamiento de persecución es idéntico.
    /// </summary>
    protected override void Move()
    {
        base.Move();
    }
}
