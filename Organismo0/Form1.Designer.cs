namespace Organismo0
{

    //!!!!!!!!!!!!!!!!!!!!!!!!!!
    //!!!!!!!!!!!!!!!!!!!!!!!!!!
    //
    //  INICIADO 20/7/2006
    //
    //!!!!!!!!!!!!!!!!!!!!!!!!!!
    //!!!!!!!!!!!!!!!!!!!!!!!!!!

    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pbFondo = new System.Windows.Forms.PictureBox();
            this.rbDibujar = new System.Windows.Forms.RadioButton();
            this.rbFicticio = new System.Windows.Forms.RadioButton();
            this.lbPoblacion = new System.Windows.Forms.Label();
            this.lbCiclos = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btPruebas = new System.Windows.Forms.Button();
            this.btAceptar = new System.Windows.Forms.Button();
            this.Archivo = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarImagenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.botonesDeDesplazamientoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.botonesZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarPeriodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarReglasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.volverAReglasPorDefectoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarColoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardadoAutomaticoDeImagenenesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.idiomaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.catellanoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inglesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btIzquierda = new System.Windows.Forms.Button();
            this.btArriba = new System.Windows.Forms.Button();
            this.btDerecha = new System.Windows.Forms.Button();
            this.btAbajo = new System.Windows.Forms.Button();
            this.btZoomMas = new System.Windows.Forms.Button();
            this.btZoomMenos = new System.Windows.Forms.Button();
            this.nudVelocidad = new System.Windows.Forms.NumericUpDown();
            this.lbRotulonud = new System.Windows.Forms.Label();
            this.lbEgo = new System.Windows.Forms.Label();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pbFondo)).BeginInit();
            this.Menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVelocidad)).BeginInit();
            this.SuspendLayout();
            // 
            // pbFondo
            // 
            this.pbFondo.BackColor = System.Drawing.Color.Black;
            this.pbFondo.Location = new System.Drawing.Point(0, 25);
            this.pbFondo.Margin = new System.Windows.Forms.Padding(4);
            this.pbFondo.Name = "pbFondo";
            this.pbFondo.Size = this.ClientSize;
            this.pbFondo.TabIndex = 0;
            this.pbFondo.TabStop = false;
            // 
            // rbDibujar
            // 
            this.rbDibujar.AutoSize = true;
            this.rbDibujar.BackColor = System.Drawing.Color.Black;
            this.rbDibujar.Font = new System.Drawing.Font("DejaVu Sans", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDibujar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.rbDibujar.Location = new System.Drawing.Point(64, 100);
            this.rbDibujar.Margin = new System.Windows.Forms.Padding(4);
            this.rbDibujar.Name = "rbDibujar";
            this.rbDibujar.Size = new System.Drawing.Size(282, 32);
            this.rbDibujar.TabIndex = 1;
            this.rbDibujar.Text = "Dibujar forma inicial.";
            this.rbDibujar.UseVisualStyleBackColor = false;
            this.rbDibujar.CheckedChanged += new System.EventHandler(this.rbDibujar_Click);
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
            // lbPoblacion
            // 
            this.lbPoblacion.AutoSize = true;
            this.lbPoblacion.BackColor = System.Drawing.Color.Black;
            this.lbPoblacion.Font = new System.Drawing.Font("DejaVu Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPoblacion.ForeColor = System.Drawing.Color.DimGray;
            this.lbPoblacion.Location = new System.Drawing.Point(16, 73);
            this.lbPoblacion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPoblacion.Name = "lbPoblacion";
            this.lbPoblacion.Size = new System.Drawing.Size(136, 24);
            this.lbPoblacion.TabIndex = 5;
            this.lbPoblacion.Text = "Población: ";
            this.lbPoblacion.Visible = false;
            // 
            // lbCiclos
            // 
            this.lbCiclos.AutoSize = true;
            this.lbCiclos.BackColor = System.Drawing.Color.Black;
            this.lbCiclos.Font = new System.Drawing.Font("DejaVu Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCiclos.ForeColor = System.Drawing.Color.DimGray;
            this.lbCiclos.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lbCiclos.Location = new System.Drawing.Point(16, 36);
            this.lbCiclos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCiclos.Name = "lbCiclos";
            this.lbCiclos.Size = new System.Drawing.Size(85, 24);
            this.lbCiclos.TabIndex = 6;
            this.lbCiclos.Text = "Ciclos:";
            this.lbCiclos.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btPruebas
            // 
            this.btPruebas.BackColor = System.Drawing.Color.Orange;
            this.btPruebas.Location = new System.Drawing.Point(64, 217);
            this.btPruebas.Margin = new System.Windows.Forms.Padding(4);
            this.btPruebas.Name = "btPruebas";
            this.btPruebas.Size = new System.Drawing.Size(100, 28);
            this.btPruebas.TabIndex = 1;
            this.btPruebas.Text = "Ciclo";
            this.btPruebas.UseVisualStyleBackColor = false;
            this.btPruebas.Visible = false;
            // 
            // btAceptar
            // 
            this.btAceptar.BackColor = System.Drawing.Color.Chartreuse;
            this.btAceptar.Location = new System.Drawing.Point(255, 278);
            this.btAceptar.Margin = new System.Windows.Forms.Padding(4);
            this.btAceptar.Name = "btAceptar";
            this.btAceptar.Size = new System.Drawing.Size(100, 28);
            this.btAceptar.TabIndex = 7;
            this.btAceptar.Text = "Aceptar";
            this.btAceptar.UseVisualStyleBackColor = false;
            this.btAceptar.Visible = false;
            // 
            // Archivo
            // 
            this.Archivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.abrirToolStripMenuItem,
            this.guardarImagenToolStripMenuItem});
            this.Archivo.Name = "Archivo";
            this.Archivo.Size = new System.Drawing.Size(71, 24);
            this.Archivo.Text = "Archivo";
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.guardarToolStripMenuItem_Click);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.abrirToolStripMenuItem.Text = "Cargar";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // guardarImagenToolStripMenuItem
            // 
            this.guardarImagenToolStripMenuItem.Name = "guardarImagenToolStripMenuItem";
            this.guardarImagenToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.guardarImagenToolStripMenuItem.Text = "Guardar imagen";
            this.guardarImagenToolStripMenuItem.Click += new System.EventHandler(this.guardarImagenToolStripMenuItem_Click);
            // 
            // Menu
            // 
            this.Menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Archivo,
            this.zoomToolStripMenuItem,
            this.opcionesToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.Menu.Size = new System.Drawing.Size(512, 28);
            this.Menu.TabIndex = 8;
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.botonesDeDesplazamientoToolStripMenuItem,
            this.botonesZoomToolStripMenuItem,
            this.cambiarPeriodoToolStripMenuItem});
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(84, 24);
            this.zoomToolStripMenuItem.Text = "Controles";
            // 
            // botonesDeDesplazamientoToolStripMenuItem
            // 
            this.botonesDeDesplazamientoToolStripMenuItem.Name = "botonesDeDesplazamientoToolStripMenuItem";
            this.botonesDeDesplazamientoToolStripMenuItem.Size = new System.Drawing.Size(271, 26);
            this.botonesDeDesplazamientoToolStripMenuItem.Text = "Botones de Desplazamiento";
            this.botonesDeDesplazamientoToolStripMenuItem.Click += new System.EventHandler(this.botonesDeDesplazamientoToolStripMenuItem_Click);
            // 
            // botonesZoomToolStripMenuItem
            // 
            this.botonesZoomToolStripMenuItem.Name = "botonesZoomToolStripMenuItem";
            this.botonesZoomToolStripMenuItem.Size = new System.Drawing.Size(271, 26);
            this.botonesZoomToolStripMenuItem.Text = "Botones Zoom";
            this.botonesZoomToolStripMenuItem.Click += new System.EventHandler(this.botonesZoomToolStripMenuItem_Click);
            // 
            // cambiarPeriodoToolStripMenuItem
            // 
            this.cambiarPeriodoToolStripMenuItem.Name = "cambiarPeriodoToolStripMenuItem";
            this.cambiarPeriodoToolStripMenuItem.Size = new System.Drawing.Size(271, 26);
            this.cambiarPeriodoToolStripMenuItem.Text = "Cambiar Periodo";
            this.cambiarPeriodoToolStripMenuItem.Click += new System.EventHandler(this.cambiarPeriodoToolStripMenuItem_Click);
            // 
            // opcionesToolStripMenuItem
            // 
            this.opcionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cambiarReglasToolStripMenuItem,
            this.volverAReglasPorDefectoToolStripMenuItem,
            this.cambiarColoresToolStripMenuItem,
            this.beepToolStripMenuItem,
            this.guardadoAutomaticoDeImagenenesToolStripMenuItem,
            this.idiomaToolStripMenuItem});
            this.opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            this.opcionesToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.opcionesToolStripMenuItem.Text = "Opciones";
            // 
            // cambiarReglasToolStripMenuItem
            // 
            this.cambiarReglasToolStripMenuItem.Name = "cambiarReglasToolStripMenuItem";
            this.cambiarReglasToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.cambiarReglasToolStripMenuItem.Text = "Cambiar Reglas";
            this.cambiarReglasToolStripMenuItem.Click += new System.EventHandler(this.cambiarReglasToolStripMenuItem_Click);
            // 
            // volverAReglasPorDefectoToolStripMenuItem
            // 
            this.volverAReglasPorDefectoToolStripMenuItem.Name = "volverAReglasPorDefectoToolStripMenuItem";
            this.volverAReglasPorDefectoToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.volverAReglasPorDefectoToolStripMenuItem.Text = "Volver a valores por defecto";
            this.volverAReglasPorDefectoToolStripMenuItem.Click += new System.EventHandler(this.volverAReglasPorDefectoToolStripMenuItem_Click);
            // 
            // cambiarColoresToolStripMenuItem
            // 
            this.cambiarColoresToolStripMenuItem.Name = "cambiarColoresToolStripMenuItem";
            this.cambiarColoresToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.cambiarColoresToolStripMenuItem.Text = "Cambiar Colores";
            this.cambiarColoresToolStripMenuItem.Click += new System.EventHandler(this.cambiarColoresToolStripMenuItem_Click);
            // 
            // beepToolStripMenuItem
            // 
            this.beepToolStripMenuItem.Name = "beepToolStripMenuItem";
            this.beepToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.beepToolStripMenuItem.Text = "Beep";
            this.beepToolStripMenuItem.Click += new System.EventHandler(this.beepToolStripMenuItem_Click);
            // 
            // guardadoAutomaticoDeImagenenesToolStripMenuItem
            // 
            this.guardadoAutomaticoDeImagenenesToolStripMenuItem.Name = "guardadoAutomaticoDeImagenenesToolStripMenuItem";
            this.guardadoAutomaticoDeImagenenesToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.guardadoAutomaticoDeImagenenesToolStripMenuItem.Text = "Guardado Automatico de Imagenes";
            this.guardadoAutomaticoDeImagenenesToolStripMenuItem.Click += new System.EventHandler(this.guardadoAutomaticoDeImagenenesToolStripMenuItem_Click);
            // 
            // idiomaToolStripMenuItem
            // 
            this.idiomaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.catellanoToolStripMenuItem,
            this.inglesToolStripMenuItem});
            this.idiomaToolStripMenuItem.Name = "idiomaToolStripMenuItem";
            this.idiomaToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.idiomaToolStripMenuItem.Text = "Idioma";
            // 
            // catellanoToolStripMenuItem
            // 
            this.catellanoToolStripMenuItem.Name = "catellanoToolStripMenuItem";
            this.catellanoToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.catellanoToolStripMenuItem.Text = "Catellano";
            this.catellanoToolStripMenuItem.Click += new System.EventHandler(this.catellanoToolStripMenuItem_Click);
            // 
            // inglesToolStripMenuItem
            // 
            this.inglesToolStripMenuItem.Name = "inglesToolStripMenuItem";
            this.inglesToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.inglesToolStripMenuItem.Text = "Inglés";
            this.inglesToolStripMenuItem.Click += new System.EventHandler(this.inglesToolStripMenuItem_Click);
            // 
            // btIzquierda
            // 
            this.btIzquierda.AutoSize = true;
            this.btIzquierda.BackColor = System.Drawing.Color.SlateBlue;
            this.btIzquierda.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btIzquierda.Location = new System.Drawing.Point(200, 194);
            this.btIzquierda.Margin = new System.Windows.Forms.Padding(4);
            this.btIzquierda.Name = "btIzquierda";
            this.btIzquierda.Size = new System.Drawing.Size(57, 50);
            this.btIzquierda.TabIndex = 10;
            this.btIzquierda.Text = "◄";
            this.btIzquierda.UseVisualStyleBackColor = false;
            this.btIzquierda.Visible = false;
            this.btIzquierda.Click += new System.EventHandler(this.btIzquierda_Click);
            // 
            // btArriba
            // 
            this.btArriba.AutoSize = true;
            this.btArriba.BackColor = System.Drawing.Color.SkyBlue;
            this.btArriba.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btArriba.Location = new System.Drawing.Point(265, 139);
            this.btArriba.Margin = new System.Windows.Forms.Padding(4);
            this.btArriba.Name = "btArriba";
            this.btArriba.Size = new System.Drawing.Size(57, 50);
            this.btArriba.TabIndex = 11;
            this.btArriba.Text = "▲";
            this.btArriba.UseVisualStyleBackColor = false;
            this.btArriba.Visible = false;
            this.btArriba.Click += new System.EventHandler(this.btArriba_Click);
            // 
            // btDerecha
            // 
            this.btDerecha.AutoSize = true;
            this.btDerecha.BackColor = System.Drawing.Color.Navy;
            this.btDerecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btDerecha.Location = new System.Drawing.Point(325, 193);
            this.btDerecha.Margin = new System.Windows.Forms.Padding(4);
            this.btDerecha.Name = "btDerecha";
            this.btDerecha.Size = new System.Drawing.Size(57, 52);
            this.btDerecha.TabIndex = 12;
            this.btDerecha.Text = "►";
            this.btDerecha.UseVisualStyleBackColor = false;
            this.btDerecha.Visible = false;
            this.btDerecha.Click += new System.EventHandler(this.btDerecha_Click);
            // 
            // btAbajo
            // 
            this.btAbajo.AutoSize = true;
            this.btAbajo.BackColor = System.Drawing.Color.Red;
            this.btAbajo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAbajo.Location = new System.Drawing.Point(265, 252);
            this.btAbajo.Margin = new System.Windows.Forms.Padding(4);
            this.btAbajo.Name = "btAbajo";
            this.btAbajo.Size = new System.Drawing.Size(57, 52);
            this.btAbajo.TabIndex = 13;
            this.btAbajo.Text = "▼";
            this.btAbajo.UseVisualStyleBackColor = false;
            this.btAbajo.Visible = false;
            this.btAbajo.Click += new System.EventHandler(this.btAbajo_Click);
            // 
            // btZoomMas
            // 
            this.btZoomMas.AutoSize = true;
            this.btZoomMas.BackColor = System.Drawing.Color.Silver;
            this.btZoomMas.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btZoomMas.Location = new System.Drawing.Point(233, 53);
            this.btZoomMas.Margin = new System.Windows.Forms.Padding(4);
            this.btZoomMas.Name = "btZoomMas";
            this.btZoomMas.Size = new System.Drawing.Size(63, 60);
            this.btZoomMas.TabIndex = 14;
            this.btZoomMas.Text = "+";
            this.btZoomMas.UseVisualStyleBackColor = false;
            this.btZoomMas.Visible = false;
            this.btZoomMas.Click += new System.EventHandler(this.ampliarToolStripMenuItem_Click);
            // 
            // btZoomMenos
            // 
            this.btZoomMenos.AutoSize = true;
            this.btZoomMenos.BackColor = System.Drawing.Color.Silver;
            this.btZoomMenos.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btZoomMenos.Location = new System.Drawing.Point(304, 53);
            this.btZoomMenos.Margin = new System.Windows.Forms.Padding(4);
            this.btZoomMenos.Name = "btZoomMenos";
            this.btZoomMenos.Size = new System.Drawing.Size(63, 60);
            this.btZoomMenos.TabIndex = 15;
            this.btZoomMenos.Text = "-";
            this.btZoomMenos.UseVisualStyleBackColor = false;
            this.btZoomMenos.Visible = false;
            this.btZoomMenos.Click += new System.EventHandler(this.reducirToolStripMenuItem_Click);
            // 
            // nudVelocidad
            // 
            this.nudVelocidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudVelocidad.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudVelocidad.Location = new System.Drawing.Point(21, 282);
            this.nudVelocidad.Margin = new System.Windows.Forms.Padding(4);
            this.nudVelocidad.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudVelocidad.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudVelocidad.Name = "nudVelocidad";
            this.nudVelocidad.Size = new System.Drawing.Size(160, 30);
            this.nudVelocidad.TabIndex = 16;
            this.nudVelocidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudVelocidad.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudVelocidad.Visible = false;
            this.nudVelocidad.ValueChanged += new System.EventHandler(this.nudVelocidad_ValueChanged);
            // 
            // lbRotulonud
            // 
            this.lbRotulonud.AutoSize = true;
            this.lbRotulonud.BackColor = System.Drawing.Color.Black;
            this.lbRotulonud.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRotulonud.ForeColor = System.Drawing.Color.DimGray;
            this.lbRotulonud.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lbRotulonud.Location = new System.Drawing.Point(20, 261);
            this.lbRotulonud.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbRotulonud.Name = "lbRotulonud";
            this.lbRotulonud.Size = new System.Drawing.Size(112, 18);
            this.lbRotulonud.TabIndex = 17;
            this.lbRotulonud.Text = "Milisegundos";
            this.lbRotulonud.Visible = false;
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
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            this.ayudaToolStripMenuItem.Click += new System.EventHandler(this.ayudaToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 321);
            this.Controls.Add(this.lbEgo);
            this.Controls.Add(this.lbRotulonud);
            this.Controls.Add(this.nudVelocidad);
            this.Controls.Add(this.btZoomMenos);
            this.Controls.Add(this.btZoomMas);
            this.Controls.Add(this.btAbajo);
            this.Controls.Add(this.btDerecha);
            this.Controls.Add(this.btArriba);
            this.Controls.Add(this.btIzquierda);
            this.Controls.Add(this.btPruebas);
            this.Controls.Add(this.lbCiclos);
            this.Controls.Add(this.lbPoblacion);
            this.Controls.Add(this.btAceptar);
            this.Controls.Add(this.rbDibujar);
            this.Controls.Add(this.pbFondo);
            this.Controls.Add(this.Menu);
            this.Controls.Add(this.rbFicticio);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(527, 358);
            this.Name = "Form1";
            this.Text = "OrgaSnismo0      ( Daniel Santos version ) ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.SizeChanged += new System.EventHandler(this.Maximized);
            this.Resize += new System.EventHandler(this.Maximized);
            ((System.ComponentModel.ISupportInitialize)(this.pbFondo)).EndInit();
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVelocidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbFondo;
        private System.Windows.Forms.RadioButton rbDibujar;
        //private System.Windows.Forms.Label lbElegir;
       // private System.Windows.Forms.RadioButton rbGun;
        private System.Windows.Forms.RadioButton rbFicticio;
        private System.Windows.Forms.Label lbCiclos;
        private System.Windows.Forms.Label lbPoblacion;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btPruebas;
	private System.Windows.Forms.Button btAceptar;
	private System.Windows.Forms.MenuStrip Menu;
	private System.Windows.Forms.ToolStripMenuItem Archivo;
    private System.Windows.Forms.ToolStripMenuItem Nuevo;
    private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
    private System.Windows.Forms.Button btIzquierda;
    private System.Windows.Forms.Button btArriba;
    private System.Windows.Forms.Button btDerecha;
    private System.Windows.Forms.Button btAbajo;
    private System.Windows.Forms.ToolStripMenuItem botonesDeDesplazamientoToolStripMenuItem;
    private System.Windows.Forms.Button btZoomMenos;
    private System.Windows.Forms.Button btZoomMas;
    private System.Windows.Forms.ToolStripMenuItem botonesZoomToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
    private System.Windows.Forms.NumericUpDown nudVelocidad;
    private System.Windows.Forms.ToolStripMenuItem cambiarPeriodoToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem cambiarReglasToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem volverAReglasPorDefectoToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem cambiarColoresToolStripMenuItem;
    private System.Windows.Forms.Label lbRotulonud;
    private System.Windows.Forms.Label lbEgo;
    private System.Windows.Forms.ToolStripMenuItem beepToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem guardarImagenToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem guardadoAutomaticoDeImagenenesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem idiomaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem catellanoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inglesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
    }
}

