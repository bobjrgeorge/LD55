using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneID;

    public LayerMask Player;
    bool canStart = true;
    public float raduis;

    public bool hasStarted = false;

    public Animator fade;

    private void Update()
    {
        canStart = Physics2D.OverlapCircle(transform.position, raduis, Player);
        if (canStart)
        {
            StartCoroutine(SceneChange());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, raduis);
    }

    IEnumerator SceneChange()
    {
        fade.SetTrigger("Fade");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneID);
    }
}
