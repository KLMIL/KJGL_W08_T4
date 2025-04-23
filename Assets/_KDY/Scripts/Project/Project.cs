using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    [Header("Project current States")]
    private int _quality;
    private int _currentWorkAmount;
    private List<EmployeeData> _assignedEmployees = new List<EmployeeData>();

    [Header("Project Info panel")]
    private TextMeshPro _projectNameText;
    private TextMeshPro _skillSummaryText;
    private TextMeshPro _workAmountText;
    private TextMeshPro _employListText;
    
    [Header("Project Tick Work")]
    private float _workTimer = 0f;
    private const float WORK_INTERVAL = 1f;

    private void Update()
    {
        _workTimer += Time.deltaTime;
        if (_workTimer >= WORK_INTERVAL)
        {
            _workTimer = 0f;
            TickWork();
        }
    }
    private void TickWork()
    {
        // 고용인이 있어야 작업 진행
        if (_assignedEmployees.Count == 0) return;
        
        // 능력 부족시 스트레스 주기
        CheckAssignedStats();
        
        
        int totalPower = 0;
        foreach (var emp in _assignedEmployees)
        {
            totalPower += emp.designSkil + emp.devSkil + emp.artSkil;
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
        Debug.Log($"✅ 프로젝트 완료: {_projectName}");
        Destroy(gameObject);
    }
    
    private void CheckAssignedStats()
    {
        int totalDesign = 0;
        int totalDev = 0;
        int totalArt = 0;

        foreach (var emp in _assignedEmployees)
        {
            totalDesign += emp.designSkil;
            totalDev += emp.devSkil;
            totalArt += emp.artSkil;
        }

        bool designInsufficient = totalDesign < _requiredDesignSkill;
        bool devInsufficient = totalDev < _requiredProgrammingSkill;
        bool artInsufficient = totalArt < _requiredArtSkill;

        if (designInsufficient || devInsufficient || artInsufficient)
        {
            Debug.LogWarning($"⚠️ [{_projectName}] 능력치 부족: " +
                             $"{(designInsufficient ? "디자인 " : "")}" +
                             $"{(devInsufficient ? "개발 " : "")}" +
                             $"{(artInsufficient ? "아트 " : "")}");
            Debug.LogWarning("여기에서 고용인들 스트레스를 주세요!");
            ApplyQualityPenalty();
        }
    }
    private void ApplyQualityPenalty()
    {
        const int penaltyAmount = 5;
        int prevQuality = _quality;
        Quality = _quality - penaltyAmount;

        Debug.LogWarning($"❗ 프로젝트 [{_projectName}] 품질 감소: {prevQuality} → {_quality}");
    }



    private void Awake()
    {
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
        foreach (var emp in _assignedEmployees)
        {
            totalDesign += emp.designSkil;
            totalDev += emp.devSkil;
            totalArt += emp.artSkil;
        }

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

    public List<EmployeeData> AssignedEmployees
    {
        get => _assignedEmployees;
        set => _assignedEmployees = value;
    }
    
    public void AddEmployee(EmployeeData data)
    {
        _assignedEmployees ??= new List<EmployeeData>();
        _assignedEmployees.Add(data);
        RefreshUI();
    }

}
