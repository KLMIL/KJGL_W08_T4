using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Employee : MonoBehaviour
{
    EmployeeData _employeeData;
    public SpriteRenderer levelupImage;
    public int yearInWork = 0;
    public void SetEmployeeData(EmployeeData employeeData)
    {
        _employeeData = employeeData;

        // Cloth 색상 랜덤화
        Transform clothTransform = transform.Find("Cloth");
        if (clothTransform != null)
        {
            SpriteRenderer clothRenderer = clothTransform.GetComponent<SpriteRenderer>();
            if (clothRenderer != null)
            {
                Color randomColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
                clothRenderer.color = randomColor;
            }
        }
    }
    public void FadeHairAlpha()
    {
        Transform hairTransform = transform.Find("Hair");
        if (hairTransform != null)
        {
            SpriteRenderer hairRenderer = hairTransform.GetComponent<SpriteRenderer>();
            if (hairRenderer != null)
            {
                Color currentColor = hairRenderer.color;
                currentColor.a = Mathf.Max(0f, currentColor.a - 0.05f);
                hairRenderer.color = currentColor;
            }
        }
    }

    public EmployeeData GetEmployeeData()
    {
        return _employeeData;
    }
    public bool IncreaseStress(float value)
    {
        _employeeData.stress += value;
        if (_employeeData.stress > EmployeeData.maxStress) return false;
        return true;
    }
    public void IncreaseExp(float expValue, ExpType expType)
    {
        switch (expType)
        {
            case ExpType.designExp:
                _employeeData.designExp += expValue; 
                if (_employeeData.designSkill >= 5) break; //Max Level 5
                if (ChekSkilLevelup(_employeeData.designSkill, _employeeData.designExp)) { _employeeData.designSkill++; StartCoroutine("Levelup"); }
                break;

            case ExpType.devExp:
                _employeeData.devExp += expValue;
                if (_employeeData.devSkill >= 5) break; //Max Level 5
                if (ChekSkilLevelup(_employeeData.devSkill, _employeeData.devExp)) { _employeeData.devSkill++; StartCoroutine("Levelup"); }                    
                break;

            case ExpType.artExp:
                _employeeData.artExp += expValue;
                if (_employeeData.artSkill >= 5) break; //Max Level 5 
                if (ChekSkilLevelup(_employeeData.artSkill, _employeeData.artExp)) { _employeeData.artSkill++; StartCoroutine(Levelup()); }
                break;
        }
    }
    IEnumerator Levelup()
    {
        Debug.Log("Levelup");
        LevelupImageOn();
        yield return new WaitForSecondsRealtime(3f);
        LevelupImageOff();
    }
    void LevelupImageOn()
    {
        levelupImage.enabled = true;
    }
    void LevelupImageOff()
    {
        levelupImage.enabled = false;
    }


    public void ResetStress()
    {
        _employeeData.stress = 0;
        Debug.Log($"{gameObject.name}, {_employeeData.stress}");
    }

    bool ChekSkilLevelup(int skilLevel, float expValue)
    {
        return levelupExpTable[skilLevel] < expValue;
    }
    public void IncreaseSalary()
    {
        if(yearInWork < 10)
        {
            yearInWork++;
            _employeeData.salary += (int)Math.Floor((ResumeManager.increaseSalary * _employeeData.salary / 100.0) + ResumeManager.baseIncrease);
        }
        
    }
    Dictionary<int, int> levelupExpTable = new Dictionary<int, int>
    {
        { 1, 300 },
        { 2, 600 },
        { 3, 1800 },
        { 4, 5400 }
    };
}
