using System.Collections;
using UnityEngine;
 
public class Attack : MonoBehaviour
{
    public InputSystem input;
    public Transform AttackPoint;
    public float Range;
    public LayerMask Enemy;
    public int damage;
    public float attackRate = 2;
    float nextAttackTime = 0;
    public Animator animator;
    public float AnimationTime;
    bool running;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
         if (input.PlayerAttacked() && !running)
         {
            running = true;
            StartCoroutine(AnimTime());
         }    
    }

    void PlayerAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, Range, Enemy);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<enemy>().TakeDamage(damage);
        }
    }


    IEnumerator AnimTime()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(AnimationTime);
        if (Time.time >= nextAttackTime)
        {
            PlayerAttack();
            nextAttackTime = Time.time + 1 / attackRate;
        }
        running = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(AttackPoint.position, Range);
    }
}
