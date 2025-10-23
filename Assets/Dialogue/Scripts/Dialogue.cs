using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.TextCore.Text;

public class Dialogue : MonoBehaviour
{
    [Header("UI Components")]

    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI characterNameComponent;

    [TextArea(1,1)]
    public string characterName;

    [Header("Dialogue Lines")]
    [TextArea(3, 10)]
    public string[] lines;

    [Header("Dialogue Settings")]
    public float textSpeed = 0.05f;

    private int index;
    private Coroutine typingCoroutine;

    void Start()
    {
        textComponent.text = string.Empty;
        characterNameComponent.text = characterName;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Skip typing animation
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                // Finish instantly
                if (typingCoroutine != null)
                    StopCoroutine(typingCoroutine);
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        typingCoroutine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;
        string line = lines[index];

        float delay = Mathf.Max(textSpeed, 0.01f); // ensures at least 0.01s per char

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
            gameObject.SetActive(false);
        }
    }
}
