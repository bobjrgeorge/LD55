using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalInteractable : MonoBehaviour
{
    public NormalDialouge dialogue;
    public InputSystem input;
    public LayerMask Player;
    bool canStart = true;
    public float raduis;

    public bool hasStarted = false;

    private void Update()
    {
        canStart = Physics2D.OverlapCircle(transform.position, raduis, Player);
        if (canStart)
        {
            if (input.PlayerInteract() && !hasStarted)
            {
                FindObjectOfType<NormalDialougeManager>().StartDialague(dialogue);
                hasStarted = true;
            }
        }
        if (input.PlayerInteract() && hasStarted)
        {
            FindObjectOfType<NormalDialougeManager>().DisplayNextSentence(dialogue);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, raduis);
    }
}
