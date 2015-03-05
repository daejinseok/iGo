# iGo
TypeAndRun, Launchy와 같은 프로그램입니다. 목표는 손이 편하게 어플리케이션 및 명령을 쉽게 실행하도록 도와주는 것 입니다.

https://github.com/daejinseok/iGo/blob/master/package/

# 이력

## v0.4
2015.03.06
* 상단의 선택된 명령이 보이도록 에디터 창 추가
* 상단의 명령 에디터 실행
* Helper클래스 추가( 실행 및 프로세스 환경 변수)
* ${cur_dir} 변수 추가
* 실행경로에 쌍타옴표 처리 ( "c:\ a\a.exe" -> c:\ a\a.exe )
* 실행에 실패할 경우 에러메세지 창 처리

## v0.3
2015.02.27
* 텍스트 에디터에서 엔터칠 때 띵소리 제거
* 핫키와, 트레이아이콘을 눌렀을 때 동일하게
* 문자입력시 리스트박스에 채우는 부분 소스정리
* alt j,k -> space, shfit+space로 변경
* space,shfit+space와 위아래 커서키 둘이 동일하게 변경
* 한글입력이면 바탕색 변경
* 리스트 박스 스크롤 제거
* 우측 하단 장난질.

## v0.2
2015.02.26
* 수정 핫키를 누르면 무조건 나오코 포커스가 가도록 수정
* 실행시, 작업 디렉토리는 실행파일 위치로 이동
* 포커스 잃으면 안보이게


## v0.1
2015.02.25
* 최초 공개


## Todo
* 폴더 및 url실행시 띵~ 개선
* 입력시 특수 문자 처리 ( ++ )
* 명령 수정, 삭제, 우선순위 조정하도록 개선
* 링크 기부로 만들기
* 리스트 채우는 최소 문자 지금은 첫자
* 통계 -> 실행되는 정보 모아서 
* 콘솔, 파일매니져, 브라우져 설정
* 계산기?

# 사양
.net framewrok 4.0이상이 잘 동작하는 곳은 거이다 잘 동작될 것이라 생각됩니다. ( https://msdn.microsoft.com/en-us/library/8z6watww(v=vs.100).aspx )

# 설치
 * igo.exe, igo.igo, igo_cmd.igo는 필수 파일입니다. 위 파일을 아무곳에나 복사 해놓습니다.
 * igo.igo파일을 열어 적당히 단축키를 정합니다. ( 기존에 사용하는 다른 키와 중복이 발생하면 오류나요 )
 * index_user.igo와 같이 igo파일을 하나 만들어 대충 명령어를 등록합니다. 
   ( typeandrun config.ini파일과 호환이 됩니다. 단 파일 인코딩은 utf8로 변경해야 합니다. 
     즉 한글이 있다면 utf8로 변경하고, config.ini를 config.igo로 변경해서 igo.exe와 같은 폴더에 넣으면 됩니다.)
 * 이제 igo.exe를 실행
 
# 사용방법
 * 단축키를 누르면 프로그램 화면이 나타남
 * 명령어 철자를 하나 정도 누르면 등록한 명령어들이 목록에 나타남
 * 엔터를 바로 치면 바로 밑에 있는 명령이 실행
 * space, shfit+space로 아래, 위 동작
 
# 시스템 명령
 * /Quit : 프로그램 종료
 * /ReLoad : 설정파일 및 igo파일을 변경되었을 때 입력하면 갱신됨.
