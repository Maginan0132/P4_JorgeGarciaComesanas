using UnityEngine;

/// <summary>
/// Sistema de disparo mediante raycast.
/// Detecta clics del ratón y lanza un raycast para determinar qué se golpea.
/// Aplica daño a los enemigos golpeados.
/// </summary>
public class ShootRaycast:MonoBehaviour
{
    // Punto de origen del raycast (posición del arma)
    [SerializeField] private GameObject spawn;
    
    /// <summary>
    /// Se ejecuta cada frame.
    /// Detecta clics del ratón izquierdo y lanza raycast.
    /// </summary>
    private void Update()
    {
        // Detectar clic del ratón izquierdo
        if(Input.GetMouseButtonDown(0))
        {
            // Nota: Instantiate de balas causar lag significativo,
            // por eso se usa raycast en su lugar.

            RaycastHit hit;
            // Dibujar línea roja de debug mostrando la dirección del raycast
            Debug.DrawRay(transform.position, transform.forward * 100f, Color.red, 1f);
            
            // Lanzar raycast desde la posición del arma en dirección forward
            if(Physics.Raycast(spawn.transform.position, transform.forward, out hit, 100f))
            {
                // Dibujar línea verde de debug mostrando donde golpea
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green, 1f);
                Debug.Log("Hit: " + hit.collider.name);
                
                // Intentar obtener componente Enemy del objeto golpeado
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                
                // Si se golpeó un enemigo, aplicar daño
                if(enemy != null)
                {
                    enemy.Health.TakeDamage(50);
                }
            }
        }
    }
}
