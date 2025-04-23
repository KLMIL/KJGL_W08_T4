using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int todayMonth;
    public CompanyScale myCompanyScale;
    [SerializeField] private string _companyName;
    private int _roomLevel = 0;

    private int _companyFunds = 20000; // 초기 자산 예시
    private const int MaxEmployeeCount = 15;

    public int CompanyFunds => _companyFunds;

    [SerializeField] GameObject activeRoomPrefab;
    GameObject leftCCTV;
    GameObject rightCCTV;

    [Header("Canvas")]
    [SerializeField] private GameObject gameFlowCanvas;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);


        // 2025-04-24 01:00 수정 - KWS
        // 게임시작, 메인으로 돌아가기 버튼 찾아서 리스너 추가
        Button startButton = gameFlowCanvas.transform.Find("GameStartImage").Find("StartButton").GetComponent<Button>();
        Button resetButton = gameFlowCanvas.transform.Find("GameOverImage").Find("ResetButton").GetComponent<Button>();

        TMP_InputField companyNameInput = gameFlowCanvas.transform.Find("GameStartImage").Find("CompanyNameInput").GetComponent<TMP_InputField>();
        companyNameInput.onValueChanged.AddListener(UpdateButtonState);

        startButton.onClick.AddListener(() =>
        {
            _companyName = companyNameInput.text;
            gameFlowCanvas.transform.Find("GameStartImage").gameObject.SetActive(false);
            Time.timeScale = 1f;
        });

        resetButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });

        startButton.interactable = false;

        // InputField에 입력이 감지되면 버튼 활성화

    }

    

    private void Start()
    {
        // 2025-04-24 01:00 수정 - KWS
        Time.timeScale = 0f;

        myCompanyScale = CompanyScale.Indie;
        todayMonth = 1;
        UIManager.Instance.UpdateFundsUI(_companyFunds);
        leftCCTV = GameObject.Find("LeftCCTV");
        rightCCTV = GameObject.Find("RightCCTV");
    }

    public int RoomLevel
    {
        get => _roomLevel;
        set => _roomLevel = Mathf.Clamp(value, 0, 3); // 0 ~ 3 단계
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

    public void UpgradeRoomLevel()
    {
        if(RoomLevel < 3)
        {
            RoomLevel += 1;
            Destroy(leftCCTV.transform.GetChild(3).gameObject);
            GameObject newActiveRoom = Instantiate(activeRoomPrefab, leftCCTV.transform);
            newActiveRoom.transform.SetSiblingIndex(RoomLevel);
            Destroy(rightCCTV.transform.GetChild(3).gameObject);
            newActiveRoom = Instantiate(activeRoomPrefab, rightCCTV.transform);
            newActiveRoom.transform.SetSiblingIndex(RoomLevel);
        }
    }

    public void UpgradeCompany()
    {
        if((int)myCompanyScale < 3)
        {
            myCompanyScale++;
        }
    }


    // 2025-04-24 01:00 수정 - KWS
    public void GameOver()
    {
        gameFlowCanvas.transform.Find("GameOverImage").Find("TitleText").GetComponent<TextMeshProUGUI>().text =
            $"{_companyName} 피산!";
        Time.timeScale = 0f;
        gameFlowCanvas.transform.Find("GameOverImage").gameObject.SetActive(true);
    }

    public void UpdateButtonState(string inputText)
    {
        gameFlowCanvas.transform.Find("GameStartImage").Find("StartButton").GetComponent<Button>().interactable = true;
    }
}