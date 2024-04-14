using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    public int MaxHealth = 100;
    int health;
    public Summons summon;
    public Transform SummonVarient;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
    }
    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        Debug.Log("Damage taken");

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (summon.pendingAdd != null)
        {
            return;
        }
        else
        {
            if(SummonVarient == null)
            {
                Destroy(gameObject);
                return;
            }
            summon.pendingAdd = SummonVarient.transform;
        }
           Destroy(gameObject);
    }
}
 