using UnityEngine;

public class DialogueInteract : MonoBehaviour
{
    [Header("NPC Info")]
    public string characterName; // shows the name for the character on the dialogue UI

    [Header("Dialogue Options")]
    public DialogueNode[] dialogueOptions; // all the questions you can ask

    [Header("References")]
    public Dialogue dialogueUI;

    private bool isTalking = false;

    // Initiates dialogue with the selected option index
    public void Interact(int optionIndex = 0)
    {
        if (isTalking) return;
        if (optionIndex < 0 || optionIndex >= dialogueOptions.Length) return;

        DialogueNode node = dialogueOptions[optionIndex];
        if (!node.unlocked) return;

        isTalking = true;

        dialogueUI.characterNameComponent.text = characterName;
        dialogueUI.lines = node.lines;
        dialogueUI.gameObject.SetActive(true);
        dialogueUI.StartDialogue(() => { isTalking = false; });
    }

    // Unlocks a dialogue option by its index
    public void UnlockOption(int index)
    {
        if (index >= 0 && index < dialogueOptions.Length)
            dialogueOptions[index].unlocked = true;
    }
}
