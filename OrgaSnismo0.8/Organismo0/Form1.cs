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
        List<PointF> celulas; // Para guardar la lista de celulas vivas;
        List<PointF> celulasant;// Para guardar la lista anterior de celulas vivas
        List<PointF> proximasvivas; // Proximas celulas que naceran
        List<PointF> proximasmuertas; // Proximas celulas que moriran
        List<PointF> muertasanteriores; // Celulas que han muerto en el turno anterior
        List<PointF> vivasanteriores; // Celulas que han nacido en el turno anterior
        List<PointF> adyacentes; // Lista de puntos que estan alrededor de cada celula viva
        List<int> continuaviva; // Lista de la cantidad de celulas adyacentes para que una celula siga viva segun las reglas 
        List<int> nacera; // Lista de la cantidad de celulas adyacentes para que una celula nazca segun las reglas


        Bitmap bitmap; //  Para el gráfico permanente.
        Graphics grafico; // Objeto Graphics para dibujar en el picture box

        Pen linea; // Para la linea exterior de las celulas
        Pen lineamuerta; // Para la linea exterior de las celulas murtas

        float tamaño = 30; //Tamaño de cada célula por defecto

        int ciclo = 1000; // Tiempo entre ciclos por defecto
        int ciclos = 0; // Cantidad de ciclos desde el inicio de la forma.
        int ciclossesion = 0; // Cantidad de cilos desde el inicio de la sesion ( para diferenciar sobre los ciclos desde el inicio de la forma )
        int tiempocalculo = 0; // Para guardar el tiempo que tarda el metodo EvaluarVida en calcular el siguiente estado

        string viva = "23"; // Valores por defecto para que una celula siga viva
        string nacida = "3"; // Valor por defecto para que una celula nazca
        string prefijo = ""; // Prefijo introducido por el usuario para los nombres de las imagenes en guardado automatico

        PointF origen; // Origen de la ventana grafica

        TextBox tbTamaño; // Textbox para cuadro de dialogo del tamaño de la celula

        Form dialogotamaño; // Dialogo para el tamaño de la celula
        Form dialogoreglas; // Dialogo para el cambio de normas
        Form DialogoColores;
        TextBox tbNacida; // Caja de texto con la cantidad de celulas adyacentes para que una celula nazca
        TextBox tbViva; //Caja de texto con la cantidad de celulas adyacentes para que una celula permanezca viva
        Label lbColorLinea; // Para el dialogo de colores
        Label lbColorInterior; // Idem anterior
        Label lbColorFondo; // Idem
        Label lbColorInteriorM; // Interior de las celulas muertas en el turno
        Label lbColorInteriorV; // Interior de las nuevas celulas en el turno

        bool dibujando = false; // Para registrar cuando se esta en el modo de dibujo        
        bool enmarcha = false; // Para registrar cuando esta el timer en marcha
        bool disponible = false; // Para la sincronia entre hilos
        bool cicloautomatico = false; // Para registrar cuando el programa cambia el ciclo por problemas de rendimiento
        bool beep = false; // Para registrar cuando esta habilitado el beep y cuando no
        bool guardadoautomatico = false; // Para registrar cuando esta activado el guardado automatico de imagenes y cuando no
        bool ingles = false;

        int cuentapartidas = 0; // Para llevar la cuenta de las partidas jugadas;

        Color exterior = Color.Red; //Color de la linea alrrededor de la celula viva por defecto
        Color interior = Color.FromArgb(155, 250, 5); // Color interior de la celula viva por defecto
        Color fondo = Color.Black; // Color del fondo por defecto
        Color interiorM = Color.Black; // Color de las celulas muertas en el turno por defecto.
        Color interiorV = Color.FromArgb(155, 250, 5); // Color de las nuevas celulas en el turno por defecto

        Thread calcular; // Hilo que obtendra la lista de celulas en cada iteracion

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = ciclo;
            celulas = new List<PointF>();
            celulasant = new List<PointF>();
            proximasvivas = new List<PointF>();
            proximasmuertas = new List<PointF>();
            muertasanteriores = new List<PointF>();
            adyacentes = new List<PointF>();
            vivasanteriores = new List<PointF>();
            continuaviva = new List<int>() { 2, 3 };
            nacera = new List<int>() { 3 };
            nudVelocidad.Value = ciclo;
            SoundPlayer reproductor = new SoundPlayer();
            reproductor.SoundLocation = Directory.GetCurrentDirectory() + "\\Rafaga-Organismo-Entrada.wav";
            reproductor.Play();
            lbEgo.ForeColor = fondo;
            lbEgo.BackColor = fondo;
            beepToolStripMenuItem.PerformClick();
            linea = new Pen(exterior, 1);
            lineamuerta = new Pen(fondo, 1);
        }

        /// <summary>
        /// 
        /// MODIFICA LA POSICION DEL LOS BOTONES Y EL TAMAÑO DEL PICTURE BOX EN FUNCION DEL TAMAÑO DEL FORMULARIO
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void Maximized(object sender, EventArgs e)
        {
            pbFondo.Size = this.ClientSize;
            btArriba.Location = new Point(this.ClientSize.Width - 100, this.ClientSize.Height - 200);
            btDerecha.Location = new Point(this.ClientSize.Width - 50, this.ClientSize.Height - 150);
            btAbajo.Location = new Point(this.ClientSize.Width - 100, this.ClientSize.Height - 100);
            btIzquierda.Location = new Point(this.ClientSize.Width - 150, this.ClientSize.Height - 150);

            btZoomMenos.Location = new Point(this.btIzquierda.Location.X, this.ClientSize.Height - 50);
            btZoomMas.Location = new Point(this.btDerecha.Location.X, btZoomMenos.Location.Y);

            nudVelocidad.Location = new Point(20, this.ClientSize.Height - 30);
            lbRotulonud.Location = new Point(nudVelocidad.Location.X, nudVelocidad.Location.Y - lbRotulonud.Height - 2);
            lbEgo.Location = new Point(this.ClientSize.Width - lbEgo.Width - 5, lbEgo.Height + 5);
        }

        /// <summary>
        /// 
        ///  OPCION PARA DIBUJAR LA FORMA INICIAL
        /// 
        /// </summary>
        ///

        private void rbDibujar_Click(object sender, EventArgs e)
        {
            rbDibujar.Hide();
            pbFondo.Update();
            tamañoToolStripMenuItem_Click(new object(), new EventArgs());
            OcultarControles();
            nudVelocidad.Visible = true; ;
            lbRotulonud.Visible = true;
            cambiarPeriodoToolStripMenuItem.Checked = true;
            dibujando = true;
        }

        ///////////////////////////
        ///
        /// MUESTRA EL CUADRO DE DIALOGO PARA INTRODUCIR EL TAMAÑO DE LAS CELULAS
        ///
        ///////////////////////////////////////////////
        ///
        private void tamañoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogotamaño = new Form();
            dialogotamaño.FormBorderStyle = FormBorderStyle.FixedDialog;
            dialogotamaño.Size = new Size(190, 130);
            dialogotamaño.BackColor = Color.SeaGreen;
            dialogotamaño.ControlBox = false;
            dialogotamaño.TopMost = true;
            Label lbTamaño = new Label();
            lbTamaño.AutoSize = true;
            lbTamaño.Font = new Font("Dejavu Sans", 10, FontStyle.Underline);
            if (ingles)
                lbTamaño.Text = "Cell size.";
            else
                lbTamaño.Text = "Tamaño de la célula.";

            lbTamaño.Location = new Point(20, 20);
            lbTamaño.Visible = true;
            tbTamaño = new TextBox();
            tbTamaño.Size = new Size(100, 50);
            tbTamaño.Location = new Point(40, 50);
            tbTamaño.Visible = true;
            tbTamaño.Text = "30";
            tbTamaño.SelectAll();
            tbTamaño.TextAlign = HorizontalAlignment.Center;
            Button btTamaño = new Button();
            if (ingles)
                btTamaño.Text = "Accept";
            else
                btTamaño.Text = "Aceptar";
            btTamaño.Location = new Point(55, 90);
            btTamaño.Visible = true;
            btTamaño.BackColor = Color.Chartreuse;
            btTamaño.Click += btTamaño_Clicked;
            dialogotamaño.Controls.Add(tbTamaño);
            dialogotamaño.Controls.Add(lbTamaño);
            dialogotamaño.Controls.Add(btTamaño);
            dialogotamaño.StartPosition = FormStartPosition.Manual;
            dialogotamaño.Location = this.Location;
            dialogotamaño.AcceptButton = btTamaño;
            dialogotamaño.Show();
        }

        /// <summary>
        /// 
        /// MANEJADORES PARA LOS BOTONES DE OPCION
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void btTamaño_Clicked(object sender, EventArgs e)
        {
            string tamaños = tbTamaño.Text;
            if (tamaños == "")
                tamaños = "20";
            tamaño = Single.Parse(tamaños);
            dialogotamaño.Close();

            // Iniciar el bitmap asocieado al picture box
            bitmap = new Bitmap(pbFondo.Size.Width, pbFondo.Size.Height);
            //Vincular el bitmap al picture box.
            pbFondo.Image = bitmap;
            // Iniciar el objeto graphics vinculado al bitmap
            grafico = Graphics.FromImage(bitmap);
            grafico.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            Pen linea = new Pen(Color.FromArgb(50, 240, 100, 50));

            // Dibujar la cuadricula para dibujar la forma
            for (float i = 0; i < pbFondo.Width; i += tamaño)
            {
                grafico.DrawLine(linea, new PointF(i, 0), new PointF(i, pbFondo.Height));
            }
            for (float i = 0; i < pbFondo.Height; i += tamaño)
            {
                grafico.DrawLine(linea, new PointF(0, i), new PointF(pbFondo.Width, i));
            }
            pbFondo.MouseClick += DibujarForma; // Para dibujar las celulas en los puntos pulsados
            btAceptar.Location = new Point(this.ClientSize.Width - btAceptar.Size.Width - 5, this.ClientSize.Height - btAceptar.Size.Height - 5);
            btAceptar.BackColor = Color.Chartreuse;
            if (ingles)
                btAceptar.Text = "Accept";
            else
                btAceptar.Text = "Aceptar";
            btAceptar.ForeColor = Color.Black;
            btAceptar.Show();
            btAceptar.Click -= btPausa_Click;
            btAceptar.Click += btAceptar_Click;
            nudVelocidad.Visible = true;
            lbRotulonud.Visible = true;
        }
        ///////////////////////////
        ///
        /// DIBUJA UN CELULA EN EL PUNTO DE LA CUADRICULA PULSADO CUANDO SE HA ELEGIDO LA OPCION DE
        /// DIBUJAR LA FORMA INICIAL
        ///
        /////////////////////////////////
        ///
        private void DibujarForma(object sender, MouseEventArgs e)
        {
            lbPoblacion.Show();

            PointF pulsado = e.Location;
            PointF celula = new PointF(((int)(pulsado.X / tamaño)) * tamaño, ((int)(pulsado.Y / tamaño)) * tamaño);
            if (!celulas.Contains(celula))
            {
                celulas.Add(celula);
                grafico.FillRectangle(new SolidBrush(interior), celula.X, celula.Y, tamaño, tamaño);
            }
            else
            {
                celulas.Remove(celula);
                grafico.FillRectangle(new SolidBrush(Color.Black), celula.X, celula.Y, tamaño, tamaño);
            }
            if (ingles)
            {
                lbPoblacion.Text = "Population: " + celulas.Count.ToString();
                lbRotulonud.Text = "Milliseconds: ";
            }
            else
            {
                lbPoblacion.Text = "Población: " + celulas.Count.ToString();
                lbRotulonud.Text = "Milisegundos: ";
            }
            pbFondo.Invalidate();
        }



        ///////////////////////////
        ///
        /// LLAMA AL METODO EVALUARVIDA Y LO INICIA CON LA FORMA DIBUJADA
        ///
        ///////////////////////////////////////////////
        ///
        private void btAceptar_Click(object sender, EventArgs e)
        {
            btAceptar.BackColor = Color.Black;
            btAceptar.ForeColor = Color.Silver;
            if (ingles)
                btAceptar.Text = "Pause";
            else
                btAceptar.Text = "Pausa";
            btAceptar.Location = new Point(this.ClientSize.Width / 2, btAceptar.Location.Y);
            btAceptar.Click -= btAceptar_Click;
            btAceptar.Click += btPausa_Click;
            pbFondo.MouseClick -= DibujarForma;
            MostrarControles();

            if (cuentapartidas == 0)
            {
                botonesZoomToolStripMenuItem.PerformClick();
                cambiarPeriodoToolStripMenuItem.PerformClick();
                cambiarPeriodoToolStripMenuItem.PerformClick();
            }
            grafico.Clear(fondo);
            lbCiclos.Visible = true;
            if (ingles)
            {
                lbCiclos.Text = "Cicles: " + ciclos.ToString();
                lbPoblacion.Text = "Population: " + celulas.Count.ToString();
            }
            else
            {
                lbCiclos.Text = "Ciclos: " + ciclos.ToString();
                lbPoblacion.Text = "Población: " + celulas.Count.ToString();
            }
            lbPoblacion.Visible = true;

            // Pintar las celulas 
            PintarCelulas();
            //Eliminar el manejador para dibujar las celulas en los puntos pulsados
            pbFondo.MouseClick -= DibujarForma;
            //Crear el hilo que evalua el estado de las celulas
            calcular = new Thread(EvaluarVida);
            calcular.Start();
            //Poner en marcha el timer 
            timer1.Start();
            enmarcha = true;
            cuentapartidas++;
            dibujando = false;
        }

        /// <summary>
        /// 
        /// REINICIA EL PROGRAMA
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            rbFicticio.Checked = true;
            rbDibujar.Show();
            if (grafico != null)
                grafico.Clear(Color.Black);
            if (pbFondo != null)
            {
                pbFondo.MouseClick -= DibujarForma;
                pbFondo.MouseClick -= DibujarForma;
            }
            celulas.Clear();
            celulasant.Clear();
            proximasmuertas.Clear();
            proximasvivas.Clear();
            muertasanteriores.Clear();
            vivasanteriores.Clear();
            adyacentes.Clear();

            btAceptar.Hide();
            lbCiclos.Hide();
            lbPoblacion.Hide();
            ciclos = 0;
            if (dialogotamaño != null)
                dialogotamaño.Hide();
            OcultarControles();
            cicloautomatico = false;
            guardadoautomatico = false;
            nuevoToolStripMenuItem.Checked = false;
            ciclo = 1000;
            nudVelocidad.BackColor = Color.White;
            nudVelocidad.Value = ciclo;
            pbFondo.Invalidate();
            ciclossesion = 0;
            if (ingles)
                inglesToolStripMenuItem_Click(new object(), new EventArgs());
            else
                catellanoToolStripMenuItem_Click(new object(), new EventArgs());

        }

        /// <summary>
        /// 
        /// ACTUALIZA LAS CELULAS VIVAS Y MUERTAS CADA CICLO Y PARA EN CASO DE QUE EL ORGANISMO MUERA O SE 
        /// ESTABILICE
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void timer1_Tick(object sender, EventArgs e)
        {
            while (disponible == false)
            {
                Monitor.Wait(this);
            }

            //Añadir a la lista de celulas vivas las nuevas celulas vivas y eliminar las muertas

            foreach (PointF p in proximasvivas)
                if (!celulas.Contains(p))
                    celulas.Add(p);

            foreach (PointF p in proximasmuertas)
            {
                bool quedanrepetidas = true;
                while (quedanrepetidas)
                    quedanrepetidas = celulas.Remove(p);
            }

            // Poner el intervalo minimo en funcion de la cantidad de celulas

            if (tiempocalculo > 0)
            {
                if (tiempocalculo > 100 && ciclo < tiempocalculo)
                    nudVelocidad.Value = tiempocalculo;
                else if (tiempocalculo > 100 && ciclo > tiempocalculo)
                    nudVelocidad.Value = tiempocalculo;
            }

            // Mover el origen del grafico al punto de origen establecido
            grafico.TranslateTransform(origen.X, origen.Y);

            if (ingles)
            {
                lbCiclos.Text = "Cicles: " + ciclos.ToString();
                lbPoblacion.Text = "Population: " + celulas.Count;
            }
            else
            {
                lbCiclos.Text = "Ciclos: " + ciclos.ToString();
                lbPoblacion.Text = "Poblaciòn: " + celulas.Count;
            }
            // Dibujar las celulas vivas
            PintarCelulas();

            //Guardar el bitmap si la opcion de guardado automatico esta activa
            if (guardadoautomatico)
            {
                int indice = prefijo.LastIndexOf('.');
                string nombre = prefijo.Substring(0, indice);
                nombre += "--" + ciclos.ToString() + ".bmp";
                bitmap.Save(nombre);
            }

            // Incrementar la cantidad de ciclos y los ciclos de la sesion
            ciclos++;
            ciclossesion++;

            // Lanzar el calculo del estado de las celulas en un proceso aparte y usar un mecanismo de
            // sincronizacion con el pintado de las celulas.

            disponible = false;
            Monitor.PulseAll(this);

            /// Si no quedan celulas  notificarlo
            if (ciclossesion > 1 && celulas.Count == 0)
            {
                timer1.Stop();
                if (ingles)
                    MessageBox.Show("OrgaSnismo dead");
                else
                    MessageBox.Show("OrgaSnismo muerto.");
                return;
            }

            // Si el organismo se ha estabilizado , notificarlo
            if (ciclossesion > 1 && proximasmuertas.Count == 0 && proximasvivas.Count == 0)
            {
                btPausa_Click(new object(), new EventArgs());
                if (ingles)
                    MessageBox.Show("OrgaSnimo stabilized");
                else
                    MessageBox.Show("OrgaSnismo estabilizado.");
                return;
            }


            if (beep && celulas.Count > 1000)
                Console.Beep(400, 1000);

        }


        /// <summary>
        /// 
        ///    MODIFICA LA LISTA DE CELULAS VIVAS APLICANDO LAS REGLAS: 
        ///    CELULA MUERTA VIVE EN SIGUIENTE TURNO SI TOCA CON TRES CELULAS VIVAS
        ///    CELULA VIVA CONTINUA VIVA SI TOCA CON DOS O TRES CELULAS VIVAS, EN OTRO CASO MUERE
        ///    
        /// </summary>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void EvaluarVida()
        {
            while (true)
            {
                // Esperar a que el metodo Timer1_Tick termine 
                while (disponible == true)
                {
                    Monitor.Wait(this);
                }
                DateTime iniciocalculo = DateTime.Now;

                // Guardar la lista de celulas vivas y muertas anteriores antes de modificarla
                muertasanteriores.Clear();
                vivasanteriores.Clear();
                foreach (PointF p in proximasmuertas)
                    muertasanteriores.Add(p);
                foreach (PointF p in proximasvivas)
                    vivasanteriores.Add(p);

                //Resetear la lista de celulas adyacentes
                adyacentes.Clear();

                //Llenar la lista de celulas adyacentes ( se incluiran las celulas vivas tambien )
                foreach (PointF p in celulas)
                {
                    adyacentes.Add(new PointF(p.X - tamaño, p.Y - tamaño));
                    adyacentes.Add(new PointF(p.X - tamaño, p.Y));
                    adyacentes.Add(new PointF(p.X - tamaño, p.Y + tamaño));
                    adyacentes.Add(new PointF(p.X, p.Y + tamaño));
                    adyacentes.Add(new PointF(p.X + tamaño, p.Y + tamaño));
                    adyacentes.Add(new PointF(p.X + tamaño, p.Y));
                    adyacentes.Add(new PointF(p.X + tamaño, p.Y - tamaño));
                    adyacentes.Add(new PointF(p.X, p.Y - tamaño));
                }

                //Limpiar la lista de proximas celulas vivas y muertas
                proximasvivas.Clear();
                proximasmuertas.Clear();

                // Diccionario de tipo punto / repeticiones para contar las veces que se repite la aparicion de una celula adyacente
                Dictionary<PointF, int> diccionario = new Dictionary<PointF, int>();

                //Llenar el diccionario con los pares Punto/Repeticiones correspondiente a cada celula adyacente.
                foreach (PointF p in adyacentes)
                {
                    if (diccionario.ContainsKey(p))
                    {
                        diccionario[p]++;
                    }
                    else
                        diccionario.Add(p, 1);
                }
                // Añadir las celulas adyacentes que se repitan el numero de veces determinado en las reglas para que
                // una celula nazca o continue viva a la lista de proximas vivas, o a la lista de proximas muertas si
                // no se repite esa cantidad de veces.
                foreach (KeyValuePair<PointF, int> k in diccionario)
                {
                    if (nacera.Contains(k.Value) && !celulas.Contains(k.Key) && !proximasvivas.Contains(k.Key))
                        proximasvivas.Add(k.Key);
                    else if (celulas.Contains(k.Key) && !continuaviva.Contains(k.Value))
                        proximasmuertas.Add(k.Key);
                }
                // Eliminar las celulas que esten aisladas

                foreach (PointF p in celulas)
                {
                    bool aislada = true;

                    if (celulas.Contains(new PointF(p.X - tamaño, p.Y - tamaño)))
                        aislada = false;
                    else if (aislada && celulas.Contains(new PointF(p.X - tamaño, p.Y)))
                        aislada = false;
                    else if (aislada && celulas.Contains(new PointF(p.X - tamaño, p.Y + tamaño)))
                        aislada = false;
                    else if (aislada && celulas.Contains(new PointF(p.X, p.Y + tamaño)))
                        aislada = false;
                    else if (aislada && celulas.Contains(new PointF(p.X + tamaño, p.Y + tamaño)))
                        aislada = false;
                    else if (aislada && celulas.Contains(new PointF(p.X + tamaño, p.Y)))
                        aislada = false;
                    else if (aislada && celulas.Contains(new PointF(p.X + tamaño, p.Y - tamaño)))
                        aislada = false;
                    else if (aislada && celulas.Contains(new PointF(p.X, p.Y - tamaño)))
                        aislada = false;

                    if (aislada && !continuaviva.Contains(0))
                        proximasmuertas.Add(p);
                }

                DateTime calculoterminado = DateTime.Now;
                TimeSpan tiempocal = calculoterminado - iniciocalculo;
                tiempocalculo = (int)tiempocal.TotalMilliseconds;
                disponible = true;
                Monitor.PulseAll(this);
            }
        }




        ////////////// MANEJADORES /////////////

        /// <summary>
        /// 
        /// MUESTRA U OCULTA LOS BOTONES DE ZOOM Y CHEQUEA O DESCHEQUEA LA OPCION DEL MENU
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void botonesZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dibujando)
                return;
            if (!botonesZoomToolStripMenuItem.Checked)
            {
                btZoomMas.Show();
                btZoomMenos.Show();
                botonesZoomToolStripMenuItem.Checked = true;
            }
            else
            {
                btZoomMas.Hide();
                btZoomMenos.Hide();
                botonesZoomToolStripMenuItem.Checked = false;
            }
        }

        /// <summary>
        /// 
        /// ZOOM MAS
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void ampliarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grafico.ScaleTransform(1.25F, 1.25F);
            grafico.TranslateTransform(pbFondo.Width / -16, pbFondo.Height / -16);
            grafico.Clear(fondo);
            Pen linea = new Pen(Color.Red, 1);
            try
            {
                foreach (PointF p in celulas)
                {
                    grafico.FillRectangle(new SolidBrush(Color.FromArgb(155, 250, 5)), p.X, p.Y, tamaño, tamaño);
                    grafico.DrawRectangle(linea, p.X, p.Y, tamaño, tamaño);
                }
            }
            catch (InvalidOperationException)
            {
                return;
            }
            pbFondo.Invalidate();
        }

        /// <summary>
        /// 
        /// ZOOM MENOS
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void reducirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grafico.ScaleTransform(0.75F, 0.75F);
            grafico.TranslateTransform(pbFondo.Width / 8, pbFondo.Height / 8);
            grafico.Clear(fondo);

            Pen linea = new Pen(Color.Red, 1);
            try
            {
                foreach (PointF p in celulas)
                {
                    grafico.FillRectangle(new SolidBrush(Color.FromArgb(155, 250, 5)), p.X, p.Y, tamaño, tamaño);
                    grafico.DrawRectangle(linea, p.X, p.Y, tamaño, tamaño);
                }
            }
            catch (InvalidOperationException)
            {
                return;
            }
            pbFondo.Invalidate();
        }

        /// <summary>
        /// 
        /// MUESTRA U OCULTA LOS BOTONES DE DESPLAZAMIENTO Y CHEQUEA O DESCHEQUEA LAS OPCIONES DEL MENU
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void botonesDeDesplazamientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dibujando)
                return;

            if (!botonesDeDesplazamientoToolStripMenuItem.Checked)
            {
                btArriba.Show();
                btAbajo.Show();
                btDerecha.Show();
                btIzquierda.Show();
                botonesDeDesplazamientoToolStripMenuItem.Checked = true;
            }
            else
            {
                btArriba.Hide();
                btAbajo.Hide();
                btDerecha.Hide();
                btIzquierda.Hide();
                botonesDeDesplazamientoToolStripMenuItem.Checked = false;
            }
        }
        /// <summary>
        /// 
        /// DESPLAZAMIENTO A LA IZQUIERDA
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btIzquierda_Click(object sender, EventArgs e)
        {
            grafico.Clear(fondo);

            grafico.TranslateTransform(origen.X - (tamaño * 5), origen.Y);
            Pen linea = new Pen(Color.Red, 1);
            try
            {
                foreach (PointF p in celulas)
                {
                    grafico.FillRectangle(new SolidBrush(Color.FromArgb(155, 250, 5)), p.X, p.Y, tamaño, tamaño);
                    grafico.DrawRectangle(linea, p.X, p.Y, tamaño, tamaño);
                }
            }
            catch (InvalidOperationException)
            {
                return;
            }
            pbFondo.Invalidate();
        }

        /// <summary>
        /// 
        /// DESPLAZAMIENTO ARRIBA
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btArriba_Click(object sender, EventArgs e)
        {
            grafico.Clear(fondo);

            grafico.TranslateTransform(origen.X, origen.Y - (tamaño * 5));
            Pen linea = new Pen(Color.Red, 1);
            try
            {
                foreach (PointF p in celulas)
                {
                    grafico.FillRectangle(new SolidBrush(Color.FromArgb(155, 250, 5)), p.X, p.Y, tamaño, tamaño);
                    grafico.DrawRectangle(linea, p.X, p.Y, tamaño, tamaño);
                }
            }
            catch (InvalidOperationException)
            {
                return;
            }
            pbFondo.Invalidate();
        }

        /// <summary>
        /// 
        /// DESPLAZAMIENTO A LA DERECHA
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDerecha_Click(object sender, EventArgs e)
        {
            grafico.Clear(fondo);

            grafico.TranslateTransform(origen.X + (tamaño * 5), origen.Y);
            Pen linea = new Pen(Color.Red, 1);

            try
            {
                foreach (PointF p in celulas)
                {
                    grafico.FillRectangle(new SolidBrush(Color.FromArgb(155, 250, 5)), p.X, p.Y, tamaño, tamaño);
                    grafico.DrawRectangle(linea, p.X, p.Y, tamaño, tamaño);
                }
            }
            catch (InvalidOperationException)
            {
                return;
            }
            pbFondo.Invalidate();
        }

        /// <summary>
        /// 
        /// DESPLAZAMIENTO ABAJO
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAbajo_Click(object sender, EventArgs e)
        {
            grafico.Clear(fondo);

            grafico.TranslateTransform(origen.X, origen.Y + (tamaño * 5));
            Pen linea = new Pen(Color.Red, 1);
            try
            {
                foreach (PointF p in celulas)
                {
                    grafico.FillRectangle(new SolidBrush(Color.FromArgb(155, 250, 5)), p.X, p.Y, tamaño, tamaño);
                    grafico.DrawRectangle(linea, p.X, p.Y, tamaño, tamaño);
                }
            }
            catch (InvalidOperationException)
            {
                return;
            }
            pbFondo.Invalidate();
        }

        /// <summary>
        /// 
        /// GUARDAR LA FORMA EN SU ESTADO ACTUAL CON LAS REGLAS Y COLORES ESTABLECIDOS
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //    Estado state = new Estado(celulas, ciclos);
            string fichero;
            FileStream fs;
            BinaryWriter bw;

            SaveFileDialog dialogosalvar = new SaveFileDialog();
            dialogosalvar.Filter = "Archivos de forma (*.fm)|*.fm";

            if (dialogosalvar.ShowDialog() == DialogResult.OK)
            {
                fichero = dialogosalvar.FileName;
                fs = new FileStream(fichero, FileMode.Create, FileAccess.Write);
                bw = new BinaryWriter(fs);
                try
                {
                    //Salvar las reglas establecidas
                    //Salvar el largo de la lista para la cantidad de celulas vivas adyacentes para que una celula permanezca viva
                    bw.Write(continuaviva.Count);
                    //Salvar el contenido de la lista
                    foreach (int i in continuaviva)
                        bw.Write(i);
                    //Salvar el largo de la lista para la cantidad de celulas adyacentes para que una celula nazca
                    bw.Write(nacera.Count);
                    //Salvar el contenido de la lista
                    foreach (int i in nacera)
                        bw.Write(i);
                    //Salvar los colores
                    //Salvar el color de la linea
                    byte exteriorr = exterior.R;
                    byte exteriorg = exterior.G;
                    byte exteriorb = exterior.B;
                    bw.Write(exteriorr);
                    bw.Write(exteriorg);
                    bw.Write(exteriorb);
                    //Salvar el interior de las celulas vivas
                    byte interiorr = interior.R;
                    byte interiorg = interior.G;
                    byte interiorb = interior.B;
                    bw.Write(interiorr);
                    bw.Write(interiorg);
                    bw.Write(interiorb);
                    //Salvar el fondo
                    byte fondor = fondo.R;
                    byte fondog = fondo.G;
                    byte fondob = fondo.B;
                    bw.Write(fondor);
                    bw.Write(fondog);
                    bw.Write(fondob);
                    //Salvar el interior de las celulas muertas en el turno
                    byte interiorMr = interiorM.R;
                    byte interiorMg = interiorM.G;
                    byte interiorMb = interiorM.B;
                    bw.Write(interiorMr);
                    bw.Write(interiorMg);
                    bw.Write(interiorMb);
                    //Salvar el interior de las celulas nacidas en el turno
                    byte interiorVr = interiorV.R;
                    byte interiorVg = interiorV.G;
                    byte interiorVb = interiorV.B;
                    bw.Write(interiorVr);
                    bw.Write(interiorVg);
                    bw.Write(interiorVb);
                    //Salvar los puntos

                    foreach (PointF p in celulas)
                    {
                        bw.Write(p.X);
                        bw.Write(p.Y);
                    }
                    //  MessageBox.Show(ciclos.ToString());
                    string aux = ciclos.ToString();
                    float cic = Single.Parse(aux);
                    bw.Write(cic);
                }
                catch (IOException er)
                {
                    MessageBox.Show("Error de escritura." + er.Message);
                }
                finally
                {
                    fs.Close();
                    bw.Close();
                }
            }
            else
                return;
        }

        /// <summary>
        /// 
        /// CARGAR UNA FORMA PREVIAMENTE GUARDADA
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Resetear el ciclo automatico , el guardado automatico, los ciclos de la sesion ,las listas y parar el timer
            cicloautomatico = false;
            guardadoautomatico = false;
            guardadoAutomaticoDeImagenenesToolStripMenuItem.Checked = false;
            timer1.Stop();
            celulas.Clear();
            ciclossesion = 0;

            if (muertasanteriores != null)
                muertasanteriores.Clear();
            if (proximasmuertas != null)
                proximasmuertas.Clear();
            if (proximasvivas != null)
                proximasvivas.Clear();
            if (vivasanteriores != null)
                vivasanteriores.Clear();
            adyacentes.Clear();

            // Restaurar el ciclo por defecto
            ciclo = 1000;
            nudVelocidad.BackColor = Color.White;
            nudVelocidad.Value = ciclo;

            //Resetear los graficos
            bitmap = new Bitmap(pbFondo.Width, pbFondo.Height);
            grafico = Graphics.FromImage(bitmap);
            grafico.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            pbFondo.Image = bitmap;
            grafico.Clear(fondo);
            pbFondo.Invalidate();

            //Lanzar el FileDialog para abrir el fichero
            OpenFileDialog dialogoabrir = new OpenFileDialog();
            dialogoabrir.Filter = "Archivos de forma (*.fm)|*.fm";
            if (dialogoabrir.ShowDialog() == DialogResult.OK)
            {
                string fichero = dialogoabrir.FileName;
                FileStream fs = new FileStream(fichero, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                List<float> lecturas = new List<float>();
                try
                {
                    //Obtener el largo de la lista de celulas vivas para que una celula permanezca viva
                    int largocontinuaviva = br.ReadInt32();
                    //Llenar la lista
                    continuaviva.Clear();
                    for (int i = 0; i < largocontinuaviva; i++)
                        continuaviva.Add(br.ReadInt32());
                    //Obtener el largo de la lista de celulas para que una celula nazca
                    int largonacera = br.ReadInt32();
                    //Llenar la lista 
                    nacera.Clear();
                    for (int i = 0; i < largonacera; i++)
                        nacera.Add(br.ReadInt32());
                    //Obtener el color de la linea exterior
                    byte exteriorr = br.ReadByte();
                    byte exteriorg = br.ReadByte();
                    byte exteriorb = br.ReadByte();
                    exterior = Color.FromArgb(exteriorr, exteriorg, exteriorb);
                    //Obtener el color del interior de las celulas vivas
                    byte interiorr = br.ReadByte();
                    byte interiorg = br.ReadByte();
                    byte interiorb = br.ReadByte();
                    interior = Color.FromArgb(interiorr, interiorg, interiorb);
                    //Obtener el color del fondo
                    byte fondor = br.ReadByte();
                    byte fondog = br.ReadByte();
                    byte fondob = br.ReadByte();
                    fondo = Color.FromArgb(fondor, fondog, fondob);
                    //Obtener el color del interior de las celulas muertas en el turno
                    byte interiorMr = br.ReadByte();
                    byte interiorMg = br.ReadByte();
                    byte interiorMb = br.ReadByte();
                    interiorM = Color.FromArgb(interiorMr, interiorMg, interiorMb);
                    //Obtener el color del interior de las celulas nacidas en el turno
                    byte interiorVr = br.ReadByte();
                    byte interiorVg = br.ReadByte();
                    byte interiorVb = br.ReadByte();
                    interiorV = Color.FromArgb(interiorVr, interiorVg, interiorVb);
                    //Cargar las celulas
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
                    MessageBox.Show("Error de lectura." + err);
                }
                finally
                {
                    fs.Close();
                    br.Close();
                }

                for (int i = 0; i < lecturas.Count - 1; i += 2)
                    celulas.Add(new PointF(lecturas[i], lecturas[i + 1]));

                ciclos = (int)lecturas[lecturas.Count - 1];
                celulasant = new List<PointF>();
                // Ocultar y mostrar los controles adecuados
                rbDibujar.Hide();
                lbPoblacion.Show();
                lbCiclos.Show();
                //Dibujar las celulas
                lock (celulas)
                {
                    foreach (PointF p in celulas)
                    {
                        grafico.FillRectangle(new SolidBrush(Color.FromArgb(155, 250, 5)), p.X, p.Y, tamaño, tamaño);
                        grafico.DrawRectangle(Pens.Red, p.X, p.Y, tamaño, tamaño);
                    }
                }

                MostrarControles();

                // Cambiar el color , texto y manejador del boton Aceptar

                if (cuentapartidas == 0)
                    btAceptar.Click += btAceptar_Click;
                else
                {
                    btAceptar.BackColor = System.Drawing.Color.Chartreuse;
                    btAceptar.Text = "Aceptar";
                    btAceptar.ForeColor = Color.Black;
                    btAceptar.Click -= btPausa_Click;
                    btAceptar.Click += btAceptar_Click;
                }

                btAceptar.Visible = true;
                btAceptar.Location = new Point(this.ClientSize.Width / 2, this.ClientSize.Height - btAceptar.Size.Height - 5);
                cuentapartidas++;

                disponible = true;

                linea = new Pen(exterior, 1);
                lineamuerta = new Pen(fondo, 1);
                pbFondo.Invalidate();
            }
            else
                return;

        }

        /// <summary>
        /// 
        /// MANEJADOR QUE HACE APARECER EL NUMERIC UP & DOWN PARA CAMBIAR EL PERIODO ENTRE CICLOS
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void cambiarPeriodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cambiarPeriodoToolStripMenuItem.Checked)
            {
                nudVelocidad.Hide();
                lbRotulonud.Hide();
                cambiarPeriodoToolStripMenuItem.Checked = false;
            }
            else
            {
                nudVelocidad.Show();
                lbRotulonud.Hide();
                cambiarPeriodoToolStripMenuItem.Checked = true;
            }
        }

        /// <summary>
        /// 
        /// CAMBIA EL VALOR DEL PERIODO SEGUN EL VALOR EN EL NUMERIC UP & DOWN
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void nudVelocidad_ValueChanged(object sender, EventArgs e)
        {
            ciclo = (int)nudVelocidad.Value;
            timer1.Interval = ciclo;
        }

        /// <summary>
        /// 
        /// MUESTRA EL DIALOGO PARA CAMBIAR LAS REGLAS DEL JUEGO
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cambiarReglasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Creacion del formulario para el cuadro de dialogo
            dialogoreglas = new Form();
            dialogoreglas.Size = new Size(700, 300);
            dialogoreglas.Owner = this;
            dialogoreglas.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            if (ingles)
                dialogoreglas.Text = "Rules by defect (23/3) change";
            else
                dialogoreglas.Text = "Cambio de las reglas por defecto (23/3)";
            dialogoreglas.BackColor = Color.SeaGreen;
            dialogoreglas.Show();
            dialogoreglas.StartPosition = FormStartPosition.Manual;
            dialogoreglas.Location = new Point(this.Location.X + 10, this.Location.Y + 10);
            // Etiqueta para la cantidad de celulas adyacentes para que una celula permanezca viva
            Label lbViva = new Label();
            dialogoreglas.Controls.Add(lbViva);
            lbViva.Font = new Font("Dejavu-Sans", 12, FontStyle.Underline);
            lbViva.AutoSize = true;
            lbViva.Visible = true;
            lbViva.Location = new Point(10, 20);
            if (ingles)
                lbViva.Text = "Number of adjacent alive cells for a cell to remain alive.";
            else
                lbViva.Text = "Cantidad de células adyacentes para que una célula permanezca viva: ";
            // Caja de texto para la cantidad de celulas adyacentes para que una celula permanezca viva
            tbViva = new TextBox();
            tbViva.Name = "tbViva";
            tbViva.AutoSize = false;
            dialogoreglas.Controls.Add(tbViva);
            tbViva.Font = new Font("Dejavu-Sans", 12);
            tbViva.TextAlign = HorizontalAlignment.Center;
            tbViva.Size = new Size(100, 25);
            tbViva.Location = new Point(lbViva.Location.X + lbViva.Width + 5, lbViva.Location.Y);
            tbViva.KeyPress += CajasReglas_KeyPress;
            tbViva.Focus();
            // Etiqueta para la cantidad de celulas adyacentes para que una celula nazca
            Label lbNacida = new Label();
            dialogoreglas.Controls.Add(lbNacida);
            lbNacida.Font = lbViva.Font;
            lbNacida.AutoSize = true;
            lbNacida.Visible = true;
            lbNacida.Location = new Point(lbViva.Location.X, lbViva.Location.Y + lbViva.Height + 30);
            if (ingles)
                lbNacida.Text = "Number of adjacent alive cells for a cell to born";
            else
                lbNacida.Text = "Cantidad de células adyacentes para que una célula nazca: ";
            // Caja de texto para la cantidad de celulas adyacentes para que una celula nazca
            tbNacida = new TextBox();
            tbNacida.Name = "tbNacida";
            dialogoreglas.Controls.Add(tbNacida);
            tbNacida.Font = tbViva.Font;
            tbNacida.TextAlign = HorizontalAlignment.Center;
            tbNacida.Size = tbViva.Size;
            tbNacida.Location = new Point(lbNacida.Location.X + lbNacida.Width + 5, lbNacida.Location.Y);
            tbNacida.KeyPress += CajasReglas_KeyPress;
            //Etiqueta con los ejemplos;
            Label lbEjemplos = new Label();
            lbEjemplos.Font = new Font("Dejavu-Sans", 9);
            lbEjemplos.Location = new Point(lbNacida.Location.X, lbNacida.Location.Y + 40);
            lbEjemplos.AutoSize = true;
            lbEjemplos.Visible = true;
            dialogoreglas.Controls.Add(lbEjemplos);
            if (ingles)
                lbEjemplos.Text = " Examples: \n 01234567 / 3 ---> Moderate growing.\n 23 / 36 ---> Hight life.\n 1357 / 1357 ---> Replicants.Fast growing.\n 235678 / 3678 ---> Diamonds, fast growing.\n 34 / 34 ---> Stable.\n 4 / 2 ---> Moderate growing.\n 51 / 346 ---> Average life. ";
            else
                lbEjemplos.Text = " Ejemplos: \n 01234567 / 3 ---> Crecimiento moderado.\n 23 / 36 ---> Hight life.\n 1357 / 1357 ---> Replicantes.Crecimiento rápido.\n 235678 / 3678 ---> Rombos, crecimiento rápido.\n 34 / 34 ---> Estable.\n 4 / 2 ---> Crecimiento moderado.\n 51 / 346 ---> Vida media. ";

        }

        /// <summary>
        ///  
        /// CONTROLA QUE SOLO SE ESCRIBEN NUMEROS DEL 0 AL 8 Y EL TEXTO NO ES MAS LARGO DE 8 DIGITOS
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CajasReglas_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox caja = (TextBox)sender;
            if (caja.Text.Length < 8 && e.KeyChar >= '0' && e.KeyChar <= '8')
                e.Handled = false;
            else if (e.KeyChar == 8) // Tecla retroceso
                e.Handled = false;
            else if (e.KeyChar == 13)// Tecla enter
            {
                if (caja.Text.Length == 0)
                {
                    MessageBox.Show("Hay que introducir algun valor.");
                    e.Handled = true;
                }
                else if (caja.Name == "tbViva")
                {
                    tbNacida.Focus();
                    e.Handled = true;
                }
                else if (caja.Name == "tbNacida")
                {
                    viva = tbViva.Text;
                    nacida = tbNacida.Text;
                    // Lista de la cantidad de celulas adyacentes para que una celula siga viva segun las reglas 
                    continuaviva.Clear();
                    for (int i = 0; i < viva.Length; i++)
                        continuaviva.Add(Int32.Parse(viva[i].ToString()));
                    // Lista de la cantidad de celulas adyacentes para que una celula nazca segun las reglas
                    nacera.Clear();
                    for (int i = 0; i < nacida.Length; i++)
                        nacera.Add(Int32.Parse(nacida[i].ToString()));
                    this.Text = "OrgaSnismo0      ( Daniel Santos version )                      ( Mantenerse viva: " + viva + "  Nacer: " + nacida + " )";
                    dialogoreglas.Close();
                }
            }
            else if (caja.Text.Length >= 8)
            {
                MessageBox.Show("No se pueden introducir más de 8 digitos");
                e.Handled = true;
            }
            else
                e.Handled = true;

        }


        /// <summary>
        /// 
        /// HACE SONAR LA RAFAGA DE SALIDA DEL PROGRAMA
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            SoundPlayer reproductor = new SoundPlayer();
            reproductor.SoundLocation = Directory.GetCurrentDirectory() + "\\Rafaga-Organismo-Salida.wav";
            reproductor.Play();
            // Hacer una pausa para dar tiempo a que suene la rafaga
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(400);
            }
            this.Dispose();
            System.Environment.Exit(0);
        }

        private void volverAReglasPorDefectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viva = "23";
            nacida = "3";
            //Limpiar las listas de valores correspondientes a las reglas
            continuaviva.Clear();
            nacera.Clear();
            //Llenar las listas anteriores con los valores de las reglas por defecto
            foreach (char c in viva)
                continuaviva.Add(Int32.Parse(c.ToString()));
            foreach (char c in nacida)
                nacera.Add(Int32.Parse(c.ToString()));
            // Cambiar el rotulo del formulario  y establecer los colores por defecto
            this.Text = "OrgaSnismo0      ( Daniel Santos edition )                      ( Mantenerse viva: " + viva + "  Nacer: " + nacida + " )";
            exterior = Color.Red; //Color de la linea alrrededor de la celula viva por defecto
            interior = Color.FromArgb(155, 250, 5); // Color interior de la celula viva por defecto
            interiorV = Color.FromArgb(155, 250, 5); // Color interior de la nueva celula en el turno por defecto
            fondo = Color.Black; // Color del fondo por defecto
            grafico.Clear(fondo);
            interiorM = Color.Black; // Color de las celulas muertas en el turno por defecto.
            lineamuerta.Color = fondo;
            try
            {
                foreach (PointF p in celulas)
                {
                    grafico.FillRectangle(new SolidBrush(Color.FromArgb(155, 250, 5)), p.X, p.Y, tamaño, tamaño);
                    grafico.DrawRectangle(linea, p.X, p.Y, tamaño, tamaño);
                }
            }
            catch (InvalidOperationException)
            {
                return;
            }
        }

        /// <summary>
        /// 
        /// MUESTRA TODOS LOS CONTROLES
        /// 
        /// </summary>

        private void MostrarControles()
        {
            btZoomMas.Visible = true;
            btZoomMenos.Visible = true;
            botonesZoomToolStripMenuItem.Checked = true;

            btIzquierda.Visible = true;
            btDerecha.Visible = true;
            btArriba.Visible = true;
            btAbajo.Visible = true;
            botonesDeDesplazamientoToolStripMenuItem.Checked = true;

            nudVelocidad.Visible = true;
            lbRotulonud.Visible = true;
            cambiarPeriodoToolStripMenuItem.Checked = true;

        }


        /// <summary>
        /// 
        /// OCULTA TODOS LOS CONTROLES
        /// 
        /// </summary>

        private void OcultarControles()
        {
            btZoomMas.Visible = false;
            btZoomMenos.Visible = false;
            botonesZoomToolStripMenuItem.Checked = false;


            btIzquierda.Visible = false;
            btDerecha.Visible = false;
            btArriba.Visible = false;
            btAbajo.Visible = false;
            botonesDeDesplazamientoToolStripMenuItem.Checked = false;

            nudVelocidad.Visible = false;
            lbRotulonud.Visible = false;
            cambiarPeriodoToolStripMenuItem.Checked = false;
        }

        private void btPausa_Click(object sender, EventArgs e)
        {
            if (enmarcha)
            {
                btAceptar.BackColor = Color.Chartreuse;
                btAceptar.ForeColor = Color.Black;
                if (ingles)
                    btAceptar.Text = "Continue";
                else
                    btAceptar.Text = "Continuar";
                enmarcha = false;

                timer1.Stop();
            }
            else
            {
                btAceptar.BackColor = Color.Black;
                if (ingles)
                    btAceptar.Text = "Pause";
                else
                    btAceptar.Text = "Pausa";
                btAceptar.ForeColor = Color.Silver;
                enmarcha = true;
                timer1.Start();

            }
        }

        /// <summary>
        /// 
        /// MUESTRA EL CUADRO DE DIALOGO PARA CAMBIAR LOS COLORES
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cambiarColoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogoColores = new Form();
            DialogoColores.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            DialogoColores.BackColor = Color.SeaGreen;
            DialogoColores.Show();
            DialogoColores.Owner = this;
            DialogoColores.AutoSize = false;
            if (ingles)
                DialogoColores.Text = "Color change.";
            else
                DialogoColores.Text = "Cambiar Colores.";
            lbColorLinea = new Label();
            lbColorLinea.Visible = true;
            if (ingles)
                lbColorLinea.Text = "Line color.";
            else
                lbColorLinea.Text = "Color de la línea.";
            lbColorLinea.Font = new Font("Dejavu-Sans", 16, FontStyle.Bold);
            DialogoColores.Controls.Add(lbColorLinea);
            lbColorLinea.Location = new Point(10, 15);
            lbColorLinea.BackColor = Color.SeaGreen;
            lbColorLinea.AutoSize = true;
            lbColorLinea.ForeColor = exterior;
            lbColorLinea.Click += CambiarColorLinea;

            lbColorInterior = new Label();
            lbColorInterior.Visible = true;
            if (ingles)
                lbColorInterior.Text = "Alive cells color.";
            else
                lbColorInterior.Text = "Color de las células vivas.";
            lbColorInterior.Font = new Font("Dejavu-Sans", 16, FontStyle.Bold);
            DialogoColores.Controls.Add(lbColorInterior);
            lbColorInterior.Location = new Point(lbColorLinea.Location.X, lbColorLinea.Location.Y + lbColorLinea.Height + 10);
            lbColorInterior.BackColor = Color.SeaGreen;
            lbColorInterior.AutoSize = true;
            lbColorInterior.ForeColor = interior;
            lbColorInterior.Click += CambiarColorInterior;

            lbColorInteriorV = new Label();
            lbColorInteriorV.Visible = true;
            if (ingles)
                lbColorInteriorV.Text = "Born cells color.";
            else
                lbColorInteriorV.Text = "Color de las nuevas células en el turno.";
            lbColorInteriorV.Font = new Font("Dejavu-Sans", 16, FontStyle.Bold);
            DialogoColores.Controls.Add(lbColorInteriorV);
            lbColorInteriorV.Location = new Point(lbColorLinea.Location.X, lbColorInterior.Location.Y + lbColorLinea.Height + 10);
            lbColorInteriorV.BackColor = Color.SeaGreen;
            lbColorInteriorV.AutoSize = true;
            lbColorInteriorV.ForeColor = interiorV;
            lbColorInteriorV.Click += CambiarColorInteriorV;

            lbColorInteriorM = new Label();
            lbColorInteriorM.Visible = true;
            if (ingles)
                lbColorInteriorM.Text = "Dead cells color.";
            else
                lbColorInteriorM.Text = "Color de las células muertas en el turno.";
            lbColorInteriorM.Font = new Font("Dejavu-Sans", 16, FontStyle.Bold);
            DialogoColores.Controls.Add(lbColorInteriorM);
            lbColorInteriorM.Location = new Point(lbColorLinea.Location.X, lbColorInteriorV.Location.Y + lbColorLinea.Height + 10);
            lbColorInteriorM.BackColor = Color.SeaGreen;
            lbColorInteriorM.AutoSize = true;
            lbColorInteriorM.ForeColor = interiorM;
            lbColorInteriorM.Click += CambiarColorInteriorM;

            lbColorFondo = new Label();
            lbColorFondo.Visible = true;
            if (ingles)
                lbColorFondo.Text = "Background color.";
            else
                lbColorFondo.Text = "Color del fondo";
            lbColorFondo.Font = new Font("Dejavu-Sans", 16, FontStyle.Bold);
            DialogoColores.Controls.Add(lbColorFondo);
            lbColorFondo.Location = new Point(lbColorLinea.Location.X, lbColorInteriorM.Location.Y + lbColorInterior.Height + 10);
            lbColorFondo.BackColor = Color.SeaGreen;
            lbColorFondo.AutoSize = true;
            lbColorFondo.ForeColor = fondo;
            lbColorFondo.Click += CambiarColorFondo;

            DialogoColores.Size = new Size(lbColorInteriorM.Width + 100, 270);
        }

        private void CambiarColorLinea(object sender, EventArgs e)
        {
            ColorDialog dialogo = new ColorDialog();
            if (dialogo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                exterior = dialogo.Color;
                lbColorLinea.ForeColor = exterior;
            }
        }
        private void CambiarColorInterior(object sender, EventArgs e)
        {
            ColorDialog dialogo = new ColorDialog();
            if (dialogo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                interior = dialogo.Color;
                lbColorInterior.ForeColor = interior;
            }

        }
        private void CambiarColorInteriorV(object sender, EventArgs e)
        {
            ColorDialog dialogo = new ColorDialog();
            if (dialogo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                interiorV = dialogo.Color;
                lbColorInteriorV.ForeColor = interiorV;
            }

        }
        private void CambiarColorInteriorM(object sender, EventArgs e)
        {
            ColorDialog dialogo = new ColorDialog();
            if (dialogo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                interiorM = dialogo.Color;
                lbColorInteriorM.ForeColor = interiorM;
            }

        }
        private void CambiarColorFondo(object sender, EventArgs e)
        {
            ColorDialog dialogo = new ColorDialog();
            if (dialogo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fondo = dialogo.Color;
                interiorM = fondo;
                lineamuerta.Color = fondo;
                lbColorFondo.ForeColor = fondo;
            }
            grafico.Clear(fondo);

            lock (celulas)
            {
                foreach (PointF p in celulas)
                {
                    grafico.FillRectangle(new SolidBrush(interior), p.X, p.Y, tamaño, tamaño);
                    grafico.DrawRectangle(linea, p.X, p.Y, tamaño, tamaño);
                }
            }
        }

        /// <summary>
        /// 
        /// PINTA LA CELULAS NUEVAS CON LOS COLORES ESCOGIDOS Y LAS MUERTAS DE COLOR NEGRO
        /// 
        /// </summary>
        /// 
        private void PintarCelulas()
        {
            if (proximasvivas.Count == 0 && proximasmuertas.Count == 0)
            {
                grafico.Clear(fondo);
                lock (celulas)
                {
                    foreach (PointF p in celulas)
                    {
                        grafico.FillRectangle(new SolidBrush(interior), p.X, p.Y, tamaño, tamaño);
                        grafico.DrawRectangle(linea, p.X, p.Y, tamaño, tamaño);
                    }
                }
            }



            foreach (PointF p in muertasanteriores) // Pintar las celulas muertas ya antes del color del fondo
            {
                grafico.FillRectangle(new SolidBrush(fondo), p.X, p.Y, tamaño, tamaño);
                grafico.DrawRectangle(lineamuerta, p.X, p.Y, tamaño, tamaño);
            }

            foreach (PointF p in vivasanteriores) // Pintar las celulas vivas ya antes del color del interior de las celulas
            {
                grafico.FillRectangle(new SolidBrush(interior), p.X, p.Y, tamaño, tamaño);
                grafico.DrawRectangle(lineamuerta, p.X, p.Y, tamaño, tamaño);
            }

            foreach (PointF p in proximasmuertas) // Pintar las celulas que mueren este turno del color elegido
            {
                grafico.FillRectangle(new SolidBrush(interiorM), p.X, p.Y, tamaño, tamaño);
                grafico.DrawRectangle(lineamuerta, p.X, p.Y, tamaño, tamaño);
            }
            foreach (PointF p in proximasvivas) // Pintar las nuevas celulas 
            {
                grafico.FillRectangle(new SolidBrush(interiorV), p.X, p.Y, tamaño, tamaño);
                grafico.DrawRectangle(linea, p.X, p.Y, tamaño, tamaño);
            }

            pbFondo.Invalidate();
        }

        private void lbEgo_MouseEnter(object sender, EventArgs e)
        {
            lbEgo.BackColor = Color.SeaGreen;
            lbEgo.ForeColor = Color.Black;
        }

        private void lbEgo_MouseLeave(object sender, EventArgs e)
        {
            lbEgo.BackColor = fondo;
            lbEgo.ForeColor = fondo;
        }

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
        /// GUARDA EL BITMAP EN LA UBICACION Y NOMBRE ELEGIDO POR EL USUARIO
        /// CUANDO SE PULSA LA OPCION DEL MENU
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guardarImagenToolStripMenuItem_Click(object sender, EventArgs e)
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
        /// ACTIVA LA OPCION DE GUARDADO AUTOMATICO DE IMAGENES QUE GUARDA CADA CICLO EL BITMAP EN LA UBICACION
        /// ELEGIDA POR EL USUARIO CON EL NOMBRE ELEGIDO POR EL USUARIO MAS EL CICLO
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guardadoAutomaticoDeImagenenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!guardadoautomatico)
            {
                SaveFileDialog dialogo = new SaveFileDialog();
                dialogo.Filter = "Bitmap: (*.bmp)|*.bmp";
                if (dialogo.ShowDialog() == DialogResult.OK)
                {
                    prefijo = dialogo.FileName;
                }
                guardadoAutomaticoDeImagenenesToolStripMenuItem.Checked = true;
                guardadoautomatico = true;
            }
            else
            {
                guardadoautomatico = false;
                guardadoAutomaticoDeImagenenesToolStripMenuItem.Checked = false;
            }
        }
        /// <summary>
        /// 
        /// ESTABLECE EL IDIOMA DE LOS MENUS Y LAS INTRUCCIONES EN CASTELLANO
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void catellanoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ingles = false;
            rbDibujar.Text = "Dibujar foma inicial.";
            lbPoblacion.Text = "Población: ";
            lbCiclos.Text = "Ciclos: ";
            lbRotulonud.Text = "Milisegundos:";
            btAceptar.Text = "Aceptar";
            Archivo.Text = "Archivo";
            nuevoToolStripMenuItem.Text = "Nuevo";
            guardarToolStripMenuItem.Text = "Guardar";
            abrirToolStripMenuItem.Text = "Cargar";
            guardarImagenToolStripMenuItem.Text = "Guardar imagen";
            zoomToolStripMenuItem.Text = "Controles";
            botonesDeDesplazamientoToolStripMenuItem.Text = "Botones de desplazamiento";
            botonesZoomToolStripMenuItem.Text = "Botones de zoom";
            cambiarPeriodoToolStripMenuItem.Text = "Cambiar Periodo";
            opcionesToolStripMenuItem.Text = "Opciones";
            cambiarReglasToolStripMenuItem.Text = "Cambiar Reglas";
            volverAReglasPorDefectoToolStripMenuItem.Text = "Volver a valores por defecto";
            cambiarColoresToolStripMenuItem.Text = "Cambiar Colores";
            guardadoAutomaticoDeImagenenesToolStripMenuItem.Text = "Guardado automático de imágenes";
            idiomaToolStripMenuItem.Text = "Idioma";
            catellanoToolStripMenuItem.Text = "Castellano";
            inglesToolStripMenuItem.Text = "Inglés";
            ayudaToolStripMenuItem.Text = "Ayuda";
        }

        /// <summary>
        /// 
        /// ESTABLECE EL IDIOMA DE LOS MENUS Y LAS INTRUCCIONES EN INGLES
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void inglesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ingles = true;
            rbDibujar.Text = "Draw initial shape.";
            lbPoblacion.Text = "Population: ";
            lbCiclos.Text = "Cicles: ";
            lbRotulonud.Text = "Milliseconds: ";
            btAceptar.Text = "Accept";
            Archivo.Text = "File";
            nuevoToolStripMenuItem.Text = "New";
            guardarToolStripMenuItem.Text = "Save";
            abrirToolStripMenuItem.Text = "Load";
            guardarImagenToolStripMenuItem.Text = "Save image";
            zoomToolStripMenuItem.Text = "Controls";
            botonesDeDesplazamientoToolStripMenuItem.Text = "Displacement buttons";
            botonesZoomToolStripMenuItem.Text = "Zoom buttons";
            cambiarPeriodoToolStripMenuItem.Text = "Change Period";
            opcionesToolStripMenuItem.Text = "Options";
            cambiarReglasToolStripMenuItem.Text = "Change Rules";
            volverAReglasPorDefectoToolStripMenuItem.Text = "Back to default values";
            cambiarColoresToolStripMenuItem.Text = "Change Colors";
            guardadoAutomaticoDeImagenenesToolStripMenuItem.Text = "Automatic image saver";
            idiomaToolStripMenuItem.Text = "Language";
            catellanoToolStripMenuItem.Text = "Spanish";
            inglesToolStripMenuItem.Text = "English";
            ayudaToolStripMenuItem.Text = "Help";
        }

        /// <summary>
        /// 
        /// MUESTRA EL CUADRO DE DIALOGO CON LAS INSTRUCCIONES 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ayuda = new Form();
            ayuda.Size = new Size(850, 500);
            ayuda.BackColor = Color.SeaGreen;
            ayuda.MaximizeBox = false;
            ayuda.FormBorderStyle = FormBorderStyle.FixedDialog;
            if (ingles)
                ayuda.Text = "OrgaSnismo0 functions.";
            else
                ayuda.Text = "Funciones de OrgaSnismo0";
            RichTextBox rtbAyuda = new RichTextBox();
            rtbAyuda.BackColor = Color.SeaGreen;
            rtbAyuda.Size = ayuda.Size;
            ayuda.Controls.Add(rtbAyuda);
            ayuda.AutoScroll = true;
            if (ingles)
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
                rtbAyuda.Text += "With the label: 'Milliseconds', allows to change the cicle time set in 1 second by defect.\n\t\t\t      If the time needed by the cpu to calculate the next state, is bigger than the cicle time\n\t\t\t      settled down, this is automaticaly changed by the program.\n\n";
                int normal3 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Accept button: ";
                int concepto4 = rtbAyuda.Text.Length;
                rtbAyuda.Text += " To start the cicles once the initial shape has benn drawn or loaded and the cicle time \n\t\t        stablished.\n\n";
                rtbAyuda.Text += "Once the game is started, the shape is going changing according to the rules at every new cicle.\nThe rules by defect, are the Conway´s rules i.e.:\nA cell remains alive, if it is adyacent to two or three alive cells, otherwise the cell dies at the next cicle.\nA cell borns, if it is adyacent to three alive cells.\n\n";
                rtbAyuda.Text += "At the screen bottom left, the population ( number of alive cells ) and the number of cicles passed from the start, are shown.\nAlso buttons for: Zoom, displacement and pause, appear.\n";
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
                rtbAyuda.Text += "Change period: To show or hide the numeric up & down with the cicle time.\n\n";
                int normal6 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Options:\n";
                int rotulo4 = rtbAyuda.Text.Length;
                rtbAyuda.Text += "Change rules: A window appers and allow to change the rules.\n\t\t  There are some rules examples with a brief description about their respective efects, at the \n\t\t  bottom of this window.\n\n";
                rtbAyuda.Text += "Back to default values: Returns to the rules, colors and cicle time by defect.\n\n";
                rtbAyuda.Text += "Change colors: Shows a dialog with the following sentences:\n\t\t     Line color, Alive cells color, Born cells color , Died cells color and Background color.\n\t\t     The color of every sentence corresponds to the color of every concept. Clicking over a \n\t\t     sentence, makes a color selection dialog to appear.\n\n";
                rtbAyuda.Text += "Beep: Activated by defect, makes a beep to sound at every cicle. It is useful in order to advice from a new \n\tscreen redraw in a long time cicles.\n\n";
                rtbAyuda.Text += "Automatic image saver: When it is activated, a directory chooser dialog is shown in order to choose the\t\t\t\t       directory where a screen shot *.bmp image is saved at every cicle.\n\n";
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
                rtbAyuda.Text += "Archivo:\n\n";
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
                rtbAyuda.Text += "Cambiar reglas: Muestra un cuadro de dialogo que permite introducir la cantidad de células adyacentes para \t\t\t      que una celula se mantenga viva ( por defecto 2 o 3 ) y la cantidad de celulas adyacentes \t\t\t      para que una célula nazca ( por defecto 3 ).\n\t\t      En la parte inferior de este cuadro de dialogo, se muestran algunos ejemplos y los efectos\t\t\t      que producen.\n\n";
                rtbAyuda.Text += "Volver a valores por defecto: Vuelve a establecer las reglas, colores y tiempo de ciclo por defecto.\n\n";
                rtbAyuda.Text += "Cambiar colores: Muestra un cuadro de dialogo donde aparecen las frases:\n\t\t       Color de la línea, Color de las células vivas, Color de las nuevas células en el turno , Color\t\t\t        de las células muertas en el turno y Color del fondo.\n\t\t        El color de letra de cada una de estas frases, corresponde al color establecido para cada\t\t\t        concepto. Haciendo click sobre estas frases, hace que se muestre una tabla de colores \t\t\t        para elegir un nuevo color.\n\n";
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
