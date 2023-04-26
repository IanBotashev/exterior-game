using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Destructible : MonoBehaviour
{
    [SerializeField] private int health;
    public UnityEvent onDeath;

    // Using late update, since any damage that can happen will happen in Update functions, so this will happen
    // after all damage is taken.
    private void LateUpdate()
    {
        DeathCheck();
    }
    
    /// <summary>
    /// Simple check every frame to see if the health counter is either 0 or less.
    /// If yes, calls OnDeath event, and destroys object thereafter.
    /// </summary>
    private void DeathCheck()
    {
        if (health > 0) return;
        
        onDeath?.Invoke();
        Destroy(gameObject);
    }
    
    /// <summary>
    /// Get health.
    /// </summary>
    /// <returns></returns>
    public int GetHealth()
    {
        return health;
    }
    
    // Two separate functions, since it'd look prettier in scripts to just do "object.TakeDamage(5)" instead of 
    // object.ChangeHealth(-5)
    /// <summary>
    /// Subtracts an amount from the health of this object.
    /// </summary>
    /// <param name="amount">Amount to subtract</param>
    public void TakeDamage(int amount)
    {
        health -= amount;
    }

    /// <summary>
    /// Adds an amount to the health of this object.
    /// </summary>
    /// <param name="amount">Amount to add.</param>
    public void Heal(int amount)
    {
        health += amount;
    }
}