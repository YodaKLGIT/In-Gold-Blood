using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int CurrentRound = 1;

    public static void SetRound(int newRound)
    {
        CurrentRound = newRound;
        Debug.Log("Round updated: " + CurrentRound);
    }
}
