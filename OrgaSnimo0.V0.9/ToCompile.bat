::Avoid command execution being displayed on the console
@ECHO off
::Put the current directory into a variable
SET currentdir=%CD%
::Put the Framework64 directory path into a variable
SET ruta64=C:\Windows\Microsoft.NET\Framework64
::Put the Framework directory path into a variable
SET ruta32=C:\Windows\Microsoft.NET\Framework
::Variable that will be one of the before directory paths
SET ruta1=""
:: Variable that will be the last .NET Framework or .NET Framework64 version paths
SET directorio=""
::If the C:/Windows/Microsoft.NET/Framework64 directory exists, assign its path to the ruta1 variable
IF EXIST %ruta64% (
	SET ruta1=%ruta64%
	::Move to the Framework64 directory
	CD %ruta64% 
	::Put the list of the names of the subdirectories of Framework64 sorted alphabetically into a temporary text file.
	DIR /o:n > %USERPROFILE%\temp.txt 
	::Put the lines of the previous text file containing the word DIR in uppercase, into another temporary text file.
	TYPE %USERPROFILE%\temp.txt | FIND "DIR" > %USERPROFILE%\temp2.txt
	::Overwrite the first temporary text file with the lines of the previous file containing the lowercase v letter.
	TYPE %USERPROFILE%\temp2.txt | FIND "v" > %USERPROFILE%\temp.txt
	::Put the last line of the previous text file into a variable
	FOR /F "TOKENS=4 DELIMS= " %%x in (%USERPROFILE%\temp.txt) do (
		SET directorio=%%x
		)
)
::If the C:/Windows/Microsoft.NET/Framework64 directory dosen´t exists, assign C:\Windows\Microsoft.NET\Framework to the ruta1 variable
IF NOT EXIST %ruta64% (
	SET ruta1=%ruta32%
	::Move to the Framework directory
	CD %ruta32% 
	::Put the list of the names of the subdirectories of Framework sorted alphabetically into a temporary text file.
	DIR /o:n > %USERPROFILE%\temp.txt 
	::Put the lines of the previous text file containing the word DIR in uppercase, into another temporary text file.
	TYPE %USERPROFILE%\temp.txt | FIND "DIR" > %USERPROFILE%\temp2.txt
	::Overwrite the first temporary text file with the lines of the previous file containing the lowercase v letter.
	TYPE %USERPROFILE%\temp2.txt | FIND "v" > %USERPROFILE%\temp.txt
	::Put the last line of the previous text file into a variable
	FOR /F "TOKENS=4 DELIMS= " %%x in (%USERPROFILE%\temp.txt) do (
		SET directorio=%%x
		)
)
::Put the .NET Framework or .NET Framework64 complete path into a variable
SET rutacompleta=%ruta1%\%directorio%
::Add the previous complete path to the PATH system variable
SET path=%path%;%rutacompleta%
::Move to the directory where the application is
CD %currentdir%
::Compile OrgaSnimo0
csc OrgaSnismo0.cs Form1.cs Form1.Designer.cs /win32icon:OrgaSnismo0Icon.ico