using UnityEngine;

public class ProjectDropReceiver : MonoBehaviour
{
    private Project _project;

    private void Awake()
    {
        _project = GetComponent<Project>();
    }

    public void OnEmployeeDropped(EmployeeData data)
    {
        if (_project != null)
        {
            _project.AddEmployee(data);
        }
    }
}