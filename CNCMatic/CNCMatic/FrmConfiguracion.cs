using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using CNCMatic.XML;
using System.Configuration;
using Configuracion;
using System.Globalization;
using System.Threading;

namespace CNCMatic
{
    public partial class FrmConfiguracion : Form
    {
        public FrmConfiguracion()
        {
            InitializeComponent();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validamosCampos())
                {
                    GrabaConfiguracionGeneral();

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido un error: " + ex.Message, "Alta Configuracion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private bool validamosCampos()
        {
            bool ok = true;

            if (txtMaxX.Text == "" || txtMaxY.Text == "" || txtMaxZ.Text == "")
            {
                ok = false;
                MessageBox.Show("Los maximos de los ejes no pueden ser cero o negativos", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ok;
            }

            //si es alta
            if (!cmbConfiguracion.Visible && txtNombrePerfil.Text.Trim() == "")
            {
                ok = false;
                MessageBox.Show("Por favor ingrese un nombre para el nuevo perfil", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ok;
            }

            if (txtLargoSeccion.Text.Trim() == "")
            {
                ok = false;
                MessageBox.Show("Por favor ingrese un valor para el largo de la seccion de curvas", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ok;
            }

            if (txtVelocMov.Text.Trim() == "")
            {
                ok = false;
                MessageBox.Show("Por favor ingrese un valor para la velocidad de movimiento", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ok;
            }

            if (txtAltura.Text.Trim() == "")
            {
                ok = false;
                MessageBox.Show("Por favor ingrese un valor para la altura de retiro de la herramienta", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ok;
            }

            if (txtGradosX.Text.Trim() == "" || txtVueltasX.Text.Trim() == "")
            {
                ok = false;
                MessageBox.Show("Por favor complete los parametros para el motor del eje X", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ok;
            }

            if (txtGradosY.Text.Trim() == "" || txtVueltasY.Text.Trim() == "")
            {
                ok = false;
                MessageBox.Show("Por favor complete los parametros para el motor del eje Y", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ok;
            }

            if (txtGradosZ.Text.Trim() == "" || txtVueltasZ.Text.Trim() == "")
            {
                ok = false;
                MessageBox.Show("Por favor complete los parametros para el motor del eje Z", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ok;
            }

            return ok;
        }
        private void FrmConfiguracion_Load(object sender, EventArgs e)
        {
            LimpiarControles();

            buscarPuertos();

            CargaConfiguracionGeneral();

            (new ToolTip()).SetToolTip(btnNuevo, "Nuevo perfil de configuracion");
            (new ToolTip()).SetToolTip(btnCancelar, "Cancela la accion actual");
            (new ToolTip()).SetToolTip(btnGrabar, "Graba el nuevo perfil o los cambios sobre el perfil seleccionado");

        }

        private void CargaConfiguracionGeneral()
        {
            //cargar configuraciones
            string xmlPath = ConfigurationManager.AppSettings["xmlDbPath"];

            XMLdb x = new XMLdb(xmlPath);
            List<XML_Config> configs = x.LeeConfiguracion();

            //ActualizaDescripcionesMatMot(configs);
            this.cmbConfiguracion.SelectedValueChanged -= new System.EventHandler(this.cmbConfiguracion_SelectedValueChanged);

            cmbConfiguracion.DataSource = configs;
            cmbConfiguracion.DisplayMember = "Descripcion";
            cmbConfiguracion.ValueMember = "Id";

            cmbConfiguracion.Refresh();

            this.cmbConfiguracion.SelectedValueChanged += new System.EventHandler(this.cmbConfiguracion_SelectedValueChanged);


            //seleccionamos la ultima configuracion
            //string ultConfigId = ConfigurationManager.AppSettings["idLastConfig"];
            string ultConfigId = x.LeeConfiguracionGral().IdLastConfig.ToString();
            cmbConfiguracion.SelectedValue = Convert.ToInt32(ultConfigId);
            cmbConfiguracion_SelectedValueChanged(this, null);

        }

        private void GrabaConfiguracionGeneral()
        {
            try
            {
                //cambiamos el cursor a waiting
                Cursor.Current = Cursors.WaitCursor;
                //culture que usamos para mostrar los campos y recuperarlos en pantalla
                CultureInfo cultAR = new CultureInfo("es-AR");

                string xmlPath = ConfigurationManager.AppSettings["xmlDbPath"];
                XMLdb x = new XMLdb(xmlPath);
                bool actualiza = false;

                //vemos si es actualizacion o alta
                XML_Config config;
                if (!cmbConfiguracion.Visible)
                {//es alta
                    config = new XML_Config();

                    //cargamos la descripcion del nuevo perfil
                    config.Descripcion = txtNombrePerfil.Text.Trim();

                    // el siguiente id de la lista de configuraciones
                    List<XML_Config> configuraciones = (List<XML_Config>)cmbConfiguracion.DataSource;
                    int maxId = 0;
                    foreach (XML_Config configuracion in configuraciones)
                    {
                        if (configuracion.Id > maxId)
                        {
                            maxId = configuracion.Id;
                        }
                    }

                    config.Id = maxId + 1;
                }
                else
                {//es actualizacion
                    config = (XML_Config)cmbConfiguracion.SelectedItem;
                    actualiza = true;
                }

                //cargamos los nuevos valores
                config.PuertoCom = portComboBox.Text;
                config.MaxX = float.Parse(txtMaxX.Text, cultAR);
                config.MaxY = float.Parse(txtMaxY.Text, cultAR);
                config.MaxZ = float.Parse(txtMaxZ.Text, cultAR);
                config.LargoSeccion = decimal.Parse(txtLargoSeccion.Text, cultAR);
                config.VelocidadMovimiento = decimal.Parse(txtVelocMov.Text, cultAR);
                config.AltoAscenso = float.Parse(txtAltura.Text, cultAR);
                config.GradosPasoX = decimal.Parse(txtGradosX.Text, cultAR);
                config.GradosPasoY = decimal.Parse(txtGradosY.Text, cultAR);
                config.GradosPasoZ = decimal.Parse(txtGradosZ.Text, cultAR);
                config.TamVueltaX = decimal.Parse(txtVueltasX.Text, cultAR);
                config.TamVueltaY = decimal.Parse(txtVueltasY.Text, cultAR);
                config.TamVueltaZ = decimal.Parse(txtVueltasZ.Text, cultAR);

                if (rbtAbsoluta.Checked)
                    config.TipoProg = "abs";
                if (rbtRelativa.Checked)
                    config.TipoProg = "rel";

                if (rbtMM.Checked)
                    config.UnidadMedida = "mm";
                if (rbtPULG.Checked)
                    config.UnidadMedida = "pulg";

                //grabamos la configuracion general
                x.GrabaConfiguracion(config);

                if (actualiza)
                    MessageBox.Show("La configuración ha sido actualizada correctamete", "Configuración - Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("La configuración ha sido dado de alta correctamete", "Configuración - Alta", MessageBoxButtons.OK, MessageBoxIcon.Information);


                //this.Hide();

                this.cmbConfiguracion.SelectedValueChanged -= new System.EventHandler(this.cmbConfiguracion_SelectedValueChanged);

                lblNombre.Visible = false;
                txtNombrePerfil.Visible = false;
                lblConfig.Visible = true;
                cmbConfiguracion.Visible = true;

                //lblConfigs.Enabled = true;
                //btnAltaConfigMatMot.Enabled = true;
                //grdConfigMatMot.Enabled = true;

                //grabamos en la configuracion que esta es la ultima configuracion seleccionada
                //ConfigurationManager.AppSettings["idLastConfig"]=config.Id.ToString();

                //Configuration appconfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //appconfig.AppSettings.Settings["idLastConfig"].Value = config.Id.ToString();
                //appconfig.Save(ConfigurationSaveMode.Modified, true);
                //ConfigurationManager.RefreshSection("appSettings");
                x.GrabaConfiguracionGral((new XML_Gral(config.Id)));
                //CargaConfiguracionGeneral();

                //cambiamos el cursor al normal
                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {
                //cambiamos el cursor al normal
                Cursor.Current = Cursors.Default;

                throw (ex);
                //MessageBox.Show("Se ha producido un error: " + ex.Message, "Alta Configuracion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            buscarPuertos();
        }
        private void buscarPuertos()
        {
            portComboBox.Items.Clear();

            foreach (string s in SerialPort.GetPortNames())
            {
                portComboBox.Items.Add(s);
            }
        }
        private void cmbConfiguracion_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                XML_Config config = (XML_Config)cmbConfiguracion.SelectedItem;
                //culture que usamos para mostrar los campos y recuperarlos en pantalla
                CultureInfo cultAR = new CultureInfo("es-AR");

                if (config != null)
                {
                    //cargamos la unidad de medida
                    switch (config.UnidadMedida)
                    {
                        case "mm": this.rbtMM.Checked = true; break;
                        case "pulg": this.rbtPULG.Checked = true; break;
                    }
                    //cargamos el tipo de programacion
                    switch (config.TipoProg)
                    {
                        case "abs": this.rbtAbsoluta.Checked = true; break;
                        case "rel": this.rbtRelativa.Checked = true; break;
                    }

                    //cargamos las coordenadas del punto maximo de trabajo
                    txtMaxX.Text = config.MaxX.ToString(cultAR);
                    txtMaxY.Text = config.MaxY.ToString(cultAR);
                    txtMaxZ.Text = config.MaxZ.ToString(cultAR);
                    txtVelocMov.Text = config.VelocidadMovimiento.ToString(cultAR);
                    txtLargoSeccion.Text = config.LargoSeccion.ToString(cultAR);
                    txtAltura.Text = config.AltoAscenso.ToString(cultAR);
                    txtGradosX.Text = config.GradosPasoX.ToString(cultAR);
                    txtGradosY.Text = config.GradosPasoY.ToString(cultAR);
                    txtGradosZ.Text = config.GradosPasoZ.ToString(cultAR);
                    txtVueltasX.Text = config.TamVueltaX.ToString(cultAR);
                    txtVueltasY.Text = config.TamVueltaY.ToString(cultAR);
                    txtVueltasZ.Text = config.TamVueltaZ.ToString(cultAR);

                    //seleccionamos el puerto
                    if (portComboBox.Items.Contains(config.PuertoCom))
                    {
                        portComboBox.Text = config.PuertoCom;
                    }
                    else
                    {
                        portComboBox.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido un error al recuperar la información del perfil: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        //private void cmbMaterial_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    cargaConfigMatMot((int)cmbMotor.SelectedValue, (int)cmbMaterial.SelectedValue);
        //}

        //private void cmbMotor_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    cargaConfigMatMot((int)cmbMotor.SelectedValue, (int)cmbMaterial.SelectedValue);
        //}

        //private void cargaConfigMatMot(int idMotor, int idMaterial)
        //{
        //    //traemos todas las configuraciones material/motor para la configuracion seleccionada
        //    List<XML_ConfigMatMot> configsMatMot = ((XML_Config)cmbConfiguracion.SelectedItem).ConfigMatMot;

        //    txtGradosX.Text = "";
        //    txtVueltasX.Text = "";

        //    //buscamos la que tenga los id seleccionados
        //    foreach (XML_ConfigMatMot configMatMot in configsMatMot)
        //    {
        //        if (configMatMot.IdMotor == idMotor && configMatMot.IdMaterial == idMaterial)
        //        {
        //            txtGradosX.Text = configMatMot.GradosPaso.ToString(new CultureInfo("es-AR"));
        //            txtVueltasX.Text = configMatMot.TamVuelta.ToString(new CultureInfo("es-AR"));
        //        }
        //    }


        //}

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            lblNombre.Visible = true;
            txtNombrePerfil.Visible = true;
            lblConfig.Visible = false;
            cmbConfiguracion.Visible = false;

            //lblConfigs.Enabled = false;
            //btnAltaConfigMatMot.Enabled = false;
            //grdConfigMatMot.Enabled = false;

            LimpiarControles();

        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            lblNombre.Visible = false;
            txtNombrePerfil.Visible = false;
            lblConfig.Visible = true;
            cmbConfiguracion.Visible = true;

            //lblConfigs.Enabled = true;
            //btnAltaConfigMatMot.Enabled = true;
            //grdConfigMatMot.Enabled = true;

            LimpiarControles();

            cmbConfiguracion_SelectedValueChanged(this, new EventArgs());
        }
        private void LimpiarControles()
        {
            txtMaxX.Text = "";
            txtMaxY.Text = "";
            txtMaxZ.Text = "";
            txtNombrePerfil.Text = "";
            txtAltura.Text = "";
            txtLargoSeccion.Text = "";
            txtVelocMov.Text = "";
            txtGradosX.Text = "";
            txtVueltasX.Text = "";
            txtGradosY.Text = "";
            txtVueltasY.Text = "";
            txtGradosZ.Text = "";
            txtVueltasZ.Text = "";
            //grdConfigMatMot.DataSource = null;

            //lblVueltas.Location = new Point(21, 65);
            //txtVueltas.Location = new Point(116, 62);

        }
        //private void btnAltaMaterial_Click(object sender, EventArgs e)
        //{
        //    txtMaterialNombre.Text = "";
        //    txtMaterialAncho.Text = "";
        //    txtMaterialEspesor.Text = "";
        //    txtMaterialLargo.Text = "";

        //    grpNuevoMaterial.Visible = true;
        //    this.Size = new Size(539, 578);
        //}
        //private void btnAltaMotor_Click(object sender, EventArgs e)
        //{
        //    txtMotorNombre.Text = "";

        //    grpNuevoMotor.Visible = true;
        //    this.Size = new Size(539, 578);
        //}
        //private void btnGrabaMaterial_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //seteamos el tipo de culture para grabar bien los decimales
        //        CultureInfo actual = Thread.CurrentThread.CurrentCulture;
        //        Thread.CurrentThread.CurrentCulture = new CultureInfo("es-AR");

        //        //XML_Material mat = new XML_Material();
        //        //mat.Descripcion = txtMaterialNombre.Text;
        //        //mat.Ancho = Decimal.Parse(txtMaterialAncho.Text);
        //        //mat.Espesor = Decimal.Parse(txtMaterialEspesor.Text);
        //        //mat.Largo = Decimal.Parse(txtMaterialLargo.Text);
        //        //matbuscamos.Id = 0;

        //        // el siguiente id de la lista de materiales
        //        List<XML_Material> materiales = (List<XML_Material>)cmbMaterial.DataSource;
        //        int maxId = 0;
        //        foreach (XML_Material material in materiales)
        //        {
        //            if (material.Id > maxId)
        //            {
        //                maxId = material.Id;
        //            }
        //        }

        //        mat.Id = maxId + 1;

        //        string xmlPath = ConfigurationManager.AppSettings["xmlDbPath"];

        //        //grabar material
        //        XMLdb x = new XMLdb(xmlPath);
        //        x.GrabaMaterial(mat);

        //        MessageBox.Show("El material ha sido dado de alta correctamete", "Alta Material", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        this.cmbMaterial.SelectedValueChanged -= new System.EventHandler(this.cmbMaterial_SelectedValueChanged);

        //        CargaMateriales();

        //        grpNuevoMaterial.Visible = false;
        //        this.Size = new Size(539, 482);
        //        //devolvemos al thread el formato actual
        //        Thread.CurrentThread.CurrentCulture = actual;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Se ha producido un error: " + ex.Message, "Alta Material", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //}
        //private void btnGrabaMotor_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        XML_Motor mot = new XML_Motor();
        //        mot.Descripcion = txtMotorNombre.Text;
        //        //mot.Id = 0;

        //        //buscamos el siguiente id de la lista de motores
        //        List<XML_Motor> motores = (List<XML_Motor>)cmbMotor.DataSource;
        //        int maxId = 0;
        //        foreach (XML_Motor motor in motores)
        //        {
        //            if (motor.Id > maxId)
        //            {
        //                maxId = motor.Id;
        //            }
        //        }

        //        mot.Id = maxId + 1;

        //        string xmlPath = ConfigurationManager.AppSettings["xmlDbPath"];

        //        //grabar motores
        //        XMLdb x = new XMLdb(xmlPath);
        //        x.GrabaMotor(mot);

        //        MessageBox.Show("El motor ha sido dado de alta correctamete", "Alta Motor", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        this.cmbMotor.SelectedValueChanged -= new System.EventHandler(this.cmbMotor_SelectedValueChanged);

        //        CargaMotores();

        //        grpNuevoMotor.Visible = false;
        //        this.Size = new Size(539, 482);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Se ha producido un error: " + ex.Message, "Alta Motor", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        //private void btnCancelaGrabaMotor_Click(object sender, EventArgs e)
        //{
        //    grpNuevoMotor.Visible = false;
        //    this.Size = new Size(539, 482);
        //}
        //private void btnCancelaGrabaMat_Click(object sender, EventArgs e)
        //{
        //    grpNuevoMaterial.Visible = false;
        //    this.Size = new Size(539, 482);
        //}
        //private void btnAltaConfigMatMot_Click(object sender, EventArgs e)
        //{
        //    grdConfigMatMot.Visible = false;
        //    grpMotores.Visible = true;
        //}
        //private void btnCancelaConfigMatMot_Click(object sender, EventArgs e)
        //{
        //    grdConfigMatMot.Visible = true;
        //    grpMotores.Visible = false;
        //}

        //private void btnGrabaConfigMatMot_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        XML_ConfigMatMot configItem = new XML_ConfigMatMot();
        //        configItem.GradosPaso = Decimal.Parse(txtGradosX.Text);
        //        configItem.IdMaterial = Convert.ToInt32(cmbMaterial.SelectedValue);
        //        configItem.IdMotor = Convert.ToInt32(cmbMotor.SelectedValue);
        //        configItem.TamVuelta = Decimal.Parse(txtVueltasX.Text);

        //        configItem.Material = EncuentraMaterial(configItem.IdMaterial);
        //        configItem.Motor = EncuentraMotor(configItem.IdMotor);

        //        // el siguiente id de la lista de items de configuraciones
        //        int maxId = 0;
        //        foreach (XML_ConfigMatMot configuracion in ((XML_Config)cmbConfiguracion.SelectedItem).ConfigMatMot)
        //        {
        //            if (configuracion.IdConfigMatMot > maxId)
        //            {
        //                maxId = configuracion.IdConfigMatMot;
        //            }
        //        }

        //        configItem.IdConfigMatMot = maxId + 1;

        //        ((XML_Config)cmbConfiguracion.SelectedItem).ConfigMatMot.Add(configItem);

        //        //cmbConfiguracion_SelectedValueChanged(this, null);

        //        grdConfigMatMot.DataSource = null;
        //        grdConfigMatMot.DataSource = ((XML_Config)cmbConfiguracion.SelectedItem).ConfigMatMot;
        //        grdConfigMatMot.AutoResizeColumns();
        //        grdConfigMatMot.AllowUserToAddRows = false;
        //        grdConfigMatMot.AllowUserToDeleteRows = false;
        //        grdConfigMatMot.ReadOnly = true;
        //        grdConfigMatMot.Columns["IdMaterial"].Visible = false;
        //        grdConfigMatMot.Columns["IdMotor"].Visible = false;

        //        grdConfigMatMot.Visible = true;
        //        grpMotores.Visible = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Se ha producido un error: " + ex.Message, "Nuevo Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        #region validaciones
        private char validarCampoDouble(char keyPressed)
        {
            if (
                !Char.IsDigit(keyPressed) &&
                keyPressed != ',' &&
                keyPressed != '.' &&
                keyPressed != Convert.ToChar(Keys.Back) &&
                keyPressed != Convert.ToChar(Keys.Tab)
                )
            {
                return new char();
            }
            else
            {
                return keyPressed;
            }
        }

        private void txtVelocMov_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = validarCampoDouble(e.KeyChar);
        }

        private void txtLargoSeccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = validarCampoDouble(e.KeyChar);
        }

        private void txtGradosX_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = validarCampoDouble(e.KeyChar);
        }

        private void txtVueltasX_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = validarCampoDouble(e.KeyChar);
        }

        private void txtMaterialEspesor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = validarCampoDouble(e.KeyChar);
        }

        private void txtMaterialLargo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = validarCampoDouble(e.KeyChar);
        }

        private void txtMaterialAncho_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = validarCampoDouble(e.KeyChar);
        }

        private void txtAltura_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = validarCampoDouble(e.KeyChar);
        }

        private void txtVueltasY_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = validarCampoDouble(e.KeyChar);
        }

        private void txtGradosY_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = validarCampoDouble(e.KeyChar);
        }

        private void txtVueltasZ_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = validarCampoDouble(e.KeyChar);
        }

        private void txtGradosZ_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = validarCampoDouble(e.KeyChar);
        }
        #endregion
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Yes == MessageBox.Show("¿Esta seguro de eliminar el perfil de configuracion actual?", "Eliminar Perfil", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    //cambiamos el cursor a waiting
                    Cursor.Current = Cursors.WaitCursor;

                    string xmlPath = ConfigurationManager.AppSettings["xmlDbPath"];
                    XMLdb x = new XMLdb(xmlPath);

                    //traemos la config seleccionada
                    XML_Config configActual = (XML_Config)cmbConfiguracion.SelectedItem;

                    //eliminamos la config
                    x.EliminarConfiguracion(configActual);

                    //cambiamos el cursor al normal
                    Cursor.Current = Cursors.Default;

                    MessageBox.Show("Perfil eliminado correctamente", "Eliminar Perfil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //cargamos nuevamente el formulario
                    FrmConfiguracion_Load(this, null);

                }

            }
            catch (Exception ex)
            {
                //cambiamos el cursor al normal
                Cursor.Current = Cursors.Default;

                MessageBox.Show("Se ha producido un error:" + ex.Message, "Eliminar Perfil", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




    }
}
