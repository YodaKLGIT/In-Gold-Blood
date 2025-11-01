using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    public string question;            // Text shown on dialogue button
    [TextArea(3, 10)]
    public string[] lines;             // Dialogue that plays when selected

    public int requiredRound = 1;      // Appear when round >= this
    public bool unlocked = true;       // Optional: can still be locked even if round reached
}
