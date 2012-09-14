﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using G.Objetos;

namespace CNCMatic
{
    public partial class FrmDibujoParams : Form
    {
        public FrmDibujoParams()
        {
            InitializeComponent();
        }

        public FrmDibujoParams(out G01_Lineal g)
        {
            InitializeComponent();

            g = new G01_Lineal();

            propiedades.SelectedObject = g;

            this.Text = "Parametros Linea";
            this.lblMensaje.Text = "Ingrese los parametros para dibujar la línea:";
        }

        public FrmDibujoParams(out G02_CirculoH g)
        {
            InitializeComponent();

            g = new G02_CirculoH();

            propiedades.SelectedObject = g;

            this.Text = "Parametros Circulo";
            this.lblMensaje.Text = "Ingrese los parametros para dibujar el circulo:";
        }

        public FrmDibujoParams(out G02_ArcoH g)
        {
            InitializeComponent();

            g = new G02_ArcoH();

            propiedades.SelectedObject = g;

            this.Text = "Parametros Arco";
            this.lblMensaje.Text = "Ingrese los parametros para dibujar el arco:";
        }

        public FrmDibujoParams(out G01_Cuadrado g)
        {
            InitializeComponent();

            g = new G01_Cuadrado();

            propiedades.SelectedObject = g;

            this.Text = "Parametros Cuadrado";
            this.lblMensaje.Text = "Ingrese los parametros para dibujar el cuadrado:";
        }

        public FrmDibujoParams(out G01_Cubo g)
        {
            InitializeComponent();

            g = new G01_Cubo();

            propiedades.SelectedObject = g;

            this.Text = "Parametros Cubo";
            this.lblMensaje.Text = "Ingrese los parametros para dibujar el cubo:";
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
