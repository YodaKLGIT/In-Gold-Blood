using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Settings")]
    public float interactRange = 3f;
    public KeyCode interactKey = KeyCode.E;

    [Header("References")]
    public DialogueMenu dialogueMenu;

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
            Debug.Log("Interact key pressed");
        }
    }

    void TryInteract()
    {
        Debug.Log("Attempting to interact");
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
        {
            DialogueInteract npc = hit.collider.GetComponent<DialogueInteract>();
            if (npc != null)
            {
                dialogueMenu.ShowOptions(npc);
            }
        }
    }
}
