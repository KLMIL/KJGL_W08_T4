using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PrintResume : MonoBehaviour
{
    List<string>[] resumeArtText;
    List<string>[] resumeDevText;
    List<string>[] resumeDesignText;

    public (string text1, string text2) GetRandomText(SkilType skilType, int abilityStatus)
    {
        string text1 = string.Empty;
        string text2 = string.Empty;

        switch (skilType)
        {
            case SkilType.design:
                if (abilityStatus == 1)
                {
                    text1 = resumeDesignText[1][Random.Range(0, resumeDesignText[1].Count)];
                    text2 = resumeDesignText[0][Random.Range(0, resumeDesignText[0].Count)];
                }
                if (abilityStatus == 2)
                {
                    text1 = resumeDesignText[2][Random.Range(0, resumeDesignText[2].Count)];
                    text2 = resumeDesignText[0][Random.Range(0, resumeDesignText[0].Count)];
                }
                if (abilityStatus == 3)
                {
                    text1 = resumeDesignText[1][Random.Range(0, resumeDesignText[1].Count)];
                    text2 = resumeDesignText[2][Random.Range(0, resumeDesignText[2].Count)];
                }
                if (abilityStatus == 4)
                {
                    text1 = resumeDesignText[2][Random.Range(0, resumeDesignText[2].Count)];
                    text2 = resumeDesignText[2][Random.Range(0, resumeDesignText[2].Count)];
                }
                break;

            case SkilType.dev:
                if (abilityStatus == 1)
                {
                    text1 = resumeDevText[1][Random.Range(0, resumeDevText[1].Count)];
                    text2 = resumeDevText[0][Random.Range(0, resumeDevText[0].Count)];
                }
                if (abilityStatus == 2)
                {
                    text1 = resumeDevText[2][Random.Range(0, resumeDevText[2].Count)];
                    text2 = resumeDevText[0][Random.Range(0, resumeDevText[0].Count)];
                }
                if (abilityStatus == 3)
                {
                    text1 = resumeDevText[1][Random.Range(0, resumeDevText[1].Count)];
                    text2 = resumeDevText[2][Random.Range(0, resumeDevText[2].Count)];
                }
                if (abilityStatus == 4)
                {
                    text1 = resumeDevText[2][Random.Range(0, resumeDevText[2].Count)];
                    text2 = resumeDevText[2][Random.Range(0, resumeDevText[2].Count)];
                }
                break;

            case SkilType.art:
                if (abilityStatus == 1)
                {
                    text1 = resumeArtText[1][Random.Range(0, resumeArtText[1].Count)];
                    text2 = resumeArtText[0][Random.Range(0, resumeArtText[0].Count)];
                }
                if (abilityStatus == 2)
                {
                    text1 = resumeArtText[2][Random.Range(0, resumeArtText[2].Count)];
                    text2 = resumeArtText[0][Random.Range(0, resumeArtText[0].Count)];
                }
                if (abilityStatus == 3)
                {
                    text1 = resumeArtText[1][Random.Range(0, resumeArtText[1].Count)];
                    text2 = resumeArtText[2][Random.Range(0, resumeArtText[2].Count)];
                }
                if (abilityStatus == 4)
                {
                    text1 = resumeArtText[2][Random.Range(0, resumeArtText[2].Count)];
                    text2 = resumeArtText[2][Random.Range(0, resumeArtText[2].Count)];
                }
                break;
        }

        return (text1, text2);
    }

    void Start()
    {
        // Design
        resumeDesignText = new List<string>[3];
        resumeDesignText[2] = new List<string>
        {
            "기획 포트폴리오 3종 완성",
            "상업용 보드게임 룰 시스템 개발",
            "2024 청년 게임기획 대회 입상",
            "한국콘텐츠진흥원 공모전 기획 부분 수상",
            "Independent Game Festival 학생 부분 입상",
            "Steam 게임 출시 후 압도적으로 긍정적",
            "지리니1 직업 12종 밸런스 기획"
        };
        resumeDesignText[1] = new List<string>
        {
            "마포구 게임공모전 기획서 제안",
            "UI/UX 와이어프레임 초안 설계 경험",
            "인디게임 기획서 30장 작성",
            "게임 리뷰 블로그 운영",
            "Unity 게임기획 퍼블리싱 세미나 발표",
            "EA 코리아 디자이너 3개월",
            "하얀사막 역기획서 작성"
        };
        resumeDesignText[0] = new List<string>
        {
            "메이플스토리 400시간 경험",
            "마비노기 길드 마스터 1년 경험",
            "스티듀밸리 2회차 클리어",
            "슬픈게임 하면서 눈물 흘림",
            "리그오브레전드 다이아 달성",
            "치지직 종합게임 스트리머 시청 2000시간",
            "매년 GOTY 발표 챙겨봄",
            "어릴때부터 상상력 좋다는 소리 자주 들음",
            "스토리 게임 좋아함",
            "세븐나이츠 카페 공략글 조회수 300만"
        };

        // Dev
        resumeDevText = new List<string>[3];
        resumeDevText[2] = new List<string>
        {
            "Git 협업 경험 다수",
            "2024 부산광역시배 게임잼 대회 수상",
            "Unity 이용 대규모 멀티플레이어 게임 개발",
            "Unreal Engine 이용 VR게임 개발",
            "Google Korea 6개월 인턴",
            "C++ 소켓 프로젝트 포트폴리오 2종",
            "교내 소프트웨어 창업 대회 수상"
        };
        resumeDevText[1] = new List<string>
        {
            "피직스 기반 충돌 시스템 구현",
            "유튜브 C++ 튜토리얼 구독자 40만",
            "A* 알고리즘을 적용한 AI 구현 경험",
            "구글플레이에 APK 빌드 및 배포 경험",
            "Unity Learn Beginner 코스 수료",
            "정보처리기사 취득",
            "2024 아주대학교 프로그래밍 경시대회 3위"
        };
        resumeDevText[0] = new List<string>
        {
            "GPT를 이용해 저녁메뉴 고를 수 있음",
            "학교에서 Grok을 사용하여 과제 A달성",
            "대구광역시에서 사과잼 구매",
            "코딩 유튜브 자주 봄",
            "웹툰 “개발자 전성시대” 애독자",
            "똥 피하기 개발하여 엄마한테 자랑",
            "파이썬 터틀로 눈사람 그려봄",
            "C언어 별그리기로 트리 만들어서 크리스마스에 사용",
            "데스크탑 컴퓨터 혼자서 조립해봄",
            "컴퓨터에 Unity 버젼 39f1, 40f1 둘다 설치 완료",
            "한글을 사랑해서 하이어라키에 한글 사용"
        };

        // Art
        resumeArtText = new List<string>[3];
        resumeArtText[2] = new List<string>
        {
            "국립현대미술관 개인전 전시",
            "서울시 캐릭터 공모전 수상",
            "데브독 스튜디오 CG 파트 인턴",
            "서울숲 야외전시물 설치",
            "캐릭터 디자인 드로잉북 2권 작성",
            "Fab 아트 에셋 3종 출시",
            "경기도지사배 광복절 기념 애니메이션 공모전 수상"
        };
        resumeArtText[1] = new List<string>
        {
            "픽셀 아트 캐릭터 50종 제작",
            "폰트 커스터마이징 경험",
            "애니메이션 리깅 기본 세팅 경험",
            "픽시브 구독자 20,000명",
            "졸업 작품 전시회 작품 1점 출품",
            "52사단 환경미화 공모전 수상",
            "GTQ 자격증 1급 보유"
        };
        resumeArtText[0] = new List<string>
        {
            "메트로폴리탄 미술관 관람",
            "안동 한국 전통 공예품 전시회 관람",
            "내컴퓨터에 포토샵 설치",
            "AI를 이용해 미소녀 사진 생성 가능",
            "인스타 그림 계정 팔로우 20명",
            "아이패드에 프로크리에이트 설치",
            "웹툰 쿠키 한달에 200개 결제",
            "유년기에 그린 그림 집 복도에 전시",
            "GPT 지브리 그림체 만들기 사용 경험",
            "옷장에 계절별 옷 30벌씩 보유",
            "클립스튜디오 정품 사용중"
        };
    }
}
