using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue2
{
    public string name;

    [TextArea(3, 5)]
    public string[] sentences;

    public GameObject wiz;

    public GameObject lightning;

    public UnityEvent ligtningSFX;
}
