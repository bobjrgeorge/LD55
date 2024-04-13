using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int health = 3;
    
    public void TakeDamage(int Playerdamage)
    {
        health -= Playerdamage;
        Debug.Log("P_Damage");

        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("deded");
    }
}
