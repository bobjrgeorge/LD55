using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class IntroMusic : MonoBehaviour
{
    public UnityEvent music;
    void Start()
    {
        StartCoroutine(StartMusic());
    }

    IEnumerator StartMusic()
    {
        yield return new WaitForSecondsRealtime(6f);
        music.Invoke();
    }
}
