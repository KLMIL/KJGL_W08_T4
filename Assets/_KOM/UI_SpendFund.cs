using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SpendFund : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI spendFundsText;

    List<Employee> allEmployee = new List<Employee>();

    void Start()
    {
        UpdateSpendFundUI();
    }
    private void FixedUpdate()
    {
        UpdateSpendFundUI();
    }
    void UpdateSpendFundUI()
    {
        int thisMonthSpendFund = 0;
        foreach (Employee emp in allEmployee)
        {
            thisMonthSpendFund += emp.GetEmployeeData().salary/12;
        }
        thisMonthSpendFund += 100; // 비서 월급
        spendFundsText.text = $"-{thisMonthSpendFund:N0} G";
    }
    public void AddEmployeeList(Employee employee) { allEmployee.Add(employee); }
    public void RemoveEmployeeList(Employee employee) { allEmployee.Remove(employee); }
}
