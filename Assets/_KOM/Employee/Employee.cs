using System.Collections.Generic;
using System;
using UnityEngine;

public class Employee : MonoBehaviour
{
    EmployeeData _employeeData;

    public void SetEmployeeData(EmployeeData EmployeeData)
    {
        _employeeData = EmployeeData;
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
                if (_employeeData.designSkil >= 4) break; //Max Level 4
                if (ChekSkilLevelup(_employeeData.designSkil, _employeeData.designExp)) { _employeeData.designSkil++; }
                break;

            case ExpType.devExp:
                _employeeData.devExp += expValue;
                if (_employeeData.devSkil >= 4) break; //Max Level 4
                if (ChekSkilLevelup(_employeeData.devSkil, _employeeData.devExp)) { _employeeData.devSkil++; }                    
                break;

            case ExpType.artExp:
                _employeeData.artExp += expValue;
                if (_employeeData.artSkil >= 4) break; //Max Level 4 
                if (ChekSkilLevelup(_employeeData.artSkil, _employeeData.artExp)) { _employeeData.artSkil++; }
                break;
        }
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
        { 1, 100 },
        { 2, 250 },
        { 3, 750 },
        { 4, 2850 }
    };
}
