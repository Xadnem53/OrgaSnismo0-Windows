::Avoid commands to be shown
@ECHO off
::Only if C:/Windows/Microsoft.NET/Framework64 exists
IF EXIST %ruta64% (
::Asign the previous path to a variable
SET ruta64=C:\Windows\Microsoft.NET\Framewrk64
::Move on to the previous path
CD %ruta64% 
::Save the list of subdirectories sorted alphabetically into a temp.txt file in the user directory.
DIR /o:n > %USERPROFILE%\temp.txt 
::Find the previous text file lines containing the word "DIR" and save them into a temp2.txt file
TYPE %USERPROFILE%\temp.txt | FIND "DIR" > %USERPROFILE%\temp2.txt
::Overwrite the previous temp.txt file with the lines in temp2.txt that contain the 'v'
TYPE %USERPROFILE%\temp2.txt | FIND "v" > %USERPROFILE%\text
::Set into a variable 'directorio', the fourth word at the file temp.txt's last line 
	FOR /F "TOKENS=4 DELIMS= " %%x in (%USERPROFILE%\temp.txt) do (
	SET directorio=%%x
	)
	ECHO %directorio%
)
