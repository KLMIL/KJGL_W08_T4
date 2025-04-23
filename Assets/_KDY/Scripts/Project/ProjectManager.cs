using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    private List<Project> _currentProjects = new List<Project>();
    private List<EmployeeData> _currentEmployeeData = new List<EmployeeData>();
    
    private GameObject _projectPrefab;
    private Transform _projectContainer;
    
    void Awake()
    {
        _projectPrefab = Resources.Load<GameObject>("_KDY/Prefabs/Project");
        _projectContainer = GameObject.Find("ProjectContainer")?.transform;
    }

    public void TestSmall()
    {
        MakeProject(ProjectType.Casual, ProjectSize.Small);
    }
    public void TestMedium()
    {
        MakeProject(ProjectType.RPG, ProjectSize.Medium);
    }
    public void TestLarge()
    {
        MakeProject(ProjectType.Strategy, ProjectSize.Large);
    }
    
    // make project with type and size
    public Project MakeProject(ProjectType type, ProjectSize size)
    {
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




    // get project with index in _currentProjects
    public Project GetCurrentProjectByIndex(int index)
    {
        if (index < 0 || index >= _currentProjects.Count)
        {
            Debug.LogWarning("Project index is out of range.");
            return null;
        }
        else
        {
            return CurrentProjects[index];
        }
    }
    
    // Getter & Setter
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
