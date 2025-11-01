using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public Dialogue dialogueUI;

    private DialogueInteract currentNPC;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowOptions(DialogueInteract npc)
    {
        currentNPC = npc;

        foreach (Transform child in buttonContainer)
            Destroy(child.gameObject);

        for (int i = 0; i < npc.dialogueOptions.Length; i++)
        {
            DialogueNode node = npc.dialogueOptions[i];

            if (!npc.IsOptionAvailable(node)) continue;

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
