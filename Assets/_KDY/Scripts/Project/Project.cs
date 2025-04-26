using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Project : MonoBehaviour
{
    [Header("Project Information")]
    private string _projectName;
    private ProjectType _projectType;
    private ProjectSize _projectSize;
    private int _completionReward;

    [Header("Project requirements")]
    private int _requiredDesignSkill;
    private int _requiredProgrammingSkill;
    private int _requiredArtSkill;
    private int _requiredWorkAmount;

    private int _totalDesignSkill;
    private int _totalProgrammingSkill;
    private int _totalArtSkill;

    [Header("Project current States")]
    private int _quality;
    private int _currentWorkAmount;
    private List<Employee> _assignedEmployees = new List<Employee>();
    private bool _isCompleted = false;

    [Header("Project Info panel")]
    private TextMeshPro _projectNameText;
    private TextMeshPro _skillSummaryText;
    private TextMeshPro _workAmountText;
    private TextMeshPro _employListText;

    // 250423-1830 추가 - KWS
    [Header("Hover UI")]
    private CurrentProjectInfo _infoCanvas;
    bool _isHovering = false;
    //[Header("Project Tick Work")]
    //private float _workTimer = 0f;
    //private const float WORK_INTERVAL = 1f;

    [Header("Project Slots")]
    private Transform[] employeeSlots;

    private void Update()
    {
        if (_isHovering)
        {
            SetInfo();
        }
    }
    public void TickWork()
    {
        // 고용인이 있어야 작업 진행
        if (_isCompleted || _assignedEmployees.Count == 0) return;

        // 능력 부족시 스트레스 주기
        CheckAssignedStats();


        int totalPower = 0;
        foreach (var emp in _assignedEmployees)
        {
            // 프로젝트에 참가중인 모든 직원 머리 깎기
            emp.FadeHairAlpha();

            var data = emp.GetEmployeeData();  // Employee → EmployeeData 추출
            totalPower += data.designSkil + data.devSkil + data.artSkil;
            float expToGive = 0;
            switch (_projectSize)
            {
                case ProjectSize.Small:
                    expToGive = 15;
                    break;
                case ProjectSize.Medium:
                    expToGive = 50;
                    break;
                case ProjectSize.Large:
                    expToGive = 150;
                    break;
                default:
                    Debug.LogWarning("Wrong project size");
                    break;
            }

            emp.IncreaseExp(expToGive, ExpType.designExp);
            emp.IncreaseExp(expToGive, ExpType.devExp);
            emp.IncreaseExp(expToGive, ExpType.artExp);
        }


        if (totalPower > 0)
        {
            _currentWorkAmount += totalPower;
            RefreshUI();
            CheckProjectCompletion();
        }
    }


    private void CheckProjectCompletion()
    {
        if (_currentWorkAmount >= _requiredWorkAmount)
        {
            CompleteProject();
        }
    }

    private void CompleteProject()
    {
        _isCompleted = true;

        float qualityFactor = Mathf.Clamp01(_quality / 100f); // 0.0 ~ 1.0
        float randomFactor = Random.Range(0.8f, 1.2f);
        int finalReward = Mathf.RoundToInt(_completionReward * qualityFactor * randomFactor);

        GameManager.Instance.AddFunds(finalReward);
        Debug.Log($"💰 프로젝트 완료: {_projectName} | 보상: {finalReward} (기본: {_completionReward}, 품질: {_quality}%)");
        Transform employeeContainer = GameObject.Find("EmployeeContainer").transform;
        foreach (var emp in _assignedEmployees)
        {
            emp.transform.SetParent(employeeContainer);
            var draggable = emp.GetComponent<DraggableEmployee>();
            if (draggable != null)
            {
                draggable.transform.SetParent(employeeContainer, false);
                draggable.currentProject = null;
                draggable.currentOwnerType = OwnerType.WaitingRoom;
                draggable.waitingRoomSlot = employeeContainer;
                transform.localPosition = new Vector3(0, 0, -1f);

            }
            emp.ResetStress();
        }

        _infoCanvas.HideProjectInfo();
        _infoCanvas.HideEmployeeInfo();

        string infoStr = $"{_projectName} 프로젝트가 완료되었습니다.\n ";
        if (_quality > 70)
        {
            infoStr += "대박! ";
        }
        else if (_quality > 40)
        {
            infoStr += "중박! ";
        }
        else
        {
            infoStr += "쪽박! ";
        }
        infoStr += $"{finalReward}G를 획득했습니다.";


        ProjectManager.Instance.notifier.ShowNotification(infoStr);

        ProjectManager.Instance.RemoveProject(this);
        transform.parent.Find("Cover").gameObject.SetActive(true);

        // 2025-04-25 15:30 수정 - KWS
        transform.parent.Find("Placard").Find("PlacardText").gameObject.GetComponent<TextMeshPro>().text = "";
        transform.parent.Find("Placard").gameObject.SetActive(false);

        Destroy(gameObject);
    }


    private void CheckAssignedStats()
    {
        int totalDesign = 0;
        int totalDev = 0;
        int totalArt = 0;

        foreach (var employee in _assignedEmployees)
        {
            var data = employee.GetEmployeeData();
            totalDesign += data.designSkil;
            totalDev += data.devSkil;
            totalArt += data.artSkil;
        }


        bool designInsufficient = totalDesign < _requiredDesignSkill;
        bool devInsufficient = totalDev < _requiredProgrammingSkill;
        bool artInsufficient = totalArt < _requiredArtSkill;

        _totalArtSkill = totalArt;
        _totalDesignSkill = totalDesign;
        _totalProgrammingSkill = totalDev;

        if (designInsufficient || devInsufficient || artInsufficient)
        {
            Debug.LogWarning($"⚠️ [{_projectName}] 능력치 부족: " +
                             $"{(designInsufficient ? "기획 " : "")}" +
                             $"{(devInsufficient ? "개발 " : "")}" +
                             $"{(artInsufficient ? "아트 " : "")}");
            ApplyStressToEmployees(20);
            ApplyQualityPenalty();
        }
        else if (!designInsufficient && !devInsufficient && !artInsufficient)
        {
            AddQuality(1);
        }
    }

    private void ApplyStressToEmployees(float amount)
    {
        for (int i = _assignedEmployees.Count - 1; i >= 0; i--)
        {
            var emp = _assignedEmployees[i];
            bool isAlive = emp.IncreaseStress(amount);

            Debug.Log($"[{_projectName}] 스트레스 증가 → {emp.GetEmployeeData().stress:F1}");

            if (!isAlive)
            {
                Debug.LogWarning($"[{_projectName}] 스트레스 100 도달 → 직원 퇴사!");
                //지출 UI 데이터 갱신
                UIManager.Instance.ui_SpendFundsCanvas.GetComponent<UI_SpendFund>().RemoveEmployeeList(emp);
                // 현재 프로젝트의 리스트에서 제거
                _assignedEmployees.RemoveAt(i);

                // 회사의 직원 리스트에서 제거
                ProjectManager.Instance.AllEmployees.Remove(emp);

                // 직원 정보를 보고 있었다면 제거
                _infoCanvas.HideEmployeeInfo();

                ProjectManager.Instance.notifier.ShowNotification($"직원 한 명이 퇴사했습니다.");

                ProjectManager.Instance.AllEmployees.Remove(emp);

                // 오브젝트 제거
                Destroy(emp.gameObject);

                GameManager.Instance.OnEmployeeRemoved();
            }
        }

        RefreshUI();
    }



    private void ApplyQualityPenalty()
    {
        const int penaltyAmount = 5;
        int prevQuality = _quality;
        Quality = _quality - penaltyAmount;

        Debug.LogWarning($"❗ 프로젝트 [{_projectName}] 품질 감소: {prevQuality} → {_quality}");
    }

    private void AddQuality(int amount)
    {
        int prevQuality = _quality;
        Quality = _quality + amount;

        Debug.LogWarning($"❗ 프로젝트 [{_projectName}] 품질 증가: {prevQuality} → {_quality}");
    }

    // 250423-1830 추가 - KWS
    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0)) return;
        _isHovering = true;
    }

    private void SetInfo()
    {
        //Debug.Log("Mouse Enter to Project");
        //_infoCanvas.ShowInfo($"Test Text");
        string[] infoStr = new string[5];
        infoStr[0] = $"{_projectName}";
        infoStr[1] = $"[진행도] {_currentWorkAmount,4} / {_requiredWorkAmount,4}";
        infoStr[2] = $"[ 품질 ] {_quality,4} /  100";
        infoStr[3] = $"[요구 역량]\n 기획: {_totalDesignSkill} / {_requiredDesignSkill} \n 개발: {_totalProgrammingSkill} / {_requiredProgrammingSkill}\n 아트: {_totalArtSkill} / {_requiredArtSkill}";
        //infoStr[4] = "";
        infoStr[4] = $"[예상 수익]\n {(int)(_completionReward * _quality / 100 * 0.8)} G ~ {(int)(_completionReward * _quality / 100 * 1.2)} G";

        bool[] enough = {
            _totalDesignSkill >= _requiredDesignSkill ? true : false,
            _totalProgrammingSkill >= _requiredProgrammingSkill ? true : false,
            _totalArtSkill >= _requiredArtSkill ? true : false,
        };
        
        _infoCanvas.ShowProjectInfo(infoStr, enough);
    }

    private void OnMouseExit()
    {
        //Debug.Log("Mouse Exit from Project");
        _isHovering = false;
        _infoCanvas.HideProjectInfo();
    }



    private void Awake()
    {
        // 슬롯 할당
        Transform spawnParent = transform.Find("SpawnPositions");
        if (spawnParent != null)
        {
            employeeSlots = new Transform[4];
            for (int i = 0; i < 4; i++)
            {
                employeeSlots[i] = spawnParent.GetChild(i);
            }
        }
        else
        {
            Debug.LogWarning("❗ SpawnPositions 오브젝트가 없습니다.");
        }

        _projectNameText = transform.Find("InfoPanel/ProjectNameText")?.GetComponent<TextMeshPro>();
        if (_projectNameText == null)
            Debug.LogWarning("프로젝트 이름 UI 할당 안됨!");

        _skillSummaryText = transform.Find("InfoPanel/SkillSummaryText")?.GetComponent<TextMeshPro>();
        if (_skillSummaryText == null)
            Debug.LogWarning("프로젝트 정보 UI 할당 안됨!");

        _workAmountText = transform.Find("InfoPanel/WorkAmountText")?.GetComponent<TextMeshPro>();
        if (_workAmountText == null)
            Debug.LogWarning("프로젝트 진행상황 UI 할당 안됨!");

        _employListText = transform.Find("InfoPanel/EmployeeListText")?.GetComponent<TextMeshPro>();
        if (_employListText == null)
            Debug.LogWarning("노동자 목록 UI 할당 안됨!");


        // 250423-1830 추가 - KWS
        _infoCanvas = FindAnyObjectByType<CurrentProjectInfo>();
    }

    public void RefreshUI()
    {
        if (_projectNameText != null)
            _projectNameText.text = _projectName;

        if (_skillSummaryText != null)
            _skillSummaryText.text = $"D:{_requiredDesignSkill} P:{_requiredProgrammingSkill} A:{_requiredArtSkill}";

        if (_workAmountText != null)
            _workAmountText.text = $"TotalW:{_requiredWorkAmount} CurrentW:{_currentWorkAmount}\nQuality:{_quality}";

        int totalDesign = 0, totalDev = 0, totalArt = 0;
        foreach (var employee in _assignedEmployees)
        {
            var data = employee.GetEmployeeData();
            totalDesign += data.designSkil;
            totalDev += data.devSkil;
            totalArt += data.artSkil;
        }
        _totalDesignSkill = totalDesign;
        _totalProgrammingSkill = totalDev;
        _totalArtSkill = totalArt;

        if (_employListText != null)
        {
            _employListText.text =
                $"Employees: {_assignedEmployees.Count}\n" +
                $"D:{totalDesign} P:{totalDev} A:{totalArt}";
        }

    }

    // Getter & Setter
    public int CompletionReward
    {
        get => _completionReward;
        set => _completionReward = value;
    }

    public string ProjectName
    {
        get => _projectName;
        set
        {
            _projectName = value;
            RefreshUI();
        }
    }

    public ProjectType Type
    {
        get => _projectType;
        set => _projectType = value;
    }

    public ProjectSize Size
    {
        get => _projectSize;
        set => _projectSize = value;
    }

    public int RequiredDesignSkill
    {
        get => _requiredDesignSkill;
        set
        {
            _requiredDesignSkill = value;
            RefreshUI();
        }
    }

    public int RequiredProgrammingSkill
    {
        get => _requiredProgrammingSkill;
        set
        {
            _requiredProgrammingSkill = value;
            RefreshUI();
        }
    }

    public int RequiredArtSkill
    {
        get => _requiredArtSkill;
        set
        {
            _requiredArtSkill = value;
            RefreshUI();
        }
    }

    public int Quality
    {
        get => _quality;
        set
        {
            _quality = Mathf.Max(0, value);
            RefreshUI();
        }
    }


    public int RequiredWorkAmount
    {
        get => _requiredWorkAmount;
        set => _requiredWorkAmount = value;
    }

    public int CurrentWorkAmount
    {
        get => _currentWorkAmount;
        set
        {
            _currentWorkAmount = value;
            RefreshUI();
        }
    }

    public List<Employee> AssignedEmployees
    {
        get => _assignedEmployees;
        set => _assignedEmployees = value;
    }


    public bool AddEmployee(Employee employee)
    {
        if (_assignedEmployees.Count >= employeeSlots.Length)
        {
            Debug.LogWarning($"❌ 프로젝트 '{_projectName}' 최대 고용 인원 초과 ({employeeSlots.Length}명)");
            return false;
        }

        int targetIndex = -1;
        for (int i = 0; i < employeeSlots.Length; i++)
        {
            if (employeeSlots[i].childCount == 0)
            {
                targetIndex = i;
                break;
            }
        }

        if (targetIndex == -1) return false;

        _assignedEmployees.Add(employee);
        RefreshUI();

        var go = employee.gameObject;
        go.transform.SetParent(employeeSlots[targetIndex], false);
        go.transform.localPosition = new Vector3(0, 0, -1f);

        var draggable = go.GetComponent<DraggableEmployee>();
        if (draggable != null)
        {
            draggable.currentOwnerType = OwnerType.Project;
            draggable.currentProject = this;
            draggable.waitingRoomSlot = null;
        }

        return true;
    }

    public void RemoveEmployee(DraggableEmployee employee)
    {
        _assignedEmployees.Remove(employee.employee);

        // 슬롯 자식 중 자신일 경우 위치 정리
        foreach (Transform slot in employeeSlots)
        {
            if (slot.childCount > 0 && slot.GetChild(0) == employee.transform)
            {
                employee.transform.SetParent(null);
                break;
            }
        }

        RefreshUI();
    }


}
