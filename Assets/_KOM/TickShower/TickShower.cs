using UnityEngine;
using UnityEngine.UI;
public class TickShower : MonoBehaviour
{
    Image tickShower;
    [SerializeField] int tickCycle = 15;
    void Start()
    {
        tickShower = GetComponent<Image>();

    }
    public void UpdateTickShower(float stress)
    {
        tickShower.fillAmount = stress / tickCycle;
    }

    public float fadeSpeed = 0.8f; // 알파값이 변하는 속도

    private bool fadingOut = true;

    private void Update()
    {
        Color color = tickShower.color;
        float alphaChange = fadeSpeed * Time.deltaTime;

        if (fadingOut)
        {
            color.a -= alphaChange;
            if (color.a <= 0.2f)
            {
                color.a = 0f;
                fadingOut = false;
            }
        }
        else
        {
            color.a += alphaChange;
            if (color.a >= 1f)
            {
                color.a = 1f;
                fadingOut = true;
            }
        }

        tickShower.color = color;
    }
}
