using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager2 : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialagueText;

    public Animator animator;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialague(Dialogue2 dialogue2)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue2.name;

        sentences.Clear();

        foreach (string sentance in dialogue2.sentences)
        {
            sentences.Enqueue(sentance);
        }
    }

    public void DisplayNextSentence(Dialogue2 dialogue2)
    {
        if (sentences.Count == 0)
        {
            EndDialague(dialogue2);
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

    public void EndDialague(Dialogue2 dialogue2)
    {
        FindObjectOfType<Interactable2>().hasStarted = false;
        animator.SetBool("IsOpen", false);
        StartCoroutine(LightningStrike(dialogue2));
    }

    IEnumerator LightningStrike(Dialogue2 dialogue2)
    {
        dialogue2.lightning.SetActive(true);
        dialogue2.ligtningSFX.Invoke();
        yield return new WaitForSecondsRealtime(1);
        dialogue2.wiz.SetActive(false);
    }
}
