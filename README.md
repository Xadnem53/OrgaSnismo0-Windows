
OrgaSnismo0, is based on the Game of life, designed by the mathematician John Conway.
There´s a extensive information in Wikipedia.

There´s nothing especial, it is one of the lots of versions of this game, but I believe to have gotten an acceptable performance, after to try many algorithms in order to get the next shape at every cicle.

I would apreciate all the sugestions in order to improve the performance when the cell population is big ( more than 2000 ) wich happens mainly when some rules that make the population grow fast as 1357 / 1357 are stablised.

Basic operation:

When the game is started, the menú bar, wich functions are commented later, and a RadioButton with the label: 'Draw intial shape' are shown.
![alt tag](http://C:\Users\jslvr\Pictures\imagen-1.png)
A dialog for to change the size of the grid shown then, appears by clicking this radioButton. The grid size is 30 pixels by defect.

Clicking over the Accept button at the grid size dialog, make the following elements to appear:

Grid:  A new yellow color cell, is created every time that one click over a grid square.
	A before created cell can be deleted just clicking over it again.

Numeric up & down: With the label: 'Milliseconds', allows to change the cicle time set in 1 			    second by defect.
			    If the time needed by the cpu to calculate the next state, is bigger 			    than the cicle time settled down, this is automaticaly changed by the 			    program.

Accept button:  To start the cicles once the initial shape has benn drawn or loaded and the 		      cicle time stablished.


Once the game is started, the shape is going changing according to the rules at every new cicle.
The rules by defect, are the Conway´s rules i.e.:
A cell remains alive, if it is adyacent to two or three alive cells, otherwise the cell dies at the next cicle.
A cell borns, if it is adyacent to three alive cells.

At the screen top left, the population ( number of alive cells ) and the number of cicles passed from the start, are shown.
Also buttons for: Zoom, displacement and pause, appear.

If the shape stabilizes or disappears, a message box is shown and the game is over.

Menu Bar: 

File:

New: To start a new game. The rules and colors before stablished are kept.

Save: Saves the current shape, rules and colors in a *.fm file.

Load: Loads a shape with the rules and colors previously saved in a *.fm file.

Save image: Saves a screen shot in a *.bmp file.





Controls:

Displacememt buttons: To show or hide the displacement buttons.

Zoom buttons: To show or hide the zoom buttons

Change period: To show or hide the numeric up & down with the cicle time.

Options:

Change rules: A window appers and allow to change the rules.
		  There are some rules examples with a brief description about their respective 		  efects, at the bottom of this window.

Back to default values: Returns to the rules, colors and cicle time by defect.

Change colors: Shows a dialog with the following sentences:
		     Line color, Alive cells color, Born cells color , Died cells color and 			     Background color.
		     The color of every sentence corresponds to the color of each element. 			     Clicking over a sentence, makes a color selection dialog to appear.


Beep: Activated by defect, makes a beep to sound at every cicle. It is useful in order to   	advice from a new screen redraw in a long time cicles.


Automatic image saver: When it is activated, a directory chooser dialog is shown in order 				to choose the directory where a screen shot *.bmp image is saved 			          at every cicle.

Language: Spanish and English languages are available.

Help: Makes to appear a description simillar to this.

