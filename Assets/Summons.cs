using System.Collections.Generic;
using UnityEngine;

public class Summons : MonoBehaviour
{
    public List<Transform> AvalibleSummons;
    public Transform pendingAdd;
    private InputSystem input;

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
            if (AvalibleSummons[0] == null)
            {
                return;
            }
            Instantiate(AvalibleSummons[0], transform.position, Quaternion.identity);
            AvalibleSummons[0].gameObject.SetActive(true);
        }
        if(input.PlayerSummonTwo())
        {
            if (AvalibleSummons[1] == null)
            {
                return;
            }
            Instantiate(AvalibleSummons[1], transform.position, Quaternion.identity);
            AvalibleSummons[1].gameObject.SetActive(true);
        }
    }
}
