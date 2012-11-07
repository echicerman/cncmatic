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
                GrabaConfiguracionGeneral();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido un error: " + ex.Message, "Alta Configuracion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void FrmConfiguracion_Load(object sender, EventArgs e)
        {
            buscarPuertos();

            //CargaMotores();

            //CargaMateriales();

            CargaConfiguracionGeneral();

            (new ToolTip()).SetToolTip(btnNuevo, "Nuevo perfil de configuracion");
            (new ToolTip()).SetToolTip(btnCancelar, "Cancela la accion actual");
            //(new ToolTip()).SetToolTip(btnAltaMaterial, "Dar de alta un nuevo material");
            //(new ToolTip()).SetToolTip(btnAltaMotor, "Dar de alta un nuevo motor");
            (new ToolTip()).SetToolTip(btnGrabar, "Graba el nuevo perfil o los cambios sobre el perfil seleccionado");

        }
        //private void CargaMotores()
        //{
        //    //cargar configuraciones
        //    string xmlPath = ConfigurationManager.AppSettings["xmlDbPath"];
        //    XMLdb x = new XMLdb(xmlPath);
        //    List<XML_Motor> motores = x.LeerMotores();

        //    cmbMotor.DataSource = motores;
        //    cmbMotor.DisplayMember = "Descripcion";
        //    cmbMotor.ValueMember = "Id";

        //    cmbMotor.Refresh();

        //    this.cmbMotor.SelectedValueChanged += new System.EventHandler(this.cmbMotor_SelectedValueChanged);

        //}

        //private void CargaMateriales()
        //{
        //    //cargar configuraciones
        //    string xmlPath = ConfigurationManager.AppSettings["xmlDbPath"];
        //    XMLdb x = new XMLdb(xmlPath);
        //    List<XML_Material> materiales = x.LeerMateriales();

        //    cmbMaterial.DataSource = materiales;
        //    cmbMaterial.DisplayMember = "Descripcion";
        //    cmbMaterial.ValueMember = "Id";

        //    cmbMaterial.Refresh();

        //    this.cmbMaterial.SelectedValueChanged += new System.EventHandler(this.cmbMaterial_SelectedValueChanged);

        //}

        private void CargaConfiguracionGeneral()
        {
            //cargar configuraciones
            string xmlPath = ConfigurationManager.AppSettings["xmlDbPath"];

            XMLdb x = new XMLdb(xmlPath);
            List<XML_Config> configs = x.LeeConfiguracion();

            //ActualizaDescripcionesMatMot(configs);

            cmbConfiguracion.DataSource = configs;
            cmbConfiguracion.DisplayMember = "Descripcion";
            cmbConfiguracion.ValueMember = "Id";

            cmbConfiguracion.Refresh();

            this.cmbConfiguracion.SelectedValueChanged += new System.EventHandler(this.cmbConfiguracion_SelectedValueChanged);


            //seleccionamos ls ultima configuracion
            string ultConfigId = ConfigurationManager.AppSettings["idLastConfig"];
            cmbConfiguracion.SelectedValue = Convert.ToInt32(ultConfigId);
            cmbConfiguracion_SelectedValueChanged(this, null);

        }
        //private void ActualizaDescripcionesMatMot(List<XML_Config> configs)
        //{
        //    foreach (XML_Config config in configs)
        //    {
        //        foreach (XML_ConfigMatMot iconfig in config.ConfigMatMot)
        //        {
        //            iconfig.Material = EncuentraMaterial(iconfig.IdMaterial);
        //            iconfig.Motor = EncuentraMotor(iconfig.IdMotor);

        //        }
        //    }
        //}

        //private string EncuentraMaterial(int idMaterial)
        //{
        //    foreach (XML_Material m in (List<XML_Material>)cmbMaterial.DataSource)
        //    {
        //        if (m.Id == idMaterial)
        //            return m.Descripcion;
        //    }
        //    return "";
        //}

        //private string EncuentraMotor(int idMotor)
        //{
        //    foreach (XML_Motor m in (List<XML_Motor>)cmbMotor.DataSource)
        //    {
        //        if (m.Id == idMotor)
        //            return m.Descripcion;
        //    }
        //    return "";
        //}

        private void GrabaConfiguracionGeneral()
        {
            try
            {
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
                config.MaxX = float.Parse(txtMaxX.Text);
                config.MaxY = float.Parse(txtMaxY.Text);
                config.MaxZ = float.Parse(txtMaxZ.Text);
                config.PuertoCom = portComboBox.Text;
                config.LargoSeccion = txtLargoSeccion.Text;
                config.VelocidadMovimiento = txtVelocMov.Text;
                config.AltoAscenso = txtAltura.Text;
                config.GradosPasoX = decimal.Parse(txtGradosX.Text);
                config.GradosPasoY = decimal.Parse(txtGradosY.Text);
                config.GradosPasoZ = decimal.Parse(txtGradosZ.Text);
                config.TamVueltaX = decimal.Parse(txtVueltasX.Text);
                config.TamVueltaY = decimal.Parse(txtVueltasX.Text);
                config.TamVueltaZ = decimal.Parse(txtVueltasX.Text);

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
                Configuration appconfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                appconfig.AppSettings.Settings["idLastConfig"].Value = config.Id.ToString();
                appconfig.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("appSettings");

                //CargaConfiguracionGeneral();
            }
            catch (Exception ex)
            {
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
            XML_Config config = (XML_Config)cmbConfiguracion.SelectedItem;
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
                txtMaxX.Text = config.MaxX.ToString();
                txtMaxY.Text = config.MaxY.ToString();
                txtMaxZ.Text = config.MaxZ.ToString();
                txtVelocMov.Text = config.VelocidadMovimiento.ToString();
                txtLargoSeccion.Text = config.LargoSeccion.ToString();
                txtAltura.Text = config.AltoAscenso.ToString();
                txtGradosX.Text = config.GradosPasoX.ToString(new CultureInfo("es-AR"));
                txtGradosY.Text = config.GradosPasoY.ToString(new CultureInfo("es-AR"));
                txtGradosZ.Text = config.GradosPasoZ.ToString(new CultureInfo("es-AR"));
                txtVueltasX.Text = config.TamVueltaX.ToString(new CultureInfo("es-AR"));
                txtVueltasY.Text = config.TamVueltaY.ToString(new CultureInfo("es-AR"));
                txtVueltasZ.Text = config.TamVueltaZ.ToString(new CultureInfo("es-AR"));

                //seleccionamos el puerto
                if (portComboBox.Items.Contains(config.PuertoCom))
                {
                    portComboBox.Text = config.PuertoCom;
                }
                else
                {
                    portComboBox.Text = "";
                }

                //grdConfigMatMot.DataSource = config.ConfigMatMot;
                //grdConfigMatMot.AutoResizeColumns();
                //grdConfigMatMot.AllowUserToAddRows = false;
                //grdConfigMatMot.AllowUserToDeleteRows = false;
                //grdConfigMatMot.ReadOnly = true;
                //grdConfigMatMot.Columns["IdMaterial"].Visible = false;
                //grdConfigMatMot.Columns["IdMotor"].Visible = false;

                ////tomamos la primera de las configuraciones de material/motor
                //if (config.ConfigMatMot != null && config.ConfigMatMot.Count > 0)
                //{
                //    XML_ConfigMatMot configMatMot = config.ConfigMatMot[0];

                //    cmbMotor.SelectedValue = configMatMot.IdMotor;
                //    cmbMaterial.SelectedValue = configMatMot.IdMaterial;
                //}
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

        private char validarCampoDouble(char keyPressed)
        {
            if (
                !Char.IsDigit(keyPressed) &&
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




    }
}
