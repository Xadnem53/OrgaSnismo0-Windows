using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Media;
using System.Runtime.CompilerServices;


namespace Organismo0
{
    public partial class Form1 : Form
    {
        List<PointF> cells; // For the current alive cells.
        List<PointF> cellsant;// For the before alive cells
        List<PointF> nextalive;  //Next cells to be born
        List<PointF> nextdead; // Next cells to be dead
        List<PointF> deadant; // For the before dead cells
        List<PointF> aliveant; // For the before born cells
        List<PointF> adjacents; // For the cells adjacent to the alive cells
        List<int> remainalive; // List of the quantity of adjacents alive cells for a cell to remain alive
        List<int> toborn; // List of the quantity of adjacent alive cells for a cell to born

        Bitmap bitmap; //  For permanent graphics
        Graphics graphic; // Graphics object to be used into the PictureBox

        Pen line; // Pen to be used for the alive cells exterior line and the grid for the initial shape drawing 
        Pen deadline; // Pen to be used for the dead cells exterior line

        float cellsize = 30; // By defect cell size

        int cycle = 1000; // cycle time by defect
        int cycles = 0; // Counter of cycles from the begining of the shape.
        int cyclessesion = 0; // Counter of cylcles from the beginning of the sesion.
        int calculationtime = 0; // For the time taken by the EvaluateLife method 

        string alive = "23"; // By defect value for a cell to remain alive
        string born = "3"; // By defect value for a cell to born
        string prefix = ""; // User prefix to be used in the automatic image saving file names

        PointF origin; // Graphics origin point

        TextBox tbcellsize; // Textbox for the cell size dialog

        Form dialogcellsize; // Cell size dialog
        Form dialogrules; // Rules change dialog
        Form dialogcolors; // Color change dialog

        TextBox tbborn; // For the number of adjacent cells for a cell to remain alive in the rules change dialog
        TextBox tbalive; //For the number of adjacent cells for a cell to born in the rules change dialog

        Label lbcellsize; // For the cell size dialog
        Label lbColorline; // For the cell exterior line color in the change colors dialog
        Label lbColorInterior; // For the alive cells interior color in the change colors dialog
        Label lbColorbackground; // For the background color in the change colors dialog
        Label lbColorInteriorM; // For the dead cells interior color in the change colors dialog
        Label lbColorInteriorV; // For the born cells interior color in the change colors dialog

        bool drawing = false; // For regist when the user enters in the shape drawing mode      
        bool ongoing = false; // For regist when the timer is running
        bool available = false; // For threads sinchronization
       // bool autocycle = false; // For regist when the program change to automatic cycle time.
        bool beep = false; // For regist when the beep is enabled
        bool autosaving = false; // For regist when the automatic image saving is enabled
        bool english = true; // For language selection

        int gamecounter = 0; // Number of games counter.

        Color exterior = Color.Red; //By defect alive cells exterior line color 
        Color interior = Color.FromArgb(155, 250, 5); // By defect alive cells interior color
        Color background = Color.Black; // By defect background color
        Color interiorM = Color.Black; // By defect dead cells interior color
        Color interiorV = Color.FromArgb(155, 250, 5); // By defect born cells interior color

        Thread calculate; // Thread for calculate the next cycle state

        Button btcellsize; //For accept the cell size in the cell size dialog

        ///////////////////////////////
        ///
        /// CONSTRUCTOR
        ///
        ////////////////////////////////////////////
        ///

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = cycle;
            cells = new List<PointF>();
            cellsant = new List<PointF>();
            nextalive = new List<PointF>();
            nextdead = new List<PointF>();
            deadant = new List<PointF>();
            adjacents = new List<PointF>();
            aliveant = new List<PointF>();
            remainalive = new List<int>() { 2, 3 };
            toborn = new List<int>() { 3 };
            nudCycle.Value = cycle;
            SoundPlayer reproductor = new SoundPlayer();
            reproductor.SoundLocation = Directory.GetCurrentDirectory() + "\\Rafaga-Organismo-Entrada.wav";
            reproductor.Play();
            lbEgo.ForeColor = background;
            lbEgo.BackColor = background;
            beepToolStripMenuItem.PerformClick();
            line = new Pen(exterior, 1);
            deadline = new Pen(background, 1);
            englishToolStripMenuItem.PerformClick();
            Icon icon = new Icon(Directory.GetCurrentDirectory() + "\\OrgaSnismo0Icon.ico");
            this.Icon = icon;
            origin = new PointF(0, 0);
        }

        /// <summary>
        /// 
        /// MODIFY THE BUTTONS LOCATION AND THE PICTURE BOX SIZE FUNCTION OF THE FORM SIZE
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void Maximized(object sender, EventArgs e)
        {
            //Update the graphic area
            pbBackGround.Size = this.ClientSize;
            //Update the controls location
            btUp.Location = new Point(this.ClientSize.Width - 100, this.ClientSize.Height - 200);
            btRight.Location = new Point(this.ClientSize.Width - 50, this.ClientSize.Height - 150);
            btDown.Location = new Point(this.ClientSize.Width - 100, this.ClientSize.Height - 100);
            btLeft.Location = new Point(this.ClientSize.Width - 150, this.ClientSize.Height - 150);
            btZoomMinus.Location = new Point(this.btLeft.Location.X, this.ClientSize.Height - 50);
            btZoomPlus.Location = new Point(this.btRight.Location.X, btZoomMinus.Location.Y);
            nudCycle.Location = new Point(20, this.ClientSize.Height - 30);
            lbNud.Location = new Point(nudCycle.Location.X, nudCycle.Location.Y - lbNud.Height - 2);
            lbEgo.Location = new Point(this.ClientSize.Width - lbEgo.Width - 5, lbEgo.Height + 5);
            btAccept.Location = new Point(this.ClientSize.Width / 2 - btAccept.Width / 2, this.ClientSize.Height - btAccept.Height - 10);
        }
        

        /// <summary>
        /// 
        ///  HANDLER FOR THE DRAW SHAPE RADIOBUTTON OPTION.
        ///  SHOWS THE CELL SIZE DIALOG AND ARRANGE THE FORM
        /// </summary>
        ///
        private void rbDrawShape_Click(object sender, EventArgs e)
        {
            rbDrawShape.Hide();
            pbBackGround.Update();
            cellsizeToolStripMenuItem_Click(new object(), new EventArgs());
            HideControls();
            nudCycle.Visible = true; ;
            lbNud.Visible = true;
            cyclechangeToolStripMenuItem.Checked = true;
            drawing = true;
        }

        ///////////////////////////
        ///
        /// SHOWS THE CELL SIZE DIALOG WHEN THE DRAW SHAPE RADIO BUTTON OR THE MENU OPTION IS CLICKED
        ///
        ///////////////////////////////////////////////
        ///
        private void cellsizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create the cell size dialog
            dialogcellsize = new Form();
            dialogcellsize.FormBorderStyle = FormBorderStyle.FixedDialog;
            dialogcellsize.Size = new Size(190, 130);
            dialogcellsize.BackColor = Color.SeaGreen;
            dialogcellsize.ControlBox = false;
            dialogcellsize.TopMost = true;
            lbcellsize = new Label();
            lbcellsize.AutoSize = true;
            lbcellsize.Font = new Font("Dejavu Sans", 10, FontStyle.Underline);
            if (english)
            {
                lbcellsize.Text = "Cell size.";
            }
            else
            {
                lbcellsize.Text = "Tamaño de la célula.";
            }
            lbcellsize.Location = new Point(20, 20);
            lbcellsize.Visible = true;

            tbcellsize = new TextBox();
            tbcellsize.Size = new Size(100, 50);
            tbcellsize.Location = new Point(40, 50);
            tbcellsize.Visible = true;
            tbcellsize.Text = "30";
            tbcellsize.SelectAll();
            tbcellsize.TextAlign = HorizontalAlignment.Center;

            btcellsize = new Button();
            if (english)
            {
                btcellsize.Text = "Accept";
            }
            else
            {
                btcellsize.Text = "Aceptar";
            }
            btcellsize.Location = new Point(55, 90);
            btcellsize.Visible = true;
            btcellsize.BackColor = Color.Chartreuse;
            btcellsize.Click += btcellsize_Clicked;

