using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public void BackToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
