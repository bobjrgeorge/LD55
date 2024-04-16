using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SisterFightEnd : MonoBehaviour
{
    public GameObject fightTrigger;
    public GameObject sisterCollider;
    public GameObject exitTrigger;
    public GameObject pow;
    public Animator sisterAnimator;
    bool hasPowed;

    public UnityEvent bossMusic;

    public UnityEvent trumpet;

    void Update()
    {
        if (sisterCollider == null)
        {
            fightTrigger.SetActive(false);
            exitTrigger.SetActive(true);
            sisterAnimator.SetBool("Dead", true);
            if (!hasPowed)
            {
                hasPowed = true;
                StartCoroutine(Pow());
            }

            bossMusic.Invoke();
        }
    }

    IEnumerator Pow()
    {
        pow.SetActive(true);
        trumpet.Invoke();
        yield return new WaitForSeconds(1f);
        pow.SetActive(false);
    }
}
