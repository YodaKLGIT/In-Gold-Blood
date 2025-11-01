using UnityEngine;
using System.Collections;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI characterNameComponent;

    [Header("Dialogue Settings")]
    public float textSpeed = 0.05f;

    [HideInInspector] public string[] lines; // Set dynamically by DialogueInteract
    private int index;
    private Coroutine typingCoroutine;
    private System.Action onDialogueEnd;

    void Start()
    {
        // Hide text until activated by DialogueInteract
        textComponent.text = string.Empty;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!gameObject.activeSelf) return;

        if (Input.GetMouseButtonDown(0))
        {
            // Skip typing animation
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                if (typingCoroutine != null)
                    StopCoroutine(typingCoroutine);
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue(System.Action onEnd = null)
    {
        if (lines == null || lines.Length == 0)
        {
            Debug.LogWarning("Dialogue has no lines assigned!");
            return;
        }

        onDialogueEnd = onEnd;
        index = 0;
        gameObject.SetActive(true);
        textComponent.text = string.Empty;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;
        string line = lines[index];

        // 0 = instant typing
        if (textSpeed <= 0f)
        {
            textComponent.text = line;
            yield break;
        }

        float delay = Mathf.Max(textSpeed, 0.01f);

        foreach (char c in line)
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(delay);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        textComponent.text = string.Empty;
        gameObject.SetActive(false);
        onDialogueEnd?.Invoke();
    }
}
