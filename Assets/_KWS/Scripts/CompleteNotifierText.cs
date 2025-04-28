using UnityEngine;
using TMPro;
using System.Collections;

public class CompleteNotifierText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _notificationText; // TextMeshProUGUI ������Ʈ
    // [SerializeField] private UnityEngine.UI.Text _notificationText; // �Ϲ� Text ��� �� �ּ� ����
    private Coroutine _currentAnimation;

    private void Awake()
    {
        // �ؽ�Ʈ �ʱ� ����
        if (_notificationText == null)
        {
            Debug.LogError("Notification Text is not assigned!");
            return;
        }

        // �ʱ� ����: ����
        SetTextAlpha(0f);
        _notificationText.text = "";
    }

    // �ܺο��� ȣ���� �ۺ� �޼���
    public void ShowNotification(string message)
    {
        // ���� �ִϸ��̼� ����
        if (_currentAnimation != null)
        {
            StopCoroutine(_currentAnimation);
        }

        // �� �ִϸ��̼� ����
        _currentAnimation = StartCoroutine(PlayNotificationAnimation(message));
    }

    private IEnumerator PlayNotificationAnimation(string message)
    {
        // �ؽ�Ʈ ����
        _notificationText.text = message;

        // �ʱ� ��ġ: ĵ���� �Ʒ��� (�� 100 ���� �Ʒ�)
        RectTransform rect = _notificationText.rectTransform;
        Vector2 startPos = new Vector2(rect.anchoredPosition.x, -500f);
        Vector2 targetPos = new Vector2(rect.anchoredPosition.x, -400f); // �߾�

        // �ʱ� ����
        SetTextAlpha(0f);
        rect.anchoredPosition = startPos;

        // 1. �ö���� ������ (0.5��)
        float fadeInDuration = 0.5f;
        float elapsed = 0f;
        while (elapsed < fadeInDuration)
        {
            //elapsed += Time.deltaTime;
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / fadeInDuration;
            // ��ġ ����
            rect.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            // ���� ����
            SetTextAlpha(t);
            yield return null;
        }
        rect.anchoredPosition = targetPos;
        SetTextAlpha(1f);

        // 2. ���� (0.3��)
        //yield return new WaitForSeconds(0.3f);
        yield return new WaitForSecondsRealtime(1.5f);

        // 3. �������� ����� (0.5��)
        float fadeOutDuration = 0.5f;
        elapsed = 0f;
        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeOutDuration;
            // ���� ����
            SetTextAlpha(1f - t);
            yield return null;
        }
        SetTextAlpha(0f);

        // �ؽ�Ʈ ����
        _notificationText.text = "";
        _currentAnimation = null;
    }

    private void SetTextAlpha(float alpha)
    {
        Color color = _notificationText.color;
        color.a = alpha;
        _notificationText.color = color;
    }
}