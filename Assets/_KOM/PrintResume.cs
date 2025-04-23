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
            "지리니1 직업 12종 밸런스 기획",
            // AI
            "게임 시나리오 공모전 금상 수상",
            "100페이지 분량의 GDD 제작 경험",
            "LiveOps 기획 및 유저 이탈 방지 전략 설계",
            "하드코어 RPG 튜토리얼 리디자인 프로젝트 참여",
            "밸런스 전담 기획으로 4인 파티 게임 완성",
            "자체 개발 보드게임 상업 유통 경험",
            "플레이 패턴 분석 기반 던전 설계 경험",
            "QA 피드백 반영을 위한 리밸런싱 기획서 작성",
            "시나리오 분기 12개 이상 구성된 VN 기획 진행",
            "국내 유명 기획 유튜브 채널 강의 출연",
            "신작 게임 CBT에서 UX 개선안 수립 및 반영",
            "카드게임 스킬 상호작용 구조도 설계",
            "멀티엔딩 스토리 구조로 IGF 수상",
            "밸런싱 전용 시뮬레이터 제작 기획 경험",
            "Live 유저 지표 기반 콘텐츠 개선 기획안 채택",
            "MMORPG 직업군 10종 이상 밸런스 작업 경험",
            "매출 5억 이상 기록한 캐주얼 게임 메인 기획",
            "라이브 게임 KPI 분석 보고서 작성 경험",
            "F2P 게임 내 재화 흐름 설계 경험",
            "튜토리얼 클릭률 향상 기획으로 유지율 18% 증가"
        };
        resumeDesignText[1] = new List<string>
        {
            "마포구 게임공모전 기획서 제안",
            "UI/UX 와이어프레임 초안 설계 경험",
            "인디게임 기획서 30장 작성",
            "게임 리뷰 블로그 운영",
            "Unity 게임기획 퍼블리싱 세미나 발표",
            "EA 코리아 디자이너 3개월",
            "하얀사막 역기획서 작성",
            // AI
            "게임 기획서 작성법 유튜브 정주행 완료",
            "인터넷 밈 기반 캐릭터 콘셉트 기획 경험",
            "게임 유저 커뮤니티에서 밸런스 토론 100회 이상 참여",
            "기획 컨셉 기획서 A4 10장 작성해 본 경험 있음",
            "TRPG 룰 커스터마이징 경험",
            "보드게임 규칙 개선 제안으로 오프라인 모임 채택",
            "게이미피케이션 논문 요약 블로그 운영",
            "UI 분석 리포트 10건 작성",
            "UX 관련 세미나 5회 참석",
            "카카오 오븐으로 와이어프레임 3종 작성",
            "시나리오 플롯 요약 50편 작성 경험",
            "2D 횡스크롤 게임 기획서 개인 블로그 공개",
            "게임 기획 관련 PDF 자료 정리 30개",
            "게임 개발 도서관에서 GDD 수집 경험 다수",
            "서브컬쳐 기반 캐릭터 시트 제작 경험",
            "게임 시놉시스 10편 완성",
            "타 게임 벤치마킹 기획서 5건 보유",
            "인디 기획 커뮤니티에서 월간 공모 3회 참여",
            "보드게임 리뷰 유튜브 출연 및 룰 설명 진행",
            "디자인씽킹 워크숍 게임 과제 우수 제출"
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
            "세븐나이츠 카페 공략글 조회수 300만",
            // AI
            "만약 내가 게임이었다면 스토리 위주 장르일 것",
            "수업시간에 공상만 하다 기획자로 불림",
            "일기장에 스토리 쓴 것만 1권 분량",
            "게임 이름 잘 짓는다고 친구들이 칭찬함",
            "만약 내가 몬스터였다면 밸런스 깨졌을 것",
            "GDD? 그거 GPT한테 시키면 되죠",
            "기획이요? 저 맨날 친구들한테 말로 떠들어요",
            "게임 스토리보다 제 인생이 더 드라마틱함",
            "게임하다 분노해서 나만의 규칙 만들었어요",
            "기획보단 기획자 친구가 더 많음",
            "스토리 게임 좋아함 (실제 기획은 안 함)",
            "상상력 좋다는 말 들은 지 오래됨",
            "게임 영상 보면서 시나리오 상상 잘함",
            "기획 노트? 다 머릿속에 있어요",
            "출근길 지하철에서 게임 아이디어 떠올림",
            "라면 끓이다가 밸런스 기획 떠올림",
            "게임 시작 전에 설정집 다 읽는 타입",
            "내 인생이 시뮬레이션이라 생각함",
            "마인크래프트 건축하면서 스토리 만들었음",
            "어릴 때 포켓몬 설정 정리 노트 만들었음",
            "GDD는 몰라도 GDRAGON은 앎"
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
            "교내 소프트웨어 창업 대회 수상",
            // AI
            "Unity 기반 모바일 게임 3종 출시",
            "멀티스레드 환경에서 네트워크 서버 구현 경험",
            "Gitlab + CI/CD 파이프라인 구축 경험",
            "Unreal Blueprint와 C++ 혼합 사용 프로젝트 수행",
            "Steam 멀티플레이어 게임 출시 및 유지보수 경험",
            "Photon 기반 실시간 멀티게임 완성",
            "모바일 광고 매니징 SDK 직접 개발",
            "DOTS 기반 대규모 군집 시뮬레이션 구현",
            "SQLite 연동 데이터 저장 시스템 설계",
            "카카오게임즈 SDK 통합 경험",
            "AssetBundle 자동화 파이프라인 스크립트 제작",
            "리플레이 기능 구현 경험 (데이터 기반)",
            "AOS형 전투 시스템 클라이언트 완성",
            "NavMesh + Custom Pathfinding 혼합 구현 경험",
            "Jenkins 자동 배포 파이프라인 구축",
            "Unity ECS 기반 Entity 관리 시스템 제작",
            "VR 기기용 피지컬 인터랙션 구현",
            "PlayFab 연동 클라우드 기능 개발",
            "AWS Lambda와 Unity의 연동 경험 보유",
            "Steam SDK 연동 업적 및 클라우드 저장 개발"
        };
        resumeDevText[1] = new List<string>
        {
            "피직스 기반 충돌 시스템 구현",
            "유튜브 C++ 튜토리얼 구독자 40만",
            "A* 알고리즘을 적용한 AI 구현 경험",
            "구글플레이에 APK 빌드 및 배포 경험",
            "Unity Learn Beginner 코스 수료",
            "정보처리기사 취득",
            "2024 아주대학교 프로그래밍 경시대회 3위",
            //AI
            "GitHub 커밋 1000회 달성",
            "Unity에서 캐릭터 이동 구현해봄",
            "리듬게임 타이밍 판정 로직 만든 경험 있음",
            "스마트폰 진동 기능 연동 구현 경험",
            "Unity Addressable 학습 완료",
            "VR 기초 예제 따라해봄",
            "Shader Graph로 하트 이펙트 구현",
            "2024 전국 대학생 알고리즘 경시대회 참가",
            "C#과 Python 혼용해서 툴 제작해봄",
            "노코드 플랫폼으로 게임 프로토타입 완성",
            "간단한 RPG 템 인벤토리 UI 구현 완료",
            "3D 모델 로딩 및 커스터마이징 스크립트 구현",
            "인앱 결제 연동 실습 경험",
            "커뮤니티용 Discord 봇 개발 경험",
            "물리엔진 활용하여 충돌 판정 구현 연습",
            "기획자의 요청으로 UI 개편 대응 경험 있음",
            "디버깅 경험 다수 (NullReferenceException 숙련자)",
            "Unity 공식 튜토리얼 완주",
            "간단한 아케이드 게임 학원 팀 프로젝트 참여",
            "코루틴과 Invoke 차이를 블로그에 정리함"
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
            "한글을 사랑해서 하이어라키에 한글 사용",
            //AI
            "코딩할 때 키보드 소리 들으면 뭔가 잘 짜는 느낌남",
            "윈도우 업데이트 끄는 방법 잘 앎",
            "내 컴퓨터에 폴더 구조 예쁘게 정리되어 있음",
            "타자 속도 네이버 테스트 623타",
            "개발자 느낌 나게 다크 테마 좋아함",
            "StackOverflow에서 답변 읽는 걸 좋아함",
            "코딩할 때 노래는 무조건 lofi",
            "CPU 온도 3도 낮추는 법 알음",
            "코드 몰라도 주석 보면 이해함",
            "지인 부탁으로 블로그에 게임 추천 글 작성",
            "프로그래밍 관련 밈 모으는 취미 있음",
            "컴퓨터 덕후라 게임 개발도 할 줄 안다고 생각함",
            "비주얼 스튜디오 테마에 집착하는 타입",
            "코딩 에러 나면 일단 재부팅함",
            "C# 하면 철자부터 C인지 Sharp인지 고민함",
            "회사에서 ‘개발자’ 친구 있음",
            "자바스크립트랑 자바가 같은 줄 알았음",
            "매직마우스로 Unity 조작 가능",
            "내가 만든 게임: 공 튕기기 (단색 배경)",
            "한글 변수명이 가독성 좋다고 믿음"
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
            "경기도지사배 광복절 기념 애니메이션 공모전 수상",
            //AI
            "캐릭터 일러스트로 웹툰 연재 50화 이상 진행",
            "픽셀 아트 게임 출시 후 스팀 리뷰 매우 긍정적",
            "국제 캐릭터 디자인 콘테스트 수상",
            "Unity Shader 기반 특수효과 직접 제작",
            "ZBrush 기반 하이폴리 모델링 전시",
            "3D 리깅 + 애니메이션 포트폴리오 유튜브 공개",
            "모션그래픽을 이용한 UI 애니메이션 작업 경험",
            "외주 프로젝트 10건 이상 클라이언트 납품 완료",
            "상업용 애니메이션 팀 배경 파트 참여",
            "에셋스토어에 3D 배경 세트 출시 경험 있음",
            "애니메이션 컷씬 스토리보드 제작 경험",
            "페인터리 텍스처링으로 리얼타임 캐릭터 완성",
            "3D 마켓플레이스에서 다운로드 수 1만 이상 달성",
            "사운드 연동 UI 애니메이션 개발 참여",
            "UI 키트 제작 및 디자인 시스템 가이드 정리 경험",
            "VR 인터페이스용 UI 구성 및 시각 디자인 작업",
            "AR용 콘텐츠 제작 전시 참가",
            "픽셀 아트 애니메이션 12프레임 루프 완성",
            "Unity Timeline으로 컷씬 제작 경험",
            "Vroid 기반 VTuber 모델 커스터마이징 작업 진행"
        };
        resumeArtText[1] = new List<string>
        {
            "픽셀 아트 캐릭터 50종 제작",
            "폰트 커스터마이징 경험",
            "애니메이션 리깅 기본 세팅 경험",
            "픽시브 구독자 20,000명",
            "졸업 작품 전시회 작품 1점 출품",
            "52사단 환경미화 공모전 수상",
            "GTQ 자격증 1급 보유",
            //AI
            "아이패드로 프로크리에이트 일러스트 연습 중",
            "픽시브 구독자 1,000명 돌파",
            "트위터에 일러스트 업로드 100회 달성",
            "Vroid Studio로 캐릭터 제작 경험",
            "클립스튜디오에서 간단한 컷씬 연출 연습",
            "스케치업으로 씬 구성 연습 중",
            "캐릭터 팔레트 분석 블로그 운영",
            "애니메이션 프레임 분해 연습 경험",
            "폰트 자작 프로젝트 진행 중",
            "웹툰 시놉시스 공모전 응모 경험",
            "GTQ 1급 자격증 보유",
            "드로잉 수업 수료증 보유",
            "픽셀 아트로 RPG 캐릭터 30종 제작",
            "UI 샘플 리디자인 개인 작업 공개",
            "2D 애니메이션 툴(Spine) 연습 프로젝트 진행",
            "유튜브 튜토리얼로 Shader 따라 그려봄",
            "만화 콘티 5화 분량 제작 후 블로그 연재",
            "SNS 기반 도트 커미션 10건 수주",
            "AI 그림 보정 작업 직접 수행",
            "피그마로 모바일 UI 리디자인 수행"
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
            "클립스튜디오 정품 사용중",
            //AI
            "캔버스 보면 뭔가 마음이 편해짐",
            "어릴 때 색연필 먹어본 적 있음",
            "AI로 미소녀 생성한 후 저장만 해둠",
            "옷 잘 입는 친구 있음",
            "유튜브 썸네일 컬러 조합 보는 거 좋아함",
            "모든 그림에는 다 이유가 있다고 생각함",
            "크레파스 냄새 맡으면 기분이 좋아짐",
            "미술관 가면 전시보다 굿즈 먼저 봄",
            "그림 그리는 친구에게 감탄 자주함",
            "눈에 보이는 걸 그림으로 그릴 줄 안다고 착각함",
            "아이패드에 클립스튜디오 설치만 해둠",
            "무드보드 만들다가 하루 다 감",
            "RGB 조합보다 CMYK가 더 멋져 보임",
            "다꾸(다이어리 꾸미기)에 진심임",
            "어릴 적 '색칠왕' 대회 참가 경험 있음",
            "미술 시간에 칭찬 들은 적 있음",
            "손그림보다 컬러링북이 더 재밌었음",
            "배경 그릴 때는 항상 구글 이미지 참고",
            "디자인은 직감으로 하는 거라 생각함",
            "아무 그림이나 보면 폰 배경화면 하고 싶어짐"
        };
    }
}
