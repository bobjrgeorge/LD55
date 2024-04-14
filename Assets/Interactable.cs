using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Dialogue dialogue;
    private InputSystem input;

    public bool hasStarted = false;

    private void Start()
    {
        input = InputSystem.Instance;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (input.PlayerInteract() && !hasStarted)
            {
                FindObjectOfType<DialogueManager>().StartDialague(dialogue);
                hasStarted = true;
            }

            if (input.PlayerInteract() && hasStarted) 
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentence(dialogue);
                dialogue.characterAnimator.SetBool("Second", true);
            }
        }
    }
}
