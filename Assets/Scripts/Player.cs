using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player: Entity
{
    private Rigidbody rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float sensitivity = 5f;
    [SerializeField] private float salto = 5f;
    private Transform camTransform;
    private bool isGrounded = false;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        camTransform = GetComponentInChildren<Camera>().transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(mouseX * sensitivity * transform.up);
        //camTransform.Rotate(-mouseY * sensitivity * camTransform.right);

        float rotationCam = camTransform.localRotation.eulerAngles.x + (-mouseY * sensitivity);
        Mathf.Clamp(rotationCam, -70, 70);
        camTransform.localRotation = Quaternion.Euler(rotationCam, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = false;
        }
    }
    public override void Move()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 movDirection = transform.right * hor + transform.forward * ver;

        Vector3 velocity = movDirection * speed;
        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * salto, ForceMode.Impulse);
        }
    }

    public override void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }    
}
