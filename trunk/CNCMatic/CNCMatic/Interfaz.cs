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
            try
            { //cargamos la configuracion por default
                string xmlPath = ConfigurationManager.AppSettings["xmlDbPath"];
                string ultConfigId = ConfigurationManager.AppSettings["idLastConfig"];

                XMLdb x = new XMLdb(xmlPath);
                XML_Config config = x.LeeConfiguracionActual(Convert.ToInt32(ultConfigId));

                return config;
            }
            catch (Exception ex)
            {
                throw (new Exception("Interfaz.ConfiguracionActual: " + ex.Message));
            }
        }

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
                    bool resultado = true;

                    //traemos la instancia de la maquina
                    var cnc = CNC.CNC.Cnc;

                    if (cnc.EstadoActual != CNC_Estados.EsperandoComando)
                    {//si no esta esperando comando

                        cnc.Label = lblEstado;
                        cnc.PuertoConexion = ConfiguracionActual().PuertoCom;
                        cnc.Configuracion = ConfiguracionActual();
                        cnc.LblPosicionActual = lblPosicActual;

                        //ya cargamos el lote de instrucciones del CNC
                        cnc.CargaLoteInstrucciones(loteInstrucciones);

                        //1: establecemos conexion    
                        resultado = cnc.EstablecerConexion();
                        if (!resultado)
                        {
                            MessageBox.Show("No se ha podido establecer la conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        cnc.Configuracion = ConfiguracionActual();
                        cnc.LblPosicionActual = lblPosicActual;

                        //ya cargamos el lote de instrucciones del CNC
                        cnc.CargaLoteInstrucciones(loteInstrucciones);

                        //iniciamos la transmision
                        resultado = cnc.IniciarTransmision();
                        if (!resultado)
                        {
                            MessageBox.Show("No se ha podido establecer la conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("Interfaz.ConectarCNC: " + ex.Message));
            }

        }

        public static void ReaudarTransmision()
        {
            try
            {
                //maquina
                var cnc = CNC.CNC.Cnc;

                cnc.ReanudarTransmision();

            }
            catch (Exception ex)
            {
                throw (new Exception("Interfaz.ReaudarTransmision: " + ex.Message));
            }
        }

        private static bool validarConfiguracionActual()
        {
            try
            {
                XML_Config config = ConfiguracionActual();

                if (config.ConfigMatMot.Count < 3)
                {
                    //no tiene la info de los tres motores
                    return false;
                }

                for (int i = 0; i < 3; i++)
                {

                    if (
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
                throw (new Exception("Interfaz.validarConfiguracionActual: " + ex.Message));
            }
        }

        public static void MoverLibre(string movimiento, ref ToolStripStatusLabel lblPosicActual)
        {
            try
            {//maquina
                var cnc = CNC.CNC.Cnc;
                cnc.PuertoConexion = ConfiguracionActual().PuertoCom;
                cnc.Configuracion = ConfiguracionActual();
                cnc.LblPosicionActual = lblPosicActual;

                cnc.EnviarMovimientoLibre(movimiento);
            }
            catch (Exception ex)
            {
                throw (new Exception("Interfaz.MoverLibre: " + ex.Message));
            }
        }

        public static void DetenerMovimientoLibre()
        {
            try
            {//maquina
                var cnc = CNC.CNC.Cnc;

                cnc.DetenerMovimientoLibre();
            }
            catch (Exception ex)
            {
                throw (new Exception("Interfaz.DetenerMovimientoLibre: " + ex.Message));
            }
        }

        public static void PausarTransmision()
        {
            try
            {//maquina
                var cnc = CNC.CNC.Cnc;

                if (cnc.EstadoActual == CNC_Estados.EsperandoComando || cnc.EstadoActual == CNC_Estados.ProcesandoComando)
                {
                    cnc.PausarTransmision();
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("Interfaz.PausarTransmision: " + ex.Message));
            }
        }

        public static void ReiniciarCNC()
        {
            try
            {
                //maquina
                var cnc = CNC.CNC.Cnc;


                cnc.Reiniciar();

            }
            catch (Exception ex)
            {
                throw (new Exception("Interfaz.ReiniciarCNC: " + ex.Message));
            }
        }

        public static void OrigenCNC(ref ToolStripStatusLabel lblPosicActual)
        {
            try
            {
                //maquina
                var cnc = CNC.CNC.Cnc;
                cnc.PuertoConexion = ConfiguracionActual().PuertoCom;
                cnc.Configuracion = ConfiguracionActual();
                cnc.LblPosicionActual = lblPosicActual;

                cnc.IrAlInicio();

            }
            catch (Exception ex)
            {
                throw (new Exception("Interfaz.OrigenCNC: " + ex.Message));
            }
        }
    }
}
