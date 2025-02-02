[33mcommit 4515f1969a98e3955bd8620e46d606d263646ed4[m[33m ([m[1;36mHEAD[m[33m -> [m[1;32mPlates기능구현[m[33m, [m[1;31morigin/Plates기능구현[m[33m)[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Wed Jan 29 17:22:57 2025 +0900

    맵툴에서 저장한 Plate/Node 불러오기 구현 완료
    노드의 콜라이더 빼버림

[33mcommit f873e1a923fb546927998659716597c84785ca90[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Tue Jan 28 17:59:26 2025 +0900

    이전 커밋에서 누락된 파일들 추가

[33mcommit 95ee386fd3b5cb041f99fb1bc8cb0774df015dcb[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Tue Jan 28 17:58:37 2025 +0900

    툴에서 제작한 맵 게임scene에서 로드하도록 구현 완료

[33mcommit 3a1dc6cf66c89530036e39889fb7094a01d69f61[m
Merge: e0a179f bd55196
Author: vpqmflwmtkrhkakt <68584361+vpqmflwmtkrhkakt@users.noreply.github.com>
Date:   Tue Jan 28 15:25:35 2025 +0900

    Merge pull request #3 from vpqmflwmtkrhkakt/맵툴제작
    
    맵툴제작

[33mcommit bd5519697ad4f83ae56b2d6e2485e29ae586d2f4[m[33m ([m[1;31morigin/맵툴제작[m[33m, [m[1;32m맵툴제작[m[33m)[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Tue Jan 28 14:41:24 2025 +0900

    새로 추가된 스크립트 누락된거 추가

[33mcommit 2e4865ba9faaa92d09d29897a127119375b7bc55[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Tue Jan 28 14:40:28 2025 +0900

    맵툴 제작 완료

[33mcommit 892b998bbd0c0da899d5f2ba744fcff62377be5e[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Sat Jan 25 17:26:17 2025 +0900

    노드 재배치 기능 개선
    plate 새로 재배치할 시 기존 노드도 같이 제거

[33mcommit eb62a0f0459f84aaae2eab4a7b3567f29ad1178a[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Sat Jan 25 15:53:05 2025 +0900

    기존 배치된 노드 재배치 기능 추가

[33mcommit 98ba40dbcb99f995acdb38b3c0da61f1b68f1344[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Sat Jan 25 15:38:09 2025 +0900

    노드 배치시 반드시 2개를 배치하도록 기능구현

[33mcommit 497c1bc56ce3038cc925805d5c11457767563308[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Sat Jan 25 14:59:06 2025 +0900

    toolplate에 toolnode 설치기능 구현

[33mcommit 580581c09c7dd826d7d260a1d963b5801e98113d[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Sat Jan 25 13:13:34 2025 +0900

    필요없어진 서클콜라이더 제거

[33mcommit 7e34d8e84b2bd1a804530efa1c1539172a36ed2e[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Fri Jan 24 20:54:45 2025 +0900

    노드 이미지의 칼라값을 통해 생성할 노드의 색상 정하는 기능 추가

[33mcommit 4b03a3171a7449f47b37fd5b376ff3a2ea78cad4[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Fri Jan 24 13:56:56 2025 +0900

    노드 색상 설정 UI 끄고 켜기 기능구현 완료

[33mcommit c7896e0a3d157d4ac21251ab0e686a808b4c8ecf[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Fri Jan 24 13:38:09 2025 +0900

    노드 색상 이미지 리소스 추가

[33mcommit 752e58c3e27e7361387e514a8b5b341cc9180c46[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Tue Jan 21 12:29:40 2025 +0900

    전반적인 추가사항
    - Plates 갯수 설정 및 생성 UI 추가
    - UI 버튼 클릭시 기존 Plates들 제거 후 새로 생성하는 맵툴만의 클래스 PlatesCreator 추가
      - 클라이언트측에서도 매우 비슷한 클래스가 있어 추후 하나의 클래스만으로 클라, 맵툴 둘 다 쓸 수 있게끔 하면 좋을듯

[33mcommit e0a179f65850a4d367e24ab43ecea0daeb9ea180[m
Merge: 4eb45f7 af577f9
Author: vpqmflwmtkrhkakt <68584361+vpqmflwmtkrhkakt@users.noreply.github.com>
Date:   Mon Jan 20 16:34:14 2025 +0900

    Merge pull request #2 from vpqmflwmtkrhkakt/Plates기능구현
    
    Plate

[33mcommit af577f90177ebca4fcff5f6a188d1cf97150378a[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Mon Jan 20 16:27:12 2025 +0900

    Plate
      - 디버그용 좌표 나타내던 TextMeshPro, 코드 제거
      - 각 Plate간 구분을 위한 흰색 바탕의 Background 추가

[33mcommit 4eb45f7e2f60753e65d13e9d59bd1a809eab07f9[m
Merge: 382817e aff48ad
Author: vpqmflwmtkrhkakt <68584361+vpqmflwmtkrhkakt@users.noreply.github.com>
Date:   Mon Jan 20 15:26:50 2025 +0900

    Merge pull request #1 from vpqmflwmtkrhkakt/Node와Line기능구현
    
    Node와line기능구현

[33mcommit aff48adc60ddf5893a855fb9e657a70531bd6204[m[33m ([m[1;31morigin/Node와Line기능구현[m[33m, [m[1;32mNode와Line기능구현[m[33m)[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Mon Jan 20 15:25:30 2025 +0900

    Node
    - 현재 연결되어 있지 않은 노드만 라인을 그리도록 bool변수 추가

[33mcommit ddcc4c8376e38e78d41c30589e6e6b190bcfd760[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Mon Jan 20 14:07:26 2025 +0900

    Line 클래스
    - Awake에서 LineRenderer 갖고오도록 변경( 이미 사전에 추가되어있고 Awake에서 가져와 캐싱만 하기 때문에 Awake에서 호출해도 문제없다고 판단)
    
    LineCreator
    Node
    - 마우스 종이동, 횡이동 이동량에 따라 종이동, 횡이동만 하도록 구현 ( 움직임이 부자연스러워 추후 개선 필요 )
    - 드래깅을 멈췄을 때 Line의 설치가능여부 조건검사 추가
    
    Mouse
    - 일단 쓰진 않는 클래스로 추후 마우스와 관련된 기능들이 많아질 시 Mouse클래스로 옮기도록

[33mcommit 2cea6b515c150f189887e930e9c208f31bb8303a[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Sat Jan 18 16:11:59 2025 +0900

    노드 클릭 후 마우스 위치까지 라인 그리는것 구현

[33mcommit 16bf9ccbce815951530839ee889f5c30f1942027[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Sat Jan 18 13:28:12 2025 +0900

    Node객체 렌더링이 Plate보다 앞에 나오도록 plate의 z값 수정
    Serilizefield화한 Color값으로 Node의 Circle 색상이 설정되게끔 수정

[33mcommit 382817e0ce898c78a47f24c976eebee3f783b785[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Sat Jan 18 13:06:14 2025 +0900

    2025_01_18_형상관리를 위한 프로젝트 추가
    - Plate 객체, Node 객체, Line 객체 추가
    - 게임 시작 시 Plate 객체 깔리도록 구현
    - Node객체 원 모양 렌더링만 하는 상황
    - Line은 클래스만 만들어놓은 상태

[33mcommit b376a4c41ead73f8bbdd1fad02f6598cb06a5fa2[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Sat Jan 18 13:04:12 2025 +0900

    Revert "Library 경로 ignore에 추가"
    
    This reverts commit cd6160d74dadc5fd177ae4650b23393709feab2f.

[33mcommit cd6160d74dadc5fd177ae4650b23393709feab2f[m
Author: vpqmflwmtkrhkakt <dkwntu@naver.com>
Date:   Sat Jan 18 13:01:39 2025 +0900

    Library 경로 ignore에 추가

[33mcommit 09ec5837f112586284912aae152589a067465964[m
Author: vpqmflwmtkrhkakt <68584361+vpqmflwmtkrhkakt@users.noreply.github.com>
Date:   Sat Jan 18 12:27:48 2025 +0900

    Initial commit
