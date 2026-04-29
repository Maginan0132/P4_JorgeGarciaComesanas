using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Entity: MonoBehaviour
{
    private Health health;

    public Entity()
    {
        health = new Health();
    }

    public Entity(int initialHealth)
    {
        health = new Health(initialHealth);
    }

    public void Awake()
    {
        health.OnDeath += Death;
    }

    public void FixedUpdate()
    {
        Move();
    }

    public virtual void Death()
    {
        Debug.Log("Entity has died.");
    }

    public virtual void Move()
    {
        Debug.Log("Entity is moving.");
    }
}
