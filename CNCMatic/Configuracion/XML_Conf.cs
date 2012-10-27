using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configuracion
{
    public class XML_Config
    {
        private int id;
        private string descripcion;
        private string puertoCom;
        private string tipoProg;
        private string unidadMedida;
        private string velocidadMovimiento;
        private string largoSeccion;
        private float maxX;
        private float maxY;
        private float maxZ;
        private List<XML_ConfigMatMot> configMatMot;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public string PuertoCom
        {
            get { return puertoCom; }
            set { puertoCom = value; }
        }
        public string TipoProg
        {
            get { return tipoProg; }
            set { tipoProg = value; }
        }
        public string UnidadMedida
        {
            get { return unidadMedida; }
            set { unidadMedida = value; }
        }
        public string LargoSeccion
        {
            get { return largoSeccion; }
            set { largoSeccion = value; }
        }
        public string VelocidadMovimiento
        {
            get { return velocidadMovimiento; }
            set { velocidadMovimiento = value; }
        }
        public float MaxX
        {
            get { return maxX; }
            set { maxX = value; }
        }
        public float MaxY
        {
            get { return maxY; }
            set { maxY = value; }
        }
        public float MaxZ
        {
            get { return maxZ; }
            set { maxZ = value; }
        }
        public List<XML_ConfigMatMot> ConfigMatMot
        {
            get { return configMatMot; }
            set { configMatMot = value; }
        }

        public XML_Config()
        {
            configMatMot = new List<XML_ConfigMatMot>();
        }
        
    }

    public class XML_ConfigMatMot
    {
        private int idConfigMatMot;
        private int idMaterial;
        private int idMotor;
        private decimal tamVuelta;
        private decimal gradosPaso;
        private string nombreMotor;
        private string nombreMaterial;

        public int IdConfigMatMot
        {
            get { return idConfigMatMot; }
            set { idConfigMatMot = value; }
        }
        public int IdMaterial
        {
            get { return idMaterial; }
            set { idMaterial = value; }
        }
        public string Material
        {
            get { return nombreMaterial; }
            set { nombreMaterial = value; }
        }
        public int IdMotor
        {
            get { return idMotor; }
            set { idMotor = value; }
        }
        public string Motor
        {
            get { return nombreMotor; }
            set { nombreMotor = value; }
        }
        public decimal TamVuelta
        {
            get { return tamVuelta; }
            set { tamVuelta = value; }
        }
        public decimal GradosPaso
        {
            get { return gradosPaso; }
            set { gradosPaso = value; }
        }

    }

    public class XML_Motor
    {
        private int id;
        private string descripcion;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }


    }

    public class XML_Material
    {
        private int id;
        private string descripcion;
        private decimal espesor;
        private decimal ancho;
        private decimal largo;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public decimal Espesor
        {
            get { return espesor; }
            set { espesor = value; }
        }
        public decimal Ancho
        {
            get { return ancho; }
            set { ancho = value; }
        }
        public decimal Largo
        {
            get { return largo; }
            set { largo = value; }
        }
    }
}
