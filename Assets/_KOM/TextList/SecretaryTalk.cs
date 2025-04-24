using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class SecretaryTalk : MonoBehaviour
{
    List<string> secretaryTalk;

    [SerializeField] TextMeshProUGUI secretaryTalkGUI;

    void Start()
    {
        InstantiateList();
    }

    //string RandomSaying() { return secretaryTalk[Random.Range(0, secretaryTalk.Count)]; }// 랜덤 대화 반환

    public void RandomSaying()
    {
        secretaryTalkGUI.text = secretaryTalk[Random.Range(0, secretaryTalk.Count)];
    }
    void InstantiateList()
    {
        secretaryTalk = new List<string>
        {
        "직원을 모집해서 프로젝트를 진행해주세요.",
        "더 큰 사무실로 이전하면 더 능력이 좋은 지원자들이 올거에요.",
        "큰 프로젝트를 성공하면 더 많은 돈을 벌 수 있어요.",
        "혼자서 하는 것 보다는 여럿이서 하는게 더 많은걸 할 수 있겠죠?",
        "이력서에 뻔한 거짓말을 쓰는 사람이 많네요.",
        "능력이 뛰어난데도 희망 연봉을 낮게 부르는 사람도 있어요.",
        "능력이 모자라도 일은 할 수 있어요!",
        "능력이 모자라면 품질도 떨어지겠죠?",
        "게임의 품질이 떨어지면 버는 돈도 같이 떨어진답니다!",
        "이번 달 월급은 얼마나 나갈까요.",
        "신입 사원은 제 역할을 할 때까지 시간이 걸리지만, 월급도 적게 받아요.",
        "이력서의 모든 경력이 다 의미 있는 건 아니에요!",
        "과중한 업무를 부여하면 손쉽게 직원을 해고할 수 있어요.",
        "너무 많은 업무를 맡기면 직원이 퇴사할 수도 있어요!"
        };
    }
}
