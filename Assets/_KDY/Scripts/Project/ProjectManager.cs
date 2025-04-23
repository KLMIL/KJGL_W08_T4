using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    public static ProjectManager Instance { get; private set; }

    private List<Project> _currentProjects = new List<Project>();
    private List<EmployeeData> _currentEmployeeData = new List<EmployeeData>();
    
    private GameObject _projectPrefab;
    private Transform _projectContainer;

    private void Awake()
    {
        // 중복 방지
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _projectPrefab = Resources.Load<GameObject>("_KDY/Prefabs/Project");
        _projectContainer = GameObject.Find("ProjectContainer")?.transform;
    }

    public void TestSmall() => MakeProject(ProjectType.Casual, ProjectSize.Small);
    public void TestMedium() => MakeProject(ProjectType.RPG, ProjectSize.Medium);
    public void TestLarge() => MakeProject(ProjectType.Strategy, ProjectSize.Large);

    public Project MakeProject(ProjectType type, ProjectSize size)
    {
        const int MaxProjects = 8;
        if (_currentProjects.Count >= MaxProjects)
        {
            Debug.LogWarning("❌ 프로젝트 최대 생성 수 (8개)를 초과했습니다.");
            return null;
        }

        GameObject projectPrefab = Instantiate(_projectPrefab, _projectContainer);
        projectPrefab.transform.SetAsLastSibling();

        Project newProject = projectPrefab.GetComponent<Project>();
        newProject.Type = type;
        newProject.Size = size;
        newProject.ProjectName = $"New {type} ({size}) Game";

        newProject.RequiredDesignSkill = ProjectDataGenerator.GetRequiredSkill(type, size, SkillType.Design);
        newProject.RequiredProgrammingSkill = ProjectDataGenerator.GetRequiredSkill(type, size, SkillType.Programming);
        newProject.RequiredArtSkill = ProjectDataGenerator.GetRequiredSkill(type, size, SkillType.Art);
        newProject.RequiredWorkAmount = ProjectDataGenerator.GetRequiredWorkAmount(size);
        newProject.CompletionReward = ProjectDataGenerator.GetRewardEstimate(type, size);
        newProject.Quality = 100;

        _currentProjects.Add(newProject);
        newProject.RefreshUI();
        return newProject;
    }

    public Project GetCurrentProjectByIndex(int index)
    {
        if (index < 0 || index >= _currentProjects.Count)
        {
            Debug.LogWarning("Project index is out of range.");
            return null;
        }

        return _currentProjects[index];
    }

    public List<Project> CurrentProjects
    {
        get => _currentProjects;
        set => _currentProjects = value;
    }

    public List<EmployeeData> CurrentEmployeeData
    {
        get => _currentEmployeeData;
        set => _currentEmployeeData = value;
    }
}
