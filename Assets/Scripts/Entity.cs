using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Clase base para todas las entidades del juego (Jugador, Enemigos).
/// Proporciona funcionalidad común como el sistema de vida y movimiento.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Entity: MonoBehaviour
{
    // Sistema de vida de la entidad
    protected Health health;

    public Health Health { get => health; set => health = value; }
    
    /// <summary>
    /// Constructor por defecto. Inicializa la salud con valor por defecto (100).
    /// </summary>
    public Entity()
    {
        health = new Health();
    }

    /// <summary>
    /// Constructor con salud inicial personalizada.
    /// </summary>
    /// <param name="initialHealth">Valor inicial de salud</param>
    public Entity(int initialHealth)
    {
        health = new Health(initialHealth);
    }

    /// <summary>
    /// Se ejecuta al iniciar el GameObject. Configura el sistema de vida
    /// y vincula el evento de muerte.
    /// </summary>
    public void Awake()
    {
        health = new Health();
        // Cuando la salud llegue a 0, se ejecutará el método Death()
        health.OnDeath += Death;
    }

    /// <summary>
    /// Se ejecuta en cada frame físico. Llamará al movimiento de la entidad.
    /// </summary>
    public void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// Método virtual que se ejecuta cuando la entidad muere.
    /// Puede ser sobrescrito por clases derivadas (Player, Enemy, etc.)
    /// </summary>
    protected virtual void Death()
    {
        Debug.Log("Entity has died.");
    }

    /// <summary>
    /// Método virtual que controla el movimiento de la entidad.
    /// Debe ser implementado por las clases derivadas.
    /// </summary>
    protected virtual void Move()
    {
        Debug.Log("Entity is moving.");
    }
}
