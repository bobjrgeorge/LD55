using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3, 5)]
    public string[] sentences;
    
    public Animator characterAnimator;

    public GameObject collider;

    public UnityEvent attackVL;
}
