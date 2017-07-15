::Avoid commands to be shown
@ECHO off
::Only if C:/Windows/Microsoft.NET/Framework64 exists
IF EXIST %ruta64% (
::Asign the previous path to a variable
SET ruta64=C:\Windows\Microsoft.NET\Framewrk64
::Move to the previous path
CD %ruta64% 
::Save the subdirectories list alphabeticaly ordered into a temp.txt file at the user directory.
DIR /o:n > %USERPROFILE%\temp.txt 
::Find the previous text file lines that contains the word "DIR" and save them into a temp2.txt file
TYPE %USERPROFILE%\temp.txt | FIND "DIR" > %USERPROFILE%\temp2.txt
::Overwrite the previous temp44.txt file with the lines in temp2.txt which contains the 'v' character 
TYPE %USERPROFILE%\temp2.txt | FIND "v" > %USERPROFILE%\text
::Set into a variable 'directorio', the fourth word at the file temp.txt last line 
	FOR /F "TOKENS=4 DELIMS= " %%x in (%USERPROFILE%\temp.txt) do (
	SET directorio=%%x
	)
	ECHO %directorio%
)
