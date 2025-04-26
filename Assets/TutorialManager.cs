using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    static TutorialManager _instance;
    public static TutorialManager Instance => _instance;

    int _tutoIndex = 0;
    [SerializeField] GameObject[] _tutoPanels;
    Canvas ui_tutoCanvas;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        } else
        {
            Destroy(gameObject);
        }
        ui_tutoCanvas = GetComponent<Canvas>();
    }

    public void ToggleTutoCanvas(bool status)
    {
        ui_tutoCanvas.enabled = status;
        if (status)
        {
            Time.timeScale = 0;
            _tutoPanels[_tutoIndex].SetActive(false);
            _tutoIndex = 0;
            _tutoPanels[_tutoIndex].SetActive(true);
        } else
        {
            GameManager.Instance.MultipleIndex--;
            GameManager.Instance.ChangeTimeMultiple();
        }
    }

    public void OnPrevClick()
    {
        if(_tutoIndex > 0)
        {
            _tutoPanels[_tutoIndex].SetActive(false);
            _tutoIndex--;
            _tutoPanels[_tutoIndex].SetActive(true);
        } else
        {
            Debug.LogWarning("첫 번째 페이지");
        }
    }

    public void OnNextClick()
    {
        if (_tutoIndex < _tutoPanels.Length-1)
        {
            _tutoPanels[_tutoIndex].SetActive(false);
            _tutoIndex++;
            _tutoPanels[_tutoIndex].SetActive(true);
        }
        else
        {
            Debug.LogWarning("마지막 페이지");
        }
    }
}
