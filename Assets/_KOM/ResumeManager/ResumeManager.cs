using System;
using UnityEngine;


public struct EmployeeData
{
    public int designSkil;
    public int devSkill;
    public int artSkill;

    public int designExp;
    public int devExp;
    public int artExp;

    public float stress;
    public int joinDay;

    public int salary;

    public EmployeeData(int minStatus, int maxStatus, int todayDate, int startSalary)
    {
        designSkil = RandomStatus(minStatus, maxStatus);
        devSkill = RandomStatus(minStatus, maxStatus);
        artSkill = RandomStatus(minStatus, maxStatus);

        designExp = 0;
        devExp = 0;
        artExp = 0;

        stress = 0f;
        joinDay = todayDate;
        salary = startSalary;
    }
    static int RandomStatus(int minStatus, int maxStatus)
    {
        return UnityEngine.Random.Range(minStatus, maxStatus + 1);
    }
}

public class ResumeManager : MonoBehaviour
{

    [SerializeField][Range(1, 5)] int newbieIndieMinStatus;
    [SerializeField][Range(1, 5)] int newbieIndieMaxStatus;
    [SerializeField] int newbieIndieSalary;

    [SerializeField][Range(1, 5)] int newbieMidsizeCompanyMinStatus;
    [SerializeField][Range(1, 5)] int newbieMidsizeCompanyMaxStatus;
    [SerializeField] int newbieMidsizeCompanySalary;

    [SerializeField][Range(1, 5)] int newbieLargeCompanyMinStatus;
    [SerializeField][Range(1, 5)] int newbieLargeCompanyMaxStatus;
    [SerializeField] int newbieLargeCompanySalary;

    [SerializeField][Range(1, 5)] int experiencedIndieMinStatus;
    [SerializeField][Range(1, 5)] int experiencedIndieMaxStatus;
    [SerializeField] int experiencedIndiSalary;

    [SerializeField][Range(1, 5)] int experiencedMidsizeCompanyMinStatus;
    [SerializeField][Range(1, 5)] int experiencedMidsizeCompanyMaxStatus;
    [SerializeField] int experiencedMidsizeCompanySalary;

    [SerializeField][Range(1, 5)] int experiencedLargeCompanyMinStatus;
    [SerializeField][Range(1, 5)] int experiencedLargeCompanyMaxStatus;
    [SerializeField] int experiencedLargeCompanySalary;

    EmployeeData NowResume;


    public EmployeeData CreateResume(bool isNewbie)
    { 
        int todayDate = GameManager.Instance.todayDate;
        int minStatus = 0;
        int maxStatus = 0;
        int salary = 0;
        switch (GameManager.Instance.myCompanyScale)
        {
            case CompanyScale.Indie:
                minStatus = isNewbie ? newbieIndieMinStatus : experiencedIndieMinStatus;
                maxStatus = isNewbie ? newbieIndieMaxStatus : experiencedIndieMaxStatus;
                salary = isNewbie ? newbieIndieSalary : experiencedIndiSalary;
                break;

            case CompanyScale.MidsizeCompany:
                minStatus = isNewbie ? newbieMidsizeCompanyMinStatus : experiencedMidsizeCompanyMinStatus;
                maxStatus = isNewbie ? newbieMidsizeCompanyMaxStatus : experiencedMidsizeCompanyMaxStatus;
                salary = isNewbie ? newbieMidsizeCompanySalary : experiencedMidsizeCompanySalary;
                break;

            case CompanyScale.LargeCompany:
                minStatus = isNewbie ? newbieLargeCompanyMinStatus : experiencedLargeCompanyMinStatus;
                maxStatus = isNewbie ? newbieLargeCompanyMaxStatus : experiencedLargeCompanyMaxStatus;
                salary = isNewbie ? newbieLargeCompanySalary : experiencedLargeCompanySalary;
                break;
            default:
                Debug.LogError("CompanyScale input Error");
                break;
        }
        int startSalary = RandomSalary(salary);
        NowResume = new EmployeeData(minStatus, maxStatus, todayDate, startSalary);
        return NowResume;
    }
    int RandomSalary(int salary)
    {
        int min = Mathf.FloorToInt(salary * 0.8f);
        int max = Mathf.CeilToInt(salary * 1.2f);
        return UnityEngine.Random.Range(min, max + 1);
    }
}
