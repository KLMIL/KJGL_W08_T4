using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Canvas ui_SpendFundsCanvas;
    public TickShower tickShower;


    [SerializeField] Image UI_TickShowing;
    [SerializeField] Canvas ui_UpgradeCanvas;
    [SerializeField] Canvas ui_ResumeCanvas;
    [SerializeField] Canvas ui_TutorialCanvas;  
    [SerializeField] GameObject ui_UpgradesPanel;
    [SerializeField] GameObject ui_ProjectsPanel;
    [SerializeField] GameObject ui_RecruitPanel;
    [SerializeField] TextMeshProUGUI _upgradeRoomText;
    [SerializeField] TextMeshProUGUI _upgradeCompanyText;
    [SerializeField] TextMeshProUGUI fundsText;
    [SerializeField] int interviewFee = 100;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        //var obj = GameObject.Find("Funds");
        //if (obj != null)
        //{
        //    fundsText = obj.GetComponent<TextMeshProUGUI>();
        //}
        //else
        //{
        //    Debug.LogWarning("'Funds' 오브젝트를 찾을 수 없습니다.");
        //}
    }

    public void UpdateFundsUI(int funds)
    {
        if (fundsText != null)
        {
            fundsText.text = $"{funds:N0} G";
        }
    }

    public void ToggleTutorialCanvas(bool status)
    {
        ui_TutorialCanvas.enabled = status;
    }

    public void ToggleUpgradesCanvas(bool status)
    {
        ui_UpgradeCanvas.enabled = status;
        ui_UpgradesPanel.SetActive(true);
        ui_ProjectsPanel.SetActive(false);
        ui_RecruitPanel.SetActive(false);
    }

    public void ToggleUpgradesPanel(bool status)
    {
        ui_UpgradesPanel.SetActive(status);
        ui_ProjectsPanel.SetActive(!status);
        ui_RecruitPanel.SetActive(!status);
    }

    public void ToggleRecruitPanel(bool status)
    {
        ui_RecruitPanel.SetActive(status);
        ui_UpgradesPanel.SetActive(!status);
        ui_ProjectsPanel.SetActive(!status);
    }

    public void ToggleProjectsPanel(bool status)
    {
        ui_ProjectsPanel.SetActive(status);
        ui_RecruitPanel.SetActive(!status);
        ui_UpgradesPanel.SetActive(!status);
    }

    public void ToggleResumeCanvas(bool isNewbie)
    {
        if (GameManager.Instance.SpendFunds(interviewFee))
        {
            Time.timeScale = 0f;
            ToggleUpgradesCanvas(false);
            ui_ResumeCanvas.enabled = true;
            OnRecruitClick(isNewbie);
        }    
    }

    public void OnRecruitClick(bool isNewbie)
    {
        
        if (isNewbie)
        {
            ui_ResumeCanvas.GetComponent<ResumeCanvas>().NewbieResumeButton();
        }
        else
        {
            ui_ResumeCanvas.GetComponent<ResumeCanvas>().ExperiencedResumeButton();
        }
    }

    public void OnUpgradeRoomClick()
    {
        if (GameManager.Instance.RoomLevel < 3)
        {
            int upgradePrice = 0;
            int nextUpgradePrice = 0;
            switch (GameManager.Instance.RoomLevel)
            {
                case 0:
                    upgradePrice = 5000;
                    nextUpgradePrice = 20000;
                    break;
                case 1:
                    upgradePrice = 20000;
                    nextUpgradePrice = 50000;
                    break;
                case 2:
                    upgradePrice = 50000;
                    nextUpgradePrice = 0;
                    break;
                default:
                    Debug.LogWarning("Wrong room level");
                    break;
            }
            if (GameManager.Instance.SpendFunds(upgradePrice))
            {
                GameManager.Instance.UpgradeRoomLevel();
                if(nextUpgradePrice == 0)
                {
                    _upgradeRoomText.SetText("사무실 구매\n(구매불가)");
                } else
                {
                    _upgradeRoomText.SetText($"사무실 구매\n{nextUpgradePrice} G");
                }
            }            
        }
        else
        {
            Debug.LogWarning("모든 방이 열렸습니다.");
        }
    }

    public void OnUpgradeCompanyClick()
    {
        if((int)GameManager.Instance.myCompanyScale < 3)
        {
            int upgradePrice = 0;
            int nextUpgradePrice = 0;
            switch ((int)GameManager.Instance.myCompanyScale)
            {
                case 1:
                    upgradePrice = 10000;
                    nextUpgradePrice = 30000;
                    break;
                case 2:
                    upgradePrice = 30000;
                    nextUpgradePrice = 0;
                    break;
                default:
                    Debug.LogWarning("Wrong room level");
                    break;
            }

            if (GameManager.Instance.SpendFunds(upgradePrice))
            {
                GameManager.Instance.UpgradeCompany();
                if (nextUpgradePrice == 0)
                {
                    _upgradeCompanyText.SetText("회사 규모 키우기\n(최대규모)");
                }
                else
                {
                    _upgradeCompanyText.SetText($"회사 규모 키우기\n{nextUpgradePrice} G");
                }
            }

        } else
        {
            Debug.LogWarning("최대규모");
        }
    }
}
