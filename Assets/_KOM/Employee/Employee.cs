using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Employee : MonoBehaviour
{
    EmployeeData _employeeData;
    public SpriteRenderer levelupImage;

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
                if (_employeeData.designSkil >= 5) break; //Max Level 5
                if (ChekSkilLevelup(_employeeData.designSkil, _employeeData.designExp)) { _employeeData.designSkil++; StartCoroutine("Levelup"); }
                break;

            case ExpType.devExp:
                _employeeData.devExp += expValue;
                if (_employeeData.devSkil >= 5) break; //Max Level 5
                if (ChekSkilLevelup(_employeeData.devSkil, _employeeData.devExp)) { _employeeData.devSkil++; StartCoroutine("Levelup"); }                    
                break;

            case ExpType.artExp:
                _employeeData.artExp += expValue;
                if (_employeeData.artSkil >= 5) break; //Max Level 5 
                if (ChekSkilLevelup(_employeeData.artSkil, _employeeData.artExp)) { _employeeData.artSkil++; StartCoroutine("Levelup"); }
                break;
        }
    }
    IEnumerable Levelup()
    {
        LevelupImageOn();
        yield return new WaitForSeconds(3f);
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
    }

    bool ChekSkilLevelup(int skilLevel, float expValue)
    {
        return levelupExpTable[skilLevel] < expValue;
    }
    public void IncreaseSalary()
    {
        _employeeData.salary += (int)Math.Floor(ResumeManager.increaseSalary * _employeeData.salary/100.0);
    }
    Dictionary<int, int> levelupExpTable = new Dictionary<int, int>
    {
        { 1, 1000 },
        { 2, 2500 },
        { 3, 7500 },
        { 4, 28500 }
    };
}
