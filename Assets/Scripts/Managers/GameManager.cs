using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int todayMonth;
    public CompanyScale myCompanyScale;
    private string _companyName;
    private int _companyLevel;
    private int _roomLevel;

    private int _companyFunds = 3000; // 초기 자산 예시
    private const int MaxEmployeeCount = 15;

    public int CompanyFunds => _companyFunds;

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

    private void Start()
    {
        myCompanyScale = CompanyScale.Indie;
        todayMonth = 1;
        UIManager.Instance.UpdateFundsUI(_companyFunds);
    }

    public int RoomLevel
    {
        get => _roomLevel;
        set => _roomLevel = Mathf.Clamp(value, 0, 3); // 0 ~ 3 단계
    }

    public int CompanyLevel
    {
        get => _companyLevel;
        set => _companyLevel = Mathf.Max(0, value);
    }

    public string CompanyName
    {
        get => _companyName;
        set => _companyName = value;
    }

    // 현재 전체 고용 인원 수 (대기실 + 프로젝트 포함)
    public int GetTotalEmployeeCount()
    {
        int inProject = 0;
        foreach (var project in ProjectManager.Instance.CurrentProjects)
        {
            inProject += project.AssignedEmployees.Count;
        }

        int inWaitingRoom = GameObject.Find("EmployeeContainer")?.transform.childCount ?? 0;

        return inProject + inWaitingRoom;
    }

    public bool CanHireMore()
    {
        return GetTotalEmployeeCount() < MaxEmployeeCount;
    }

    public void OnEmployeeRemoved()
    {
        Debug.Log("고용인 1명 퇴사  전체 인원 수 감소");
    }

    public void AddFunds(int amount)
    {
        _companyFunds += amount;
        UIManager.Instance.UpdateFundsUI(_companyFunds);
    }

    public bool SpendFunds(int amount)
    {
        if (_companyFunds >= amount)
        {
            _companyFunds -= amount;
            UIManager.Instance.UpdateFundsUI(_companyFunds);
            return true;
        }

        Debug.LogWarning("자산 부족!");
        return false;
    }
}