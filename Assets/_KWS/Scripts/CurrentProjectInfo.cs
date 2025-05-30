﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentProjectInfo : MonoBehaviour
{
    [SerializeField] private Image _infoImage;

    [SerializeField] private GameObject _projectObject;
    [SerializeField] private TextMeshProUGUI _projectProgress;
    [SerializeField] private TextMeshProUGUI _projectName;
    [SerializeField] private TextMeshProUGUI _progressText;
    [SerializeField] private TextMeshProUGUI _qualityText;
    [SerializeField] private TextMeshProUGUI _requireText;
    //[SerializeField] private TextMeshProUGUI _welfareText;
    [SerializeField] private TextMeshProUGUI _expectCost;
    [SerializeField] private TextMeshProUGUI _projectNotEnoughDesign;
    [SerializeField] private TextMeshProUGUI _projectNotEnoughDev;
    [SerializeField] private TextMeshProUGUI _projectNotEnoughArt;


    [SerializeField] private GameObject _employeeObject;
    [SerializeField] private TextMeshProUGUI _skillText;
    //[SerializeField] private TextMeshProUGUI _stressText;
    [SerializeField] private GameObject _stressBarObject;
    [SerializeField] private TextMeshProUGUI _joinMonthText;
    [SerializeField] private TextMeshProUGUI _salaryText;

    //public void ShowInfo(string text, bool isLeftArea)
    public void ShowProjectInfo(string[] text, bool[] enough) //(string text)
    {
        _projectName.SetText(text[0]);
        _progressText.SetText(text[1]);
        _qualityText.SetText(text[2]);
        _requireText.SetText(text[3]);
        //_welfareText.SetText(text[4]);
        _expectCost.SetText(text[4]);
        _projectObject.SetActive(true);
        _infoImage.gameObject.SetActive(true);
        _projectProgress.SetText(text[5]);
        if (!enough[0])
        {
            _projectNotEnoughDesign.gameObject.SetActive(true);
        }
        if (!enough[1]) 
        {
            _projectNotEnoughDev.gameObject.SetActive(true);
        }
        if (!enough[2])
        {
            _projectNotEnoughArt.gameObject.SetActive(true);
        }

    }

    public void HideProjectInfo()
    {
        _infoImage.gameObject.SetActive(false);
        _projectObject.SetActive(false);

        _progressText.SetText("");
        _qualityText.SetText("");
        _requireText.SetText("");
        //_welfareText.SetText("");
        _expectCost.SetText("");

        _projectNotEnoughDesign.gameObject.SetActive(false);
        _projectNotEnoughDev.gameObject.SetActive(false);
        _projectNotEnoughArt.gameObject.SetActive(false);
    }


    public void ShowEmployeeInfo(string[] text)
    {
        _skillText.SetText(text[0]);
        //_stressText.SetText("스트레스:");
        _stressBarObject.transform.parent.gameObject.SetActive(true);
        _stressBarObject.GetComponent<UI_Stressbar>().UpdateHP(int.Parse(text[1]));
        _joinMonthText.SetText(text[2]);
        _salaryText.SetText(text[3]);

        _employeeObject.SetActive(true);
        _infoImage.gameObject.SetActive(true);
    }

    public void HideEmployeeInfo()
    {
        _infoImage.gameObject.SetActive(false);
        _employeeObject.SetActive(false);
        _stressBarObject.transform.parent.gameObject.SetActive(false);

        _skillText.SetText("");
        //_stressText.SetText("");
        _joinMonthText.SetText("");
        _salaryText.SetText("");
    }
}
