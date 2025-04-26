using System.Collections.Generic;
using UnityEngine;

public static class ProjectDataGenerator
{
    private static List<string> _allNames = new()
    {
        "서연고림", "보링", "페타", "미디엄소드", "와플스토리", "탑블레이드&베이비소울",
        "DODGE: 배틀블라인드", "레귤러 스크라이크", "딜레이어택", "미스파이어", "레귤러포스",
        "령관크래프트", "워스매시", "마운트앤탑블레이드", "에이지 오브 킹덤", "염곰IV",
        "메인노티카: 어보브 원", "던전앤웨이터", "로스트엄크", "다크보울", "전설의레전드세븐",
        "마리아 시스터즈", "셀레스커피", "할로우데이", "테일즈워커", "모노로이드",

        // 마구 만든 이름
        "다크소울즈", "레전드오브베이비", "초월전쟁", "환상검객", "딥스토리",
        "미스헌터즈", "디멘션런너", "슈퍼어택", "뉴소울", "로얄블레이드",
        "하이퍼소드", "크레이지매니저", "킹덤파이터", "갓오브던전", "언더소울",
        "데몬슬레이어즈", "에픽파이터", "아스트로워커", "블레이즈소드", "리버스스토리",
        "사이버크래프트", "에어워스매시", "스텔라워커", "네크로포스", "미스틱소울즈",
        "트윈블레이드", "디지털소드", "썬더매니저", "글로리헌터", "카오스런너",
        "트랜스스토리", "하모닉파이터", "라이트오브킹", "프리즈매스터", "쉐도우어택",
        "스피릿소울", "메가블레이드", "어비스소울", "로스트소드", "어웨이크런너",
        "블러드헌터", "스톰파이터", "이터널스토리", "네오워커", "프로토크래프트",
        "스펙트럼소울", "크로닉소드", "섀도우크래프트", "리버티스토리", "엘리트런너",
        "퓨리블레이드", "썬라이트소울", "어센션크래프트", "인페르노워커", "브라이트헌터",
        "나이트스토리", "소닉크래프트", "디스트로이어즈", "디바인소울", "템페스트런너",
        "테라소드", "크림소울", "익스트림어택", "버서커매니저", "로얄소드",
        "사일런트워커", "폴라리스소드", "그림소울", "네메시스런너", "하이퍼매니저",
        "엘리시온스토리", "루미너스파이터", "다이나믹블레이드", "마그넷어택", "블리츠소울",
        "가디언크래프트", "프레셔스매스터", "그레이브런너", "디바소울", "언브레이커블",
        "에버그레이스", "터보소울", "제로매스터", "헌터즈웨이", "포가튼스토리",
        "사이버블레이드", "스펠크래프트", "레디언트소울", "라이트매니저", "러쉬어택",
        "디지털헌터", "인피니트크래프트", "노바스토리", "에메랄드소울", "딥크래프트",
        "클리어런너", "디코이소울", "소울러너", "타이탄매니저", "그레이드블레이드",
        "팬텀스토리", "매직헌터", "미스소울", "블레이드원더", "울티마런너",
        "이블포스", "헌터즈코어", "엑소소울", "매그넘스토리"
    };


    public static string GetRandomProjectName()
    {
        if (_allNames.Count == 0)
        {
            Debug.LogWarning("⚠️ 사용할 수 있는 프로젝트 이름이 없습니다.");
            return "이상한 프로젝트";
        }

        int index = Random.Range(0, _allNames.Count);
        string name = _allNames[index];
        _allNames.RemoveAt(index); // ✅ 중복 방지
        return name;
    }

    public static int GetRequiredSkill(ProjectSize size)
    {
        return size switch
        {
            ProjectSize.Small => Random.Range(3, 6),
            ProjectSize.Medium => Random.Range(7, 12),
            ProjectSize.Large => Random.Range(10, 15),
            _ => 1
        };
    }

    public static int GetRequiredWorkAmount(ProjectSize size)
    {
        return size switch
        {
            ProjectSize.Small => Random.Range(14, 23),
            ProjectSize.Medium => Random.Range(86, 131),
            ProjectSize.Large => Random.Range(346, 519),
            _ => 10
        };
    }

    public static int GetRewardEstimate(ProjectSize size)
    {
        return size switch
        {
            ProjectSize.Small => Random.Range(1000, 2001),
            ProjectSize.Medium => Random.Range(4000, 10001),
            ProjectSize.Large => Random.Range(15000, 30001),
            _ => 500
        };
    }
}