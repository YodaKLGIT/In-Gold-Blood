using UnityEngine;
using System.Collections;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI characterNameComponent;

    [Header("Dialogue Settings")]
    public float textSpeed = 0.04f;

    [HideInInspector] public string[] lines;
    private int index;
    private Coroutine typingCoroutine;
    private System.Action onDialogueEnd;

    void OnEnable()
    {
        StartDialogue(null);
    }

    public void StartDialogue(System.Action onEnd)
    {
        index = 0;
        textComponent.text = string.Empty;
        onDialogueEnd = onEnd;
        typingCoroutine = StartCoroutine(TypeLine());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopCoroutine(typingCoroutine);
                textComponent.text = lines[index];
            }
        }
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;
        string line = lines[index];

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
            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            onDialogueEnd?.Invoke();
        }
    }
}
