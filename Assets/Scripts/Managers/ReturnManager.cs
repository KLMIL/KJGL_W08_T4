using UnityEngine;


public class ReturnManager : MonoBehaviour
{
    public static ReturnManager Instance { get; private set; }

    public int currentCycle { get; private set; } = 1;
    public int currentEarnings { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddEarnings(int amount)
    {
        currentEarnings += amount;
    }

    public void ResetForNextCycle()
    {
        currentCycle++;
        currentEarnings = 0;
    }
}