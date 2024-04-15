using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossManagement : MonoBehaviour
{
    public GameObject boss;

    public GameObject barrier;
    public GameObject summonIcon;
    public GameObject wiz;
    public UnityEvent music;
    public GameObject bossHealth;

    void Update()
    {
        if (!boss)
        {
            barrier.SetActive(false);
            summonIcon.SetActive(true);
            wiz.SetActive(true);
            music.Invoke();
            bossHealth.SetActive(false);
        }
    }
}
