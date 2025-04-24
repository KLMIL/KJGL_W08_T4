using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    public static ProjectManager Instance { get; private set; }

    private List<Project> _currentProjects = new List<Project>();
    private List<Employee> _allEmployees = new List<Employee>();

    private GameObject _projectPrefab;
    private Transform _projectContainer;
    private Transform[] _projectSlots = new Transform[8];

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _projectPrefab = Resources.Load<GameObject>("_KDY/Prefabs/Project");
        _projectContainer = GameObject.Find("ProjectContainer")?.transform;

        for (int i = 0; i < 8; i++)
        {
            var slot = _projectContainer.Find("Slot" + (i + 1).ToString());
            if (slot != null)
            {
                _projectSlots[i] = slot;
            }
            else
            {
                 Debug.LogWarning($"⚠️ Slot{i} not found in ProjectContainer");
            }
        }
    }

    public void TestSmall() => MakeProject(ProjectType.Casual, ProjectSize.Small);
    public void TestMedium() => MakeProject(ProjectType.RPG, ProjectSize.Medium);
    public void TestLarge() => MakeProject(ProjectType.Strategy, ProjectSize.Large);

    private int GetUnlockedProjectSlotCount()
    {
        switch (GameManager.Instance.RoomLevel)
        {
            case 0: return 2;
            case 1: return 4;
            case 2: return 6;
            case 3: return 8;
            default: return 2;
        }
    }

    public Project MakeProject(ProjectType type, ProjectSize size)
    {
        int maxSlots = GetUnlockedProjectSlotCount();

        for (int i = 0; i < maxSlots; i++)
        {
            if (_projectSlots[i].childCount == 0)
            {
                GameObject projectObject = Instantiate(_projectPrefab, _projectSlots[i]);
                projectObject.transform.localPosition = Vector3.zero;

                Project newProject = projectObject.GetComponent<Project>();
                newProject.Size = size;
                string projectName = ProjectDataGenerator.GetRandomProjectName();
                newProject.ProjectName = projectName;
                newProject.RequiredDesignSkill = ProjectDataGenerator.GetRequiredSkill(size);
                newProject.RequiredProgrammingSkill = ProjectDataGenerator.GetRequiredSkill(size);
                newProject.RequiredArtSkill = ProjectDataGenerator.GetRequiredSkill(size);
                newProject.RequiredWorkAmount = ProjectDataGenerator.GetRequiredWorkAmount(size);
                newProject.CompletionReward = ProjectDataGenerator.GetRewardEstimate(size);

                newProject.Quality = 100;

                _currentProjects.Add(newProject);
                newProject.RefreshUI();
                UIManager.Instance.ToggleUpgradesCanvas(false);
                return newProject;
            }
        }

        Debug.LogWarning("❌ 프로젝트 생성 실패: 모든 슬롯이 사용 중입니다.");
        return null;
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

    public List<Employee> AllEmployees
    {
        get => _allEmployees;
        set => _allEmployees = value;
    }


    // 2025-04-24 10:30 수정 - KWS
    public void TickWork()
    {
        // 월급 정산(대기실 + 프로젝트 모든 직원)
        foreach (var employee in _allEmployees)
        {
            GameManager.Instance.SpendFunds(employee.GetEmployeeData().salary / 12);    
        }
        
        // 프로젝트 정산
        foreach(var project in _currentProjects)
        {
            project.TickWork();
        }
    }
}
