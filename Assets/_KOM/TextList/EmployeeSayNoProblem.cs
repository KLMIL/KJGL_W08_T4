using System.Collections.Generic;
using UnityEngine;

public class EmployeeSaying : MonoBehaviour
{
    List<string> employeeSayNoProblem;
    List<string> employeeSayLackDesign;
    List<string> employeeSayLackDev;
    List<string> employeeSayLackArt;
    List<string> employeeSayExcessiveStress;

    void Start()
    {
        InstantiateList();
    }

    public string RandomSayingNoProblem() { return employeeSayNoProblem[Random.Range(0, employeeSayNoProblem.Count)]; }
    public string RandomSayingLackDesign() { return employeeSayLackDesign[Random.Range(0, employeeSayLackDesign.Count)]; }
    public string RandomSayingLackDev() { return employeeSayLackDev[Random.Range(0, employeeSayLackDev.Count)]; }
    public string RandomSayingLackArt() { return employeeSayLackArt[Random.Range(0, employeeSayLackArt.Count)]; }
    public string RandomSayingExcessiveStress() { return employeeSayExcessiveStress[Random.Range(0, employeeSayExcessiveStress.Count)]; }

    void InstantiateList()
    {
        employeeSayNoProblem = new List<string>
        {
            "문제 없이 진행되는 것 같아.",
            "오늘은 가족과 저녁을 먹을 수 있겠어.",
            "퇴근하고 인조이 해야지.",
            "일이 술술 풀리네…",
            "회사에 오는게 즐거운걸!",
            "코드가 정말 깔끔하게 나왔네. 한 번에 끝날 것 같아.",
            "이제 막바지니까 더 집중해야겠다. 꼼꼼한 마무리!",
            "오늘은 버그도 없고, 작업이 너무 잘 된다.",
            "디자인이 생각보다 훨씬 더 잘 나왔어."
        };
        employeeSayLackDesign = new List<string>
        {
            "추가 기획이 필요할 것 같은데…",
            "이 정도 기획으로 프로젝트가 제대로 진행될까?",
            "고민할 시간이 부족해.",
            "다른 아이디어가 더 필요해.",
            "기획안이 부족해서 다들 헷갈려하고 있어.",
            "누가 기획을 이따위로 짠거야?",
            "밸런스가 엉망이네…",
            "이 기획이 과연 먹힐까?"
        };

        employeeSayLackDev = new List<string>
        {
            "안돼! 오늘 작업한 브랜치가 날라갔어!",
            "어…충돌이 났네…그냥 합치면 되겠지?",
            "public으로 접근하려니까 오류가 생겨. 왜 이게 안 되는 거지?",
            "Serialize에 할당한 오브젝트가 전부 해제됐어. 이건 복구하기 어렵겠는데…",
            "이 코드가 제대로 작동할 리가 없잖아. 분명히 실수한 부분이 있을 거야..",
            "왜 계속 빌드가 실패하는거야?",
            "GPT가 준대로 했는데 버그가 있네…GPT야 이건 어떻게 고치지?"
        };

        employeeSayLackArt = new List<string>
        {
            "왜 항상 인코딩만 하면 된다고 말하면서 작업이 안 끝나는거야?",
            "그놈의 에프터 이팩트는 맨날 저장이 안 됐대.",
            "실수로 레이어를 합치고 저장해버렸다! 처음부터 다시 해야해…",
            "렌더링이 너무 오래 걸려. 오늘 안에 끝내려면 뭘 포기해야 할지 모르겠다.",
            "지금 작업하는 걸 끝내려면 아침까지 해야 할 것 같아.",
            "렌더링에서 색깔이 다르게 나오네 …",
            "이게 왜 깨져? 디자인 다시 해야겠다."
        };
        employeeSayExcessiveStress = new List<string>
        {
            "점심 먹을 시간도 없네 …",
            "커피 몇 잔을 마셔야 버틸 수 있을까?",
            "박카스 한병 사올까?",
            "완성되면 성과급 주겠지?",
            "회식은 언제 하는거지?",
            "너무 힘들다 … 일주일만 쉬고 싶어.",
        };
    }
}

