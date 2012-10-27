using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Configuracion;
using System.Data;
using System.Globalization;
using System.Threading;

namespace CNCMatic.XML
{
    public class XMLdb
    {
        public string filePath;
        private XmlReaderSettings settings;
        //public int ultConfigId;

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
            try
            {
                //leemos nuevamente las configuraciones en un dataset, y agregamos
                //un nuevo datarow con la nueva configuracion y luego grabamos el xml
                DataSet ds = new DataSet();
                ds.ReadXml(this.filePath);

                DataTable dt = ds.Tables["configuracion"];

                DataRow dr;

                //en caso que no exista la tabla, la creamos
                if (dt != null)
                {
                    dr = dt.NewRow();
                }
                else
                {
                    DataColumn dc;

                    dt = new DataTable("configuracion");

                    dc = new DataColumn("Id");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Descripcion");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("PuertoCom");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("UnidadMedida");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("TipoProg");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("MaxX");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("MaxY");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("MaxZ");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("VelocidadMovimiento");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("LargoSeccion");
                    dt.Columns.Add(dc);

                    dr = dt.NewRow();

                    ds.Tables.Add(dt);
                }

                bool actualiza = false;

                //verificamos que no exista la configuracion a grabar,
                //sino actualizamos
                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToInt32(row["Id"]) == config.Id)
                    {
                        dr = row;
                        actualiza = true;
                    }
                }

                dr["Id"] = config.Id;
                dr["Descripcion"] = config.Descripcion;
                dr["PuertoCom"] = config.PuertoCom;
                dr["UnidadMedida"] = config.UnidadMedida;
                dr["TipoProg"] = config.TipoProg;
                dr["MaxX"] = config.MaxX.ToString();
                dr["MaxY"] = config.MaxY.ToString();
                dr["MaxZ"] = config.MaxZ.ToString();
                dr["VelocidadMovimiento"] = config.VelocidadMovimiento.ToString();
                dr["LargoSeccion"] = config.LargoSeccion.ToString();


                if (!actualiza)
                    dt.Rows.Add(dr);

                ds.WriteXml(this.filePath);

