using UnityEngine;

[System.Serializable]
public class NormalDialouge
{
    public string name;

    [TextArea(3, 5)]
    public string[] sentences;
}

