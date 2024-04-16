using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public Animator fade;
    public void BackToTitle()
    {
        StartCoroutine(SceneChange());
    }

    IEnumerator SceneChange()
    {
        fade.SetTrigger("Fade");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Title");
    }
}
