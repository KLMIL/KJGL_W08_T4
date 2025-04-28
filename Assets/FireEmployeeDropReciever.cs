using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class FireEmployeeDropReciever : MonoBehaviour
{
    Transform fireRoom;

    private void Awake()
    {
        if(fireRoom == null)
        {
            fireRoom = this.transform;
        }
    }

    public bool OnEmployeeDropped(DraggableEmployee draggable)
    {
        if (draggable == null) return false;
        Debug.LogWarning($"직원 해고");
        Employee emp = draggable.GetComponent<Employee>();

        //지출 UI 데이터 갱신
        UIManager.Instance.ui_SpendFundsCanvas.GetComponent<UI_SpendFund>().RemoveEmployeeList(emp);
        // 회사의 직원 리스트에서 제거
        ProjectManager.Instance.AllEmployees.Remove(emp);
        ProjectManager.Instance.notifier.ShowNotification($"직원 한 명이 해고 됐습니다.");

        ProjectManager.Instance.AllEmployees.Remove(emp);

        // 오브젝트 제거
        Destroy(emp.gameObject);

        GameManager.Instance.OnEmployeeRemoved();
        return true;
    }
}
