using UnityEngine;
using TMPro;
using System.Collections;

public class CompleteNotifierText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _notificationText; // TextMeshProUGUI 컴포넌트
    // [SerializeField] private UnityEngine.UI.Text _notificationText; // 일반 Text 사용 시 주석 해제
    private Coroutine _currentAnimation;

    private void Awake()
    {
        // 텍스트 초기 설정
        if (_notificationText == null)
        {
            Debug.LogError("Notification Text is not assigned!");
            return;
        }

        // 초기 상태: 투명
        SetTextAlpha(0f);
        _notificationText.text = "";
    }

    // 외부에서 호출할 퍼블릭 메서드
    public void ShowNotification(string message)
    {
        // 기존 애니메이션 중지
        if (_currentAnimation != null)
        {
            StopCoroutine(_currentAnimation);
        }

        // 새 애니메이션 시작
        _currentAnimation = StartCoroutine(PlayNotificationAnimation(message));
    }

    private IEnumerator PlayNotificationAnimation(string message)
    {
        // 텍스트 설정
        _notificationText.text = message;

        // 초기 위치: 캔버스 아래쪽 (약 100 유닛 아래)
        RectTransform rect = _notificationText.rectTransform;
        Vector2 startPos = new Vector2(rect.anchoredPosition.x, -500f);
        Vector2 targetPos = new Vector2(rect.anchoredPosition.x, -400f); // 중앙

        // 초기 상태
        SetTextAlpha(0f);
        rect.anchoredPosition = startPos;

        // 1. 올라오며 진해짐 (0.5초)
        float fadeInDuration = 0.5f;
        float elapsed = 0f;
        while (elapsed < fadeInDuration)
        {
            //elapsed += Time.deltaTime;
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / fadeInDuration;
            // 위치 보간
            rect.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            // 알파 보간
            SetTextAlpha(t);
            yield return null;
        }
        rect.anchoredPosition = targetPos;
        SetTextAlpha(1f);

        // 2. 유지 (0.3초)
        //yield return new WaitForSeconds(0.3f);
        yield return new WaitForSecondsRealtime(1.5f);

        // 3. 연해지며 사라짐 (0.5초)
        float fadeOutDuration = 0.5f;
        elapsed = 0f;
        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeOutDuration;
            // 알파 보간
            SetTextAlpha(1f - t);
            yield return null;
        }
        SetTextAlpha(0f);

        // 텍스트 비우기
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