            dialogcellsize.Controls.Add(tbcellsize);
            dialogcellsize.Controls.Add(lbcellsize);
            dialogcellsize.Controls.Add(btcellsize);
            dialogcellsize.StartPosition = FormStartPosition.Manual;
            dialogcellsize.Location = this.Location;
            dialogcellsize.AcceptButton = btcellsize;
            dialogcellsize.Show();
        }

        /// <summary>
        /// 
        /// HANDLER FOR THE CELL SIZE DIALOG ACCEPT BUTTON
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void btcellsize_Clicked(object sender, EventArgs e)
        {
            string cellsizes = tbcellsize.Text;
            if (cellsizes == "")
            {
                cellsizes = "20";
            }
            bool converted = Single.TryParse(cellsizes, out cellsize);
            if (!converted)
            {
                if (english)
                {
                    MessageBox.Show("Bad number.");
                }
                else
                {
                    MessageBox.Show("Número erroneo.");
                }
                tbcellsize.ResetText();
                tbcellsize.Focus();
            }
            dialogcellsize.Close();
            // Initialize the bitmap for permanent graphics
            bitmap = new Bitmap(pbBackGround.Size.Width, pbBackGround.Size.Height);
            //Link the bitmap with the PictureBox
            pbBackGround.Image = bitmap;
            // Initialize the object graphics linked to the bitmap
            graphic = Graphics.FromImage(bitmap);
            graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //Set the pen for the grid lines
            line = new Pen(Color.FromArgb(50, 240, 100, 50));
            // Draw the grid for the initial shape drawing
            for (float i = 0; i < pbBackGround.Width; i += cellsize)
            {
                graphic.DrawLine(line, new PointF(i, 0), new PointF(i, pbBackGround.Height));
            }
            for (float i = 0; i < pbBackGround.Height; i += cellsize)
            {
                graphic.DrawLine(line, new PointF(0, i), new PointF(pbBackGround.Width, i));
            }
            pbBackGround.MouseClick += DrawShape; // For draw a cell when the user make mouse click over a grid square
            //Set the shape accept button
            btAccept.Location = new Point(this.ClientSize.Width - btAccept.Size.Width - 5, this.ClientSize.Height - btAccept.Size.Height - 5);
            btAccept.BackColor = Color.Chartreuse;
            if (english)
            {
                btAccept.Text = "Accept";
            }
            else
            {
                btAccept.Text = "Aceptar";
            }
            btAccept.ForeColor = Color.Black;
            btAccept.Show();
            btAccept.Click -= btPause_Click;
            btAccept.Click += btAccept_Click;
            // To show the numeric up and down for the cycle time
            nudCycle.Visible = true;
            lbNud.Visible = true;
            // To show the label with the cell population
            lbPopulation.Show();
        }


        ///////////////////////////
        ///
        /// DRAWS A CELL WHEN THE USER MAKE MOUSE CLICK OVER A GRID SQUARE OR DELETE IT IF THE CELL WAS
        /// ALREADY DRAWN
        ///
        /////////////////////////////////
        ///
        private void DrawShape(object sender, MouseEventArgs e)
        {
            //Obtain the point where the user did mouse click
            PointF pulsado = e.Location;
            PointF celula = new PointF(((int)(pulsado.X / cellsize)) * cellsize, ((int)(pulsado.Y / cellsize)) * cellsize);
            // Draw the cell and include it in the alive cells list if it didn´t exists, or remove it if was already created
            if (!cells.Contains(celula))
            {
                cells.Add(celula);
                graphic.FillRectangle(new SolidBrush(interior), celula.X, celula.Y, cellsize, cellsize);
            }
            else
            {
                cells.Remove(celula);
                graphic.FillRectangle(new SolidBrush(Color.Black), celula.X, celula.Y, cellsize, cellsize);
            }
            // Modify the population and cycle time labels
            if (english)
            {
                lbPopulation.Text = "Population: " + cells.Count.ToString();
                lbNud.Text = "Milliseconds: ";
            }
            else
            {
                lbPopulation.Text = "Población: " + cells.Count.ToString();
                lbNud.Text = "Milisegundos: ";
            }
            pbBackGround.Invalidate();
        }



        ///////////////////////////
        ///
        /// HANDLER FOR THE SHAPE ACCEPT BUTTON.
        /// CALLS TO THE EVALUATELIFE METHOD AND INITIALIZE IT WITH THE DRAWN SHAPE
        ///
        ///////////////////////////////////////////////
        ///
        private void btAccept_Click(object sender, EventArgs e)
        {
            //Change the label text, color and handler for the accept button
            btAccept.BackColor = Color.Black;
            btAccept.ForeColor = Color.Silver;
            if (english)
            {
                btAccept.Text = "Pause";
            }
            else
            {
                btAccept.Text = "Pausa";
            }
            btAccept.Location = new Point(this.ClientSize.Width / 2, btAccept.Location.Y);
            btAccept.Click -= btAccept_Click;
            btAccept.Click += btPause_Click;
            //Remove the mouse click handler from the picture box
            pbBackGround.MouseClick -= DrawShape;
            //Show the controls for displacement and zoom
            ShowControls();
            //Update the menu items
            if (gamecounter == 0)
            {
                zoombuttonsToolStripMenuItem.PerformClick();
                cyclechangeToolStripMenuItem.PerformClick();
                cyclechangeToolStripMenuItem.PerformClick();
            }
            //Delete the grid
            graphic.Clear(background);
            //Show the cycle counter label
            lbCycles.Visible = true;
            if (english)
            {
                lbCycles.Text = "Cycles: " + cycles.ToString();
                lbPopulation.Text = "Population: " + cells.Count.ToString();
            }
            else
            {
                lbCycles.Text = "ciclos: " + cycles.ToString();
                lbPopulation.Text = "Población: " + cells.Count.ToString();
            }
            //Ensure that the label for cell population is visible
            lbPopulation.Visible = true;

            //Restore de cells exterior line pen
            line = new Pen(exterior, 1);

            // Redraw the initial shape
            DrawCells();

            //Iniatialize the Thread for shape state calculation
            calculate = new Thread(EvaluateLife);
            calculate.Start();

            //Initialize the timer
            timer1.Start();
            ongoing = true;
            gamecounter++;
            drawing = false;
        }

        /// <summary>
        /// 
        /// HANDLER FOR THE NEW GAME MENU OPTION
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stop the timer
            timer1.Stop();
            //Uncheck the radio button for new shape drawing and show it
            rbFicticio.Checked = true;
            rbDrawShape.Show();
            //Erase the graphic area and remove the grid mouse click handler
            if (graphic != null)
            {
                graphic.Clear(Color.Black);
            }
            if (pbBackGround != null)
            {
                pbBackGround.MouseClick -= DrawShape;
                pbBackGround.MouseClick -= DrawShape;
            }
            // Reset the cell lists
            cells.Clear();
            cellsant.Clear();
            nextdead.Clear();
            nextalive.Clear();
            deadant.Clear();
            aliveant.Clear();
            adjacents.Clear();
            //Hide the labels for the cycle count and pupulation and the accept/pause button
            btAccept.Hide();
            lbCycles.Hide();
            lbPopulation.Hide();
            //Reset the cycles counter
            cycles = 0;
            //Ensure that the cell size dialog is hiden 
            if (dialogcellsize != null)
            {
                dialogcellsize.Hide();
            }
            //Hide the display moving and zoom controls
            HideControls();
            //Reset the image saving and uncheck the menu New option
            autosaving = false;
            newToolStripMenuItem.Checked = false;
            //Stablish the by defect cycle time and numeric up & down back color
            cycle = 1000;
            nudCycle.BackColor = Color.White;
            nudCycle.Value = cycle;
            pbBackGround.Invalidate();
            cyclessesion = 0;
            //Keep the before language selection
            if (english)
            {
                englishToolStripMenuItem_Click(new object(), new EventArgs());
            }
            else
            {
                catellanoToolStripMenuItem_Click(new object(), new EventArgs());
            }
        }

        /// <summary>
        /// 
        /// UPDATES THE ALIVE AND DEAD CELLS EACH CYCLE AND STOPS THE PROGRAM IN CASE THE SHAPE
        /// HAS STABILIZED OR DISAPEARED
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Wait until the next calculated state be ready
            while (available == false)
            {
                Monitor.Wait(this);
            }

            //Add the new cells to the alive cells list and remove the dead ones
            foreach (PointF p in nextalive)
            {
                if (!cells.Contains(p))
                {
                    cells.Add(p);
                }
            }
            foreach (PointF p in nextdead)
            {
                bool repeteatedleft = true;
                while (repeteatedleft)
                {
                    repeteatedleft = cells.Remove(p);
                }
            }

            // Stablish the cycle time function of the shape state calculation time
            if (calculationtime > 0)
            {
                if (calculationtime > 100 && cycle < calculationtime)
                {
                    nudCycle.Value = calculationtime;
                }
                else if (calculationtime > 100 && cycle > calculationtime)
                {
                    nudCycle.Value = calculationtime;
                }
            }

            // Move the graphics origin according to the stablished origin point
            graphic.TranslateTransform(origin.X, origin.Y);

            if (english)
            {
                lbCycles.Text = "Cycles: " + cycles.ToString();
                lbPopulation.Text = "Population: " + cells.Count;
            }
            else
            {
                lbCycles.Text = "Ciclos: " + cycles.ToString();
                lbPopulation.Text = "Poblaciòn: " + cells.Count;
            }

            // Draw the current shape state
            DrawCells();

            //Save the bitmap if the image autosave option is active
            if (autosaving)
            {
                int indice = prefix.LastIndexOf('.');
                string nombre = prefix.Substring(0, indice);
                nombre += "--" + cycles.ToString() + ".bmp";
                bitmap.Save(nombre);
            }

            // Increment the current cycle account and the sesion cycles account
            cycles++;
            cyclessesion++;

            // Release the monitor for thread synchronization
            available = false;
            Monitor.PulseAll(this);

            /// Show a notice and stop the game in case that all the cells are dead
            if (cyclessesion > 1 && cells.Count == 0)
            {
                timer1.Stop();
                if (english)
                {
                    MessageBox.Show("OrgaSnismo dead");
                }
                else
                {
                    MessageBox.Show("OrgaSnismo muerto.");
                }
                return;
            }

            // Show a notice and stop the game in case the shape has stabilized
            if (cyclessesion > 1 && nextdead.Count == 0 && nextalive.Count == 0)
            {
                btPause_Click(new object(), new EventArgs());
                if (english)
                {
                    MessageBox.Show("OrgaSnimo stabilized");
                }
                else
                {
                    MessageBox.Show("OrgaSnismo estabilizado.");
                }
                return;
            }

            //To sound a beep if the option is active
            if (beep && cells.Count > 1000)
            {
                Console.Beep(400, 1000);
            }
        }


        /// <summary>
        /// 	
        ///    MODIFY THE LISTS OF CELLS BY APPLYING THE RULES EACH CYCLE
        /// </summary>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void EvaluateLife()
        {
            while (true)
            {
                // Wait until the Timer1_Tick finish 
                while (available == true)
                {
                    Monitor.Wait(this);
                }
                DateTime calcstart = DateTime.Now;

                // Save the current alive and dead cell lists before modify them 
                deadant.Clear();
                aliveant.Clear();
                foreach (PointF p in nextdead)
                {
                    deadant.Add(p);
                }
                foreach (PointF p in nextalive)
                {
                    aliveant.Add(p);
                }
                //Reset the adjacent cells list
                adjacents.Clear();

                //Fill in the adjacent cells list ( all the alive cells will be also included )
                foreach (PointF p in cells)
                {
                    adjacents.Add(new PointF(p.X - cellsize, p.Y - cellsize));
                    adjacents.Add(new PointF(p.X - cellsize, p.Y));
                    adjacents.Add(new PointF(p.X - cellsize, p.Y + cellsize));
                    adjacents.Add(new PointF(p.X, p.Y + cellsize));
                    adjacents.Add(new PointF(p.X + cellsize, p.Y + cellsize));
                    adjacents.Add(new PointF(p.X + cellsize, p.Y));
                    adjacents.Add(new PointF(p.X + cellsize, p.Y - cellsize));
                    adjacents.Add(new PointF(p.X, p.Y - cellsize));
                }

                //Reset the next living and dead cells lists
                nextalive.Clear();
                nextdead.Clear();

                // Dictionary of type point / repetitions for count the times that an adjacent cell is repeated in the adjacent cells list
                Dictionary<PointF, int> diccionario = new Dictionary<PointF, int>();

                //Fill in the dictionary with the Point/Repetition pairs corresponding to each cell in the adjacent cells list
                foreach (PointF p in adjacents)
                {
                    if (diccionario.ContainsKey(p))
                    {
                        diccionario[p]++;
                    }
                    else
                    {
                        diccionario.Add(p, 1);
                    }
                }
                // Add the adjacent cells which has been repeteated the number of times determined in the rules
                // for a cell to remain alive or born into the next alive cells list and add to the next dead cells 
                // list the rest of cells.

                foreach (KeyValuePair<PointF, int> k in diccionario)
                {
                    if (toborn.Contains(k.Value) && !cells.Contains(k.Key) && !nextalive.Contains(k.Key))
                    {
                        nextalive.Add(k.Key);
                    }
                    else if (cells.Contains(k.Key) && !remainalive.Contains(k.Value))
                    {
                        nextdead.Add(k.Key);
                    }
                }

                //Remove the isolated cells
                foreach (PointF p in cells)
                {
                    bool aislada = true;

                    if (cells.Contains(new PointF(p.X - cellsize, p.Y - cellsize)))
                    {
                        aislada = false;
                    }
                    else if (aislada && cells.Contains(new PointF(p.X - cellsize, p.Y)))
                    {
                        aislada = false;
                    }
                    else if (aislada && cells.Contains(new PointF(p.X - cellsize, p.Y + cellsize)))
                    {
                        aislada = false;
                    }
                    else if (aislada && cells.Contains(new PointF(p.X, p.Y + cellsize)))
                    {
                        aislada = false;
                    }
                    else if (aislada && cells.Contains(new PointF(p.X + cellsize, p.Y + cellsize)))
                    {
                        aislada = false;
                    }
                    else if (aislada && cells.Contains(new PointF(p.X + cellsize, p.Y)))
                    {
                        aislada = false;
                    }
                    else if (aislada && cells.Contains(new PointF(p.X + cellsize, p.Y - cellsize)))
                    {
                        aislada = false;
                    }
                    else if (aislada && cells.Contains(new PointF(p.X, p.Y - cellsize)))
                    {
                        aislada = false;
                    }
                    if (aislada && !remainalive.Contains(0))
                    {
                        nextdead.Add(p);
                    }
                }
                //Update the shape state calculation time
                DateTime finishcalc = DateTime.Now;
                TimeSpan tiempocal = finishcalc - calcstart;
                calculationtime = (int)tiempocal.TotalMilliseconds;
                //Release the monitor for thread synchronization
                available = true;
                Monitor.PulseAll(this);
            }
        }



        /// <summary>
        /// 
        /// HANDLER FOR SHOW OR HIDE THE ZOOM BUTTONS AND CHECK OR UNCHECK THE MENU OPTION
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void zoombuttonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (drawing)
            {
                return;
            }
            if (!zoombuttonsToolStripMenuItem.Checked)
            {
                btZoomPlus.Show();
                btZoomMinus.Show();
                zoombuttonsToolStripMenuItem.Checked = true;
            }
            else
            {
                btZoomPlus.Hide();
                btZoomMinus.Hide();
                zoombuttonsToolStripMenuItem.Checked = false;
            }
        }

        /// <summary>
        /// 
        /// HANDLER FOR THE ZOOM EXPAND BUTTON
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void ampliarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graphic.ScaleTransform(1.25F, 1.25F);
            graphic.TranslateTransform(pbBackGround.Width / -16, pbBackGround.Height / -16);
            graphic.Clear(background);
            DrawCells();
        }

        /// <summary>
        /// 
        /// HANDLER FOR THE ZOOM REDUCTION BUTTON
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void reducirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graphic.ScaleTransform(0.75F, 0.75F);
            graphic.TranslateTransform(pbBackGround.Width / 8, pbBackGround.Height / 8);
            graphic.Clear(background);
            DrawCells();
        }

        /// <summary>
        /// 
        /// HANDLER FOR TO SHOW OR HIDE THE SCROLL BUTTONS AND CHECKS OR UNCHECKS THE
        /// MENU OPTION 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void displacementbuttonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (drawing)
            {
                return;
            }
            if (!displacementbuttonsToolStripMenuItem.Checked)
            {
                btUp.Show();
                btDown.Show();
                btRight.Show();
                btLeft.Show();
                displacementbuttonsToolStripMenuItem.Checked = true;
            }
            else
            {
                btUp.Hide();
                btDown.Hide();
                btRight.Hide();
                btLeft.Hide();
                displacementbuttonsToolStripMenuItem.Checked = false;
            }
        }

        /// <summary>
        /// 
        /// HANDLER FOR THE LEFT SCROLL BUTTON
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void btLeft_Click(object sender, EventArgs e)
        {
            graphic.Clear(background);
            graphic.TranslateTransform(origin.X - (cellsize * 5), origin.Y);
            DrawCells();
        }

        /// <summary>
        /// 
        /// HANDLER FOR THE SCROLL UP BUTTON
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btUp_Click(object sender, EventArgs e)
        {
            graphic.Clear(background);
            graphic.TranslateTransform(origin.X, origin.Y - (cellsize * 5));
            DrawCells();
        }

        /// <summary>
        /// 
        /// HANDLER FOR THE SCROLL RIGHT BUTTON
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btRight_Click(object sender, EventArgs e)
        {
            graphic.Clear(background);
            graphic.TranslateTransform(origin.X + (cellsize * 5), origin.Y);
            DrawCells();
        }

        /// <summary>
        /// 
        /// HANDLER FOR THE SCROLL DOWN BUTTON
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDown_Click(object sender, EventArgs e)
        {
            graphic.Clear(background);
            graphic.TranslateTransform(origin.X, origin.Y + (cellsize * 5));
            DrawCells();
        }

        /// <summary>
        /// 
        /// HANDLER FOR THE SAVE MENU OPTION. 
        /// SAVES THE CURRENT SHAPE, RULES AND COLORS IN A *.fm FILE
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fichero;
            FileStream fs;
            BinaryWriter bw;

            SaveFileDialog shapesavedialog = new SaveFileDialog();
            if (english)
            {
                shapesavedialog.Filter = "Shape files (*.fm)|*.fm";
            }
            else
            {
                shapesavedialog.Filter = "Ficheros de forma (*.fm)|*.fm";
            }

            if (shapesavedialog.ShowDialog() == DialogResult.OK)
            {
                fichero = shapesavedialog.FileName;
                fs = new FileStream(fichero, FileMode.Create, FileAccess.Write);
                bw = new BinaryWriter(fs);
                try
                {
                    //Save the current rules
                    //Write the length of the list with the rules for a cell to remain alive 
                    bw.Write(remainalive.Count);
                    //Write the values into the list with the rules for a cell to remain alive
                    foreach (int i in remainalive)
                        bw.Write(i);
                    //Write the length of the list with the rules for a cell to born
                    bw.Write(toborn.Count);
                    //Write the values into the list with the rules for a cell to born
                    foreach (int i in toborn)
                        bw.Write(i);
                    //Save the colors
                    //Save the living cells exterior line color by its rgb values
                    byte exteriorr = exterior.R;
                    byte exteriorg = exterior.G;
                    byte exteriorb = exterior.B;
                    bw.Write(exteriorr);
                    bw.Write(exteriorg);
                    bw.Write(exteriorb);
                    //Save the living cells interior cells color by its rgb values
                    byte interiorr = interior.R;
                    byte interiorg = interior.G;
                    byte interiorb = interior.B;
                    bw.Write(interiorr);
                    bw.Write(interiorg);
                    bw.Write(interiorb);
                    //Save the background color by its rgb values
                    byte backgroundr = background.R;
                    byte backgroundg = background.G;
                    byte backgroundb = background.B;
                    bw.Write(backgroundr);
                    bw.Write(backgroundg);
                    bw.Write(backgroundb);
                    //Save the dead cells interior color by its rgb values
                    byte interiorMr = interiorM.R;
                    byte interiorMg = interiorM.G;
                    byte interiorMb = interiorM.B;
                    bw.Write(interiorMr);
                    bw.Write(interiorMg);
                    bw.Write(interiorMb);
                    //Save the born cells interior color by its rgb values
                    byte interiorVr = interiorV.R;
                    byte interiorVg = interiorV.G;
                    byte interiorVb = interiorV.B;
                    bw.Write(interiorVr);
                    bw.Write(interiorVg);
                    bw.Write(interiorVb);
                    //Save the points corresponding to the living cells

                    foreach (PointF p in cells)
                    {
                        bw.Write(p.X);
                        bw.Write(p.Y);
                    }

                    string aux = cycles.ToString();
                    float cic;
                    bool converted = Single.TryParse(aux, out cic);
                    if (converted)
                    {
                        bw.Write(cic);
                    }
                    else
                    {
                        throw new IOException("File writing error");
                    }
                }
                catch (IOException er)
                {
                    MessageBox.Show("File writing error." + er.Message);
                }
                finally
                {
                    fs.Close();
                    bw.Close();
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 
        /// HANDLER FOR THE LOAD MENU OPTION.
        /// LOADS THE SHAPE, COLORS AND RULES PRIOR SAVED IN A *.fm FILE
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Reset the automatic image saving, the cell lists and to stop the timer
            autosaving = false;
            autoimagesavingToolStripMenuItem.Checked = false;
            timer1.Stop();
            cells.Clear();
            cyclessesion = 0;

            if (deadant != null)
            {
                deadant.Clear();
            }
            if (nextdead != null)
            {
                nextdead.Clear();
            }
            if (nextalive != null)
            {
                nextalive.Clear();
            }
            if (aliveant != null)
            {
                aliveant.Clear();
            }
            adjacents.Clear();

            // Restore the by defect cycle time and the numeric up & down backcolor.
            cycle = 1000;
            nudCycle.BackColor = Color.White;
            nudCycle.Value = cycle;

            //Reset the graphics
            bitmap = new Bitmap(pbBackGround.Width, pbBackGround.Height);
            graphic = Graphics.FromImage(bitmap);
            graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            pbBackGround.Image = bitmap;
            graphic.Clear(background);
            pbBackGround.Invalidate();

            //Show the openfile dialog for open the prior saved file
            OpenFileDialog fileloaddialog = new OpenFileDialog();
            if (english)
            {
                fileloaddialog.Filter = "Shape files (*.fm)|*.fm";
            }
            else
            {
                fileloaddialog.Filter = "Ficheros de forma (*.fm)|*.fm";
            }
            if (fileloaddialog.ShowDialog() == DialogResult.OK)
            {
                string fichero = fileloaddialog.FileName;
                FileStream fs = new FileStream(fichero, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                List<float> lecturas = new List<float>();
                try
                {
                    //Obtain the list with the rules for a cell to remain alive length
                    int largoremainalive = br.ReadInt32();
                    //Fill in the list with the rules for a cell to remain alive
                    remainalive.Clear();
                    for (int i = 0; i < largoremainalive; i++)
                        remainalive.Add(br.ReadInt32());
                    //Obtain the list with the rules for a cell to born length
                    int largotoborn = br.ReadInt32();
                    //Fill in the list with the rules for a cell to born
                    toborn.Clear();
                    for (int i = 0; i < largotoborn; i++)
                        toborn.Add(br.ReadInt32());
                    //Obtain the living cells exterior line color by its rgb values
                    byte exteriorr = br.ReadByte();
                    byte exteriorg = br.ReadByte();
                    byte exteriorb = br.ReadByte();
                    exterior = Color.FromArgb(exteriorr, exteriorg, exteriorb);
                    //Obtain the living cells interior color by its rgb values
                    byte interiorr = br.ReadByte();
                    byte interiorg = br.ReadByte();
                    byte interiorb = br.ReadByte();
                    interior = Color.FromArgb(interiorr, interiorg, interiorb);
                    //Obtain the background color by its rgb values
                    byte backgroundr = br.ReadByte();
                    byte backgroundg = br.ReadByte();
                    byte backgroundb = br.ReadByte();
                    background = Color.FromArgb(backgroundr, backgroundg, backgroundb);
                    //Obtain the dead cells interior color by its rgb values
                    byte interiorMr = br.ReadByte();
                    byte interiorMg = br.ReadByte();
                    byte interiorMb = br.ReadByte();
                    interiorM = Color.FromArgb(interiorMr, interiorMg, interiorMb);
                    //Obtain the born cells interior color by its rgb values
                    byte interiorVr = br.ReadByte();
                    byte interiorVg = br.ReadByte();
                    byte interiorVb = br.ReadByte();
                    interiorV = Color.FromArgb(interiorVr, interiorVg, interiorVb);
                    //Load the points corresponding to the living cells
                    do
                    {
                        lecturas.Add(br.ReadSingle());
                    }
                    while (true);
                }
                catch (EndOfStreamException)
                {
                    fs.Close();
                    br.Close();
                }
                catch (IOException err)
                {
                    MessageBox.Show("File reading error." + err);
                }
                finally
                {
                    fs.Close();
                    br.Close();
                }

                for (int i = 0; i < lecturas.Count - 1; i += 2)
                    cells.Add(new PointF(lecturas[i], lecturas[i + 1]));

                cycles = (int)lecturas[lecturas.Count - 1];
                cellsant = new List<PointF>();
                // Show and hide the suitable controls
                rbDrawShape.Hide();
                lbPopulation.Show();
                lbCycles.Show();
                //To draw the cells
                DrawCells();
                //To show the display displacement and zoom controls
                ShowControls();
                // Change the color, label and Click event handler of the Accept button
                if (gamecounter == 0)
                {
                    btAccept.Click += btAccept_Click;
                }
                else
                {
                    btAccept.BackColor = System.Drawing.Color.Chartreuse;
                    btAccept.Text = "Aceptar";
                    btAccept.ForeColor = Color.Black;
                    btAccept.Click -= btPause_Click;
                    btAccept.Click += btAccept_Click;
                }
                btAccept.Visible = true;
                btAccept.Location = new Point(this.ClientSize.Width / 2, this.ClientSize.Height - btAccept.Size.Height - 5);
                gamecounter++;
                available = true;
                line = new Pen(exterior, 1);
                deadline = new Pen(background, 1);
                pbBackGround.Invalidate();
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 
        /// MENU CYCLE TIME CHANGE HANDLER.
        /// SHOWS THE NUMERIC UP & DOWN FOR MAKE POSSIBLE THE CYCLE TIME CHANGE
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void cyclechangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cyclechangeToolStripMenuItem.Checked)
            {
                nudCycle.Hide();
                lbNud.Hide();
                cyclechangeToolStripMenuItem.Checked = false;
            }
            else
            {
                nudCycle.Show();
                lbNud.Hide();
                cyclechangeToolStripMenuItem.Checked = true;
            }
        }

        /// <summary>
        /// 
        /// NUMERIC UP & DOWN VALUE CHANGED EVENT HANDLER.
        /// TO ASSIGN THE VALUE IN THE NUMERIC UP & DOWN TO THE CYCLE TIME VARIABLE
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void nudCycle_ValueChanged(object sender, EventArgs e)
        {
            cycle = (int)nudCycle.Value;
            timer1.Interval = cycle;
        }

        /// <summary>
        /// 
        /// SHOWS THE DIALOG FOR CHANGE THE RULES
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ruleschangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create the form
            dialogrules = new Form();
            dialogrules.Size = new Size(700, 300);
            dialogrules.Owner = this;
            dialogrules.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            if (english)
            {
                dialogrules.Text = "Rules by defect (23/3) change";
            }
            else
            {
                dialogrules.Text = "Cambio de las reglas por defecto (23/3)";
            }
            dialogrules.BackColor = Color.SeaGreen;
            dialogrules.Show();
            dialogrules.StartPosition = FormStartPosition.Manual;
            dialogrules.Location = new Point(this.Location.X + 10, this.Location.Y + 10);
            // Label for the quantity of adjacent cells for a cell to reamin alive TextBox title
            Label lbalive = new Label();
            dialogrules.Controls.Add(lbalive);
            lbalive.Font = new Font("Dejavu-Sans", 12, FontStyle.Underline);
            lbalive.AutoSize = true;
            lbalive.Visible = true;
            lbalive.Location = new Point(10, 20);
            if (english)
            {
                lbalive.Text = "Number of adjacent alive cells for a cell to remain alive.";
            }
            else
            {
                lbalive.Text = "Cantidad de células adyacentes para que una célula permanezca viva: ";
            }
            // TextBox for introduce the quantity of adjacent cells for a cell to remain alive
            tbalive = new TextBox();
            tbalive.Name = "tbalive";
            tbalive.AutoSize = false;
            dialogrules.Controls.Add(tbalive);
            tbalive.Font = new Font("Dejavu-Sans", 12);
            tbalive.TextAlign = HorizontalAlignment.Center;
            tbalive.Size = new Size(100, 25);
            tbalive.Location = new Point(lbalive.Location.X + lbalive.Width + 5, lbalive.Location.Y);
            tbalive.KeyPress += RuleBoxes_KeyPress;
            tbalive.Focus();
            // Label for the quantity of adjacent cells for a cell to born TextBox title
            Label lbborn = new Label();
            dialogrules.Controls.Add(lbborn);
            lbborn.Font = lbalive.Font;
            lbborn.AutoSize = true;
            lbborn.Visible = true;
            lbborn.Location = new Point(lbalive.Location.X, lbalive.Location.Y + lbalive.Height + 30);
            if (english)
            {
                lbborn.Text = "Number of adjacent alive cells for a cell to born";
            }
            else
            {
                lbborn.Text = "Cantidad de células adyacentes para que una célula nazca: ";
            }
            // TextBox for introduce the quantity of adjacent cells for a cell to born.
            tbborn = new TextBox();
            tbborn.Name = "tbborn";
            dialogrules.Controls.Add(tbborn);
            tbborn.Font = tbalive.Font;
            tbborn.TextAlign = HorizontalAlignment.Center;
            tbborn.Size = tbalive.Size;
            tbborn.Location = new Point(lbborn.Location.X + lbborn.Width + 5, lbborn.Location.Y);
            tbborn.KeyPress += RuleBoxes_KeyPress;
            //Label for the rules examples
            Label lbEjemplos = new Label();
            lbEjemplos.Font = new Font("Dejavu-Sans", 9);
            lbEjemplos.Location = new Point(lbborn.Location.X, lbborn.Location.Y + 40);
            lbEjemplos.AutoSize = true;
            lbEjemplos.Visible = true;
            dialogrules.Controls.Add(lbEjemplos);
            if (english)
            {
                lbEjemplos.Text = " Examples: \n 01234567 / 3 ---> Moderate growing.\n 23 / 36 ---> Hight life.\n 1357 / 1357 ---> Replicants.Fast growing.\n 235678 / 3678 ---> Diamonds, fast growing.\n 34 / 34 ---> Stable.\n 4 / 2 ---> Moderate growing.\n 51 / 346 ---> Average life. ";
            }
            else
            {
                lbEjemplos.Text = " Ejemplos: \n 01234567 / 3 ---> Crecimiento moderado.\n 23 / 36 ---> Hight life.\n 1357 / 1357 ---> Replicantes.Crecimiento rápido.\n 235678 / 3678 ---> Rombos, crecimiento rápido.\n 34 / 34 ---> Estable.\n 4 / 2 ---> Crecimiento moderado.\n 51 / 346 ---> Vida media. ";
            }
        }

        /// <summary>
        ///  
        /// HANDLER TO CONSTRAIN THE INTRODUCED CHARACTERS TO DIGITS FROM 0 TO 8 AND ENSURE THAT THE
        /// INTRODUCED TEXT IS NO LONGER THAN 8
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RuleBoxes_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox caja = (TextBox)sender;
            if (caja.Text.Length < 8 && e.KeyChar >= '0' && e.KeyChar <= '8')
            {
                e.Handled = false;
            }
            else if (e.KeyChar == 8) // Backspace key
                e.Handled = false;
            else if (e.KeyChar == 13)// Enter key
            {
                if (caja.Text.Length == 0)
                {
                    if (english)
                    {
                        MessageBox.Show("A value must be input.");
                    }
                    else
                    {
                        MessageBox.Show("Hay que introducir algun valor.");
                    }
                    e.Handled = true;
                }
                else if (caja.Name == "tbalive")
                {
                    tbborn.Focus();
                    e.Handled = true;
                }
                else if (caja.Name == "tbborn")
                {
                    alive = tbalive.Text;
                    born = tbborn.Text;
                    // Update the list with the quantities of adjacent cells for a cell to remain alive according to the rules
                    remainalive.Clear();
                    for (int i = 0; i < alive.Length; i++)
                    {
                        remainalive.Add(Int32.Parse(alive[i].ToString()));
                    }
                    // Update the list with the quantities of adjacent cells for a cell to born according the rules
                    toborn.Clear();
                    for (int i = 0; i < born.Length; i++)
                    {
                        toborn.Add(Int32.Parse(born[i].ToString()));
                    }
                    if (english)
                    {
                        this.Text = "OrgaSnismo0      ( Daniel Santos version )                      ( Remain alive: " + alive + "  Born: " + born + " )";
                    }
                    else
                    {
                        this.Text = "OrgaSnismo0      ( Daniel Santos version )                      ( Mantenerse viva: " + alive + "  Nacer: " + born + " )";
                    }
                    dialogrules.Close();
                }
            }
            else if (caja.Text.Length >= 8)
            {
                if (english)
                {
                    MessageBox.Show("8 digits is the maximum allowed");
                }
                else
                {
                    MessageBox.Show("No se pueden introducir más de 8 digitos");
                }
                e.Handled = true;
            }
            else
            {
                e.Handled = true;
            }
        }


        /// <summary>
        /// 
        /// MAKES SOUND THE TUNE AT THE PROGRAM CLOSING
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            SoundPlayer reproductor = new SoundPlayer();
            reproductor.SoundLocation = Directory.GetCurrentDirectory() + "\\Rafaga-Organismo-Salida.wav";
            reproductor.Play();
            // Pause the game in order to give time for the tune to sound
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(400);
            }
            this.Dispose();
            System.Environment.Exit(0);
        }

        ////////////////////////////
        ///
        ///  TO COME BACK TO THE BY DEFECT RULES WHEN THE MENU OPTION IS CLICKED
        ///
        ////////////////////////////////////////////
        ///
        private void backtodefectvaluesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alive = "23";
            born = "3";
            //Reset the list of adjacent cells quantity for a cell to remain alive and the list of
            //adjacent cells quatity for a cell to born
            remainalive.Clear();
            toborn.Clear();
            //Fill in the before lists with the by defect values
            foreach (char c in alive)
            {
                remainalive.Add(Int32.Parse(c.ToString()));
            }
            foreach (char c in born)
            {
                toborn.Add(Int32.Parse(c.ToString()));
            }
            //Update the Form title with the by defect values
            if (english)
            {
                this.Text = "OrgaSnismo0      ( Daniel Santos edition )                      ( Remain alive: " + alive + "  Born: " + born + " )";
            }
            else
            {
                this.Text = "OrgaSnismo0      ( Daniel Santos edition )                      ( Mantenerse viva: " + alive + "  Nacer: " + born + " )";
            }
            exterior = Color.Red; //By defect cell exterior line color
            line = new Pen(exterior, 1);
            interior = Color.FromArgb(155, 250, 5); // By defect alive cell interior color
            interiorV = Color.FromArgb(155, 250, 5); // By defect born cell interior color
            background = Color.Black; // By defect background color
            graphic.Clear(background);
            interiorM = Color.Black; // By defect dead cell interior color
            deadline.Color = background;
            //Redraw the shape
            DrawCells();
        }

        /// <summary>
        /// 
        /// SHOWS THE DISPLACEMENT AND ZOOM BUTTONS
        /// 
        /// </summary>

        private void ShowControls()
        {
            btZoomPlus.Visible = true;
            btZoomMinus.Visible = true;
            zoombuttonsToolStripMenuItem.Checked = true;

            btLeft.Visible = true;
            btRight.Visible = true;
            btUp.Visible = true;
            btDown.Visible = true;
            displacementbuttonsToolStripMenuItem.Checked = true;

            nudCycle.Visible = true;
            lbNud.Visible = true;
            cyclechangeToolStripMenuItem.Checked = true;
        }


        /// <summary>
        /// 
        /// HIDES THE DISPLACEMENT AND ZOOM BUTTONS
        /// 
        /// </summary>

        private void HideControls()
        {
            btZoomPlus.Visible = false;
            btZoomMinus.Visible = false;
            zoombuttonsToolStripMenuItem.Checked = false;

            btLeft.Visible = false;
            btRight.Visible = false;
            btUp.Visible = false;
            btDown.Visible = false;
            displacementbuttonsToolStripMenuItem.Checked = false;

            nudCycle.Visible = false;
            lbNud.Visible = false;
            cyclechangeToolStripMenuItem.Checked = false;
        }

        /////////////////////////
        ///
        /// HANDLER FOR THE PAUSE BUTTON CLICK
        ///
        /////////////////////////////////////////////
        ///
        private void btPause_Click(object sender, EventArgs e)
        {
            if (ongoing)
            {
                btAccept.BackColor = Color.Chartreuse;
                btAccept.ForeColor = Color.Black;
                if (english)
                {
                    btAccept.Text = "Continue";
                }
                else
                {
                    btAccept.Text = "Continuar";
                }
                ongoing = false;

                timer1.Stop();
            }
            else
            {
                btAccept.BackColor = Color.Black;
                if (english)
                {
                    btAccept.Text = "Pause";
                }
                else
                {
                    btAccept.Text = "Pausa";
                }
                btAccept.ForeColor = Color.Silver;
                ongoing = true;
                timer1.Start();
            }
        }

        /// <summary>
        /// 
        /// SHOWS THE DIALOG FOR CHANGE THE COLORS
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorchangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogcolors = new Form();
            dialogcolors.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            dialogcolors.BackColor = Color.SeaGreen;
            dialogcolors.Show();
            dialogcolors.Owner = this;
            dialogcolors.AutoSize = false;
            if (english)
            {
                dialogcolors.Text = "Color change.";
            }
            else
            {
                dialogcolors.Text = "Cambiar Colores.";
            }
            lbColorline = new Label();
            lbColorline.Visible = true;
            if (english)
            {
                lbColorline.Text = "Line color.";
            }
            else
            {
                lbColorline.Text = "Color de la línea.";
            }
            lbColorline.Font = new Font("Dejavu-Sans", 16, FontStyle.Bold);
            dialogcolors.Controls.Add(lbColorline);
            lbColorline.Location = new Point(10, 15);
            lbColorline.BackColor = Color.SeaGreen;
            lbColorline.AutoSize = true;
            lbColorline.ForeColor = exterior;
            lbColorline.Click += LineColorChange;

            lbColorInterior = new Label();
            lbColorInterior.Visible = true;
            if (english)
            {
                lbColorInterior.Text = "Alive cells color.";
            }
            else
            {
                lbColorInterior.Text = "Color de las células vivas.";
            }
            lbColorInterior.Font = new Font("Dejavu-Sans", 16, FontStyle.Bold);
            dialogcolors.Controls.Add(lbColorInterior);
            lbColorInterior.Location = new Point(lbColorline.Location.X, lbColorline.Location.Y + lbColorline.Height + 10);
            lbColorInterior.BackColor = Color.SeaGreen;
            lbColorInterior.AutoSize = true;
            lbColorInterior.ForeColor = interior;
            lbColorInterior.Click += InteriorColorChange;

            lbColorInteriorV = new Label();
            lbColorInteriorV.Visible = true;
            if (english)
            {
                lbColorInteriorV.Text = "Born cells color.";
            }
            else
            {
                lbColorInteriorV.Text = "Color de las nuevas células en el turno.";
            }
            lbColorInteriorV.Font = new Font("Dejavu-Sans", 16, FontStyle.Bold);
            dialogcolors.Controls.Add(lbColorInteriorV);
            lbColorInteriorV.Location = new Point(lbColorline.Location.X, lbColorInterior.Location.Y + lbColorline.Height + 10);
            lbColorInteriorV.BackColor = Color.SeaGreen;
            lbColorInteriorV.AutoSize = true;
            lbColorInteriorV.ForeColor = interiorV;
            lbColorInteriorV.Click += InteriorColorChangeV;

            lbColorInteriorM = new Label();
            lbColorInteriorM.Visible = true;
            if (english)
            {
                lbColorInteriorM.Text = "Dead cells color.";
            }
            else
            {
                lbColorInteriorM.Text = "Color de las células muertas en el turno.";
            }
            lbColorInteriorM.Font = new Font("Dejavu-Sans", 16, FontStyle.Bold);
            dialogcolors.Controls.Add(lbColorInteriorM);
            lbColorInteriorM.Location = new Point(lbColorline.Location.X, lbColorInteriorV.Location.Y + lbColorline.Height + 10);
            lbColorInteriorM.BackColor = Color.SeaGreen;
            lbColorInteriorM.AutoSize = true;
            lbColorInteriorM.ForeColor = interiorM;
            lbColorInteriorM.Click += InteriorColorChangeM;

            lbColorbackground = new Label();
            lbColorbackground.Visible = true;
            if (english)
            {
                lbColorbackground.Text = "Background color.";
            }
            else
            {
                lbColorbackground.Text = "Color del fondo.";
            }
            lbColorbackground.Font = new Font("Dejavu-Sans", 16, FontStyle.Bold);
            dialogcolors.Controls.Add(lbColorbackground);
            lbColorbackground.Location = new Point(lbColorline.Location.X, lbColorInteriorM.Location.Y + lbColorInterior.Height + 10);
            lbColorbackground.BackColor = Color.SeaGreen;
            lbColorbackground.AutoSize = true;
            lbColorbackground.ForeColor = background;
            lbColorbackground.Click += BackgroundColorChange;

            dialogcolors.Size = new Size(lbColorInteriorM.Width + 100, 270);
        }

        //////////////////////////////////////
        ///
        /// HANDLER FOR THE LIVING CELLS EXTERIOR LINE COLOR CHANGE
        ///
        //////////////////////////////////////////////////
        ///
        private void LineColorChange(object sender, EventArgs e)
        {
            ColorDialog dialogo = new ColorDialog();
            if (dialogo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                exterior = dialogo.Color;
                line = new Pen(exterior, 1);
                lbColorline.ForeColor = exterior;
            }
        }

        //////////////////////////////
        ///
        ///	HANDLER FOR THE LIVING CELLS INTERIOR COLOR CHANGE
        ///
        ////////////////////////////////////////////
        ///
        private void InteriorColorChange(object sender, EventArgs e)
        {
            ColorDialog dialogo = new ColorDialog();
            if (dialogo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                interior = dialogo.Color;
                lbColorInterior.ForeColor = interior;
            }

        }

        ////////////////////////////////////
        ///
        /// HANDLER FOR THE BORN CELLS INTERIOR COLOR CHANGE
        ///
        ///////////////////////////////////////////////
        ///
        private void InteriorColorChangeV(object sender, EventArgs e)
        {
            ColorDialog dialogo = new ColorDialog();
            if (dialogo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                interiorV = dialogo.Color;
                lbColorInteriorV.ForeColor = interiorV;
            }

        }

        ///////////////////////////////////
        ///
        /// HANDLER FOR THE DEAD CELLS INTERIOR COLOR CHANGE
        ///
        /////////////////////////////////////////////////
        ///
        private void InteriorColorChangeM(object sender, EventArgs e)
        {
            ColorDialog dialogo = new ColorDialog();
            if (dialogo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                interiorM = dialogo.Color;
                lbColorInteriorM.ForeColor = interiorM;
            }

        }

        ////////////////////////////
        ///
        ///	HANDLER FOR THE BACKGROUND COLOR CHANGE
        ///
        ///////////////////////////////////////////
        ///
        private void BackgroundColorChange(object sender, EventArgs e)
        {
            ColorDialog dialogo = new ColorDialog();
            if (dialogo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                background = dialogo.Color;
                interiorM = background;
                deadline.Color = background;
                lbColorbackground.ForeColor = background;
            }
            graphic.Clear(background);
        }

        /// <summary>
        /// 
        /// DRAWS THE NEW AND DEAD CELLS
        /// 
        /// </summary>
        /// 
        private void DrawCells()
        {
            try
            {
                graphic.Clear(background);
                lock (cells)
                {
                    foreach (PointF p in cells)
                    {
                        graphic.FillRectangle(new SolidBrush(interior), p.X, p.Y, cellsize, cellsize);
                        graphic.DrawRectangle(line, p.X, p.Y, cellsize, cellsize);
                    }
                }

                foreach (PointF p in deadant) // To draw the dead cells with the background color
                {
                    graphic.FillRectangle(new SolidBrush(background), p.X, p.Y, cellsize, cellsize);
                    graphic.DrawRectangle(deadline, p.X, p.Y, cellsize, cellsize);
                }

                foreach (PointF p in aliveant) // Draw the remaining cells
                {
                    graphic.FillRectangle(new SolidBrush(interior), p.X, p.Y, cellsize, cellsize);
                    graphic.DrawRectangle(line, p.X, p.Y, cellsize, cellsize);
                }

                foreach (PointF p in nextdead) // Draw the new dead cells with the selected interior color
                {
                    graphic.FillRectangle(new SolidBrush(interiorM), p.X, p.Y, cellsize, cellsize);
                    graphic.DrawRectangle(deadline, p.X, p.Y, cellsize, cellsize);
                }
                foreach (PointF p in nextalive) // Draw the born cells with the selected interior color
                {
                    graphic.FillRectangle(new SolidBrush(interiorV), p.X, p.Y, cellsize, cellsize);
                    graphic.DrawRectangle(line, p.X, p.Y, cellsize, cellsize);
                }

                pbBackGround.Invalidate();
            }
            catch (InvalidOperationException)
            {
                return;
            }
        }

        ///////////////////////////////
        ///
        /// SHOWS THE EGO LABEL WHEN THE MOUSE IS OVER IT
        ///
        ////////////////////////////////////////
        ///
        private void lbEgo_MouseEnter(object sender, EventArgs e)
        {
            lbEgo.BackColor = Color.SeaGreen;
            lbEgo.ForeColor = Color.Black;
        }

        ///////////////////////////////
        ///
        /// HIDES THE EGO LABEL WHEN THE MOUSE LEAVES IT
        ///
        ////////////////////////////////////////
        ///
        private void lbEgo_MouseLeave(object sender, EventArgs e)
        {
            lbEgo.BackColor = background;
            lbEgo.ForeColor = background;
        }

        ///////////////////////////
        ///
        ///	ENABLES OR DISABLES THE BEEP OPTION WHEN THE OPTION IS CLICKED AT THE MENU
        ///
        ///////////////////////////////////////
        ///
        private void beepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!beep)
            {
                beepToolStripMenuItem.Checked = true;
                beep = true;
            }
            else
            {
                beepToolStripMenuItem.Checked = false;
                beep = false;
            }
        }
        /// <summary>
        /// 
        /// SAVES A BMP IMAGE INTO THE USER SELECTED FOLDER WHEN THE MENU OPTION IS CLICKED
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void saveimageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialogo = new SaveFileDialog();
            dialogo.Filter = "Bipmap (*.bmp)|*.bmp";
            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                string ruta = dialogo.FileName;
                bitmap.Save(ruta);
            }
        }

        /// <summary>
        /// 
        /// ENABLES THE AUTOMATIC IMAGE SAVING OPTION THAT SAVES A BMP IMAGE FILE AT EVERY CYCLE. THE FILE
        /// IS SAVED AT THE USER SELECTED FOLDER. THE FILE NAME, IS THE ONE CHOOSEN BY THE USER WITH THE CYCLE
        /// NUMBER ADDED
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoimagesavingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!autosaving)
            {
                SaveFileDialog dialogo = new SaveFileDialog();
                dialogo.Filter = "Bitmap: (*.bmp)|*.bmp";
                if (dialogo.ShowDialog() == DialogResult.OK)
                {
                    prefix = dialogo.FileName;
                }
                autoimagesavingToolStripMenuItem.Checked = true;
                autosaving = true;
            }
            else
            {
                autosaving = false;
                autoimagesavingToolStripMenuItem.Checked = false;
            }
        }
        /// <summary>
        /// 
        /// CHANGES THE MENU , INSTRUCTIONS AND CONTROLS LABELS TO SPANISH LANGUAGE
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void catellanoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            english = false;
            rbDrawShape.Text = "Dibujar foma inicial.";
            lbPopulation.Text = "Población: ";
            lbCycles.Text = "Ciclos: ";
            lbNud.Text = "Milisegundos:";
            btAccept.Text = "Aceptar";
            MenuFile.Text = "Archivo";
            newToolStripMenuItem.Text = "Nuevo";
            SaveToolStripMenuItem.Text = "Guardar";
            loadToolStripMenuItem.Text = "Cargar";
            saveimageToolStripMenuItem.Text = "Guardar imagen";
            zoomToolStripMenuItem.Text = "Controles";
            displacementbuttonsToolStripMenuItem.Text = "Botones de desplazamiento";
            zoombuttonsToolStripMenuItem.Text = "Botones de zoom";
            cyclechangeToolStripMenuItem.Text = "Cambiar Periodo";
            optionsToolStripMenuItem.Text = "Opciones";
            ruleschangeToolStripMenuItem.Text = "Cambiar Reglas";
            backtodefectvaluesToolStripMenuItem.Text = "Volver a valores por defecto";
            colorchangeToolStripMenuItem.Text = "Cambiar Colores";
            autoimagesavingToolStripMenuItem.Text = "Guardado automático de imágenes";
            languageToolStripMenuItem.Text = "Idioma";
            catellanoToolStripMenuItem.Text = "Castellano";
            englishToolStripMenuItem.Text = "Inglés";
            helpToolStripMenuItem.Text = "Ayuda";
        }

        /// <summary>
        /// 
        /// CHANGES THE MENU , INSTRUCTIONS AND CONTROLS LABELS TO ENGLISH LANGUAGE
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            english = true;
            rbDrawShape.Text = "Draw initial shape.";
            lbPopulation.Text = "Population: ";
            lbCycles.Text = "Cycles: ";
            lbNud.Text = "Milliseconds: ";
            btAccept.Text = "Accept";
            MenuFile.Text = "File";
            newToolStripMenuItem.Text = "New";
            SaveToolStripMenuItem.Text = "Save";
            loadToolStripMenuItem.Text = "Load";
            saveimageToolStripMenuItem.Text = "Save image";
            zoomToolStripMenuItem.Text = "Controls";
            displacementbuttonsToolStripMenuItem.Text = "Displacement buttons";
            zoombuttonsToolStripMenuItem.Text = "Zoom buttons";
            cyclechangeToolStripMenuItem.Text = "Change Period";
            optionsToolStripMenuItem.Text = "Options";
            ruleschangeToolStripMenuItem.Text = "Change Rules";
            backtodefectvaluesToolStripMenuItem.Text = "Back to default values";
            colorchangeToolStripMenuItem.Text = "Change Colors";
            autoimagesavingToolStripMenuItem.Text = "Automatic image saving";
            languageToolStripMenuItem.Text = "Language";
            catellanoToolStripMenuItem.Text = "Spanish";
            englishToolStripMenuItem.Text = "English";
            helpToolStripMenuItem.Text = "Help";
        }

        /// <summary>
        /// 
        /// SHOWS THE FORM WITH THE GAME INSTRUCTIONS
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ayuda = new Form();
            ayuda.Size = new Size(850, 500);
            ayuda.BackColor = Color.SeaGreen;
            ayuda.MaximizeBox = false;
            ayuda.FormBorderStyle = FormBorderStyle.FixedDialog;
            if (english)
            {
                ayuda.Text = "OrgaSnismo0 functions.";
            }
            else
            {
                ayuda.Text = "Funciones de OrgaSnismo0";
            }
            RichTextBox rtbAyuda = new RichTextBox();
            rtbAyuda.BackColor = Color.SeaGreen;
            rtbAyuda.Size = ayuda.Size;
            ayuda.Controls.Add(rtbAyuda);
            ayuda.AutoScroll = true;
            if (english)
            {
                rtbAyuda.Text = "Basic operation:\n\n";
                int rotulo1 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "OrgaSnismo0, is based in the Game of life, designed by the mathematician John Conway.\nThere´s a extensive information in Wikipedia.\n\n";
                rtbAyuda.Text += "When the game is started, the menú bar, wich functions are commented later, and a RadioButton with the label: 'Draw intial shape' are shown.\nA dialog for to change the size of the grid shown then, appears by clicking this radioButton. The grid size is 30 pixels by defect.\n\n";
                rtbAyuda.Text += "Clicking over the Accept button at the grid size dialog, make the following elements to appear:\n\n";
                int normal = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Grid: ";
                int concepto1 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "A new yellow color cell, is created every time that one click over a grid square.\n\tA before created cell can be deleted just clicking over it.\n\n";
                int normal2 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Numeric up & down: ";
                int concepto2 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "With the label: 'Milliseconds', allows to change the cycle time set in 1 second by defect.\n\t\t\t      If the time needed by the cpu to calculate the next state, is bigger than the cycle time\n\t\t\t      settled down, this is automaticaly changed by the program.\n\n";
                int normal3 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Accept button: ";
                int concepto4 = rtbAyuda.Text.Length;
                rtbAyuda.Text += " To start the cycles once the initial shape has benn drawn or loaded and the cycle time \n\t\t        stablished.\n\n";
                rtbAyuda.Text += "Once the game is started, the shape is going changing according to the rules at every new cycle.\nThe rules by defect, are the Conway´s rules i.e.:\nA cell remains alive, if it is adyacent to two or three alive cells, otherwise the cell dies at the next cycle.\nA cell borns, if it is adyacent to three alive cells.\n\n";
                rtbAyuda.Text += "At the screen bottom left, the population ( number of alive cells ) and the number of cycles passed from the start, are shown.\nAlso buttons for: Zoom, displacement and pause, appear.\n";
                rtbAyuda.Text += "If the shape stabilizes or disappears, a message box is shown and the game is over.\n\n";
                int normal4 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Menu Bar: \n\n";
                int rotulo2 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "File:\n";
                int concepto5 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "New: To start a new game. The rules and colors before stablished are kept.\n\n";
                rtbAyuda.Text += "Save: Saves the current shape, rules and colors in a *.fm file.\n\n";
                rtbAyuda.Text += "Load: Loads a shape with the rules and colors previously saved in a *.fm file.\n\n";
                rtbAyuda.Text += "Save image: Saves a screen shot in a *.bmp file.";
                int normal5 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "\n\nControls:\n";
                int rotulo3 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Displacememt buttons: To show or hide the displacement buttons.\n\n";
                rtbAyuda.Text += "Zoom buttons: To show or hide the zoom buttons\n\n";
                rtbAyuda.Text += "Change period: To show or hide the numeric up & down with the cycle time.\n\n";
                int normal6 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Options:\n";
                int rotulo4 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Change rules: A window appers and allow to change the rules.\n\t\t  There are some rules examples with a brief description about their respective efects, at the \n\t\t  bottom of this window.\n\n";
                rtbAyuda.Text += "Back to default values: Returns to the rules, colors and cycle time by defect.\n\n";
                rtbAyuda.Text += "Change colors: Shows a dialog with the following sentences:\n\t\t     Line color, Alive cells color, Born cells color , dead cells color and Background color.\n\t\t     The color of every sentence corresponds to the color of every concept. Clicking over a \n\t\t     sentence, makes a color selection dialog to appear.\n\n";
                rtbAyuda.Text += "Beep: Activated by defect, makes a beep to sound at every cycle. It is useful in order to advice from a new \n\tscreen redraw in a long time cycles.\n\n";
                rtbAyuda.Text += "Automatic image saving: When it is activated, a directory chooser dialog is shown in order to choose the\t\t\t\t       directory where a screen shot *.bmp image is saved at every cycle.\n\n";
                rtbAyuda.Text += "Language: Spanish and English languages are available.\n\n";
                int normal7 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Help: ";
                int rotulo5 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Makes this window to appear.";
                int normal8 = rtbAyuda.Text.Length;

                rtbAyuda.Select(0, rotulo1);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 17, FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
                rtbAyuda.Select(rotulo1, normal);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 13, FontStyle.Regular);
                rtbAyuda.Select(normal, concepto1);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 13, FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
                rtbAyuda.Select(concepto1, normal2);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 13, FontStyle.Regular);
                rtbAyuda.Select(normal2, concepto2);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 13, FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
                rtbAyuda.Select(concepto2, normal3);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 13, FontStyle.Regular);
                rtbAyuda.Select(normal3, concepto4);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 13, FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
                rtbAyuda.Select(concepto4, normal4);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 13, FontStyle.Regular);
                rtbAyuda.Select(normal4, rotulo2);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 17, FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
                rtbAyuda.Select(rotulo2, concepto5);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 13, FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
                rtbAyuda.Select(concepto5, normal5);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 13, FontStyle.Regular);
                rtbAyuda.Select(normal5, rotulo3);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 13, FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
                rtbAyuda.Select(rotulo3, normal6);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 13, FontStyle.Regular);
                rtbAyuda.Select(normal6, rotulo4);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 13, FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
                rtbAyuda.Select(rotulo4, normal7);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 13, FontStyle.Regular);
                rtbAyuda.Select(normal7, rotulo5);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 13, FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
                rtbAyuda.Select(rotulo5, normal8);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 13, FontStyle.Regular);

                rtbAyuda.DeselectAll();
                rtbAyuda.AutoScrollOffset.Offset(0, 0);
            }
            else
            {
                rtbAyuda.Text = "Funcionamiento básico:\n\n";
                int rotulo1 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "OrgaSnismo0, está basado en el juego de la vida, diseñado por el matemático John Conway.\nPuede encontrarse extensa información en Wikipedia.\n\n";
                rtbAyuda.Text += "Cuando se inicia el programa, se muestra la barra de menu, cuyas opciones se comentan más adelante, y el RadioButton 'Dibujar forma inicial'.\nSeleccionando este RadioButton, aparece un cuadro de dialogo para cambiar el tamaño de la rejilla que se mostrará a continuación. Por defecto, el tamaño es 30.\n\n";
                rtbAyuda.Text += "Al pulsar el botón 'Aceptar' del dialogo anterior, se muestran los siguientes elementos:\n\n";
                int normal = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Rejilla: ";
                int concepto1 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Pulsando sobre un cuadrado, se crea una nueva célula, la cual se muestra en color amarillo.\n\t    Para borrar una célula, basta con volver a pulsar sobre ella.\n\n";
                int normal2 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Numeric up & down: ";
                int concepto2 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Con el rotulo: 'Milisegundos', permite cambiar el tiempo de ciclo, establecido por \n\t\t\t      defecto, en 1 segundo.\n\t\t\t      Si el tiempo que la cpu necesita para calcular el siguiente estado, es mayor que \n\t\t\t      el tiempo de ciclo establecido, este último es cambiado automáticamente por el \t\t\t\t      programa.\n\n";
                int normal3 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Botón Aceptar: ";
                int concepto4 = rtbAyuda.Text.Length;
                rtbAyuda.Text += " Para comenzar los ciclos una vez que se ha dibujado la forma inicial deseada y \n\t\t\tajustado el tiempo de ciclo en su caso.\n\n";
                rtbAyuda.Text += "Una vez iniciado el juego, la forma dibujada irá cambiando en base a las reglas.\nPor defecto, las reglas son las establecidas por Conway, es decir:\nUna célula se mantiene viva, si toca con dos o tres células vivas, en otro caso muere en el siguiente ciclo.\nUna célula nace, si toca con tres células vivas.\n\n";
                rtbAyuda.Text += "En la esquina superior izquierda, se muestra la población ( número de células vivas) y la cantidad de ciclos transcurrida desde el inicio.\nTambién aparecen los botones de desplamiento, zoom y pausa.\n";
                rtbAyuda.Text += "Si la forma se estabiliza o desaparece, se muestra un cuadro de dialogo y el juego finaliza.\n\n";
                int normal4 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Barra de menú: \n";
                int rotulo2 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "MenuFile:\n\n";
                int concepto5 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Nuevo: inicia un nuevo juego. Se mantendrán las reglas y colores que se hayan establecido anteriormente.\n\n";
                rtbAyuda.Text += "Guardar: Guarda la forma actual con los colores y reglas establecidas, dentro del directorio elegido, en un \t     fichero *.fm.\n\n";
                rtbAyuda.Text += "Cargar: Carga una forma previamente guardada en un fichero *.fm, con las reglas y colores que tuviese la \t   forma en el momento de guardarla.\n\n";
                rtbAyuda.Text += "Guardar imagen: Guarda una imagen *.bmp de la forma actual en el directorio elegido.";
                int normal5 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "\n\nControles:\n\n";
                int rotulo3 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Botones de desplazamiento: Para ocultar o visulizar los botones de desplazamiento.\n\n";
                rtbAyuda.Text += "Botones zoom: Para ocultar o visulizar los botones de zoom\n\n";
                rtbAyuda.Text += "Cambiar periodo: Para ocultar o visualizar el Numeric up & down con el tiempo ciclo.\n\n";
                int normal6 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Opciones:\n\n";
                int rotulo4 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Cambiar reglas: Muestra un cuadro de dialogo que permite introducir la cantidad de células adyacentes para \t\t\t      que una celula se mantenga viva ( por defecto 2 o 3 ) y la cantidad de células adyacentes \t\t\t      para que una célula nazca ( por defecto 3 ).\n\t\t      En la parte inferior de este cuadro de dialogo, se muestran algunos ejemplos y los efectos\t\t\t      que producen.\n\n";
                rtbAyuda.Text += "Volver a valores por defecto: Vuelve a establecer las reglas, colores y tiempo de ciclo por defecto.\n\n";
                rtbAyuda.Text += "Cambiar colores: Muestra un cuadro de dialogo donde aparecen las frases:\n\t\t       Color de la línea, Color de las células alives, Color de las nuevas células en el turno , Color\t\t\t        de las células muertas en el turno y Color del fondo.\n\t\t        El color de letra de cada una de estas frases, corresponde al color establecido para cada\t\t\t        concepto. Haciendo click sobre estas frases, hace que se muestre una tabla de colores \t\t\t        para elegir un nuevo color.\n\n";
                rtbAyuda.Text += "Beep: Activado por defecto, hace que suene un beep al inicio de cada ciclo. Es útil para avisar del redibujado \tde un nuevo estado, en poblaciones grandes ( más de 1000 ).\n\n";
                rtbAyuda.Text += "Guardado automático de imágenes: Al activarlo, se muestra un cuadro de dialogo para elegir el directorio \t\t\t\t\t\t      donde se guardará una imagen bmp de la pantalla en cada nuevo ciclo.\n\n";
                rtbAyuda.Text += "Idioma: Se puede elegir entre Castellano o Inglés\n\n";
                int normal7 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Ayuda: ";
                int rotulo5 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Hace que se muestre este cuadro de dialogo.";
                int normal8 = rtbAyuda.Text.Length;

                rtbAyuda.Select(0, rotulo1);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 17, FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
                rtbAyuda.Select(rotulo1, normal);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 13, FontStyle.Regular);
                rtbAyuda.Select(normal, concepto1);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans0", 13, FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
                rtbAyuda.Select(concepto1, normal2);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans0", 13, FontStyle.Regular);
                rtbAyuda.Select(normal2, concepto2);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans0", 13, FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
                rtbAyuda.Select(concepto2, normal3);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans0", 13, FontStyle.Regular);
                rtbAyuda.Select(normal3, concepto4);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans0", 13, FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
                rtbAyuda.Select(concepto4, normal4);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans0", 13, FontStyle.Regular);
                rtbAyuda.Select(normal4, rotulo2);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 17, FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
                rtbAyuda.Select(rotulo2, concepto5);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans0", 13, FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
                rtbAyuda.Select(concepto5, normal5);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans0", 13, FontStyle.Regular);
                rtbAyuda.Select(normal5, rotulo3);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 13, FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
                rtbAyuda.Select(rotulo3, normal6);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans0", 13, FontStyle.Regular);
                rtbAyuda.Select(normal6, rotulo4);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 13, FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
                rtbAyuda.Select(rotulo4, normal7);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans0", 13, FontStyle.Regular);
                rtbAyuda.Select(normal7, rotulo5);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans", 13, FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
                rtbAyuda.Select(rotulo5, normal8);
                rtbAyuda.SelectionFont = new Font("Dejavu-Sans0", 13, FontStyle.Regular);

                rtbAyuda.DeselectAll();
            }
            rtbAyuda.Select(0, 0);
            ayuda.Show();
        }
    }
}
