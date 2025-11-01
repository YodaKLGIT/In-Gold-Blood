using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance { get; private set; }

    [Header("Current Round")]
    public int currentRound = 1;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetRound(int round)
    {
        currentRound = Mathf.Max(1, round);
    }

    public void NextRound()
    {
        currentRound++;
    }
}
