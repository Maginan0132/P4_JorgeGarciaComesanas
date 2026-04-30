using UnityEngine;
using TMPro;

/// <summary>
/// Gestiona la actualización de la interfaz de usuario (UI).
/// Muestra la vida del jugador y el número de enemigos vivos.
/// Se suscribe a eventos del GameManager y Health para actualizaciones en tiempo real.
/// </summary>
public class UIUpdater : MonoBehaviour
{
    // Referencia al texto de vida del jugador en la UI
    [SerializeField] private TextMeshProUGUI lifeText;
    
    // Referencia al texto del número de enemigos vivos en la UI
    [SerializeField] private TextMeshProUGUI enemyText;

    /// <summary>
    /// Inicializa la UI al comenzar.
    /// Se suscribe a los eventos de cambio de vida y número de enemigos.
    /// </summary>
    void Start()
    {
        // Mostrar número inicial de enemigos
        UpdateEnemyText(GameManager.INSTANCE.Enemies);
        
        // Suscribirse al evento de cambio de número de enemigos
        GameManager.INSTANCE.EnemyNumberChanged += UpdateEnemyText;
        
        // Mostrar vida inicial del jugador
        lifeText.text = "100";
        
        // Suscribirse al evento de cambio de vida del jugador
        GameManager.INSTANCE.PlayerHealth.OnHealthChanged += UpdateLifeText;
    }

    /// <summary>
    /// Actualiza el texto de vida en la UI.
    /// Se llama cada vez que cambia la vida del jugador.
    /// </summary>
    /// <param name="life">Nuevo valor de vida</param>
    private void UpdateLifeText(int life)
    {
        lifeText.text = life.ToString();
    }

    /// <summary>
    /// Actualiza el texto de número de enemigos en la UI.
    /// Se llama cada vez que un enemigo es eliminado.
    /// </summary>
    /// <param name="enemies">Número actual de enemigos vivos</param>
    private void UpdateEnemyText(int enemies)
    {
        enemyText.text = enemies.ToString();
    }
}
