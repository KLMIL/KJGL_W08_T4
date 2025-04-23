using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentProjectInfo : MonoBehaviour
{
    [SerializeField] private Image _infoImage;
    [SerializeField] private TextMeshProUGUI _infoText;

    //public void ShowInfo(string text, bool isLeftArea)
    public void ShowInfo(string text) //(string text)
    {
        _infoText.SetText(text);
        _infoImage.gameObject.SetActive(true);
    }

    public void HideInfo()
    {
        if (_infoImage != null)
            _infoImage.gameObject.SetActive(false);

        if (_infoText != null)
            _infoText.text = "";
    }
}