# iGo
TypeAndRun, Launchy와 같은 프로그램입니다. 목표는 손이 편하게 어플리케이션 및 명령을 쉽게 실행하도록 도와주는 것 입니다.

https://github.com/daejinseok/iGo/blob/master/package/

# 이력

## v0.2    2015.02.26
  * 수정 핫키를 누르면 무조건 나오코 포커스가 가도록 수정
  * 실행시, 작업 디렉토리는 실행파일 위치로 이동
  * 포커스 잃으면 안보이게


## v0.1    2015.02.25
  * 최초 공개


## Todo
* 리스트 박스 스크롤 제거
* 띵 소리남
* 한ㄱ르은 흰바탕의 궁서체
* alt j, k 변경, 등등 커서 동기화
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
 * alt+j, k로 아래, 위 동작
 
# 시스템 명령
 * /Quit : 프로그램 종료
 * /ReLoad : 설정파일 및 igo파일을 변경되었을 때 입력하면 갱신됨.
