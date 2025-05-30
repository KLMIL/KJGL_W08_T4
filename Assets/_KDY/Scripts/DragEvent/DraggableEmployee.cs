﻿using UnityEngine;

public enum OwnerType { None, WaitingRoom, Project }
public class DraggableEmployee : MonoBehaviour
{
    public Employee employee;

    private Vector3 _offset;
    private bool _dragging;
    private bool _isHovering;

    private SpriteRenderer _spriteRenderer;
    private Vector3 _originalPosition;
    public OwnerType currentOwnerType;
    public Project currentProject;
    public Transform waitingRoomSlot;

    private OwnerType originalOwnerType;
    private Project originalProject;
    private Transform originalWaitingSlot;


    // 250423-1830 추가 - KWS
    [Header("Hover UI")]
    private CurrentProjectInfo _infoCanvas;



    public void Init(EmployeeData data)
    {
        employee = GetComponent<Employee>();
        if (employee != null)
        {
            employee.SetEmployeeData(data);
        }
        else
        {
            Debug.LogError("Employee 컴포넌트를 찾을 수 없습니다.");
        }

        // 250423-1830 추가 - KWS
        _infoCanvas = FindAnyObjectByType<CurrentProjectInfo>();
    }
    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //_spriteRenderer.sortingOrder = 999;
        transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
    }

    void OnMouseDown()
    {
        _offset = transform.position - GetMouseWorldPosition();
        _originalPosition = transform.position;
        _dragging = true;

        _infoCanvas.HideEmployeeInfo();

        // 🔥 드래그 시작 시 현재 소속 백업!
        originalOwnerType = currentOwnerType;
        originalProject = currentProject;
        originalWaitingSlot = waitingRoomSlot;

        // 현재 소속 해제
        if (currentOwnerType == OwnerType.Project && currentProject != null)
        {
            currentProject.RemoveEmployee(this);
            currentOwnerType = OwnerType.None;
            currentProject = null;
        }
        else if (currentOwnerType == OwnerType.WaitingRoom && waitingRoomSlot != null)
        {
            transform.SetParent(null); // 대기실에서 분리
            currentOwnerType = OwnerType.None;
            waitingRoomSlot = null;
        }
    }



    void OnMouseUp()
    {
        _dragging = false;

        var hits = Physics2D.OverlapPointAll(GetMouseWorldPosition());
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out ProjectDropReceiver projectDrop))
            {
                if (projectDrop.OnEmployeeDropped(this)) return;
            }

            if (hit.TryGetComponent(out WaitingRoomDropReceiver waitingDrop))
            {
                if (waitingDrop.OnEmployeeDropped(this)) return;
            }

            if (hit.TryGetComponent(out FireEmployeeDropReciever fireDrop))
            {
                if (fireDrop.OnEmployeeDropped(this)) return;
            }
        }

        //ShowInfo();

        ReturnToOrigin();
    }


    // 250423-1830 추가 - KWS
    private void OnMouseEnter()
    {
        if (!_dragging)
        {
            //Debug.Log("Mouse Enter to Employee");
            _isHovering = true;
        }
    }

    private void ShowInfo()
    {
        string[] infoStr = new string[4];
        infoStr[0] = employee.GetEmployeeData().designSkill == 5 ? $"기획: {employee.GetEmployeeData().designSkill} (Max)" : $"기획: {employee.GetEmployeeData().designSkill}";
        infoStr[0] += employee.GetEmployeeData().devSkill == 5 ? $" \n개발: {employee.GetEmployeeData().devSkill} (Max)" : $" \n개발: {employee.GetEmployeeData().devSkill}";
        infoStr[0] += employee.GetEmployeeData().artSkill == 5 ? $" \n아트: {employee.GetEmployeeData().artSkill} (Max)" : $" \n아트: {employee.GetEmployeeData().artSkill}";
        //infoStr[0] = $"기획: {employee.GetEmployeeData().designSkil} \n개발: {employee.GetEmployeeData().devSkil} \n아트: {employee.GetEmployeeData().artSkil}";
        infoStr[1] = ((int)employee.GetEmployeeData().stress).ToString();

        int year = employee.GetEmployeeData().joinMonth / 12 + 2000;
        int month = employee.GetEmployeeData().joinMonth % 12 + 1;
        infoStr[2] = $"[입사] {year}년 {month}월";
        infoStr[3] = $"[연봉] {employee.GetEmployeeData().salary} G";
        _infoCanvas.ShowEmployeeInfo(infoStr);
    }

    private void OnMouseExit()
    {
        //Debug.Log("Mouse Exit from Project");
        _isHovering = false;
        _infoCanvas.HideEmployeeInfo();
    }



    private void ReturnToOrigin()
    {
        switch (originalOwnerType)
        {
            case OwnerType.Project:
                if (originalProject != null)
                {
                    bool result = originalProject.AddEmployee(employee);
                    if (!result)
                        Debug.LogError("⚠️ 되돌리기 실패: 프로젝트 슬롯이 꽉 찼을 수 있음");
                }
                else
                {
                    Debug.LogError("⚠️ 되돌리기 실패: 원래 프로젝트가 null임");
                }
                break;

            case OwnerType.WaitingRoom:
                if (originalWaitingSlot != null)
                {
                    transform.SetParent(originalWaitingSlot, false);
                    transform.localPosition = new Vector3(0, 0, -1f);
                    currentOwnerType = OwnerType.WaitingRoom;
                    waitingRoomSlot = originalWaitingSlot;
                }
                else
                {
                    Debug.LogError("⚠️ 되돌리기 실패: 대기실 슬롯 정보 없음");
                }
                break;

            default:
                Debug.LogError("❌ 원래 소속이 명확하지 않음.");
                break;
        }
    }





    void Update()
    {
        if (_dragging)
        {
            transform.position = GetMouseWorldPosition() + _offset;
        }
        else if (_isHovering)
        {
            ShowInfo();
        }

    }

    private Vector3 GetMouseWorldPosition()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 10; // 카메라와의 거리
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}