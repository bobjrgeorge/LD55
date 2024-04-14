using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EnemyTypes : MonoBehaviour
{
    public Transform[] enemyTypes;
    public List<int> EnemyList = new List<int>();
    int code;
    public int EnemyTypeAmount;
    public List<int> ButtonList = new List<int>();
    public GameObject[] objs;
    GameObject N_objs;

    // Start is called before the first frame update
    void Start()
    {
        EnemyTypeCode();
    }

    void EnemyTypeCode()
    {
        EnemyList = new List<int>(new int[enemyTypes.Length]);
        for (int i = 0; i < enemyTypes.Length; i++)
        {
            code = Random.Range(0, i + 1);
            if (EnemyList.Contains(code))
            {
                code = Random.Range(0, i + 1);
            }
            EnemyList[i] = code;
            Debug.Log(enemyTypes[i].name + code);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
  