using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Configuracion;

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
                c.MaxX = reader.ReadElementContentAsFloat ();
                c.MaxY = reader.ReadElementContentAsFloat();
                c.MaxZ = reader.ReadElementContentAsFloat();
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

    
}
