using UnityEngine;

public class Health
{
    private int currentHealth;

    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public Health()
    {
        currentHealth = 100; // Default health value
    }

    public Health(int initialHealth)
    {
        currentHealth = initialHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            OnDeath?.Invoke();
        }
        OnHealthChanged?.Invoke(currentHealth);
    }

    public System.Action<int> OnHealthChanged;
    public System.Action OnDeath;
}
