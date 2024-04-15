using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NormalDialougeManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialagueText;

    public Animator animator;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialague(NormalDialouge dialogue)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentance in dialogue.sentences)
        {
            sentences.Enqueue(sentance);
        }
    }

    public void DisplayNextSentence(NormalDialouge dialogue)
    {
        if (sentences.Count == 0)
        {
            EndDialague();
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

    public void EndDialague()
    {
        FindObjectOfType<NormalInteractable>().hasStarted = false;
        animator.SetBool("IsOpen", false);
    }
}
