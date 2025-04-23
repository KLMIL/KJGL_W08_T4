using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] Canvas ui_UpgradeCanvas;
    [SerializeField] Canvas ui_ResumeCanvas;
    [SerializeField] GameObject ui_UpgradesPanel;
    [SerializeField] GameObject ui_ProjectsPanel;
    [SerializeField] GameObject ui_RecruitPanel;
    
    private TextMeshProUGUI fundsText;
    [SerializeField] int interviewFee;
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
        var obj = GameObject.Find("Funds");
        if (obj != null)
        {
            fundsText = obj.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogWarning("'Funds' 오브젝트를 찾을 수 없습니다.");
        }
    }

    public void UpdateFundsUI(int funds)
    {
        if (fundsText != null)
        {
            fundsText.text = $"Funds: {funds:N0} G";
        }
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
        GameManager.Instance.SpendFunds(interviewFee);
        ToggleUpgradesCanvas(false);
        ui_ResumeCanvas.enabled = true;
        OnRecruitClick(isNewbie);
    }

    public void OnRecruitClick(bool isNewbie)
    {
        if (isNewbie)
        {
            ui_ResumeCanvas.GetComponent<ResumeCanvas>().NewbieResumeButton();
        }
        else
        {
            ui_ResumeCanvas.GetComponent <ResumeCanvas>().ExperiencedResumeButton();
        }
    }
}
