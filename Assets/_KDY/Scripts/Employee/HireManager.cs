using UnityEngine;
using UnityEngine.UI;

public class HireManager : MonoBehaviour
{
    private Transform employeeContainer;
    private const int MaxEmployeeCount = 15;
    private bool _isNewbie = true;

    public bool IsNewbie
    {
        get => _isNewbie;
        set => _isNewbie = value;
    }

    private void Awake()
    {
        employeeContainer = GameObject.Find("EmployeeContainer")?.transform;
    }

    public void HireEmployee(EmployeeData resume)
    {
        if (!GameManager.Instance.CanHireMore())
        {
            Debug.LogWarning("❌ 고용 인원 초과 (최대 15명)");
            return;
        }
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

        var instance = Instantiate(prefab, employeeContainer);
        var draggable = instance.GetComponent<DraggableEmployee>();
        if (draggable != null)
        {
            draggable.Init(data);
        
            // ✅ 대기실 소속으로 세팅
            draggable.currentOwnerType = OwnerType.WaitingRoom;
            draggable.waitingRoomSlot = instance.transform.parent;
        }
        
        // 경력직은 일단 머리 쳐냄
        if (!IsNewbie)
        {
            for (int i = 0; i < 10; i++)
                instance.GetComponent<Employee>().FadeHairAlpha();
        }
        
        // 프로젝트 매니저에 회사 소속으로 등록
        ProjectManager.Instance.AllEmployees.Add(draggable.GetComponent<Employee>());
    }

}