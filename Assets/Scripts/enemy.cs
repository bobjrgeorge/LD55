using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int MaxHealth = 100;
    int health;
    public Summons summon;
    public Transform SummonVarient;
    // Start is called before the first frame update
    void Start()
    {
        health = MaxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        health -= damage;
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
