set cs_file=list

csc %cs_file%.cs
@if %ERRORLEVEL% neq 0 goto NOT_OK_END
@%cs_file%.exe
:NOT_OK_END
