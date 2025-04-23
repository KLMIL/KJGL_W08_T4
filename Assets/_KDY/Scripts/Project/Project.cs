using System.Collections.Generic;
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
    private List<EmployeeData> _assignedEmployees;

    // Getter & Setter
    public int CompletionReward
    {
        get => _completionReward;
        set => _completionReward = value;
    }
    public string ProjectName
    {
        get => _projectName;
        set => _projectName = value;
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
        set => _requiredDesignSkill = value;
    }

    public int RequiredProgrammingSkill
    {
        get => _requiredProgrammingSkill;
        set => _requiredProgrammingSkill = value;
    }

    public int RequiredArtSkill
    {
        get => _requiredArtSkill;
        set => _requiredArtSkill = value;
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
        set => _currentWorkAmount = value;
    }

    public List<EmployeeData> AssignedEmployees
    {
        get => _assignedEmployees;
        set => _assignedEmployees = value;
    }
}