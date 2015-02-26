set cs_file=pg_info

csc %cs_file%.cs
@if %ERRORLEVEL% neq 0 goto NOT_OK_END
@%cs_file%.exe
:NOT_OK_END
