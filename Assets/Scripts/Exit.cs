using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public InputSystem input;
    public LayerMask Player;
    bool canStart = true;
    public float raduis;
    public Animator fade;

    public bool hasStarted = false;


    private void Update()
    {
        canStart = Physics2D.OverlapCircle(transform.position, raduis, Player);
        if (canStart)
        {
            Debug.Log("bye");
            if (input.PlayerInteract() && !hasStarted)
            {
                StartCoroutine(Transition()); 
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, raduis);
    }

    IEnumerator Transition()
    {
        fade.SetTrigger("Fade");
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene("Outside");
        yield return null;
    }
}
