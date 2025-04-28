using TMPro;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class ResumeCanvas : MonoBehaviour
{
    ResumeManager resumeManager;
    PrintResume printResume;

    EmployeeData nowEmployData;

    [SerializeField] TextMeshProUGUI DesignText1GUI;
    [SerializeField] TextMeshProUGUI DesignText2GUI;
    [SerializeField] TextMeshProUGUI DevText1GUI;
    [SerializeField] TextMeshProUGUI DevText2GUI;
    [SerializeField] TextMeshProUGUI ArtText1GUI;
    [SerializeField] TextMeshProUGUI ArtText2GUI;
    [SerializeField] TextMeshProUGUI SalaryValuyeGUI;

    string DesignText1; string DesignText2;
    string DevText1; string DevText2;
    string ArtText1; string ArtText2;

    void Start()
    {
        //resumePage.SetActive(false);
        resumeManager = ResumeManager.Instance;
        printResume = ResumeManager.Instance.printResume;

    }
    public void NewbieResumeButton()
    {
        //resumePage.SetActive(true);
        nowEmployData = resumeManager.CreateResume(true);

        (DesignText1, DesignText2) = printResume.GetRandomText(SkilType.design, nowEmployData.designSkill);
        (DevText1, DevText2) = printResume.GetRandomText(SkilType.dev, nowEmployData.devSkill);
        (ArtText1, ArtText2) = printResume.GetRandomText(SkilType.art, nowEmployData.artSkill);
        SalaryValuyeGUI.text = nowEmployData.salary.ToString() + " G";
        PrintResume();
    }
    public void ExperiencedResumeButton()
    {
        //resumePage.SetActive(true);
        nowEmployData = resumeManager.CreateResume(false);

        (DesignText1, DesignText2) = printResume.GetRandomText(SkilType.design, nowEmployData.designSkill);
        (DevText1, DevText2) = printResume.GetRandomText(SkilType.dev, nowEmployData.devSkill);
        (ArtText1, ArtText2) = printResume.GetRandomText(SkilType.art, nowEmployData.artSkill);
        SalaryValuyeGUI.text = nowEmployData.salary.ToString()+" G";
        PrintResume();
        // 경력직 고용시에만 경력직으로 할당
        GameManager.Instance.GetComponent<HireManager>().IsNewbie = false;
    }
    void PrintResume()
    {
        DesignText1GUI.text = DesignText1;
        DesignText2GUI.text = DesignText2;
        DevText1GUI.text = DevText1;
        DevText2GUI.text = DevText2;
        ArtText1GUI.text = ArtText1;
        ArtText2GUI.text = ArtText2;
    }

    public void ChooseEmployee()
    {
        GetComponent<Canvas>().enabled = false;
        GameManager.Instance.GetComponent<HireManager>().HireEmployee(nowEmployData);
        GameManager.Instance.MultipleIndex--;
        GameManager.Instance.ChangeTimeMultiple();
        
        // 고용 후 HireManager에서 isNewbie 초기화
        GameManager.Instance.GetComponent<HireManager>().IsNewbie = true;
    }
    public void ExitResumePage()
    {
        GetComponent<Canvas>().enabled = false;
        GameManager.Instance.MultipleIndex--;
        GameManager.Instance.ChangeTimeMultiple();
        // 고용 취소해도 isNewbie 초기화
        GameManager.Instance.GetComponent<HireManager>().IsNewbie = true;
    }
}
