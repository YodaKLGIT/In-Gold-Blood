using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject buttonPrefab; // A prefab for each dialogue option
    public Transform buttonContainer; // Where buttons will appear
    public Dialogue dialogueUI; // Reference to the main Dialogue script

    private DialogueInteract currentNPC;

    void Start()
    {
        gameObject.SetActive(false);
    }

    // Called when player interacts with an NPC
    public void ShowOptions(DialogueInteract npc)
    {
        currentNPC = npc;

        // Clear old buttons
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        // Create new buttons for unlocked dialogue options
        for (int i = 0; i < npc.dialogueOptions.Length; i++)
        {
            DialogueNode node = npc.dialogueOptions[i];
            if (!node.unlocked) continue;

            GameObject btnObj = Instantiate(buttonPrefab, buttonContainer);
            TextMeshProUGUI btnText = btnObj.GetComponentInChildren<TextMeshProUGUI>();
            btnText.text = node.question;

            int index = i;
            btnObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                HideMenu();
                npc.Interact(index);
            });
        }

        gameObject.SetActive(true);
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
    }
}
