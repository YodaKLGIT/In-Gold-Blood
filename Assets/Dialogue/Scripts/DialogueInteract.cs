using UnityEngine;

public class DialogueInteract : MonoBehaviour
{
    [Header("NPC Info")]
    public string characterName;

    [Header("Dialogue Options")]
    public DialogueNode[] dialogueOptions;

    [Header("References")]
    public Dialogue dialogueUI;

    private bool isTalking = false;

    public void Interact(int optionIndex)
    {
        if (isTalking) return;
        if (optionIndex < 0 || optionIndex >= dialogueOptions.Length) return;

        DialogueNode node = dialogueOptions[optionIndex];
        if (!IsOptionAvailable(node)) return;

        isTalking = true;

        dialogueUI.characterNameComponent.text = characterName;
        dialogueUI.lines = node.lines;
        dialogueUI.gameObject.SetActive(true);
        dialogueUI.StartDialogue(() => { isTalking = false; });
    }

    public bool IsOptionAvailable(DialogueNode node)
    {
        return GameManager.CurrentRound >= node.requiredRound && node.unlocked;
    }

    public void UnlockOption(int index)
    {
        if (index >= 0 && index < dialogueOptions.Length)
            dialogueOptions[index].unlocked = true;
    }
}
