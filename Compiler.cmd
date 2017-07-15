::Evitar que se muestre la ejecucion de los comandos en consola
@ECHO off
::Comprobar si existe el directorio: C:/Windows/Microsoft.NET/Framework64
::Asignar a una variable, la ruta al directorio anterior
SET ruta64=C:\Windows\Microsoft.NET\Framework64
IF EXIST %ruta64% (
::Moverse al directorio anterior
CD %ruta64% 
::Meter la lista de subdirectorios ordenados alfabeticamente por su nombre, en un fichero de texto temporal en la carpeta del usuario
DIR /o:n > %USERPROFILE%\temp.txt 
::Meter las lineas del fichero de texto anterior que contengan la palabra DIR en mayusculas en un fichero temp2
TYPE %USERPROFILE%\temp.txt | FIND "DIR" > %USERPROFILE%\temp2.txt
::Meter las lineas del fichero anterior que contengan la letra v minuscula en el fichero temp
TYPE %USERPROFILE%\temp2.txt | FIND "v" > %USERPROFILE%\temp.txt
::Meter en una variable la ultima fila del fichero temp.txt
	FOR /F "TOKENS=4 DELIMS= " %%x in (%USERPROFILE%\temp.txt) do (
	@SET directorio=%%x
	)
	ECHO %directorio%
)
