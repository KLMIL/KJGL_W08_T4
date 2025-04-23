using UnityEngine;
using UnityEngine.UI;

public class UI_Stressbar : MonoBehaviour
{
    [SerializeField] Image stressBar;

    float maxStressBar_Gauge = 100;
    [SerializeField] float currentStressBar_Gauge = 0;

    public void UpdateHP(float stress)
    {
        currentStressBar_Gauge = stress;
        UpdateStressBar();
    }
    void UpdateStressBar()
    {
        stressBar.fillAmount = (float)currentStressBar_Gauge / maxStressBar_Gauge;
    }
}