using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Dialogue dialogue;
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
            Debug.Log("hi");
            if (input.PlayerInteract() && !hasStarted)
            {
                FindObjectOfType<DialogueManager>().StartDialague(dialogue);
                hasStarted = true;
            }
        }
        if (input.PlayerInteract() && hasStarted)
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence(dialogue);
            dialogue.characterAnimator.SetBool("Second", true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, raduis);
    }
}
