using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GoopAttk : MonoBehaviour
{
    public float RollSpeed;
    public float ammo;
    Rigidbody2D rb;
    public int goopDamage;
    public float Range;
    public LayerMask Enemy;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector2.right * RollSpeed * Time.deltaTime);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, Range, Enemy);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<enemy>().TakeDamage(goopDamage);
        }
    }
/*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, Range, Enemy);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<enemy>().TakeDamage(goopDamage);
            }
        }
    }
*/
}
