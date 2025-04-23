using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public int todayMonth;
    public CompanyScale myCompanyScale;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }
}