                //grabamos los items para la configuracion por material/motor
                GrabaConfiguracionMatMot(config.Id, config.ConfigMatMot);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void GrabaConfiguracionMatMot(int idConfig, List<XML_ConfigMatMot> configs)
        {
            try
            {
                //leemos nuevamente las configuraciones en un dataset, y agregamos
                //un nuevo datarow con la nueva configuracion y luego grabamos el xml
                DataSet ds = new DataSet();
                ds.ReadXml(this.filePath);

                DataTable dt = ds.Tables["configuracionMatMot"];

                DataRow dr;

                if (dt != null)
                {
                    dr = dt.NewRow();
                }
                else
                {
                    DataColumn dc;

                    dt = new DataTable("configuracionMatMot");

                    dc = new DataColumn("Id");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("IdConfig");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("IdMaterial");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("IdMotor");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("GradosPaso");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("TamVuelta");
                    dt.Columns.Add(dc);

                    dr = dt.NewRow();

                    ds.Tables.Add(dt);
                }

                //grabamos o actualizamos cada item
                foreach (XML_ConfigMatMot config in configs)
                {
                    bool actualiza = false;

                    //verificamos que no existan los items de configuracion a grabar,
                    //sino actualizamos
                    foreach (DataRow row in dt.Rows)
                    {
                        if (Convert.ToInt32(row["Id"]) == config.IdConfigMatMot &&
                            Convert.ToInt32(row["IdConfig"]) == idConfig
                            )
                        {
                            dr = row;
                            actualiza = true;
                        }
                    }

                    if (!actualiza)
                        dr = dt.NewRow();

                    dr["Id"] = config.IdConfigMatMot;
                    dr["IdConfig"] = idConfig;
                    dr["IdMaterial"] = config.IdMaterial;
                    dr["IdMotor"] = config.IdMotor;
                    dr["GradosPaso"] = config.GradosPaso.ToString();
                    dr["TamVuelta"] = config.TamVuelta.ToString();


                    if (!actualiza)
                        dt.Rows.Add(dr);
                }

                ds.WriteXml(this.filePath);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void GrabaMotor(XML_Motor motor)
        {
            try
            {
                //leemos nuevamente los motores en un dataset, y agregamos
                //un nuevo datarow con el nuevo motor y luego grabamos el xml
                DataSet ds = new DataSet();
                ds.ReadXml(this.filePath);

                DataTable dt = ds.Tables["motores"];

                DataRow dr;

                if (dt != null)
                {
                    dr = dt.NewRow();
                }
                else
                {
                    DataColumn dc;

                    dt = new DataTable("motores");

                    dc = new DataColumn("Id");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Descripcion");
                    dt.Columns.Add(dc);

                    dr = dt.NewRow();

                    ds.Tables.Add(dt);
                }

                dr["Id"] = motor.Id;
                dr["Descripcion"] = motor.Descripcion;

                dt.Rows.Add(dr);

                ds.WriteXml(this.filePath);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void GrabaMaterial(XML_Material material)
        {
            try
            {
                //leemos nuevamente los motores en un dataset, y agregamos
                //un nuevo datarow con el nuevo material y luego grabamos el xml
                DataSet ds = new DataSet();
                ds.ReadXml(this.filePath);

                DataTable dt = ds.Tables["materiales"];
                DataRow dr;

                if (dt != null)
                {
                    dr = dt.NewRow();
                }
                else
                {
                    DataColumn dc;
                    dt = new DataTable("materiales");

                    dc = new DataColumn("Id");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Descripcion");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Espesor");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Ancho");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Largo");
                    dt.Columns.Add(dc);

                    dr = dt.NewRow();

                    ds.Tables.Add(dt);
                }

                dr["Id"] = material.Id;
                dr["Descripcion"] = material.Descripcion;
                dr["Espesor"] = material.Espesor;
                dr["Ancho"] = material.Ancho;
                dr["Largo"] = material.Largo;

                dt.Rows.Add(dr);

                ds.WriteXml(this.filePath);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<XML_Config> LeeConfiguracion()
        {


            //seteamos el tipo de culture para grabar bien los decimales
            CultureInfo actual = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-AR");

            DataSet ds = new DataSet();
            ds.ReadXml(this.filePath);

            DataTable dtConfig = ds.Tables["configuracion"];
            DataTable dtConfigMatMot = ds.Tables["configuracionMatMot"];

            XML_Config c;
            List<XML_Config> cs = new List<XML_Config>();
            if (dtConfig != null)
            {
                foreach (DataRow dr in dtConfig.Rows)
                {
                    //leemos la configuracion general
                    c = new XML_Config();

                    c.Id = Convert.ToInt32(dr["Id"]);
                    c.Descripcion = dr["Descripcion"].ToString();
                    c.PuertoCom = dr["PuertoCom"].ToString();
                    c.UnidadMedida = dr["UnidadMedida"].ToString();
                    c.TipoProg = dr["TipoProg"].ToString();
                    c.MaxX = float.Parse(dr["MaxX"].ToString());
                    c.MaxY = float.Parse(dr["MaxY"].ToString());
                    c.MaxZ = float.Parse(dr["MaxZ"].ToString());
                    c.VelocidadMovimiento = dr["VelocidadMovimiento"].ToString();
                    c.LargoSeccion = dr["LargoSeccion"].ToString();

                    c.ConfigMatMot = new List<XML_ConfigMatMot>();

                    if (dtConfigMatMot != null)
                    {
                        XML_ConfigMatMot configMatMot;
                        foreach (DataRow dr2 in dtConfigMatMot.Rows)
                        {
                            if (dr2["IdConfig"].ToString() == c.Id.ToString())
                            {
                                configMatMot = new XML_ConfigMatMot();

                                configMatMot.IdConfigMatMot = Convert.ToInt32(dr2["Id"]);
                                configMatMot.IdMaterial = Convert.ToInt32(dr2["IdMaterial"]);
                                configMatMot.IdMotor = Convert.ToInt32(dr2["IdMotor"]);
                                configMatMot.GradosPaso = Convert.ToDecimal(dr2["GradosPaso"]);
                                configMatMot.TamVuelta = Convert.ToDecimal(dr2["TamVuelta"]);

                                c.ConfigMatMot.Add(configMatMot);
                            }
                        }
                    }
                    cs.Add(c);
                }
            }

            //devolvemos al thread el formato actual
            Thread.CurrentThread.CurrentCulture = actual;

            return cs;
        }

        public List<XML_Material> LeerMateriales()
        {
            //seteamos el tipo de culture para grabar bien los decimales
            CultureInfo actual = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-AR");

            DataSet ds = new DataSet();
            ds.ReadXml(this.filePath);

            DataTable dt = ds.Tables["materiales"];

            XML_Material m;
            List<XML_Material> ms = new List<XML_Material>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    m = new XML_Material();

                    m.Id = Convert.ToInt32(dr["Id"]);
                    m.Descripcion = dr["Descripcion"].ToString();
                    m.Espesor = Convert.ToDecimal(dr["Espesor"]);
                    m.Ancho = Convert.ToDecimal(dr["Ancho"]);
                    m.Largo = Convert.ToDecimal(dr["Largo"]);
                    ms.Add(m);
                }
            }

            //devolvemos al thread el formato actual
            Thread.CurrentThread.CurrentCulture = actual;

            return ms;

        }

        public List<XML_Motor> LeerMotores()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(this.filePath);

            DataTable dt = ds.Tables["motores"];

            XML_Motor m;
            List<XML_Motor> ms = new List<XML_Motor>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    m = new XML_Motor();

                    m.Id = Convert.ToInt32(dr["Id"]);
                    m.Descripcion = dr["Descripcion"].ToString();

                    ms.Add(m);
                }
            }
            return ms;
        }

        public XML_Config LeeConfiguracionActual(int idConfig)
        {


            //seteamos el tipo de culture para grabar bien los decimales
            CultureInfo actual = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-AR");

            DataSet ds = new DataSet();
            ds.ReadXml(this.filePath);

            DataTable dtConfig = ds.Tables["configuracion"];
            DataTable dtConfigMatMot = ds.Tables["configuracionMatMot"];

            XML_Config c = null;

            if (dtConfig != null)
            {
                foreach (DataRow dr in dtConfig.Rows)
                {
                    //leemos la configuracion general
                    if (Convert.ToInt32(dr["Id"]) == idConfig)
                    {

                        c = new XML_Config();

                        c.Id = Convert.ToInt32(dr["Id"]);
                        c.Descripcion = dr["Descripcion"].ToString();
                        c.PuertoCom = dr["PuertoCom"].ToString();
                        c.UnidadMedida = dr["UnidadMedida"].ToString();
                        c.TipoProg = dr["TipoProg"].ToString();
                        c.MaxX = float.Parse(dr["MaxX"].ToString());
                        c.MaxY = float.Parse(dr["MaxY"].ToString());
                        c.MaxZ = float.Parse(dr["MaxZ"].ToString());
                        c.VelocidadMovimiento = dr["VelocidadMovimiento"].ToString();
                        c.LargoSeccion = dr["LargoSeccion"].ToString();

                        c.ConfigMatMot = new List<XML_ConfigMatMot>();

                        if (dtConfigMatMot != null)
                        {
                            XML_ConfigMatMot configMatMot;
                            foreach (DataRow dr2 in dtConfigMatMot.Rows)
                            {
                                if (dr2["IdConfig"].ToString() == c.Id.ToString())
                                {
                                    configMatMot = new XML_ConfigMatMot();

                                    configMatMot.IdConfigMatMot = Convert.ToInt32(dr2["Id"]);
                                    configMatMot.IdMaterial = Convert.ToInt32(dr2["IdMaterial"]);
                                    configMatMot.IdMotor = Convert.ToInt32(dr2["IdMotor"]);
                                    configMatMot.GradosPaso = Convert.ToDecimal(dr2["GradosPaso"]);
                                    configMatMot.TamVuelta = Convert.ToDecimal(dr2["TamVuelta"]);

                                    c.ConfigMatMot.Add(configMatMot);
                                }
                            }
                        }
                    }
                }
            }

            //devolvemos al thread el formato actual
            Thread.CurrentThread.CurrentCulture = actual;

            return c;
        }
    }


}
