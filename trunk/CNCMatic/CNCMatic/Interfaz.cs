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

                }
                else
                {

                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw (new Exception("ConectarCNC: " + ex.Message));
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
