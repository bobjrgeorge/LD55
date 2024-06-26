using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject fad;
    public Animator fade;

    public GameObject controlls;

    public void Start()
    {
        StartCoroutine(Fade());
    }
    public void Play()
    {
        StartCoroutine(Go());
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ControllsScreen()
    {
        controlls.SetActive(true);
    }

    public void Back()
    {
        controlls.SetActive(false);
    }

    IEnumerator Fade()
    {
        yield return new WaitForSecondsRealtime(1);
        fad.SetActive(false);
    }

    IEnumerator Go()
    {
        fad.SetActive(true);
        fade.SetTrigger("Fade");
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene("House");
        yield return null;
    }
}
