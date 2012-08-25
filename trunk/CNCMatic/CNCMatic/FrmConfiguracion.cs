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

        }

        private void FrmConfiguracion_Load(object sender, EventArgs e)
        {
            buscarPuertos();

            CargaMotores();

            CargaMateriales();

            CargaConfiguracionGeneral();

            
        }
        
        private void CargaMotores()
        {
            //cargar configuraciones
            XMLdb x = new XMLdb("xmlDB.xml");
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
            XMLdb x = new XMLdb("xmlDB.xml");
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
            XMLdb x = new XMLdb("xmlDB.xml");
            List<XML_Config> configs = x.LeeConfiguracion();

            cmbConfiguracion.DataSource = configs;
            cmbConfiguracion.DisplayMember = "Descripcion";
            cmbConfiguracion.ValueMember = "Id";

            cmbConfiguracion.Refresh();

            this.cmbConfiguracion.SelectedValueChanged += new System.EventHandler(this.cmbConfiguracion_SelectedValueChanged);

            //seleccionamos ls ultima configuracion
            cmbConfiguracion.SelectedValue= x.ultConfigId;
            cmbConfiguracion_SelectedValueChanged(this, null);


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

        private void cargaConfigMatMot(int idMotor,int idMaterial)
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
    }
}
