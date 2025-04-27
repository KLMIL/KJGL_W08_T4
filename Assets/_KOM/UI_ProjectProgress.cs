using UnityEngine;
using UnityEngine.UI;

public class UI_ProjectProgress : MonoBehaviour
{
    [SerializeField] Image[] progressbar = new Image[8];

    public void UpdateBar(int num, float progress)
    {
        progressbar[num].fillAmount = progress;
    }
}