using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    private List<Project> _currentProjects;
    private List<EmployeeData> _currentEmployeeData;
    
    private GameObject _projectPrefab;
    private Transform _projectContainer;
    
    void Awake()
    {
        _projectPrefab = Resources.Load<GameObject>("Assets/_KDY/Prefabs/Project.prefab"); // Project.prefab 경로
        _projectContainer = GameObject.Find("ProjectContainer")?.transform;
    }
    
    // make project with type and size
    public Project MakeProject(ProjectType type, ProjectSize size)
    {
        GameObject projectGO = Instantiate(_projectPrefab, _projectContainer);
        projectGO.transform.SetAsLastSibling();

        Project newProject = projectGO.GetComponent<Project>();
        newProject.Type = type;
        newProject.Size = size;
        newProject.ProjectName = $"New {type} ({size}) Game";

        newProject.RequiredDesignSkill = ProjectDataGenerator.GetRequiredSkill(type, size, SkillType.Design);
        newProject.RequiredProgrammingSkill = ProjectDataGenerator.GetRequiredSkill(type, size, SkillType.Programming);
        newProject.RequiredArtSkill = ProjectDataGenerator.GetRequiredSkill(type, size, SkillType.Art);
        newProject.RequiredWorkAmount = ProjectDataGenerator.GetRequiredWorkAmount(size);
        newProject.CompletionReward = ProjectDataGenerator.GetRewardEstimate(type, size);

        _currentProjects.Add(newProject);
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
