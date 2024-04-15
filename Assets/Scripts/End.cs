using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{

    public Animator fade;

    public GameObject fader;

    public void Start()
    {
        StartCoroutine(FadeDissable());
        Cursor.lockState = CursorLockMode.None;
    }
    public void BackToTitle()
    {
        StartCoroutine(SceneChange());
        fader.SetActive(true);
    }

    IEnumerator FadeDissable()
    {
        yield return new WaitForSecondsRealtime(2);
        fader.SetActive(false);
    }

    IEnumerator SceneChange()
    {
        fade.SetTrigger("Fade");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Title");
    }
}
