using UnityEngine;
using UnityEngine.UI;

public class HireManager : MonoBehaviour
{
    public Button hireNewbieButton;
    public Button hireExperiencedButton;

    private Transform employeeSpawnPoint;

    private void Awake()
    {
        hireNewbieButton.onClick.AddListener(() => HireEmployee(true));
        hireExperiencedButton.onClick.AddListener(() => HireEmployee(false));
        
        employeeSpawnPoint = GameObject.Find("EmployeeSpawnPoint")?.transform;
    }

    private void HireEmployee(bool isNewbie)
    {
        var resume = ResumeManager.Instance.CreateResume(isNewbie);
        SpawnDraggableEmployee(resume);
    }

    private void SpawnDraggableEmployee(EmployeeData data)
    {
        var prefab = Resources.Load<GameObject>("_KDY/Prefabs/DraggableEmployee");
        if (prefab == null)
        {
            Debug.LogError("❌ DraggableEmployee 프리팹이 _KDY/Resources/Prefabs 안에 없습니다.");
            return;
        }

        var instance = Instantiate(prefab, employeeSpawnPoint);
        var draggable = instance.GetComponent<DraggableEmployee>();
        if (draggable != null)
        {
            draggable.Init(data);
        }
        else
        {
            Debug.LogWarning("⚠️ DraggableEmployee 컴포넌트가 프리팹에 없습니다.");
        }
    }
}