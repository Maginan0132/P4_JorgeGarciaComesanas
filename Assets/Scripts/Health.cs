using UnityEngine;

/// <summary>
/// Sistema de vida reutilizable para cualquier entidad del juego.
/// Gestiona el daño, la muerte y notifica cambios mediante eventos.
/// </summary>
public class Health
{
    private int currentHealth;

    /// <summary>
    /// Propiedad que devuelve o establece la salud actual.
    /// </summary>
    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    /// <summary>
    /// Constructor por defecto. Inicializa la salud a 100.
    /// </summary>
    public Health()
    {
        currentHealth = 100;
    }

    /// <summary>
    /// Constructor con salud inicial personalizada.
    /// </summary>
    /// <param name="initialHealth">Valor inicial de salud</param>
    public Health(int initialHealth)
    {
        currentHealth = initialHealth;
    }

    /// <summary>
    /// Reduce la salud actual por la cantidad de daño recibida.
    /// Si la salud llega a 0 o menos, invoca el evento OnDeath.
    /// </summary>
    /// <param name="damage">Cantidad de daño a restar</param>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        // Si la salud es negativa, establecerla a 0
        if (currentHealth < 0)
        {
            currentHealth = 0;
            // Invocar evento de muerte
            OnDeath?.Invoke();
        }
        // Invocar evento de cambio de salud
        OnHealthChanged?.Invoke(currentHealth);
    }

    /// <summary>
    /// Evento que se dispara cada vez que cambia la salud.
    /// Parámetro: nuevo valor de salud.
    /// </summary>
    public System.Action<int> OnHealthChanged;
    
    /// <summary>
    /// Evento que se dispara cuando la salud llega a 0.
    /// </summary>
    public System.Action OnDeath;
}
