﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Canvas ui_SpendFundsCanvas;
    public Canvas ui_projectProgressCavas;
    public TickShower tickShower;

    [SerializeField] Canvas ui_UpgradeCanvas;
    [SerializeField] Canvas ui_ResumeCanvas;
    [SerializeField] Canvas ui_TutorialCanvas;  
    [SerializeField] GameObject ui_UpgradesPanel;
    [SerializeField] GameObject ui_ProjectsPanel;
    [SerializeField] GameObject ui_RecruitPanel;
    [SerializeField] TextMeshProUGUI _upgradeRoomText;
    [SerializeField] TextMeshProUGUI _upgradeCompanyText;
    [SerializeField] TextMeshProUGUI fundsText;
    [SerializeField] int newbieInterviewFee = 100;
    [SerializeField] int experiencedInterviewFee = 300;

    // 2025-04-27 16:00 수정 - KWS
    [SerializeField] private Button _gameJamProjectButton;
    [SerializeField] private Button _indieProjectButton;
    [SerializeField] private Button _AAAProjectButton;

    [SerializeField] private Image _indieUpgradeImage;
    [SerializeField] private Image _AAAUpgradeImage;

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
        // 2025-04-27 16:00 수정 - KWS
        _gameJamProjectButton.enabled = true;
        _indieProjectButton.enabled = false;
        _AAAProjectButton.enabled = false;
    }

    public void UpdateFundsUI(int funds)
    {
        if (fundsText != null)
        {
            fundsText.text = $"{funds:N0} G";
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
        if (isNewbie)
        {
            if (GameManager.Instance.CompanyFunds - newbieInterviewFee >= 0)
            {
                if (GameManager.Instance.SpendFunds(newbieInterviewFee))
                {
                    Time.timeScale = 0f;
                    ToggleUpgradesCanvas(false);
                    ui_ResumeCanvas.enabled = true;
                    OnRecruitClick(isNewbie);
                }
            }
            else
            {
                Debug.LogWarning("돈이 부족합니다.");
            }
        }
        else
        {
            if (GameManager.Instance.CompanyFunds - experiencedInterviewFee >= 0)
            {
                if (GameManager.Instance.SpendFunds(experiencedInterviewFee))
                {
                    Time.timeScale = 0f;
                    ToggleUpgradesCanvas(false);
                    ui_ResumeCanvas.enabled = true;
                    OnRecruitClick(isNewbie);
                }
            }
            else
            {
                Debug.LogWarning("돈이 부족합니다.");
            }
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
            if (GameManager.Instance.CompanyFunds - upgradePrice >= 0)
            {
                GameManager.Instance.SpendFunds(upgradePrice);
                GameManager.Instance.UpgradeRoomLevel();
                if(nextUpgradePrice == 0)
                {
                    _upgradeRoomText.SetText("사무실 구매\n(구매불가)");
                } else
                {
                    _upgradeRoomText.SetText($"사무실 구매\n({nextUpgradePrice} G)");
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

            if (GameManager.Instance.CompanyFunds - upgradePrice >= 0)
            {
                switch ((int)GameManager.Instance.myCompanyScale)
                {
                    case 1:
                        _indieProjectButton.enabled = true;
                        _indieUpgradeImage.gameObject.SetActive(false);
                        break;
                    case 2:
                        _AAAProjectButton.enabled = true;
                        _AAAUpgradeImage.gameObject.SetActive(false);
                        break;
                    default:
                        break;
                }


                GameManager.Instance.SpendFunds(upgradePrice);
                GameManager.Instance.UpgradeCompany();
                if (nextUpgradePrice == 0)
                {
                    _upgradeCompanyText.SetText("회사 규모 키우기\n(최대규모)");
                }
                else
                {
                    _upgradeCompanyText.SetText($"회사 규모 키우기\n({nextUpgradePrice} G)");
                }
            }

        } else
        {
            Debug.LogWarning("최대규모");
        }
    }
}
