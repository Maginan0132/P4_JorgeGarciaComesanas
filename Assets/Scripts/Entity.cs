using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Entity: MonoBehaviour
{
    protected Health health;

    public Health Health { get => health; set => health = value; }
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
        health = new Health();
        health.OnDeath += Death;
    }

    public void FixedUpdate()
    {
        Move();
    }

    protected virtual void Death()
    {
        Debug.Log("Entity has died.");
    }

    protected virtual void Move()
    {
        Debug.Log("Entity is moving.");
    }
}
