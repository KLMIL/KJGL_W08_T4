using UnityEngine;
using UnityEngine.UI;
public class TickShower : MonoBehaviour
{
    Image tickShower;
    [SerializeField] int tickCycle = 15;
    void Start()
    {
        tickShower.GetComponent<Image>();
    }
    public void UpdateTickShower(float stress)
    {
        tickShower.fillAmount = stress / tickCycle;
    }
}
