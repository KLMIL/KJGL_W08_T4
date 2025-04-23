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
        
        if (_employListText != null)
            _employListText.text = $"Employees:{_assignedEmployees.Count}";
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
        set => _quality = value;
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
}
