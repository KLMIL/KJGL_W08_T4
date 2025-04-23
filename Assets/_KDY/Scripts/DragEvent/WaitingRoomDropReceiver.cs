using UnityEngine;

public class WaitingRoomDropReceiver : MonoBehaviour
{
    private Transform employeeContainer;

    private void Awake()
    {
        if (employeeContainer == null)
            employeeContainer = transform; // 기본은 자기 자신
    }

    public bool OnEmployeeDropped(DraggableEmployee draggable)
    {
        if (draggable == null) return false;

        // 최대 수 확인
        if (employeeContainer.childCount >= 15)
        {
            Debug.LogWarning("⚠️ 대기실 초과로 수용 불가");
            return false;
        }

        // 실제 배치
        draggable.transform.SetParent(employeeContainer, false);
        draggable.transform.localPosition = new Vector3(0, 0, -1f);

        draggable.currentOwnerType = OwnerType.WaitingRoom;
        draggable.waitingRoomSlot = employeeContainer;
        draggable.currentProject = null;

        return true;
    }
}