using UnityEngine;

public class DraggableEmployee : MonoBehaviour
{
    public EmployeeData employeeData;

    private Vector3 _offset;
    private bool _dragging;

    private SpriteRenderer _spriteRenderer;
    private int _originalOrder;
    
    void Start()
    {
        // 예시용 더미 데이터
        employeeData = new EmployeeData(10, 20, 1, 300);
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _originalOrder = _spriteRenderer.sortingOrder;
    }

    void OnMouseDown()
    {
        _offset = transform.position - GetMouseWorldPosition();
        _dragging = true;
        _spriteRenderer.sortingOrder = 999;
    }

    void OnMouseUp()
    {
        _dragging = false;
        _spriteRenderer.sortingOrder = _originalOrder;
        // 드롭 처리
        var hits = Physics2D.OverlapPointAll(GetMouseWorldPosition());
        foreach (var hit in hits)
        {
            var dropTarget = hit.GetComponent<ProjectDropReceiver>();
            if (dropTarget != null)
            {
                dropTarget.OnEmployeeDropped(employeeData);
                Destroy(gameObject);
                break;
            }
            else
            {
                Debug.Log("DropTarget 감지 안됨");
            }
        }
    }

    void Update()
    {
        if (_dragging)
        {
            transform.position = GetMouseWorldPosition() + _offset;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 10; // 카메라와의 거리
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}