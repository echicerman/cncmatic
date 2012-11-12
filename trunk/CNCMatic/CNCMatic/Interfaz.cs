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
        private static readonly log4net.ILog logger = LogManager.LogManager.GetLogger();

        public static XML_Config ConfiguracionActual()
        {
            try
            {   //cargamos la configuracion por default
                string xmlPath = ConfigurationManager.AppSettings["xmlDbPath"];
                //string ultConfigId = ConfigurationManager.AppSettings["idLastConfig"];

                XMLdb x = new XMLdb(xmlPath);
                string ultConfigId = x.LeeConfiguracionGral().IdLastConfig.ToString();

                XML_Config config = x.LeeConfiguracionActual(Convert.ToInt32(ultConfigId));

                logger.Info("Leemos configuracion actual en Interfaz");

                return config;
            }
            catch (Exception ex)
            {
                logger.Error("Interfaz.ConfiguracionActual: " + ex.Message);
                throw (new Exception("Interfaz.ConfiguracionActual: " + ex.Message));
            }
        }

        public static bool ConectarCNC(ref SafeControls.SafeToolStripStatusLabel lblEstado, List<string> loteInstrucciones, ref SafeControls.SafeToolStripStatusLabel lblPosicActual, ref SafeControls.SafeToolStripProgressBar pgrBar)
        {
            try
            {
                //validamos que exista config para los 3 motores, y que tengan la configuracion del gxp y tam vuelta
                if (!validarConfiguracionActual())
                {
                    logger.Warn("Validando configuracion: Configuracion Mal");
                    MessageBox.Show("Por favor, verifique la configuracion, dado que no se encuentra la configuracion para los tres motores, o alguno de los parametros necesarios no estan configurados", "Error en Configuracion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    bool resultado = true;

                    //traemos la instancia de la maquina
                    var cnc = CNC.CNC.Cnc;

                    if (cnc.EstadoActual != CNC_Estados.EsperandoComando)
                    {//si no esta esperando comando -> conectamos

                        logger.Info("Estado actual del CNC: " + cnc.EstadoActual + ". Iniciando conexión con CNC.");

                        cnc.Label = lblEstado;
                        cnc.PuertoConexion = ConfiguracionActual().PuertoCom;
                        cnc.Configuracion = ConfiguracionActual();
                        cnc.LblPosicionActual = lblPosicActual;
                        cnc.BarraProgreso = pgrBar;

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
                        logger.Info("Estado actual del CNC: " + cnc.EstadoActual + ". Iniciando transmisión al CNC.");

                        cnc.Configuracion = ConfiguracionActual();
                        cnc.LblPosicionActual = lblPosicActual;
                        cnc.BarraProgreso = pgrBar;

                        //ya cargamos el lote de instrucciones del CNC
                        cnc.CargaLoteInstrucciones(loteInstrucciones);

                        logger.Info("Lote de instrucciones cargados");

                        //iniciamos la transmision
                        int result =cnc.IniciarTransmision();
                        
                        if (result == -1)
                        {
                            resultado = false;
                            logger.Error("Error de comunicación con la máquina CNC");
                            MessageBox.Show("Error de comunicación con la máquina CNC", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        if (result == 0)
                        {
                            resultado = false;
                            logger.Warn("No hay instrucciones para ser enviadas a la máquina CNC");
                            MessageBox.Show("No hay instrucciones para ser enviadas a la máquina CNC", "Envío instrucciones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        //volvemos para atras el mensaje
                        if(!resultado)
                            lblEstado.Text = "Conectando (paso 3 de 3): Conexión establecida";
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

                //no tiene alguna info de los tres motores
                if (config.GradosPasoX <= 0)
                    return false;
                if (config.TamVueltaX <= 0)
                    return false;
                if (config.GradosPasoY <= 0)
                    return false;
                if (config.TamVueltaY <= 0)
                    return false;
                if (config.GradosPasoZ <= 0)
                    return false;
                if (config.TamVueltaZ <= 0)
                    return false;

                logger.Info("Validando configuracion: Configuracion OK");
                return true;

            }
            catch (Exception ex)
            {
                throw (new Exception("Interfaz.validarConfiguracionActual: " + ex.Message));
            }
        }

        public static void MoverLibre(string movimiento, ref SafeControls.SafeToolStripStatusLabel lblPosicActual)
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
                    logger.Info("Pausando Transmision: se puede pausar - estado actual CNC " + cnc.EstadoActual);

                    cnc.PausarTransmision();
                }
                logger.Warn("Pausando Transmision: no se puede pausar - estado actual CNC " + cnc.EstadoActual);
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


                cnc.ReiniciarCNC(true);

            }
            catch (Exception ex)
            {
                throw (new Exception("Interfaz.ReiniciarCNC: " + ex.Message));
            }
        }

        public static int DetenerCNC()
        {
            try
            {
                //maquina
                var cnc = CNC.CNC.Cnc;


                return cnc.Detener();

            }
            catch (Exception ex)
            {
                throw (new Exception("Interfaz.DetenerCNC: " + ex.Message));
            }
        }

        public static void OrigenCNC(ref SafeControls.SafeToolStripStatusLabel lblEstado, ref SafeControls.SafeToolStripStatusLabel lblPosicActual)
        {
            try
            {
                //maquina
                var cnc = CNC.CNC.Cnc;
                cnc.Label = lblEstado;
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

        public static void DesconectarCNC()
        {
            try
            {
                //maquina
                var cnc = CNC.CNC.Cnc;


                cnc.Desconectar();

            }
            catch (Exception ex)
            {
                throw (new Exception("Interfaz.ReiniciarCNC: " + ex.Message));
            }
        }
    }
}
