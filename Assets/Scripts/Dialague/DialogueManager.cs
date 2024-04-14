using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialagueText;

    public Animator animator;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialague(Dialogue dialogue) 
    {
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;

        dialogue.characterAnimator.SetBool("First", true);

        sentences.Clear();

        foreach (string sentance in dialogue.sentences)
        {
            sentences.Enqueue(sentance);
        }
    }

    public void DisplayNextSentence(Dialogue dialogue)
    {
        if(sentences.Count == 0)
        {
            EndDialague(dialogue);
            return;
        }
 
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialagueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialagueText.text += letter;
            yield return null;
        }
    }

    public void EndDialague(Dialogue dialogue)
    {
        dialogue.characterAnimator.SetBool("Third", true);
        dialogue.collider.SetActive(true); 
        FindObjectOfType<Interactable>().hasStarted = false;
        animator.SetBool("IsOpen", false);
        dialogue.attackVL.Invoke();
    }
}
