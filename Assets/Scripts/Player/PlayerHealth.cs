using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Player, Entity
{
    [SerializeField] protected float maxHealth;
    private float m_CurrentHealth;

    private void Awake()
    {
        m_CurrentHealth = maxHealth;
    }

    public void ReceiveHit(float _damage)
    {
        m_CurrentHealth -= _damage;

        if (m_CurrentHealth <= 0)
        {
            StartDeath();
        }
    }

    public void StartDeath()
    {
        Destroy(gameObject);
    }
}
