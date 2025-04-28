using UnityEngine;


public struct EmployeeData
{
    public int designSkill;
    public int devSkill;
    public int artSkill;

    public float designExp;
    public float devExp;
    public float artExp;

    public float stress;
    public const float maxStress = 100;
    public int joinMonth;

    public int salary;
    /// <summary>
    /// (int minStatus, int maxStatus, int todayMonth, int startSalary)
    /// </summary>
    /// <param name="minStatus"></param>
    /// <param name="maxStatus"></param>
    /// <param name="todayMonth"></param>
    /// <param name="startSalary"></param>
    public EmployeeData(int minStatus, int maxStatus, int todayMonth, int startSalary)
    {
        designSkill = RandomStatus(minStatus, maxStatus);
        devSkill = RandomStatus(minStatus, maxStatus);
        artSkill = RandomStatus(minStatus, maxStatus);

        designExp = 0;
        devExp = 0;
        artExp = 0;

        stress = 0f;
        joinMonth = todayMonth;
        salary = startSalary;
    }
    static int RandomStatus(int minStatus, int maxStatus)
    {
        return UnityEngine.Random.Range(minStatus, maxStatus + 1);
    }
}
public class ResumeManager : MonoBehaviour
{
    public static ResumeManager Instance { get; private set; }
    public PrintResume printResume;
    public static int increaseSalary = 30;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        printResume = GetComponent<PrintResume>();
    }

    [SerializeField][Range(1, 5)] int newbieIndieMinStatus;
    [SerializeField][Range(1, 5)] int newbieIndieMaxStatus;
    //[SerializeField] int newbieIndieSalary;

    [SerializeField][Range(1, 5)] int newbieMidsizeCompanyMinStatus;
    [SerializeField][Range(1, 5)] int newbieMidsizeCompanyMaxStatus;
    //[SerializeField] int newbieMidsizeCompanySalary;

    [SerializeField][Range(1, 5)] int newbieLargeCompanyMinStatus;
    [SerializeField][Range(1, 5)] int newbieLargeCompanyMaxStatus;
    //[SerializeField] int newbieLargeCompanySalary;

    [SerializeField][Range(1, 5)] int experiencedIndieMinStatus;
    [SerializeField][Range(1, 5)] int experiencedIndieMaxStatus;
    //[SerializeField] int experiencedIndiSalary;

    [SerializeField][Range(1, 5)] int experiencedMidsizeCompanyMinStatus;
    [SerializeField][Range(1, 5)] int experiencedMidsizeCompanyMaxStatus;
    //[SerializeField] int experiencedMidsizeCompanySalary;

    [SerializeField][Range(1, 5)] int experiencedLargeCompanyMinStatus;
    [SerializeField][Range(1, 5)] int experiencedLargeCompanyMaxStatus;
    //[SerializeField] int experiencedLargeCompanySalary;

    EmployeeData NowResume;


    public EmployeeData CreateResume(bool isNewbie)
    { 
        int todayMonth = GameManager.Instance.todayMonth;
        int minStatus = 0;
        int maxStatus = 0;
        int salary = 0;
        switch (GameManager.Instance.myCompanyScale)
        {
            case CompanyScale.Indie:
                minStatus = isNewbie ? newbieIndieMinStatus : experiencedIndieMinStatus;
                maxStatus = isNewbie ? newbieIndieMaxStatus : experiencedIndieMaxStatus;
                //salary = isNewbie ? newbieIndieSalary : experiencedIndiSalary;
                break;

            case CompanyScale.MidsizeCompany:
                minStatus = isNewbie ? newbieMidsizeCompanyMinStatus : experiencedMidsizeCompanyMinStatus;
                maxStatus = isNewbie ? newbieMidsizeCompanyMaxStatus : experiencedMidsizeCompanyMaxStatus;
                //salary = isNewbie ? newbieMidsizeCompanySalary : experiencedMidsizeCompanySalary;
                break;

            case CompanyScale.LargeCompany:
                minStatus = isNewbie ? newbieLargeCompanyMinStatus : experiencedLargeCompanyMinStatus;
                maxStatus = isNewbie ? newbieLargeCompanyMaxStatus : experiencedLargeCompanyMaxStatus;
                //salary = isNewbie ? newbieLargeCompanySalary : experiencedLargeCompanySalary;
                break;
            default:
                Debug.LogError("CompanyScale input Error");
                break;
        }
        NowResume = new EmployeeData(minStatus, maxStatus, todayMonth, salary);
        int totalStat = NowResume.artSkill + NowResume.designSkill + NowResume.devSkill;
        NowResume.salary = RandomSalary(totalStat * 300);
        return NowResume;
    }
    int RandomSalary(int salary)
    {
        int min = Mathf.FloorToInt(salary * 0.8f);
        int max = Mathf.CeilToInt(salary * 1.2f);
        return Random.Range(min, max + 1);
    }
}