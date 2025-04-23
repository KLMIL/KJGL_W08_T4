using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private TextMeshProUGUI  fundsText;

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
        var obj = GameObject.Find("Funds");
        if (obj != null)
        {
            fundsText = obj.GetComponent<TextMeshProUGUI >();
        }
        else
        {
            Debug.LogWarning("'Funds' 오브젝트를 찾을 수 없습니다.");   
        }
    }

    public void UpdateFundsUI(int funds)
    {
        if (fundsText != null)
        {
            fundsText.text = $"Funds: {funds:N0} G";
        }
    }
}