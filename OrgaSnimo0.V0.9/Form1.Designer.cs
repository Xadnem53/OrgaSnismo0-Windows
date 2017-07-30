namespace Organismo0
{

    partial class Form1
    {

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pbBackGround = new System.Windows.Forms.PictureBox();
            this.rbDrawShape = new System.Windows.Forms.RadioButton();
            this.rbFicticio = new System.Windows.Forms.RadioButton();
            this.lbPopulation = new System.Windows.Forms.Label();
            this.lbCycles = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btAccept = new System.Windows.Forms.Button();
            this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveimageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displacementbuttonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoombuttonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cyclechangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ruleschangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backtodefectvaluesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorchangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoimagesavingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.catellanoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btLeft = new System.Windows.Forms.Button();
            this.btUp = new System.Windows.Forms.Button();
            this.btRight = new System.Windows.Forms.Button();
            this.btDown = new System.Windows.Forms.Button();
            this.btZoomPlus = new System.Windows.Forms.Button();
            this.btZoomMinus = new System.Windows.Forms.Button();
            this.nudCycle = new System.Windows.Forms.NumericUpDown();
            this.lbNud = new System.Windows.Forms.Label();
            this.lbEgo = new System.Windows.Forms.Label();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbBackGround
            // 
            this.pbBackGround.BackColor = System.Drawing.Color.Black;
            this.pbBackGround.Location = new System.Drawing.Point(0, 25);
            this.pbBackGround.Margin = new System.Windows.Forms.Padding(4);
            this.pbBackGround.Name = "pbBackGround";
            this.pbBackGround.Size = this.ClientSize;
            this.pbBackGround.TabIndex = 0;
            this.pbBackGround.TabStop = false;
            // 
            // rbDrawShape
            // 
            this.rbDrawShape.AutoSize = true;
            this.rbDrawShape.BackColor = System.Drawing.Color.Black;
            this.rbDrawShape.Font = new System.Drawing.Font("DejaVu Sans", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDrawShape.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.rbDrawShape.Location = new System.Drawing.Point(64, 100);
            this.rbDrawShape.Margin = new System.Windows.Forms.Padding(4);
            this.rbDrawShape.Name = "rbDrawShape";
            this.rbDrawShape.Size = new System.Drawing.Size(282, 32);
            this.rbDrawShape.TabIndex = 1;
            this.rbDrawShape.Text = "Draw initial shape.";
            this.rbDrawShape.UseVisualStyleBackColor = false;
            this.rbDrawShape.CheckedChanged += new System.EventHandler(this.rbDrawShape_Click);
            // 
            // rbFicticio
            // 
            this.rbFicticio.Checked = true;
            this.rbFicticio.Location = new System.Drawing.Point(0, 0);
            this.rbFicticio.Margin = new System.Windows.Forms.Padding(4);
            this.rbFicticio.Name = "rbFicticio";
            this.rbFicticio.Size = new System.Drawing.Size(139, 30);
            this.rbFicticio.TabIndex = 9;
            this.rbFicticio.TabStop = true;
            this.rbFicticio.Visible = false;
            // 
            // lbPopulation
            // 
            this.lbPopulation.AutoSize = true;
            this.lbPopulation.BackColor = System.Drawing.Color.Black;
            this.lbPopulation.Font = new System.Drawing.Font("DejaVu Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPopulation.ForeColor = System.Drawing.Color.DimGray;
            this.lbPopulation.Location = new System.Drawing.Point(16, 73);
            this.lbPopulation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPopulation.Name = "lbPopulation";
            this.lbPopulation.Size = new System.Drawing.Size(136, 24);
            this.lbPopulation.TabIndex = 5;
            this.lbPopulation.Text = "Population: ";
            this.lbPopulation.Visible = false;
            // 
            // lbCycles
            // 
            this.lbCycles.AutoSize = true;
            this.lbCycles.BackColor = System.Drawing.Color.Black;
            this.lbCycles.Font = new System.Drawing.Font("DejaVu Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCycles.ForeColor = System.Drawing.Color.DimGray;
            this.lbCycles.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lbCycles.Location = new System.Drawing.Point(16, 36);
            this.lbCycles.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCycles.Name = "lbCycles";
            this.lbCycles.Size = new System.Drawing.Size(85, 24);
            this.lbCycles.TabIndex = 6;
            this.lbCycles.Text = "cycles:";
            this.lbCycles.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btAccept
            // 
            this.btAccept.BackColor = System.Drawing.Color.Chartreuse;
            this.btAccept.Location = new System.Drawing.Point(255, 278);
            this.btAccept.Margin = new System.Windows.Forms.Padding(4);
            this.btAccept.Name = "btAccept";
            this.btAccept.Size = new System.Drawing.Size(100, 28);
            this.btAccept.TabIndex = 7;
            this.btAccept.Text = "Accept";
            this.btAccept.UseVisualStyleBackColor = false;
            this.btAccept.Visible = false;
            // 
            // MenuFile
            // 
            this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.SaveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.saveimageToolStripMenuItem});
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(71, 24);
            this.MenuFile.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.SaveToolStripMenuItem.Text = "Save";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveimageToolStripMenuItem
            // 
            this.saveimageToolStripMenuItem.Name = "saveimageToolStripMenuItem";
            this.saveimageToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.saveimageToolStripMenuItem.Text = "Save image";
            this.saveimageToolStripMenuItem.Click += new System.EventHandler(this.saveimageToolStripMenuItem_Click);
            // 
            // Menu
            // 
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.zoomToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "Menu";
            this.menu.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menu.Size = new System.Drawing.Size(512, 28);
            this.menu.TabIndex = 8;
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displacementbuttonsToolStripMenuItem,
            this.zoombuttonsToolStripMenuItem,
            this.cyclechangeToolStripMenuItem});
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(84, 24);
            this.zoomToolStripMenuItem.Text = "Controls";
            // 
            // displacementbuttonsToolStripMenuItem
            // 
            this.displacementbuttonsToolStripMenuItem.Name = "displacementbuttonsToolStripMenuItem";
            this.displacementbuttonsToolStripMenuItem.Size = new System.Drawing.Size(271, 26);
            this.displacementbuttonsToolStripMenuItem.Text = "Displacement buttons";
            this.displacementbuttonsToolStripMenuItem.Click += new System.EventHandler(this.displacementbuttonsToolStripMenuItem_Click);
            // 
            // zoombuttonsToolStripMenuItem
            // 
            this.zoombuttonsToolStripMenuItem.Name = "zoombuttonsToolStripMenuItem";
            this.zoombuttonsToolStripMenuItem.Size = new System.Drawing.Size(271, 26);
            this.zoombuttonsToolStripMenuItem.Text = "Zoom buttons";
            this.zoombuttonsToolStripMenuItem.Click += new System.EventHandler(this.zoombuttonsToolStripMenuItem_Click);
            // 
            // cyclechangeToolStripMenuItem
            // 
            this.cyclechangeToolStripMenuItem.Name = "cyclechangeToolStripMenuItem";
            this.cyclechangeToolStripMenuItem.Size = new System.Drawing.Size(271, 26);
            this.cyclechangeToolStripMenuItem.Text = "Change period";
            this.cyclechangeToolStripMenuItem.Click += new System.EventHandler(this.cyclechangeToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ruleschangeToolStripMenuItem,
            this.backtodefectvaluesToolStripMenuItem,
            this.colorchangeToolStripMenuItem,
            this.beepToolStripMenuItem,
            this.autoimagesavingToolStripMenuItem,
            this.languageToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // ruleschangeToolStripMenuItem
            // 
            this.ruleschangeToolStripMenuItem.Name = "ruleschangeToolStripMenuItem";
            this.ruleschangeToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.ruleschangeToolStripMenuItem.Text = "Change rules";
            this.ruleschangeToolStripMenuItem.Click += new System.EventHandler(this.ruleschangeToolStripMenuItem_Click);
            // 
            // backtodefectvaluesToolStripMenuItem
            // 
            this.backtodefectvaluesToolStripMenuItem.Name = "backtodefectvaluesToolStripMenuItem";
            this.backtodefectvaluesToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.backtodefectvaluesToolStripMenuItem.Text = "Back to default values";
            this.backtodefectvaluesToolStripMenuItem.Click += new System.EventHandler(this.backtodefectvaluesToolStripMenuItem_Click);
            // 
            // colorchangeToolStripMenuItem
            // 
            this.colorchangeToolStripMenuItem.Name = "colorchangeToolStripMenuItem";
            this.colorchangeToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.colorchangeToolStripMenuItem.Text = "Change colors";
            this.colorchangeToolStripMenuItem.Click += new System.EventHandler(this.colorchangeToolStripMenuItem_Click);
            // 
            // beepToolStripMenuItem
            // 
            this.beepToolStripMenuItem.Name = "beepToolStripMenuItem";
            this.beepToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.beepToolStripMenuItem.Text = "Beep";
            this.beepToolStripMenuItem.Click += new System.EventHandler(this.beepToolStripMenuItem_Click);
            // 
            // autoimagesavingToolStripMenuItem
            // 
            this.autoimagesavingToolStripMenuItem.Name = "autoimagesavingToolStripMenuItem";
            this.autoimagesavingToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.autoimagesavingToolStripMenuItem.Text = "Automatic image saving";
            this.autoimagesavingToolStripMenuItem.Click += new System.EventHandler(this.autoimagesavingToolStripMenuItem_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.catellanoToolStripMenuItem,
            this.englishToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.languageToolStripMenuItem.Text = "Language";
            // 
            // catellanoToolStripMenuItem
            // 
            this.catellanoToolStripMenuItem.Name = "catellanoToolStripMenuItem";
            this.catellanoToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.catellanoToolStripMenuItem.Text = "Catellano";
            this.catellanoToolStripMenuItem.Click += new System.EventHandler(this.catellanoToolStripMenuItem_Click);
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.englishToolStripMenuItem.Text = "English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // btLeft
            // 
            this.btLeft.AutoSize = true;
            this.btLeft.BackColor = System.Drawing.Color.SlateBlue;
            this.btLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btLeft.Location = new System.Drawing.Point(200, 194);
            this.btLeft.Margin = new System.Windows.Forms.Padding(4);
            this.btLeft.Name = "btLeft";
            this.btLeft.Size = new System.Drawing.Size(57, 50);
            this.btLeft.TabIndex = 10;
            this.btLeft.Text = "◄";
            this.btLeft.UseVisualStyleBackColor = false;
            this.btLeft.Visible = false;
            this.btLeft.Click += new System.EventHandler(this.btLeft_Click);
            // 
            // btUp
            // 
            this.btUp.AutoSize = true;
            this.btUp.BackColor = System.Drawing.Color.SkyBlue;
            this.btUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btUp.Location = new System.Drawing.Point(265, 139);
            this.btUp.Margin = new System.Windows.Forms.Padding(4);
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(57, 50);
            this.btUp.TabIndex = 11;
            this.btUp.Text = "▲";
            this.btUp.UseVisualStyleBackColor = false;
            this.btUp.Visible = false;
            this.btUp.Click += new System.EventHandler(this.btUp_Click);
            // 
            // btRight
            // 
            this.btRight.AutoSize = true;
            this.btRight.BackColor = System.Drawing.Color.Navy;
            this.btRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btRight.Location = new System.Drawing.Point(325, 193);
            this.btRight.Margin = new System.Windows.Forms.Padding(4);
            this.btRight.Name = "btRight";
            this.btRight.Size = new System.Drawing.Size(57, 52);
            this.btRight.TabIndex = 12;
            this.btRight.Text = "►";
            this.btRight.UseVisualStyleBackColor = false;
            this.btRight.Visible = false;
            this.btRight.Click += new System.EventHandler(this.btRight_Click);
            // 
            // btDown
            // 
            this.btDown.AutoSize = true;
            this.btDown.BackColor = System.Drawing.Color.Red;
            this.btDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btDown.Location = new System.Drawing.Point(265, 252);
            this.btDown.Margin = new System.Windows.Forms.Padding(4);
            this.btDown.Name = "btDown";
            this.btDown.Size = new System.Drawing.Size(57, 52);
            this.btDown.TabIndex = 13;
            this.btDown.Text = "▼";
            this.btDown.UseVisualStyleBackColor = false;
            this.btDown.Visible = false;
            this.btDown.Click += new System.EventHandler(this.btDown_Click);
            // 
            // btZoomPlus
            // 
            this.btZoomPlus.AutoSize = true;
            this.btZoomPlus.BackColor = System.Drawing.Color.Silver;
            this.btZoomPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btZoomPlus.Location = new System.Drawing.Point(233, 53);
            this.btZoomPlus.Margin = new System.Windows.Forms.Padding(4);
            this.btZoomPlus.Name = "btZoomPlus";
            this.btZoomPlus.Size = new System.Drawing.Size(63, 60);
            this.btZoomPlus.TabIndex = 14;
            this.btZoomPlus.Text = "+";
            this.btZoomPlus.UseVisualStyleBackColor = false;
            this.btZoomPlus.Visible = false;
            this.btZoomPlus.Click += new System.EventHandler(this.ampliarToolStripMenuItem_Click);
            // 
            // btZoomMinus
            // 
            this.btZoomMinus.AutoSize = true;
            this.btZoomMinus.BackColor = System.Drawing.Color.Silver;
            this.btZoomMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btZoomMinus.Location = new System.Drawing.Point(304, 53);
            this.btZoomMinus.Margin = new System.Windows.Forms.Padding(4);
            this.btZoomMinus.Name = "btZoomMinus";
            this.btZoomMinus.Size = new System.Drawing.Size(63, 60);
            this.btZoomMinus.TabIndex = 15;
            this.btZoomMinus.Text = "-";
            this.btZoomMinus.UseVisualStyleBackColor = false;
            this.btZoomMinus.Visible = false;
            this.btZoomMinus.Click += new System.EventHandler(this.reducirToolStripMenuItem_Click);
            // 
            // nudCycle
            // 
            this.nudCycle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudCycle.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudCycle.Location = new System.Drawing.Point(21, 282);
            this.nudCycle.Margin = new System.Windows.Forms.Padding(4);
            this.nudCycle.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudCycle.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudCycle.Name = "nudCycle";
            this.nudCycle.Size = new System.Drawing.Size(160, 30);
            this.nudCycle.TabIndex = 16;
            this.nudCycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudCycle.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCycle.Visible = false;
            this.nudCycle.ValueChanged += new System.EventHandler(this.nudCycle_ValueChanged);
            // 
            // lbNud
            // 
            this.lbNud.AutoSize = true;
            this.lbNud.BackColor = System.Drawing.Color.Black;
            this.lbNud.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNud.ForeColor = System.Drawing.Color.DimGray;
            this.lbNud.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lbNud.Location = new System.Drawing.Point(20, 261);
            this.lbNud.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbNud.Name = "lbNud";
            this.lbNud.Size = new System.Drawing.Size(112, 18);
            this.lbNud.TabIndex = 17;
            this.lbNud.Text = "Milliseconds";
            this.lbNud.Visible = false;
            // 
            // lbEgo
            // 
            this.lbEgo.AutoSize = true;
            this.lbEgo.BackColor = System.Drawing.Color.SeaGreen;
            this.lbEgo.Font = new System.Drawing.Font("DejaVu Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEgo.Location = new System.Drawing.Point(233, 36);
            this.lbEgo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbEgo.Name = "lbEgo";
            this.lbEgo.Size = new System.Drawing.Size(122, 28);
            this.lbEgo.TabIndex = 18;
            this.lbEgo.Text = "Xadnem";
            this.lbEgo.MouseEnter += new System.EventHandler(this.lbEgo_MouseEnter);
            this.lbEgo.MouseLeave += new System.EventHandler(this.lbEgo_MouseLeave);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 321);
            this.Controls.Add(this.lbEgo);
            this.Controls.Add(this.lbNud);
            this.Controls.Add(this.nudCycle);
            this.Controls.Add(this.btZoomMinus);
            this.Controls.Add(this.btZoomPlus);
            this.Controls.Add(this.btDown);
            this.Controls.Add(this.btRight);
            this.Controls.Add(this.btUp);
            this.Controls.Add(this.btLeft);
            this.Controls.Add(this.lbCycles);
            this.Controls.Add(this.lbPopulation);
            this.Controls.Add(this.btAccept);
            this.Controls.Add(this.rbDrawShape);
            this.Controls.Add(this.pbBackGround);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.rbFicticio);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(527, 358);
            this.Name = "Form1";
            this.Text = "OrgaSnismo0      ( Daniel Santos version ) ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.SizeChanged += new System.EventHandler(this.Maximized);
            this.Resize += new System.EventHandler(this.Maximized);
            ((System.ComponentModel.ISupportInitialize)(this.pbBackGround)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCycle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        private System.Windows.Forms.PictureBox pbBackGround;
        private System.Windows.Forms.RadioButton rbDrawShape;

        private System.Windows.Forms.RadioButton rbFicticio;
        private System.Windows.Forms.Label lbCycles;
        private System.Windows.Forms.Label lbPopulation;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btAccept;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem MenuFile;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
        private System.Windows.Forms.Button btLeft;
        private System.Windows.Forms.Button btUp;
        private System.Windows.Forms.Button btRight;
        private System.Windows.Forms.Button btDown;
        private System.Windows.Forms.ToolStripMenuItem displacementbuttonsToolStripMenuItem;
        private System.Windows.Forms.Button btZoomMinus;
        private System.Windows.Forms.Button btZoomPlus;
        private System.Windows.Forms.ToolStripMenuItem zoombuttonsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown nudCycle;
        private System.Windows.Forms.ToolStripMenuItem cyclechangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ruleschangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backtodefectvaluesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorchangeToolStripMenuItem;
        private System.Windows.Forms.Label lbNud;
        private System.Windows.Forms.Label lbEgo;
        private System.Windows.Forms.ToolStripMenuItem beepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveimageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoimagesavingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem catellanoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    }
}

