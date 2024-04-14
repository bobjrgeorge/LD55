using System.Collections.Generic;
using UnityEngine;

public class Summons : MonoBehaviour
{
    public List<Transform> AvalibleSummons;
    public Transform pendingAdd;
    private InputSystem input;
    bool canSummon;
    public string ID;
    //public enemy S_Enemy;
    void Start()
    {
        input = InputSystem.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(pendingAdd != null)
        {
            AvalibleSummons.Add(pendingAdd);
            pendingAdd = null;
        }

        if(input.PlayerSummonOne())
        {
            for (int i = 0; i < Object.FindObjectsOfType<Summons>().Length; i++) 
            {
                if (Object.FindObjectsOfType<Summons>()[i] != this)
                {
                    if (Object.FindObjectsOfType<Summons>()[i].ID == ID) 
                    {
                        Destroy(gameObject);
                    }
                }
            }
            if (AvalibleSummons[0] == null)
            {
                return;
            }
            Instantiate(AvalibleSummons[0], transform.position, Quaternion.identity);
            AvalibleSummons[0].gameObject.SetActive(true);
        }
    }
}
