using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DXF;
using G.Traducciones;
using G.Servicios;
using G.Objetos;
using System.IO;
using MacGen;
using DXF.Objetos;
using Configuracion;
using System.Reflection;

namespace CNCMatic
{
    public partial class Principal : Form
    {
        //Boolean flag = true;
        //int i = 0;
        private string mCncFile;
        private clsProcessor mProcessor = clsProcessor.Instance();
        private clsSettings mSetup = clsSettings.Instance();
        private MG_CS_BasicViewer mViewer;
        public bool pausado = false;


        public Principal()
        {
            InitializeComponent();

            //cargamos informacion en la barra de estado
            this.lblUserName.Text = "User: " + Environment.UserName;
            this.lblOsVersion.Text = "OS Version: " + Environment.OSVersion;
            this.lblMachName.Text = "PC: " + Environment.MachineName;

            mViewer = this.MG_Viewer1;
            //mProcessor.OnAddBlock += new clsProcessor.OnAddBlockEventHandler(mProcessor_OnAddBlock);

            MG_CS_BasicViewer.OnSelection += new MG_CS_BasicViewer.OnSelectionEventHandler(mViewer_OnSelection);
            MG_CS_BasicViewer.MouseLocation += new MG_CS_BasicViewer.MouseLocationEventHandler(mViewer_MouseLocation);
            MG_CS_BasicViewer.OnStatus += new MG_CS_BasicViewer.OnStatusEventHandler(mViewer_OnStatus);

            mSetup.MachineActivated += new clsSettings.MachineActivatedEventHandler(mSetup_MachineActivated);

            mSetup.LoadAllMachines(System.IO.Directory.GetCurrentDirectory() + "\\Data");
            mProcessor.Init(mSetup.Machine);
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            try
            {
                //enviamos al cnc al origen
                Interfaz.OrigenCNC(ref lblEstado, ref lblPosicionActual);

                //bloqueamos el inicio
                SetControlPropertyThreadSafe(btnInicio, "Enabled", false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Conectar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            AgregaTextoEditor(true, "");
            this.LimpiarPrevisualizador();
        }

        private void LimpiarPrevisualizador()
        {
            OpenFile(System.IO.Directory.GetCurrentDirectory() + "\\Samples\\Limpiar.cnc");
        }

        private void btnStop2_Click(object sender, EventArgs e)
        {
            this.lblEstado.Text = "Conexión cancelada por el usuario";

            Interfaz.DetenerCNC();

            //Habilita todas las funciones
            LimpiarControles();
            btnStop2.Enabled = false;
            btnConnect.Enabled = true;
            btnConnect.Visible = true;
            btnPause.Enabled = false;

            //habilitamos nuevamente el menu de configuracion
            configuracionToolStripMenuItem.Enabled = true;

            btnRestart.Enabled = false;

            //reiniciamos la barra
            prgBar.Value = 0;
        }

        private void LimpiarControles()
        {
            //Habilita todas las funciones y borra el fresado actual
            btnPlay.Enabled = true;
            btnInicio.Enabled = false;
            gbMovXY.Enabled = false;
            gbMovZ.Enabled = false;
            txtLineaManual.Enabled = true;
            btnLimpiar.Enabled = true;
            toolStrip1.Enabled = true;
            txtPreview.Enabled = true;


        }

        private void LimpiarControlesSafe()
        {
            //Habilita todas las funciones y borra el fresado actual en modo seguro para thread
            SetControlPropertyThreadSafe(btnPlay, "Enabled", true);
            SetControlPropertyThreadSafe(btnInicio, "Enabled", false);
            //SetControlPropertyThreadSafe(btnStop2, "Enabled", true);
            SetControlPropertyThreadSafe(gbMovXY, "Enabled", false);
            SetControlPropertyThreadSafe(gbMovZ, "Enabled", false);
            SetControlPropertyThreadSafe(txtLineaManual, "Enabled", true);
            SetControlPropertyThreadSafe(btnLimpiar, "Enabled", true);
            SetControlPropertyThreadSafe(toolStrip1, "Enabled", true);
            SetControlPropertyThreadSafe(txtPreview, "Enabled", true);


        }

        /// <summary>
        /// Funcion que agrega una nueva linea de texto al editor visual
        /// </summary>
        /// <param name="limpia">Establece si se debe vaciar el editor previamente</param>
        /// <param name="text">Texto a agregar en el editor</param>
        private void AgregaTextoEditor(bool limpia, string text)
        {
            //si no se se limpia sumamos el texto
            if (!limpia)
                this.txtPreview.Text += text;
            else
                this.txtPreview.Text = text;

            //si el texto no es blanco, sumamos una nueva linea
            if (text != "")
                this.txtPreview.Text += Environment.NewLine;
        }
        /// <summary>
        /// Funcion que limpia el texto contenido sobre el editor visual
        /// </summary>
        private void LimpiaTextoEditor()
        {
            this.txtPreview.Text = "";
        }

        private void dXFFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //realizamos la busqueda del archivo
                if (importaDXF.ShowDialog() == DialogResult.OK)
                {
                    //Realizamos la importacion del DXF
                    DxfDoc doc = new DxfDoc();
                    doc.Cargar(importaDXF.FileName);

                    //Analizamos las figuras
                    //if (!doc.AnalizarFiguras(Interfaz.ConfiguracionActual()))
                    //{
                    //    MessageBox.Show("Error: Se han encontrado figuras que superan el área de trabajo definido", "Importar DXF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}

                    //Realizamos la traduccion de las figuras a código G
                    List<string> sl = new List<string>();
                    sl.AddRange(Traduce.Lineas(doc.Lineas));
                    sl.AddRange(Traduce.Arcos(doc.Arcos));
                    sl.AddRange(Traduce.Circulos(doc.Circulos));
                    //sl.AddRange(Traduce.Elipses(doc.Elipses));
                    sl.AddRange(Traduce.Puntos(doc.Puntos));
                    sl.AddRange(Traduce.Polilineas(doc.Polilineas));

                    //Optimizamos las líneas de código G
                    sl = OptimizarCodigoG(sl);

                    //Mostramos el G en pantalla y previsualizamos
                    if (sl != null)
                    {
                        //Creo un archivo temporal para previsualizar
                        string curTempFileName = System.IO.Directory.GetCurrentDirectory() + "\\Samples\\Temp";

                        //limpiamos el texto contenido en el editor
                        LimpiaTextoEditor();

                        using (StreamWriter sw = File.CreateText(curTempFileName))
                        {
                            foreach (string s in sl)
                            {
                                AgregaTextoEditor(false, s);
                                sw.WriteLine(s);
                            }

                            sw.Close();
                            OpenFile(curTempFileName);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se han encontrado figuras para importar", "Importar DXF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido un error " + ex.Message, "Importar DXF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private double ObtieneDistancia(DXF.Objetos.Vector3d puntoActual, DXF.Objetos.Vector3d puntoInicioFigura)
        {
            double distancia;
            distancia = Math.Sqrt(Math.Pow((puntoActual.X - puntoInicioFigura.X), 2) + Math.Pow((puntoActual.Y - puntoInicioFigura.Y), 2) + Math.Pow((puntoActual.Z - puntoInicioFigura.Z), 2));
            return distancia;
        }

        private List<string> OptimizarCodigoG(List<string> list)
        {
            List<string> newList = new List<string>();
            Vector3d puntoActual = new Vector3d(0, 0, 0);

            //Eliminamos movimientos sobrantes
            list = QuitartMovimientos(list);

            // Variables
            int j;
            string tempFig;
            string auxiliar1;
            string auxiliar2;
            int posAux;
            int posFig;

            while (list.Count > 0)
            {
                auxiliar1 = list[0];
                posAux = 0;
                tempFig = list[1];
                posFig = 1;
                j = 2;
                while (j <= list.Count - 1)
                {
                    auxiliar2 = list[j];
                    if (ObtieneDistancia(
                            puntoActual,
                            new Vector3d(
                                Convert.ToDouble(PuntoInicioX(auxiliar1)),
                                Convert.ToDouble(PuntoInicioY(auxiliar1)),
                                Convert.ToDouble(PuntoInicioZ(auxiliar1)))
                            ) >
                        ObtieneDistancia(
                            puntoActual,
                            new Vector3d(
                                Convert.ToDouble(PuntoInicioX(auxiliar2)),
                                Convert.ToDouble(PuntoInicioY(auxiliar2)),
                                Convert.ToDouble(PuntoInicioZ(auxiliar2))
                            )
                        )
                    )
                    {
                        auxiliar1 = auxiliar2;
                        posAux = j;
                        tempFig = list[j + 1];
                        posFig = j + 1;
                        j = 0;
                    }
                    j = j + 2;
                }
                newList.Add(auxiliar1);
                newList.Add(tempFig);

                puntoActual = new Vector3d(Convert.ToDouble(PuntoInicioX(tempFig)), Convert.ToDouble(PuntoInicioY(tempFig)), Convert.ToDouble(PuntoInicioZ(tempFig)));

                //Elimino los elementos agregados a la nueva lista
                list.RemoveAt(posAux);
                list.RemoveAt(posAux);//Vuelvo a eliminar el mismo, porque al eliminarlo por primera vez, se corren un indice menos.
            }

            //Eliminamos movimientos cuando el pto de fin de una figura coincide con el de la siguiente figura
            newList = EliminarCodigoInnecesario(newList);

            //Agregamos a cada G00 la instruccion para que levante en z, se desplace y baje.
            newList = AgregarAccionesAG00(newList);

            return newList;
        }

        private List<string> QuitartMovimientos(List<string> list)
        {
            List<string> lista = new List<string>();
            foreach (string lineaG in list)
            {
                string subcadena;

                string[] partes = lineaG.Split('G');

                if (partes.Length > 2)
                {
                    subcadena = partes[2];
                }
                else
                {
                    subcadena = partes[1];
                }

                lista.Add("G" + subcadena.Trim());
            }
            return lista;
        }

        private List<string> EliminarCodigoInnecesario(List<string> newList)
        {
            if (newList.Count > 0)
            {

                List<string> lista = new List<string>();
                Vector3d punto1 = new Vector3d(0, 0, 0);
                Vector3d punto2 = new Vector3d(Convert.ToDouble(PuntoInicioX(newList[0])), Convert.ToDouble(PuntoInicioY(newList[0])), Convert.ToDouble(PuntoInicioZ(newList[0])));
                if (!(ObtenerDoubleTresDecimales(punto1) == ObtenerDoubleTresDecimales(punto2)))
                {
                    lista.Add(newList[0]);
                }

                int i = 1;

                while (i < newList.Count - 1)
                {
                    punto1 = new Vector3d(Convert.ToDouble(PuntoInicioX(newList[i])), Convert.ToDouble(PuntoInicioY(newList[i])), Convert.ToDouble(PuntoInicioZ(newList[i])));
                    punto2 = new Vector3d(Convert.ToDouble(PuntoInicioX(newList[i + 1])), Convert.ToDouble(PuntoInicioY(newList[i + 1])), Convert.ToDouble(PuntoInicioZ(newList[i + 1])));

                    if (ObtenerDoubleTresDecimales(punto1) == ObtenerDoubleTresDecimales(punto2))
                    {
                        lista.Add(newList[i]);
                    }
                    else
                    {
                        lista.Add(newList[i]);
                        lista.Add(newList[i + 1]);
                    }
                    i = i + 2;
                }

                lista.Add(newList[i]);

                return lista;
            }
            return null;
        }

        private Vector3d ObtenerDoubleTresDecimales(Vector3d num)
        {
            Vector3d d = new Vector3d();
            d.X = (double)(int)(num.X * 1000) / 1000;
            d.Y = (double)(int)(num.Y * 1000) / 1000;
            d.Z = (double)(int)(num.Z * 1000) / 1000;
            return d;
        }

        private List<string> AgregarAccionesAG00(List<string> newList)
        {
            //double deltaSubida = 0.5;
            double deltaSubida = Convert.ToDouble(Interfaz.ConfiguracionActual().AltoAscenso);

            string linea;
            string[] valoresZ;
            int valorZ;
            if (newList != null)
            {
                if (newList.Count > 0)
                {
                    List<string> lista = new List<string>();
                    foreach (string lineaG in newList)
                    {

                        if (lineaG.Substring(0, 3).Equals("G00"))
                        {
                            valoresZ = lineaG.Split('Z');
                            valorZ = Convert.ToInt16(valoresZ[1]);
                            linea = "G00 Z" + Convert.ToString(valorZ + deltaSubida) + Environment.NewLine + valoresZ[0] + "Z" +
                                Convert.ToString(valorZ + deltaSubida) + Environment.NewLine + "G00 Z" + Convert.ToString(valorZ);
                        }
                        else
                        {
                            linea = lineaG;
                        }
                        lista.Add(linea);
                    }
                    return lista;
                }
            }
            return null;
        }

        private string movimiento(string lineaG)
        {
            return lineaG.Substring(0, 3);
        }

        private string PuntoInicioX(string lineaG)
        {
            string[] partes = lineaG.Split(' ');

            string subcadena = partes[1].Substring(1, partes[1].Length - 1);

            return subcadena;
        }

        private string PuntoInicioY(string lineaG)
        {
            string[] partes = lineaG.Split(' ');

            string subcadena = partes[2].Substring(1, partes[2].Length - 1);

            return subcadena;
        }

        private string PuntoInicioZ(string lineaG)
        {
            string[] partes = lineaG.Split(' ');

            string subcadena = partes[3].Substring(1, partes[3].Length - 1);

            return subcadena;
        }

        private string PuntoFin(string lineaG)
        {
            return lineaG.Substring(1, 3);
        }

        private void gCodeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //realizamos la busqueda del archivo
            if (importaG.ShowDialog() == DialogResult.OK)
            {
                //importacion de codigo G
                Importacion imp = new Importacion();

                List<string> lineas = imp.leeGfile(importaG.FileName);

                foreach (string s in lineas)
                {
                    if (s.Equals("archivo no valido"))
                    {
                        MessageBox.Show("El archivo importado no es válido", "Error de importación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        this.txtPreview.Text += (s + Environment.NewLine);
                    }
                }

                //Muestra codigo en el previsualizador
                OpenFile(importaG.FileName);

                //string mensaje = "¿Desea que el sistema intente optimizar el código G importado? Atención: esta operación podría variar el orden de fresado preestablecido.";

                //DialogResult r = MessageBox.Show(mensaje, "Optimización de código G", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                //if (r == DialogResult.Yes)
                //{
                //    //lanzamos optimizacion de código G
                //}

            }




        }

        private void txtLineaManual_KeyPress(object sender, KeyPressEventArgs e)
        {
            //si la tecla presionada es Enter, agregamos la linea al editor
            if (e.KeyChar == (char)Keys.Enter)
            {
                string linea = this.txtLineaManual.Text.Trim();
                
                if (Char.ToUpper(linea[0]) != 'G' && Char.ToUpper(linea[0]) != 'M')
                {
                    ////limpiamos el enter ingresado
                    //e.KeyChar = new char();
                    MessageBox.Show("Instrucción \"" + linea + "\" no válida, por favor corregir", "Ingreso manual de instrucciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (!Char.IsNumber(linea[1]))
                {
                    ////limpiamos el enter ingresado
                    //e.KeyChar = new char();
                    MessageBox.Show("Instrucción \"" + linea + "\" no válida, por favor corregir", "Ingreso manual de instrucciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                AgregaTextoEditor(false, this.txtLineaManual.Text.Trim());
                this.txtLineaManual.Text = "";

                //Muestra figura en el previsualizador
                PrevisualizarFigurasManual();
            }

        }


        private void txtPreview_KeyPress(object sender, KeyPressEventArgs e)
        {
            //si la tecla presionada es Enter, agregamos la linea al editor
            if (e.KeyChar == (char)Keys.Enter)
            {
                ////limpiamos el enter ingresado
                e.KeyChar = new char();

                //removemos los blancos
                List<string> lineas = txtPreview.Lines.ToList();
                while (lineas.Contains(""))
                {
                    lineas.Remove("");
                }
                
                txtPreview.Text = "";
                foreach (string linea in lineas)
                {
                    txtPreview.Text += linea + Environment.NewLine;
                }

                foreach (string linea in lineas)
                {
                    if (Char.ToUpper(linea[0]) != 'G' && Char.ToUpper(linea[0]) != 'M')
                    {
                        ////limpiamos el enter ingresado
                        //e.KeyChar = new char();
                        MessageBox.Show("Instrucción \"" + linea + "\" no válida, por favor corregir", "Ingreso manual de instrucciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (!Char.IsNumber(linea[1]))
                    {
                        ////limpiamos el enter ingresado
                        //e.KeyChar = new char();
                        MessageBox.Show("Instrucción \"" + linea + "\" no válida, por favor corregir", "Ingreso manual de instrucciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                txtPreview.Select(txtPreview.Text.Length, 0);

                //Muestra figura en el previsualizador
                PrevisualizarFigurasManual();
            }
        }

        private void acercaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acerca a = new Acerca();
            a.ShowDialog();
        }

        #region DibujosManuales
        private void dToolStripMenuItem2_Click(object sender, EventArgs e)
        {

            G01_Cuadrado g;

            FrmDibujoParams dibujoParams = new FrmDibujoParams(out g);
            dibujoParams.ShowDialog();

            if (dibujoParams.modificado)
            {
                AgregaTextoEditor(false, g.ToString());

                //Muestra figura en el previsualizador
                PrevisualizarFigurasManual();
            }
        }

        private void menuItemCubo_Click(object sender, EventArgs e)
        {
            G01_Cubo g;

            FrmDibujoParams dibujoParams = new FrmDibujoParams(out g);
            dibujoParams.ShowDialog();

            if (dibujoParams.modificado)
            {
                AgregaTextoEditor(false, g.ToString());

                //Muestra figura en el previsualizador
                PrevisualizarFigurasManual();
            }
        }
        private void btnLinea_Click(object sender, EventArgs e)
        {
            G01_Lineal g;

            FrmDibujoParams dibujoParams = new FrmDibujoParams(out g);
            dibujoParams.ShowDialog();

            if (dibujoParams.modificado)
            {
                AgregaTextoEditor(false, g.ToString());

                //Muestra figura en el previsualizador
                PrevisualizarFigurasManual();
            }
        }

        private void btnArco_Click(object sender, EventArgs e)
        {
            G02_ArcoH g;

            FrmDibujoParams dibujoParams = new FrmDibujoParams(out g);
            dibujoParams.ShowDialog();

            if (dibujoParams.modificado)
            {
                AgregaTextoEditor(false, g.ToString());

                //Muestra figura en el previsualizador
                PrevisualizarFigurasManual();
            }

        }

        private void PrevisualizarFigurasManual()
        {
            string curTempFileName = System.IO.Directory.GetCurrentDirectory() + "\\Samples\\Temp";

            using (StreamWriter sw = File.CreateText(curTempFileName))
            {
                sw.WriteLine(txtPreview.Text);
                sw.Close();
                OpenFile(curTempFileName);
            }
        }

        private void btnCirculo_Click(object sender, EventArgs e)
        {
            G02_CirculoH g;

            FrmDibujoParams dibujoParams = new FrmDibujoParams(out g);
            dibujoParams.ShowDialog();

            if (dibujoParams.modificado)
            {
                AgregaTextoEditor(false, g.ToString());

                //Muestra figura en el previsualizador
                PrevisualizarFigurasManual();
            }
        }
        #endregion

        private void configuracionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmConfiguracion()).ShowDialog();

        }

        private void Principal_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Virgin == true)
            {
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                this.Location = Properties.Settings.Default.ViewFormLocation;
                this.Size = Properties.Settings.Default.ViewFormSize;
            }

            SetDefaultViews();
            Properties.Settings.Default.Virgin = false;
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPreview.Text.Trim() == "")
                {
                    MessageBox.Show("No hay información para guardar", "Guardar como...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Codigo G(*.gcode)|*.gcode";
                save.DefaultExt = "*.gcode";
                save.FileName = "*.gcode";
                save.ShowDialog();

                string filename = save.FileName;

                if (filename == "*.gcode" || filename == "")
                {
                    MessageBox.Show("Error: no se ha especificado un nombre de archivo valido", "Guardar como...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //guardamos el contenido del txtPreview en un archivo .gcode
                if (guardaGfile(filename))
                    MessageBox.Show("Se ha guardado el archivo correctamente", "Guardar como...", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido un error: " + ex.Message, "Guardar como...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool guardaGfile(string path)
        {
            try
            {

                StreamWriter sr = new StreamWriter(path);

                foreach (string linea in txtPreview.Lines)
                {
                    if (linea.Trim() != "")
                    {
                        sr.WriteLine(linea);
                    }
                }

                sr.Close();

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }


        }

        #region Previsualizador
        static void crearArchivoTemp()
        {

            string curTempFileName = "";

            curTempFileName = Path.GetTempFileName();

            //Ahora creamos fisicamente el archivo

            using (StreamWriter sw = File.CreateText(curTempFileName))
            {

                sw.WriteLine("Primera linea del archivo");

                sw.Close();

            }

            Console.WriteLine("Se ha creado el archivo temporal satisfactoriamente!!");

            Console.ReadLine();

        }
        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    Properties.Settings.Default.ViewFormLocation = this.Location;
                    Properties.Settings.Default.ViewFormSize = this.Size;
                }

                //mandamos a desconectar el puerto
                Interfaz.DesconectarCNC();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cerrar: " + ex.Message, "Salir", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mProcessor_OnAddBlock(int idx, int ct)
        {
            try
            {
                this.prgBar.Maximum = ct;
                this.prgBar.Value = idx;
                if (ct > 10000)
                {
                    //Refresh every 1000 blocks 
                    if (1000 % idx == 0)
                    {
                        mViewer.FindExtents();
                        mViewer.Redraw(true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("mProcessor_OnAddBlock: " + ex.Message, ex);
            }
        }

        private void mViewer_MouseLocation(float x, float y)
        {
            Coordinates.Text = "X=" + x.ToString("0.000") + " Y=" + y.ToString("0.000");
        }

        private void ViewportActivated(object sender, System.EventArgs e)
        {

        }

        private void SetDefaultViews()
        {
            //Case "Top" 
            MG_Viewer1.Pitch = 0f;
            MG_Viewer1.Roll = 0f;
            MG_Viewer1.Yaw = 0f;
            MG_Viewer1.FindExtents();
            mViewer.Redraw(true);
        }

        private void mViewer_OnSelection(System.Collections.Generic.List<clsMotionRecord> hits)
        {
            lblStatus.Text = hits[0].Codestring;
            string[] tipString = new string[hits.Count];
            for (int r = 0; r <= hits.Count - 1; r++)
            {
                tipString[r] = hits[r].Codestring;
            }
            this.CodeTip.SetToolTip(mViewer, string.Join(Environment.NewLine, tipString));
        }

        private void mViewer_OnStatus(string msg, int index, int max)
        {
            lblStatus.Text = msg;
            prgBar.Maximum = max;
            prgBar.Value = index;
            toolStrip2.Refresh();
        }

        private void mSetup_MachineActivated(clsMachine m)
        {
            {
                MG_Viewer1.RotaryDirection = (RotaryDirection)m.RotaryDir;
                MG_Viewer1.RotaryPlane = (Axis)m.RotaryAxis;
                MG_Viewer1.RotaryType = (RotaryMotionType)m.RotaryType;
                MG_Viewer1.ViewManipMode = MG_CS_BasicViewer.ManipMode.SELECTION;

                MG_Viewer1.FindExtents();
                mViewer.Redraw(true);

            }
        }

        private void ViewButtonClicked(object sender, EventArgs e)
        {
            string tag = sender.GetType().GetProperty("Tag").GetValue(sender, null).ToString();
            switch (tag)
            {
                case "Fit":
                    if (Control.ModifierKeys == Keys.Shift)
                    {
                        MG_Viewer1.FindExtents();
                    }
                    else
                    {
                        mViewer.FindExtents();
                    }

                    break;
                case "Pan":
                    mViewer.ViewManipMode = MG_CS_BasicViewer.ManipMode.PAN;
                    tsbPan.Checked = true;
                    tsbRotate.Checked = false;
                    tsbZoom.Checked = false;
                    tsbFence.Checked = false;
                    tsbSelect.Checked = false;
                    break;
                case "Fence":
                    mViewer.ViewManipMode = MG_CS_BasicViewer.ManipMode.FENCE;
                    tsbFence.Checked = true;
                    tsbRotate.Checked = false;
                    tsbZoom.Checked = false;
                    tsbPan.Checked = false;
                    tsbSelect.Checked = false;
                    break;
                case "Zoom":
                    mViewer.ViewManipMode = MG_CS_BasicViewer.ManipMode.ZOOM;
                    tsbZoom.Checked = true;
                    tsbRotate.Checked = false;
                    tsbFence.Checked = false;
                    tsbPan.Checked = false;
                    tsbSelect.Checked = false;
                    break;
                case "Rotate":
                    mViewer.ViewManipMode = MG_CS_BasicViewer.ManipMode.ROTATE;
                    tsbRotate.Checked = true;
                    tsbZoom.Checked = false;
                    tsbFence.Checked = false;
                    tsbPan.Checked = false;
                    tsbSelect.Checked = false;
                    break;
                case "Select":
                    mViewer.ViewManipMode = MG_CS_BasicViewer.ManipMode.SELECTION;
                    tsbRotate.Checked = false;
                    tsbZoom.Checked = false;
                    tsbFence.Checked = false;
                    tsbPan.Checked = false;
                    tsbSelect.Checked = true;
                    break;
            }

        }
        private void mnuViewOrient_Click(object sender, System.EventArgs e)
        {
            switch (((System.Windows.Forms.ToolStripMenuItem)sender).Tag.ToString())
            {
                case "Superior":
                    mViewer.Pitch = 0;
                    mViewer.Roll = 0;
                    mViewer.Yaw = 0;
                    break;
                case "Frontal":
                    mViewer.Pitch = 270;
                    mViewer.Roll = 0;
                    mViewer.Yaw = 360;
                    break;
                case "Lateral":
                    mViewer.Pitch = 270;
                    mViewer.Roll = 0;
                    mViewer.Yaw = 270;
                    break;
                case "Isometrica":
                    mViewer.Pitch = 315;
                    mViewer.Roll = 0;
                    mViewer.Yaw = 315;
                    break;
            }
            mViewer.FindExtents();
            mViewer.Redraw(true);
        }
        private void Principal_ResizeEnd(object sender, EventArgs e)
        {
            MG_Viewer1.FindExtents();
            mViewer.Redraw(true);
        }

        private void DisplayCheckChanged(object sender, EventArgs e)
        {
            if (mViewer == null) return;

            mViewer.DrawRapidLines = mnuRapidLines.Checked;
            mViewer.DrawRapidPoints = mnuRapidPoints.Checked;
            mViewer.DrawAxisLines = mnuAxisLines.Checked;
            mViewer.DrawAxisIndicator = mnuAxisindicator.Checked;
            mViewer.Redraw(true);
        }

        private void ProcessFile(string fileName)
        {
            try
            {
                if (fileName == null)
                {
                    return;
                }
                if (!System.IO.File.Exists(fileName))
                {
                    lblStatus.Text = "Archivo no existe";
                    return;
                }
                lblStatus.Text = "Procesando...";
                MG_CS_BasicViewer.MotionBlocks.Clear();
                mProcessor.Init(mSetup.Machine);
                mProcessor.ProcessFile(fileName, MG_CS_BasicViewer.MotionBlocks);

                BreakPointSlider.Maximum = MG_CS_BasicViewer.MotionBlocks.Count - 1;
                if (mViewer.BreakPoint > MG_CS_BasicViewer.MotionBlocks.Count - 1)
                {
                    mViewer.BreakPoint = MG_CS_BasicViewer.MotionBlocks.Count - 1;
                }
                mViewer.GatherTools();
                lblStatus.Text = "Hecho";
                prgBar.Value = 0;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "";
                MessageBox.Show("Se ha producido un error:" + ex.Message, "Previsualizar comandos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BreakPointSlider_ValueChanged(object sender, EventArgs e)
        {
            mViewer.BreakPoint = BreakPointSlider.Value;
            mViewer.Redraw(true);
        }

        //private void tsbOpen_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog1.ShowDialog();
        //    this.Refresh();
        //    if (OpenFileDialog1.FileName.Length > 0)
        //    {
        //        OpenFile(OpenFileDialog1.FileName);
        //    }
        //}

        private void OpenFile(string fileName)
        {
            long[] ticks = new long[2];
            mCncFile = fileName;
            mSetup.MatchMachineToFile(mCncFile);

            ProcessFile(mCncFile);
            mViewer.BreakPoint = MG_CS_BasicViewer.MotionBlocks.Count - 1;

            mViewer.Pitch = mSetup.Machine.ViewAngles[0];
            mViewer.Roll = mSetup.Machine.ViewAngles[1];
            mViewer.Yaw = mSetup.Machine.ViewAngles[2];
            mViewer.Init();

            ticks[0] = DateTime.Now.Ticks;
            MG_Viewer1.FindExtents();
            ticks[1] = DateTime.Now.Ticks;
            MG_Viewer1.DynamicViewManipulation = (ticks[1] - ticks[0]) < 2000000;
            mViewer.Redraw(true);
        }
        #endregion

        //List<string> lineas = Metodos.CilindroCentrado(20, 30, 5, 1,5);
        //List<string> lineas = Metodos.GastarPlano(0,0,20, 20, 1);
        //List<string> lineas = Metodos.Escalera(50, 50, 50, 10, 10);
        //List<string> lineas = Metodos.Escalera(15, 15, 15, 5, 5);

        //this.txtPreview.Text += ("G00 Z15" + Environment.NewLine);

        //foreach (string s in lineas)
        //    this.txtPreview.Text += (s + Environment.NewLine);


        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                this.LimpiarPrevisualizador();
                PrevisualizarFigurasManual();

                //---validamos que haya instrucciones para ser enviadas
                //obtenemos las lineas del previsualizador y removemos los blancos
                List<string> loteInstrucciones = txtPreview.Lines.ToList();
                while (loteInstrucciones.Contains(""))
                {
                    loteInstrucciones.Remove("");
                }

                if (loteInstrucciones.Count == 0)
                {
                    MessageBox.Show("No posee instrucciones para ser transferidas al CNC", "Transferencia CNC", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }


                //sino esta en un estado válido para continuar...
                if (lblEstado.Text != "Conexión OK (3/3): Conexión establecida" && lblEstado.Text != "Fin del procesamiento" && !pausado)
                {
                    //ver que hacemos...
                }
                else
                {
                    //sino venimos de una pausa
                    if (!pausado)
                    {
                        DialogResult dr = MessageBox.Show("Se iniciará el envío de las instrucciones al CNC." + Environment.NewLine + "¿Desea Continuar?", "Transferencia CNC", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (dr == DialogResult.Yes)
                        {
                            if (Interfaz.ConectarCNC(ref lblEstado, loteInstrucciones, ref lblPosicionActual, ref this.prgBar))
                            {
                                //bloqueamos controles
                                btnPlay.Enabled = false;
                                btnInicio.Enabled = false;
                                btnStop2.Enabled = true;
                                gbMovXY.Enabled = false;
                                gbMovZ.Enabled = false;
                                txtLineaManual.Enabled = false;
                                btnLimpiar.Enabled = false;
                                toolStrip1.Enabled = false;
                                txtPreview.Enabled = false;

                                btnPause.Enabled = true;

                                //bloqueamos el menu para que no modifiquen la configuracion
                                configuracionToolStripMenuItem.Enabled = false;
                            }
                        }


                    }
                    else
                    {//reanuda la ejecucion luego de una pausa

                        DialogResult dr = MessageBox.Show("Se reanudará el envío de las instrucciones al CNC." + Environment.NewLine + "¿Desea Continuar?", "Reanudar Transferencia CNC", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (dr == DialogResult.Yes)
                        {
                            Interfaz.ReaudarTransmision();

                            //bloqueamos controles
                            btnPlay.Enabled = false;
                            btnInicio.Enabled = false;
                            btnStop2.Enabled = true;
                            gbMovXY.Enabled = false;
                            gbMovZ.Enabled = false;
                            txtLineaManual.Enabled = false;
                            btnLimpiar.Enabled = false;
                            toolStrip1.Enabled = false;
                            txtPreview.Enabled = false;

                            btnPause.Enabled = true;

                            //salimos de la pausa
                            this.pausado = false;

                            //bloqueamos el menu para que no modifiquen la configuracion
                            configuracionToolStripMenuItem.Enabled = false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Interfaz.ConectarCNC: IniciarTransmision: CNC.PrepararComandos: Comando no soportado:"))
                {
                    string mensaje = ex.Message.Replace("Interfaz.ConectarCNC: IniciarTransmision: CNC.PrepararComandos: ","");
                    
                    //existen comandos no válidos
                    MessageBox.Show("Error: " + mensaje, "Comando no soportado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Acerca()).ShowDialog();
        }
        private void btnPause_Click(object sender, EventArgs e)
        {
            try
            {
                //habilitamos los controles permitidos en una pausa
                btnStop2.Enabled = true;
                btnPlay.Enabled = true;
                btnInicio.Enabled = false;
                btnPause.Enabled = false;

                //ponemos el modo en pausado 
                this.pausado = true;

                //enviamos pausa al cnc
                Interfaz.PausarTransmision();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void btnMovZ_Arr_MouseDown(object sender, MouseEventArgs e)
        {
            //avanzamos en Z
            MoverLibre(CNC.CNC_Mensajes_Send.Zavance);
        }
        private void btnMovZ_Arr_MouseUp(object sender, MouseEventArgs e)
        {
            //detenemos movimiento
            DetenerMovimientoLibre();
        }
        private void btnMovZ_Aba_MouseDown(object sender, MouseEventArgs e)
        {
            //retrocedemos en Z
            MoverLibre(CNC.CNC_Mensajes_Send.Zretroc);
        }
        private void btnMovZ_Aba_MouseUp(object sender, MouseEventArgs e)
        {
            //detenemos movimiento
            DetenerMovimientoLibre();
        }
        private void btnMovXY_Arr_MouseDown(object sender, MouseEventArgs e)
        {
            //avanzamos en Y
            MoverLibre(CNC.CNC_Mensajes_Send.Yavance);
        }
        private void btnMovXY_Arr_MouseUp(object sender, MouseEventArgs e)
        {
            //detenemos movimiento
            DetenerMovimientoLibre();
        }
        private void btnMovXY_Aba_MouseUp(object sender, MouseEventArgs e)
        {
            //detenemos movimiento
            DetenerMovimientoLibre();
        }
        private void btnMovXY_Aba_MouseDown(object sender, MouseEventArgs e)
        {
            //retrocedemos en Y
            MoverLibre(CNC.CNC_Mensajes_Send.Yretroc);
        }
        private void btnMovXY_Der_MouseDown(object sender, MouseEventArgs e)
        {
            //avanzamos en X
            MoverLibre(CNC.CNC_Mensajes_Send.Xavance);
        }
        private void btnMovXY_Izq_MouseUp(object sender, MouseEventArgs e)
        {
            //detenemos movimiento
            DetenerMovimientoLibre();
        }
        private void btnMovXY_Izq_MouseDown(object sender, MouseEventArgs e)
        {
            //retrocedemos en X
            MoverLibre(CNC.CNC_Mensajes_Send.Xretroc);
        }
        private void btnMovXY_Der_MouseUp(object sender, MouseEventArgs e)
        {
            //detenemos movimiento
            DetenerMovimientoLibre();
        }

        private void MoverLibre(string movimiento)
        {
            try
            {
                Interfaz.MoverLibre(movimiento, ref lblPosicionActual);
            }
            catch (Exception ex)
            {
                MessageBox.Show("CNCMatic.MoverLibre: " + ex.Message, "Movimiento Libre", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DetenerMovimientoLibre()
        {
            try
            {
                Interfaz.DetenerMovimientoLibre();
            }
            catch (Exception ex)
            {
                MessageBox.Show("CNCMatic.DetenerMovimientoLibre: " + ex.Message, "Movimiento Libre", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //con esta funcion controlamos los cambios de estado para replicar comportamiento en el form
        private void lblEstado_TextChanged(object sender, EventArgs e)
        {
            //se termina de establecer la conexion, entones se liberan los controles
            if (sender.ToString() == "Conexión OK (3/3): Conexión establecida")
            {
                LimpiarControlesSafe();

                SetControlPropertyThreadSafe(btnConnect, "Visible", false);
                SetControlPropertyThreadSafe(btnStop2, "Enabled", false);

                SetControlPropertyThreadSafe(btnInicio, "Enabled", true);
                SetControlPropertyThreadSafe(gbMovXY, "Enabled", true);
                SetControlPropertyThreadSafe(gbMovZ, "Enabled", true);

                //MessageBox.Show("Conexión exitosa!", "Conexion CNC", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            //si hubo error en el handshake, liberamos la pantalla
            if (sender.ToString() == "Conexión (2/3): error en el handshake. Intente nuevamente.")
            {
                LimpiarControlesSafe();

                SetControlPropertyThreadSafe(btnConnect, "Enabled", true);
                SetControlPropertyThreadSafe(btnStop2, "Enabled", false);
                SetControlPropertyThreadSafe(btnRestart, "Enabled", false);

                //reiniciamos la barra
                prgBar.Value = 0;

                return;
            }

            //se finaliza la ejecucion de las instrucciones, entones se liberan los controles
            if (sender.ToString() == "Fin del procesamiento")
            {
                LimpiarControlesSafe();

                SetControlPropertyThreadSafe(btnConnect, "Visible", false);
                SetControlPropertyThreadSafe(btnStop2, "Enabled", false);
                SetControlPropertyThreadSafe(btnInicio, "Enabled", false);
                SetControlPropertyThreadSafe(gbMovXY, "Enabled", false);
                SetControlPropertyThreadSafe(gbMovZ, "Enabled", false);
                SetControlPropertyThreadSafe(btnPause, "Enabled", false);
                SetControlPropertyThreadSafe(btnPlay, "Enabled", false);

                //habilitamos el menu de configuracion
                configuracionToolStripMenuItem.Enabled = true;

                return;
            }

            //reiniciado
            if (sender.ToString() == "CNC restarted")
            {
                LimpiarControlesSafe();

                SetControlPropertyThreadSafe(btnConnect, "Enabled", true);
                SetControlPropertyThreadSafe(btnConnect, "Visible", true);
                SetControlPropertyThreadSafe(btnStop2, "Enabled", false);
                SetControlPropertyThreadSafe(btnRestart, "Enabled", false);
                SetControlPropertyThreadSafe(btnPause, "Enabled", false);

                //reiniciamos la barra
                prgBar.Value = 0;

                return;
            }

            //stop de emergencia yendo al inicio
            if (sender.ToString() == "Parada de Emergencia presionado")
            {

                LimpiarControlesSafe();

                SetControlPropertyThreadSafe(btnConnect, "Enabled", true);
                SetControlPropertyThreadSafe(btnConnect, "Visible", true);
                SetControlPropertyThreadSafe(btnStop2, "Enabled", false);
                SetControlPropertyThreadSafe(btnRestart, "Enabled", false);
                SetControlPropertyThreadSafe(btnPause, "Enabled", false);

                //reiniciamos la barra
                prgBar.Value = 0;

                return;
            }
        }

        //For ThreadSafe called to edit components
        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);
        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
            }

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Se procederá a establecer la conexión con el CNC." + Environment.NewLine + "¿Desea Continuar?", "Conexión CNC", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {

                    //mensaje de advertencia al usuario
                    MessageBox.Show("Asegurese por favor de quitar todo objeto del área de trabajo, la herramienta procederá a trasladarse al origen", "Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    //obtenemos las lineas del previsualizador y removemos los blancos
                    List<string> loteInstrucciones = new List<string>();

                    if (Interfaz.ConectarCNC(ref lblEstado, loteInstrucciones, ref lblPosicionActual, ref this.prgBar))
                    {

                        //bloqueamos controles
                        btnPlay.Enabled = false;
                        btnInicio.Enabled = false;
                        btnPause.Enabled = false;
                        gbMovXY.Enabled = false;
                        gbMovZ.Enabled = false;
                        txtLineaManual.Enabled = false;
                        btnLimpiar.Enabled = false;
                        toolStrip1.Enabled = false;
                        txtPreview.Enabled = false;

                        btnStop2.Enabled = true;
                        btnConnect.Enabled = false;


                        btnRestart.Enabled = true;

                        //bloqueamos el menu de configuracion
                        //configuracionToolStripMenuItem.Enabled = false;

                    }


                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("no existe."))
                {
                    this.lblEstado.Text = "";

                    MessageBox.Show("Error: la maquina no está conectada", "Conectar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Error: " + ex.Message, "Conectar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            try
            {
                //limipiamos el estado
                this.lblEstado.Text = "";

                Interfaz.ReiniciarCNC();
            }

            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message, "Reiniciar", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void salirMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("¿Desea salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (dr == DialogResult.Yes)
                this.Close();
        }




    }

}
