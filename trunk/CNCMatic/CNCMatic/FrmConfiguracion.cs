using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO.Ports;
using CNCMatic.XML;
using System.Configuration;

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
            GrabaConfiguracionGeneral();
        }

        private void FrmConfiguracion_Load(object sender, EventArgs e)
        {
            buscarPuertos();

            CargaMotores();

            CargaMateriales();

            CargaConfiguracionGeneral();

            (new ToolTip()).SetToolTip(btnNuevo, "Nuevo perfil de configuracion");
            (new ToolTip()).SetToolTip(btnCancelar, "Cancela la accion actual");
            (new ToolTip()).SetToolTip(btnAltaMaterial, "Dar de alta un nuevo material");
            (new ToolTip()).SetToolTip(btnAltaMotor, "Dar de alta un nuevo motor");
            (new ToolTip()).SetToolTip(btnGrabar, "Graba el nuevo perfil o los cambios sobre el perfil seleccionado");

        }

        private void CargaMotores()
        {
            //cargar configuraciones
            string xmlPath = ConfigurationManager.AppSettings["xmlDbPath"];
            XMLdb x = new XMLdb(xmlPath);
            List<XML_Motor> motores = x.LeerMotores();

            cmbMotor.DataSource = motores;
            cmbMotor.DisplayMember = "Descripcion";
            cmbMotor.ValueMember = "Id";

            cmbMotor.Refresh();

            this.cmbMotor.SelectedValueChanged += new System.EventHandler(this.cmbMotor_SelectedValueChanged);

        }

        private void CargaMateriales()
        {
            //cargar configuraciones
            string xmlPath = ConfigurationManager.AppSettings["xmlDbPath"];
            XMLdb x = new XMLdb(xmlPath);
            List<XML_Material> materiales = x.LeerMateriales();

            cmbMaterial.DataSource = materiales;
            cmbMaterial.DisplayMember = "Descripcion";
            cmbMaterial.ValueMember = "Id";

            cmbMaterial.Refresh();

            this.cmbMaterial.SelectedValueChanged += new System.EventHandler(this.cmbMaterial_SelectedValueChanged);

        }

        private void CargaConfiguracionGeneral()
        {
            //cargar configuraciones
            string xmlPath = ConfigurationManager.AppSettings["xmlDbPath"];

            XMLdb x = new XMLdb(xmlPath);
            List<XML_Config> configs = x.LeeConfiguracion();

            cmbConfiguracion.DataSource = configs;
            cmbConfiguracion.DisplayMember = "Descripcion";
            cmbConfiguracion.ValueMember = "Id";

            cmbConfiguracion.Refresh();

            this.cmbConfiguracion.SelectedValueChanged += new System.EventHandler(this.cmbConfiguracion_SelectedValueChanged);

            //seleccionamos ls ultima configuracion
            cmbConfiguracion.SelectedValue = x.ultConfigId;
            cmbConfiguracion_SelectedValueChanged(this, null);

        }
        private void GrabaConfiguracionGeneral()
        {
            string xmlPath = ConfigurationManager.AppSettings["xmlDbPath"];

            //cargar configuraciones
            XMLdb x = new XMLdb(xmlPath);

            XML_Config config = new XML_Config();
            config.MaxX = Convert.ToDecimal(txtMaxX.Text);
            config.MaxY = Convert.ToDecimal(txtMaxY.Text);
            config.MaxZ = Convert.ToDecimal(txtMaxZ.Text);
            config.PuertoCom = portComboBox.SelectedText;
            config.Descripcion = txtNombrePerfil.Text.Trim();
            
            if (rbtAbsoluta.Checked)
                config.TipoProg = "abs";
            if (rbtRelativa.Checked)
                config.TipoProg = "rel";

            if (rbtMM.Checked)
                config.UnidadMedida = "mm";
            if (rbtPULG.Checked)
                config.UnidadMedida = "pulg";

            x.GrabaConfiguracion(config);
            
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

            //seleccionamos el puerto
            if (portComboBox.Items.Contains(config.PuertoCom))
            {
                portComboBox.SelectedItem = config.PuertoCom;
            }

            //tomamos la primera de las configuraciones de material/motor
            XML_ConfigMatMot configMatMot = config.ConfigMatMot[0];

            cmbMotor.SelectedValue = configMatMot.IdMotor;
            cmbMaterial.SelectedValue = configMatMot.IdMaterial;

        }

        private void cmbMaterial_SelectedValueChanged(object sender, EventArgs e)
        {
            cargaConfigMatMot((int)cmbMotor.SelectedValue, (int)cmbMaterial.SelectedValue);
        }

        private void cmbMotor_SelectedValueChanged(object sender, EventArgs e)
        {
            cargaConfigMatMot((int)cmbMotor.SelectedValue, (int)cmbMaterial.SelectedValue);
        }

        private void cargaConfigMatMot(int idMotor, int idMaterial)
        {
            //traemos todas las configuraciones material/motor para la configuracion seleccionada
            List<XML_ConfigMatMot> configsMatMot = ((XML_Config)cmbConfiguracion.SelectedItem).ConfigMatMot;

            txtGrados.Text = "";
            txtVueltas.Text = "";

            //buscamos la que tenga los id seleccionados
            foreach (XML_ConfigMatMot configMatMot in configsMatMot)
            {
                if (configMatMot.IdMotor == idMotor && configMatMot.IdMaterial == idMaterial)
                {
                    txtGrados.Text = configMatMot.GradosPaso.ToString();
                    txtVueltas.Text = configMatMot.TamVuelta.ToString();
                }
            }


        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnGrabar.Location = new Point(190, 385);
            grpConfGral.Location = new Point(9, 77);
            grpConfigMaterial.Location = new Point(9, 238);
            this.Size = new Size(485, 474);
            lblNombre.Visible = true;
            txtNombrePerfil.Visible = true;
            lblConfig.Enabled = false;
            cmbConfiguracion.Enabled = false;
            LimpiarControles();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnGrabar.Location = new Point(190, 360);
            grpConfGral.Location = new Point(9, 47);
            grpConfigMaterial.Location = new Point(9, 215);
            this.Size = new Size(485, 446);
            lblNombre.Visible = false;
            txtNombrePerfil.Visible = false;
            lblConfig.Enabled = true;
            cmbConfiguracion.Enabled = true;

            LimpiarControles();

            cmbConfiguracion_SelectedValueChanged(this, new EventArgs());


        }

        private void LimpiarControles()
        {
            txtMaxX.Text = "";
            txtMaxY.Text = "";
            txtMaxZ.Text = "";
            txtGrados.Text = "";
            txtNombrePerfil.Text = "";
            txtVueltas.Text = "";

            lblVueltas.Location = new Point(21, 65);
            txtVueltas.Location = new Point(116, 62);

        }

        private void btnAltaMaterial_Click(object sender, EventArgs e)
        {
            lblVueltas.Location = new Point(21, 79);
            txtVueltas.Location = new Point(116, 76);

            lblMaterialNombre.Visible = true;
            txtMaterialNombre.Visible = true;

            btnGrabaMaterial.Enabled = true;
        }

        private void btnAltaMotor_Click(object sender, EventArgs e)
        {
            lblGrados.Location = new Point(249, 79);
            txtGrados.Location = new Point(344, 76);
            
            lblMotorNombre.Visible = true;
            txtMotorNombre.Visible = true;

            btnGrabaMotor.Enabled = true;
        }

        private void btnGrabaMaterial_Click(object sender, EventArgs e)
        {
            XML_Material mat = new XML_Material();
            
        }

        private void btnGrabaMotor_Click(object sender, EventArgs e)
        {
            try
            {
                XML_Motor mot = new XML_Motor();
                mot.Descripcion = txtMotorNombre.Text;
                //mot.Id = 0;

                //buscamos el siguiente id de la lista de motores
                List<XML_Motor> motores = (List<XML_Motor>)cmbMotor.DataSource;
                int maxId = 0;
                foreach (XML_Motor motor in motores)
                {
                    if (motor.Id > maxId)
                    {
                        maxId = motor.Id;
                    }
                }

                mot.Id = maxId + 1;

                string xmlPath = ConfigurationManager.AppSettings["xmlDbPath"];

                //cargar configuraciones
                XMLdb x = new XMLdb(xmlPath);
                x.GrabaMotor(mot);

                MessageBox.Show("El motor ha sido dado de alta correctamete", "Alta Motor", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargaMotores();
                btnGrabaMotor.Enabled = false;
                lblMotorNombre.Visible = false;
                txtMotorNombre.Visible = false;
                lblGrados.Location=new Point(249, 68);
                txtGrados.Location=new Point(344, 65);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido un error: " + ex.Message, "Alta Motor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
