using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CNCMatic.XML
{
    public class XMLdb
    {
        public string filePath;
        private XmlReaderSettings settings;
        public int ultConfigId;

        public XMLdb(string filePath)
        {
            this.filePath = filePath;

            this.settings = new XmlReaderSettings();

            this.settings.IgnoreWhitespace = true;
            this.settings.IgnoreComments = true;
            this.settings.IgnoreProcessingInstructions = true;
            this.settings.ProhibitDtd = true;
            this.settings.CloseInput = true;
        }

        public void GrabaConfiguracion(XML_Config config)
        {

            XDocument doc = XDocument.Load(this.filePath);
            XElement configNodo = new XElement("Configuracion");
            configNodo.Add(new XElement("descripcion",config.Descripcion));
            configNodo.Add(new XElement("puerto",config.PuertoCom));
            configNodo.Add(new XElement("UnidadMedida", config.UnidadMedida));
            configNodo.Add(new XElement("TipoProgramacion",config.TipoProg));
            configNodo.Add(new XElement("XMax",config.MaxX.ToString()));
            configNodo.Add(new XElement("YMax",config.MaxY.ToString()));
            configNodo.Add(new XElement("ZMax",config.MaxZ.ToString()));

            
            doc.Descendants("Configuraciones").Single().Add(configNodo);
            
            doc.Save(this.filePath);
            
            
        }

        public void GrabaMotor(XML_Motor motor)
        {
            XDocument doc = XDocument.Load(this.filePath);
            XElement motorNodo = new XElement("Motor");
            motorNodo.Add(new XElement("idMotor", motor.Id));
            motorNodo.Add(new XElement("descripcion", motor.Descripcion));

            doc.Descendants("Motores").Single().Add(motorNodo);

            doc.Save(this.filePath);
        }

        public List<XML_Config> LeeConfiguracion()
        {

            XmlReader reader = XmlReader.Create(this.filePath, this.settings);
            reader.MoveToContent();
            reader.ReadToDescendant("Configuraciones");
            reader.MoveToFirstAttribute();
            this.ultConfigId = Convert.ToInt32(reader.Value);

            XML_Config c;
            List<XML_Config> cs = new List<XML_Config>();
           
            while (reader.ReadToFollowing("Configuracion"))
            {
                //XmlReader motor=motores.ReadSubtree();
                c = new XML_Config();

                reader.ReadToFollowing("idConfig");
                c.Id = reader.ReadElementContentAsInt();
                c.Descripcion = reader.ReadElementContentAsString();
                c.PuertoCom = reader.ReadElementContentAsString();
                c.UnidadMedida = reader.ReadElementContentAsString();
                c.TipoProg = reader.ReadElementContentAsString();
                c.MaxX = reader.ReadElementContentAsDecimal();
                c.MaxY = reader.ReadElementContentAsDecimal();
                c.MaxZ = reader.ReadElementContentAsDecimal();
                //
                XML_ConfigMatMot configMatMot;
                while (reader.ReadToFollowing("ConfigMatMot"))
                {
                    //XmlReader motor=motores.ReadSubtree();
                    configMatMot = new XML_ConfigMatMot();

                    reader.ReadToFollowing("idMaterial");

                    configMatMot.IdConfig = c.Id;
                    configMatMot.IdMaterial = reader.ReadElementContentAsInt();
                    configMatMot.IdMotor = reader.ReadElementContentAsInt();
                    configMatMot.TamVuelta = reader.ReadElementContentAsDecimal();
                    configMatMot.GradosPaso = reader.ReadElementContentAsDecimal();

                    c.ConfigMatMot.Add(configMatMot);

                }

                cs.Add(c);
            }

            reader.Close();

            return cs;
        }

        public List<XML_Material> LeerMateriales()
        {
            XmlReader reader = XmlReader.Create(this.filePath, this.settings);
            reader.MoveToContent();
            reader.ReadToDescendant("Materiales");

            XML_Material m;
            List<XML_Material> ms = new List<XML_Material>();

            while (reader.ReadToFollowing("Material"))
            {
                //XmlReader motor=motores.ReadSubtree();
                m = new XML_Material();

                reader.ReadToFollowing("idMaterial");
                m.Id = reader.ReadElementContentAsInt();
                m.Descripcion = reader.ReadElementContentAsString();
                m.Espesor = reader.ReadElementContentAsDecimal();
                m.Ancho = reader.ReadElementContentAsDecimal();
                m.Largo = reader.ReadElementContentAsDecimal();
                ms.Add(m);
            }
            reader.Close();
            return ms;

        }

        public List<XML_Motor> LeerMotores()
        {
            XmlReader reader = XmlReader.Create(this.filePath, this.settings);
            reader.MoveToContent();
            reader.ReadToDescendant("Motores");

            XML_Motor m;
            List<XML_Motor> ms = new List<XML_Motor>();

            while (reader.ReadToFollowing("Motor"))
            {
                //XmlReader motor=motores.ReadSubtree();
                m = new XML_Motor();

                reader.ReadToFollowing("idMotor");
                m.Id = reader.ReadElementContentAsInt();
                m.Descripcion = reader.ReadElementContentAsString();
                ms.Add(m);
            }
            reader.Close();
            return ms;


        }
    }

    public class XML_Config
    {
        private int id;
        private string descripcion;
        private string puertoCom;
        private string tipoProg;
        private string unidadMedida;
        private decimal maxX;
        private decimal maxY;
        private decimal maxZ;
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
        public decimal MaxX
        {
            get { return maxX; }
            set { maxX = value; }
        }
        public decimal MaxY
        {
            get { return maxY; }
            set { maxY = value; }
        }
        public decimal MaxZ
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
        private int idConfig;
        private int idMaterial;
        private int idMotor;
        private decimal tamVuelta;
        private decimal gradosPaso;

        public int IdConfig
        {
            get { return idConfig; }
            set { idConfig = value; }
        }
        public int IdMaterial
        {
            get { return idMaterial; }
            set { idMaterial = value; }
        }
        public int IdMotor
        {
            get { return idMotor; }
            set { idMotor = value; }
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
