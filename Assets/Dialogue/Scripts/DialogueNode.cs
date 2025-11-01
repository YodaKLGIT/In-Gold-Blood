using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    [TextArea(1, 2)]
    public string question; // What the player clicks on (the dialogue option title)

    [TextArea(3, 10)]
    public string[] lines; // The NPC's response lines

    public bool unlocked = true; // Can be locked until triggered by event
}
