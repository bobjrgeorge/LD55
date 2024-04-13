using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Attack : MonoBehaviour
{
    private InputSystem input;
    public Transform AttackPoint;
    public float Range;
    public LayerMask Enemy;
    public int damage;
    public float attackRate = 2;
    float nextAttackTime = 0;
    public Animator animator;

    // Start is called before the first frame update
    private void Awake()
    {
        input = InputSystem.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (input.PlayerAttacked())
            {
                PlayerAttack();
                nextAttackTime = Time.time + 1 / attackRate;
                animator.SetTrigger("Attack");
            }
                
        }
        
    }

    void PlayerAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, Range, Enemy);
        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy == null)
            {
                return;
            }
            enemy.GetComponent<enemy>().TakeDamage(damage); 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(AttackPoint.position, Range);
    }
}
