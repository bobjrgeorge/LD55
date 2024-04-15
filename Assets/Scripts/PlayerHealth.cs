using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Animator animator;

    public Animator fade;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    [SerializeField]
    int health = 3;

    public void TakeDamage(int Playerdamage)
    {
        animator.SetTrigger("Hit");
        health -= Playerdamage;
        Debug.Log("P_Damage");

        if (health <= 2)
        {
            heart3.SetActive(false);
        }
        if (health <= 1)
        {
            heart2.SetActive(false);
        }

        if (health <= 0)
        {
            heart1.SetActive(false);
            StartCoroutine(dead());
        }
    }
    
    IEnumerator dead()
    {
        fade.SetTrigger("Fade");
        yield return new WaitForSeconds(2);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
 