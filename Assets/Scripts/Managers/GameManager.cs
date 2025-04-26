using System.Collections;
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

    private int _companyFunds = 3000; // 초기 자산 예시
    private const int MaxEmployeeCount = 40;

    public int CompanyFunds => _companyFunds;

    [SerializeField] GameObject activeRoomPrefab;
    GameObject leftCCTV;
    GameObject rightCCTV;
    [SerializeField] int secSalary = 100;
    [Header("Canvas")]
    [SerializeField] private GameObject gameFlowCanvas;
    public bool IsTuto { get; set; } = false;

    // 2025-04-24 10:30 수정 - KWS
    [Header("Game Timer")]
    private float _workTimer = 0f;
    private const float WORK_INTERVAL = 15f;
    [SerializeField] private TextMeshProUGUI _MultipleText;
    [SerializeField] private TextMeshProUGUI _YearText;
    [SerializeField] private TextMeshProUGUI _MonthText;

    [SerializeField] private SecretaryTalk _SecretaryTalk;

    [SerializeField] private TextMeshPro _companyNameText;

    [SerializeField] private int multipleIndex = 1;

    [SerializeField] SpriteRenderer backgroundSprite;
    public int MultipleIndex
    {
        get { return multipleIndex; }
        set
        {
            if (value < 0)
            {
                multipleIndex = 4 + value;
            }
            else
            {
                multipleIndex = value;
            }
        }
    }

    bool isOver = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;


        // 2025-04-24 01:00 수정 - KWS
        // 게임시작, 메인으로 돌아가기 버튼 찾아서 리스너 추가
        Button startButton = gameFlowCanvas.transform.Find("GameStartImage").Find("StartButton").GetComponent<Button>();
        Button resetButton = gameFlowCanvas.transform.Find("GameOverImage").Find("ResetButton").GetComponent<Button>();

        TMP_InputField companyNameInput = gameFlowCanvas.transform.Find("GameStartImage").Find("CompanyNameInput").GetComponent<TMP_InputField>();
        companyNameInput.onValueChanged.AddListener(UpdateButtonState);

        startButton.onClick.AddListener(() =>
        {
            _companyName = companyNameInput.text;
            _companyNameText.text = "(주)" + _companyName;
            gameFlowCanvas.transform.Find("GameStartImage").gameObject.SetActive(false);
            Time.timeScale = 1f;
            TutorialManager.Instance.ToggleTutoCanvas(true);
        });

        resetButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });

        startButton.interactable = false;

        // InputField에 입력이 감지되면 버튼 활성화

    }

    // 2025-04-24 10:30 수정 - KWS
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsTuto)
            {
                TutorialManager.Instance.ToggleTutoCanvas(true);
            }
        }
        _workTimer += Time.deltaTime;
        UIManager.Instance.tickShower.UpdateTickShower(_workTimer);
        if (_workTimer >= WORK_INTERVAL)
        {
            _workTimer = 0f;
            TickWork();
        }
    }

    // 2025-04-24 10:30 수정 - KWS
    private void TickWork()
    {
        ProjectManager.Instance.TickWork();
        todayMonth++;
        if(todayMonth % 12 == 0)
        {
            ProjectManager.Instance.IncreaseAllSalary();
        }
        //비서 대화 바꾸기
        _SecretaryTalk.RandomSaying();
        // 시계 시간 바꿔주기
        _YearText.text = (2000 + (todayMonth / 12)).ToString();
        _MonthText.text = ((todayMonth % 12) + 1).ToString("D2");
        //비서 월급 주기
        SpendFunds(secSalary);
    }



    private void Start()
    {
        // 2025-04-24 01:00 수정 - KWS
        Time.timeScale = 0f;

        myCompanyScale = CompanyScale.Indie;
        todayMonth = 0;
        UIManager.Instance.UpdateFundsUI(_companyFunds);
        leftCCTV = GameObject.Find("LeftCCTV");
        rightCCTV = GameObject.Find("RightCCTV");
        _SecretaryTalk = FindAnyObjectByType<SecretaryTalk>();
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
        ReturnManager.Instance.AddEarnings(amount);
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
        // 자산 부족시 게임 오버
        GameOver();
        return false;
    }

    public void UpgradeRoomLevel()
    {
        if (RoomLevel < 3)
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
        if ((int)myCompanyScale < 3)
        {
            // 여기서 배경 바꿔야함
            myCompanyScale++;
        }

        switch (myCompanyScale)
        {
            case CompanyScale.MidsizeCompany:
                Debug.LogError("Called here");
                backgroundSprite.sprite = Resources.Load<Sprite>("CompanyBackground/CompanyBackground_Midsize");
                break;
            case CompanyScale.LargeCompany:
                backgroundSprite.sprite = Resources.Load<Sprite>("CompanyBackground/CompanyBackground_Large");
                break;
            default:
                break;
        }
    }


    // 2025-04-24 01:00 수정 - KWS
    public void GameOver()
    {
        if (isOver) return;

        isOver = true;
        int thisCycle = ReturnManager.Instance.currentCycle;
        int thisCycleMoney = ReturnManager.Instance.currentEarnings;
        // 회차 반영
        ReturnManager.Instance.ResetForNextCycle();

        // 게임 오버 UI 처리
        gameFlowCanvas.transform.Find("GameOverImage").Find("TitleText").GetComponent<TextMeshProUGUI>().text =
            $"{_companyName} 파산!";

        gameFlowCanvas.transform.Find("GameOverImage").Find("InfoText").GetComponent<TextMeshProUGUI>().text =
            $"이번 {thisCycle} 회차에 번 총 수입\n{thisCycleMoney}G";

        Time.timeScale = 0f;
        gameFlowCanvas.transform.Find("GameOverImage").gameObject.SetActive(true);
    }


    public void UpdateButtonState(string inputText)
    {
        gameFlowCanvas.transform.Find("GameStartImage").Find("StartButton").GetComponent<Button>().interactable = true;
    }


    public void ChangeTimeMultiple()
    {
        multipleIndex++;
        multipleIndex %= 4;

        switch (multipleIndex)
        {
            case 0:
                _MultipleText.text = "X 0.5";
                Time.timeScale = 0.5f;
                break;
            case 1:
                _MultipleText.text = "X 1";
                Time.timeScale = 1f;
                break;
            case 2:
                _MultipleText.text = "X 2";
                Time.timeScale = 2f;
                break;
            case 3:
                _MultipleText.text = "X 4";
                Time.timeScale = 4f;
                break;
            default:
                break;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // 멈춰진 시간 복구
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 다시 로드
    }
}