using UnityEngine;

public class ProjectDropReceiver : MonoBehaviour
{
    private Project _project;

    private void Awake()
    {
        _project = GetComponent<Project>();
    }

    public Project GetProject()
    {
        return _project;
    }
    
    public bool OnEmployeeDropped(DraggableEmployee draggable)
    {
        return _project != null && _project.AddEmployee(draggable.employee);
    }
}