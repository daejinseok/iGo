# iGo
TypeAndRun, Launchy와 같은 프로그램입니다. 목표는 손이 편하게 어플리케이션 및 명령을 쉽게 실행하도록 도와주는 것 입니다.

https://github.com/daejinseok/iGo/blob/master/package/iGo_v0.1.ZIP

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
