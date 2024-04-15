using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summons : MonoBehaviour
{
    public List<Transform> AvalibleSummons;
    public Transform pendingAdd;
    public InputSystem input;
    public bool SummonOne;
    public bool SummonTwo;
    public bool canReset = true;
    public GoopAttk goop;
    public float goopAmmo;
    public Animator animator;

    //public enemy S_Enemy;

    // Update is called once per frame
    void Update()
    {
        if (pendingAdd != null)
        {
            AvalibleSummons.Add(pendingAdd);
            pendingAdd = null;
        }
        if (input.PlayerSummonOne())
        {
            if (AvalibleSummons[0] == null)
            {
                return;
            }
            Instantiate(AvalibleSummons[0], transform.position, Quaternion.identity);
            AvalibleSummons[0].gameObject.SetActive(true);
            animator.SetTrigger("Summon");
        }

        if (input.PlayerSummonTwo())
        {
            if (AvalibleSummons[1] == null)
            {
                return;
            }
            Instantiate(AvalibleSummons[1], transform.position, Quaternion.identity);
            AvalibleSummons[1].gameObject.SetActive(true);
            animator.SetTrigger("Summon");
        }

        if (input.PlayerSummonTreee())
        {
            if (AvalibleSummons[2] == null)
            {
                return;
            }
            if (goopAmmo <= 0)
            {
                return;
            }
            Instantiate(AvalibleSummons[2], transform.position, Quaternion.identity);
            goopAmmo -= 1;
            AvalibleSummons[2].gameObject.SetActive(true);
            animator.SetTrigger("Summon");
        }
            

    }


}
 