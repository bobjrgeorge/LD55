using UnityEngine;
using UnityEngine.Events;

public class BossMusic : MonoBehaviour
{
    public LayerMask Player;
    bool canStart = true;
    public float raduis;

    public UnityEvent music;
    public GameObject bossHealth;

    private void Update()
    {
        canStart = Physics2D.OverlapCircle(transform.position, raduis, Player);
        if (canStart)
        {
            music.Invoke();
            bossHealth.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, raduis);
    }
}
