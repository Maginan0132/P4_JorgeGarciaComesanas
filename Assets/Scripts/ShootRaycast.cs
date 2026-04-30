using UnityEngine;

public class ShootRaycast:MonoBehaviour
{
    [SerializeField] private GameObject spawn;
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //Instantiate(bala, transform.position, bala.transform.rotation); Una manera de hacerlo, probablemente cause una pechá de lag

            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward * 100f, Color.red, 1f);
            if(Physics.Raycast(spawn.transform.position, transform.forward, out hit, 100f))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green, 1f);
                Debug.Log("Hit: " + hit.collider.name);
                Enemy enemy = hit.collider.GetComponent<Enemy>(); //Si el enemigo tuviera un método para recibir daño, se llamaría aquí
                if(enemy != null)
                {
                    enemy.Health.TakeDamage(50);
                }
            }
        }
    }
}
