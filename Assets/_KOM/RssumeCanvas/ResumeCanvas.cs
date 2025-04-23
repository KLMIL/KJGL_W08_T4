using TMPro;
using UnityEngine;

public class ResumeCanvas : MonoBehaviour
{
    ResumeManager resumeManager;
    PrintResume printResume;

    EmployeeData nowEmployData;

    [SerializeField] GameObject resumePage;

    [SerializeField] TextMeshProUGUI DesignText1GUI;
    [SerializeField] TextMeshProUGUI DesignText2GUI;
    [SerializeField] TextMeshProUGUI DevText1GUI;
    [SerializeField] TextMeshProUGUI DevText2GUI;
    [SerializeField] TextMeshProUGUI ArtText1GUI;
    [SerializeField] TextMeshProUGUI ArtText2GUI;

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

        (DesignText1, DesignText2) = printResume.GetRandomText(SkilType.design, nowEmployData.designSkil);
        (DevText1, DevText2) = printResume.GetRandomText(SkilType.dev, nowEmployData.devSkil);
        (ArtText1, ArtText2) = printResume.GetRandomText(SkilType.art, nowEmployData.artSkil);
        PrintResume();
    }
    public void ExperiencedResumeButton()
    {
        //resumePage.SetActive(true);
        nowEmployData = resumeManager.CreateResume(false);

        (DesignText1, DesignText2) = printResume.GetRandomText(SkilType.design, nowEmployData.designSkil);
        (DevText1, DevText2) = printResume.GetRandomText(SkilType.dev, nowEmployData.devSkil);
        (ArtText1, ArtText2) = printResume.GetRandomText(SkilType.art, nowEmployData.artSkil);
        PrintResume();
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
        //GameObject EmployeePrefab = Instantiate()
        //EmployeePrefab.GetComponent<Employee>().SetEmployeeData(nowEmployData);
        GetComponent<Canvas>().enabled = false;
        GameManager.Instance.GetComponent<HireManager>().HireEmployee(nowEmployData);
    }
    public void ExitResumePage()
    {
        GetComponent<Canvas>().enabled = false;
    }
}
