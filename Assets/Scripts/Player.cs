using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controlador del jugador en primera persona.
/// Gestiona movimiento con WASD, salto con espacio, y rotación del ratón.
/// Hereda de Entity para reutilizar el sistema de vida.
/// </summary>
public class Player: Entity
{
    private Rigidbody rb;
    
    // Velocidad de movimiento del jugador
    [SerializeField] private float speed = 5f;
    
    // Sensibilidad del ratón para la rotación
    [SerializeField] private float sensitivity = 5f;
    
    // Fuerza del salto
    [SerializeField] private float salto = 5f;
    
    // Transform de la cámara para controlar su rotación
    private Transform camTransform;
    
    // Indica si el jugador está tocando el suelo (para permitir salto)
    private bool isGrounded = false;

    /// <summary>
    /// Inicializa el jugador al comenzar la escena.
    /// Configura referencias, cámara y bloquea el cursor.
    /// </summary>
    public void Start()
    {
        // Registrar la salud del jugador en el GameManager
        GameManager.INSTANCE.PlayerHealth = health;
        
        // Obtener componentes necesarios
        rb = GetComponent<Rigidbody>();
        camTransform = GetComponentInChildren<Camera>().transform;
        
        // Bloquear y ocultar el cursor del ratón
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Controla la rotación del jugador y cámara según el movimiento del ratón.
    /// Se ejecuta cada frame.
    /// </summary>
    public void Update()
    {
        // Obtener entrada del ratón
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        // Rotación horizontal: rota el cuerpo del jugador
        transform.Rotate(mouseX * sensitivity * transform.up);

        // Rotación vertical: rota la cámara (limitada a -70 y 70 grados)
        float rotationCam = camTransform.localRotation.eulerAngles.x + (-mouseY * sensitivity);
        // Clamp para limitar la rotación vertical
        Mathf.Clamp(rotationCam, -70, 70);
        camTransform.localRotation = Quaternion.Euler(rotationCam, 0, 0);
    }

    /// <summary>
    /// Se ejecuta cuando el jugador entra en colisión con otro objeto.
    /// Detecta suelo y colisiones con enemigos.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        // Si el jugador toca el suelo, permitir salto
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        // Si choca con un enemigo, recibir daño
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            health.TakeDamage(10);
        }
    }

    /// <summary>
    /// Se ejecuta cuando el jugador sale de colisión con otro objeto.
    /// Detecta cuando deja de tocar el suelo.
    /// </summary>
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    /// <summary>
    /// Se ejecuta cuando el jugador entra en un trigger (área).
    /// Detecta cuando entra en la zona de victoria.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Goal"))
        {
            GameManager.INSTANCE.Victory();
        }
    }
    
    /// <summary>
    /// Controla el movimiento del jugador con WASD y el salto con espacio.
    /// Se ejecuta cada frame físico (FixedUpdate vía Entity).
    /// </summary>
    protected override void Move()
    {
        // Obtener entrada de movimiento (WASD)
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        // Calcular dirección de movimiento en función de la orientación del jugador
        Vector3 movDirection = transform.right * hor + transform.forward * ver;

        // Aplicar velocidad manteniendo la componente Y (gravedad)
        Vector3 velocity = movDirection * speed;
        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;

        // Salto: solo si está en el suelo
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * salto, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Se ejecuta cuando el jugador muere.
    /// Reinicia la escena actual.
    /// </summary>
    protected override void Death()
    {
        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }    
}
