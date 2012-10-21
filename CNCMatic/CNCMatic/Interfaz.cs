using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using CNC;
using Configuracion;
using CNCMatic.XML;
using System.Windows.Forms;

namespace CNCMatic
{
    public static class Interfaz
    {
        public static XML_Config ConfiguracionActual()
        {
            //cargamos la configuracion por default
            string xmlPath = ConfigurationManager.AppSettings["xmlDbPath"];
            string ultConfigId = ConfigurationManager.AppSettings["idLastConfig"];

            XMLdb x = new XMLdb(xmlPath);
            XML_Config config = x.LeeConfiguracionActual(Convert.ToInt32(ultConfigId));

            return config;
        }

        //public static bool EnviaConfiguracion()
        //{
        //    try
        //    {
        //        //traemos la instancia de la maquina
        //        var cnc = CNC.CNC.Cnc;

        //        //validamos que este en estado Conectado para transferir configuracion
        //        if (cnc.EstadoActual == CNC.CNC_Estados.Conectado)
        //        {
        //            cnc.EnviarConfiguracion(ConfiguracionActual());
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (new Exception("EnviaConfiguracion: " + ex.Message));
        //    }


        //}

        public static bool ConectarCNC(ref ToolStripStatusLabel lblEstado, List<string> loteInstrucciones, ref ToolStripStatusLabel lblPosicActual)
        {
            try
            {
                //validamos que exista config para los 3 motores, y que tengan la configuracion del gxp y tam vuelta
                if (!validarConfiguracionActual())
                {
                    MessageBox.Show("Por favor, verifique la configuracion, dado que no se encuentra la configuracion para los tres motores, o alguno de los parametros necesarios no estan configurados", "Error en Configuracion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    //traemos la instancia de la maquina
                    var cnc = CNC.CNC.Cnc;

                    cnc.Label = lblEstado;
                    cnc.PuertoConexion = ConfiguracionActual().PuertoCom;
                    cnc.Configuracion = ConfiguracionActual();
                    cnc.LblPosicionActual = lblPosicActual;

                    //ya cargamos el lote de instrucciones del CNC
                    cnc.CargaLoteInstrucciones(loteInstrucciones);

                    bool resultado = true;

                    //1: establecemos conexion    
                    resultado = cnc.EstablecerConexion();
                    if (resultado)
                    {
                        MessageBox.Show("No se ha podido establecer la conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {

                    }

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("ConectarCNC: " + ex.Message));
            }

        }

        private static bool validarConfiguracionActual()
        {
            try
            {
                XML_Config config=ConfiguracionActual();
                
                if(config.ConfigMatMot.Count<3)
                {
                    //no tiene la info de los tres motores
                    return false;
                }

                for (int i = 0; i < 3; i++)
                {

                    if  (
                        (config.ConfigMatMot[i].GradosPaso <= 0) ||
                        (config.ConfigMatMot[i].TamVuelta <= 0)
                        )
                    {
                        //no estan correcto los valores parametrizados
                        return false;

                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (new Exception("validarConfiguracionActual: " + ex.Message));
            }
        }

        //public static bool EnviarSetDeInstrucciones(List<string> loteInstrucciones)
        //{
        //    try
        //    {
        //        bool resultado = false;

        //        //traemos la instancia de la maquina
        //        var cnc = CNC.CNC.Cnc;

        //        //validamos que este en estado WAITINGCOMMAND para transferir configuracion
        //        if (cnc.EstadoActual == CNC.CNC_Estados.EsperandoComando)
        //        {
        //            cnc.CargaLoteInstrucciones(loteInstrucciones);

        //            cnc.IniciarTransmision();
        //        }

        //        return resultado;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (new Exception("EnviarSetDeInstrucciones: " + ex.Message));
        //    }

        //}

    }
}
