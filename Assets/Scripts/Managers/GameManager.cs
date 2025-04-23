using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int todayMonth;
    public CompanyScale myCompanyScale;

    private void Awake()
    {
        // 중복 싱글톤 방지
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬 전환에도 유지하려면
    }

    private void Start()
    {
        myCompanyScale = CompanyScale.Indie;
        todayMonth = 1;
    }
}