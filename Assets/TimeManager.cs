
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [HideInInspector]
    public float SlowDownFactor;
    public float slowdownLength = 2;
    public float slowTimeScale;


    public void Slowmotion()
    {
        Time.timeScale = SlowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
