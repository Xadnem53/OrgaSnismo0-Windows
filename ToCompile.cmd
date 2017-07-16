::Avoid commands to be shown
@ECHO off
::Variable declaration
SET ruta64=C:\Windows\Microsoft.NET\Framework64
SET ruta32=C:\Windows\Microsoft.NET\Framework
SET directorio=""
SET ruta1=""
::If system is 64 bits
IF EXIST %ruta64% (
	SET ruta1=%ruta64%
	::Move on to the previous path
	CD %ruta64% 
	::Save the list of subdirectories sorted alphabetically into a temp.txt file in the user directory.
	DIR /o:n > %USERPROFILE%\temp.txt 
	::Find the previous text file lines containing the word "DIR" and save them into a temp2.txt filel fichero de texto anterior que contengan la palabra DIR en mayusculas en un fichero temp2
	TYPE %USERPROFILE%\temp.txt | FIND "DIR" > %USERPROFILE%\temp2.txt
	::Overwrite the previous temp.txt file with the lines in temp2.txt that contain the 'v'
	TYPE %USERPROFILE%\temp2.txt | FIND "v" > %USERPROFILE%\temp.txt
	::Set into a variable 'directorio', the fourth word at the file temp.txt's last line 
	FOR /F "TOKENS=4 DELIMS= " %%x in (%USERPROFILE%\temp.txt) do (
		SET directorio=%%x
		)
)
::If system is 32 bits
IF NOT EXIST %ruta64% (
	SET ruta1=%ruta32%
	::Move on to the previous path
	CD %ruta32% 
	::Save the list of subdirectories sorted alphabetically into a temp.txt file in the user directory.
	DIR /o:n > %USERPROFILE%\temp.txt 
	::Find the previous text file lines containing the word "DIR" and save them into a temp2.txt filel
	TYPE %USERPROFILE%\temp.txt | FIND "DIR" > %USERPROFILE%\temp2.txt
	::Overwrite the previous temp.txt file with the lines in temp2.txt that contain the 'v'
	TYPE %USERPROFILE%\temp2.txt | FIND "v" > %USERPROFILE%\temp.txt
	::Set into a variable 'directorio', the fourth word at the file temp.txt's last line 
	FOR /F "TOKENS=4 DELIMS= " %%x in (%USERPROFILE%\temp.txt) do (
		SET directorio=%%x
		)
)
::Add the Framework folder to the system path
SET rutacompleta=%ruta1%\%directorio%
CD %rutacompleta%
SET path=%path%;%rutacompleta%
::TO BE COMPLETED .... :)

