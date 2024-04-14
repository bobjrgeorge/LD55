using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable2 : MonoBehaviour
{
    public Dialogue2 dialogue;
    private InputSystem input;
    public LayerMask Player;
    bool canStart = true;
    public float raduis;

    public bool hasStarted = false;

    private void Awake()
    {
        input = InputSystem.Instance;
    }

    private void Update()
    {
        canStart = Physics2D.OverlapCircle(transform.position, raduis, Player);
        if (canStart)
        {
            Debug.Log("hi");
            if (input.PlayerInteract() && !hasStarted)
            {
                FindObjectOfType<DialogueManager2>().StartDialague(dialogue);
                hasStarted = true;
            }
        }
        if (input.PlayerInteract() && hasStarted)
        {
            FindObjectOfType<DialogueManager2>().DisplayNextSentence(dialogue);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, raduis);
    }
}
