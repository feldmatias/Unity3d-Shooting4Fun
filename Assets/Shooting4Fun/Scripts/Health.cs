using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    private float health;

    private bool isDead = false;
    private IDeathable target;

    public float HealthPercentage { get { return health / maxHealth; } }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = maxHealth;
        target = GetComponent<IDeathable>();
    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !isDead){
            health = 0;
            isDead = true;
            Die();
        }
    }

    public void ReceiveHealth(float healthReceived)
    {
        health += healthReceived;
        if (health > maxHealth){
            health = maxHealth;
        }
    }

    public bool IsFull()
    {
        return health >= maxHealth;
    }

    protected virtual void Die()
    {
        target.Die();
        Invoke("Delete", 20);
    }

    private void Delete()
    {
        Destroy(gameObject);
    }
}
