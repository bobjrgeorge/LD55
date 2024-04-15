using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using System.Linq;

public class GoopAttk : MonoBehaviour
{
    public float RollSpeed;
    Rigidbody2D rb;
    public int goopDamage;
    public float Range;
    public LayerMask Enemy;
    public List<GameObject> SummonSorter;
    public string ID;
    public Summons summons;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        ID = name + summons.AvalibleSummons;

        for (int i = 0; i < Object.FindObjectsOfType<GoopAttk>().Length; i++)
        {
            if (Object.FindObjectsOfType<GoopAttk>()[i] != this)
            {
                if (Object.FindObjectsOfType<GoopAttk>()[i].ID == ID)
                {
                    GameObject summon = FindObjectOfType<GoopAttk>().gameObject;
                    SummonSorter.Add(summon);
                    List<GameObject> onlyUniqueObjects = SummonSorter.Distinct().ToList();
                    for(int j = 0; j < SummonSorter.Count; j++)
                    {
                        Destroy(SummonSorter[j]);
                        summons.goopAmmo += 0.5f;
                    }
                }
                else
                {
                    summons.goopAmmo -= 1;
                }
            }
        }
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
}
