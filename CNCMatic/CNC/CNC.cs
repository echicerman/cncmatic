using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Configuracion;
using VirtualSerial;

namespace CNC
{
    /// <summary>
    /// Conjunto de estados en los que se puede encontrar la maquina
    /// </summary>
    public struct CNC_Estados
    {
        //SERIALPORTCONNECTED: estado inicial, ni bien se enchufa la maquina
        //se envia cualquier cadena, la maquina cuenta los chars y devuelve ese numero
        public static string SerialPortConectado = "SERIALPORTCONNECTED";

        //HANDSHAKEACKRECEIVED: si recibe "ok", pasa al siguiente estado
        //caso contrario vuelve a SERIALPORTCONNECTED
        public static string HandShakeRecibido = "HANDSHAKEACKRECEIVED";

        //CNCMATICCONNECTED: establecida la conexion CNC-CNCMatic
        public static string Conectado = "CNCMATICCONNECTED";

        //CONFIGURED: no espera nada, solo mueve la punta al origen y pone la maquina
        //en el cero y responde "Posicion de Origen"
        public static string Configurado = "CONFIGURED";

        //WAITINGCOMMAND: se espera comando que debe iniciar con G o M,
        //sino envía "Error en Comando", sino si esta OK puede enviar "Comando Soportado"
        //o "Comando No Soportado". Si se soporta el comando se pone en PROCESSINGCOMMAND
        //sino se queda en WAITINGCOMMAND
        public static string EsperandoComando = "WAITINGCOMMAND";

        //PROCESSINGCOMMAND: la maquina se esta moviendo, al finalizar vuelve
        //a WAITINGCOMMAND, puede terminar bien: "Comando Ejecutado" o mal
        //"Sensor Fin de Carrera"
        public static string ProcesandoComando = "PROCESSINGCOMMAND";

        //FREEMOVES: la maquina se mueve en según lo requerido
        public static string MovimientoLibre = "FREEMOVES";


    }

    /// <summary>
    /// Conjunto de mensajes utilizados para la comunicación desde el CNCMatic al CNC
    /// </summary>
    public struct CNC_Mensajes_Send
    {
        //(-(-(-(SERIALPORTCONNECTED)-)-)-)
        //Mensaje para establecer la conexion con el CNC
        public static string HandShake = "Hola_CNC";

        //(-(-(-(HANDSHAKERECEIVED)-)-)-)
        //Handshake recibido ok
        public static string HandShakeOk = "ok";
        //Handshake recibido no ok
        public static string HandShakeBad = "bad";

        //(-(-(-(CNCMATICCONNECTED)-)-)-)
        //string de configuracion de parametros
        public static string ConfigString = "";

        //(-(-(-(FREEMOVES)-)-)-)
        //FREEMOVES: inicializa el estado FREEMOVES
        public static string MovimientoLibre = "freemoves";

        //Movimientos: son los comandos que indica a la maquina hacia que direccion moverse libre
        public static string Xavance = "+X";
        public static string Xretroc = "-X";
        public static string Yavance = "+Y";
        public static string Yretroc = "-Y";
        public static string Zavance = "+Z";
        public static string Zretroc = "-Z";

        //(-(-(-(MENSAJES VARIOS)-)-)-)-)
        //RESET: cuando termine de procesar el comando actual pone en estado 
        //de SERIALPORTCONNECTED
        public static string Reset = "reset";

        //STATUS: cuando termine de procesar el comando actual devuelve el id 
        //del status actual 
        public static string Status = "status";

        //STOP: cuando termine de procesar el comando actual pone en estado 
        //de SERIALPORTCONNECTED
        public static string Stop = "stop";

        //POSITION: devuelve la posicion actual de la punta
        public static string PosicionActual = "position";


    }

    /// <summary>
    /// Conjunto de mensajes utilizados para la comunicación desde el CNC al CNCMatic
    /// </summary>
    public struct CNC_Mensajes_Recep
    {
        //(-(-(-(CNCMATICCONNECTED)-)-)-)
        //Configuracion OK
        public static string ConfigStringOK = "CFGOK";
        //Configuracion BAD
        public static string ConfigStringBAD = "CFGE";

        //(-(-(-(CONFIGURED)-)-)-)
        //Posicion Origen
        public static string PosicionOrigen = "PO";

        //(-(-(-(WAITINGCOMMAND)-)-)-)
        //Comando Soportado
        public static string ComandoSoportado = "CMDS";
        //Comando No Soportado
        public static string ComandoNoSoportado = "CMDNS";
        //Comando Erroneo
        public static string ComandoErroneo = "CMDE";

        //(-(-(-(PROCESINGCOMMAND)-)-)-)
        //Comando terminado OK
        public static string ComandoEjecutado = "CMDDONE";
        //Comando terminado por un fin de de carrera en un eje
        public static string SensorFinCarrera = "SFC"; //<<-- tambien aplica para cuando la maquina esta en FREEMOVES
        //Comando terminado por una parada de emergencia por hardware
        public static string ParadaEmergencia = "PE";

        //(-(-(-(FREEMOVES)-)-)-)
        //FREEMOVE: responde OK al cambio de estado
        public static string MovimientoLibreOK = "CNCFM";

        //(-(-(-(MENSAJES VARIOS)-)-)-)-)
        //RESET: cuando termine de procesar el comando actual pone en estado 
        //de SERIALPORTCONNECTED y responde
        public static string Reset = "CNCR";

        //STOP: cuando termine de procesar el comando actual pone en estado 
        //de SERIALPORTCONNECTED y responde
        public static string Stop = "CNCSFM";

        //STATUS: cuando termine de procesar el comando actual devuelve el id 
        //del status actual 
        public static string Status = "CNCS:";
    }

    /// <summary>
    /// Instancia que representa la maquina, implementada a traves del patron SINGLETON para siempre
    /// manejar internamente la misma instancia de la clase
    /// </summary>
    public class CNC
    {
        private static CNC instancia;

        private CNC() { }

        public static CNC Cnc
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new CNC();
                }
                return instancia;
            }
        }
        private List<string> colaMensajes = new List<string>();

        private List<string> loteInstrucciones = new List<string>();
        private int proximaInstruccion;

        public System.Windows.Forms.ToolStripStatusLabel Label { get; set; }

        //variable que indica si estamos transmitiendo comandos
        private bool transmision;

        private string estadoActual;
        public string EstadoActual
        {
            get
            {
                //actualizamos el estado de la maquina antes de devolverlo
                //this.PedirEstadoCNC();

                return this.estadoActual;
            }
        }
        public string UltimoMensajeSend { get; set; }
        public string UltimoMensajeRecep { get; set; }
        public string PuertoConexion { get; set; }

        public bool EnviarConfiguracion(XML_Config configuracion)
        {
            try
            {
                //si la maquina esta CNCMATICCONNECTED espera la configuracion
                if (estadoActual == CNC_Estados.Conectado)
                {
                    string stringConfiguracion = "";
                    decimal GxP, TamV, Valor;

                    for (int i = 0; i < 3; i++)
                    {
                        GxP = configuracion.ConfigMatMot[i].GradosPaso;
                        TamV = configuracion.ConfigMatMot[i].TamVuelta;
                        Valor = 360 / (GxP * TamV);
                        switch (i)
                        {
                            case 0: stringConfiguracion += Valor.ToString().Replace(",", ".") + ";"; break;
                            case 1: stringConfiguracion += Valor.ToString().Replace(",", ".") + ";"; break;
                            case 2: stringConfiguracion += Valor.ToString().Replace(",", "."); break;
                        }
                    }

                    //enviamos el string de configuracion
                    enviar(stringConfiguracion);

                    //recibimos respuesta
                    string recep = recibir(1000);

                    //vemos el estado de la respuesta
                    if (recep == CNC_Mensajes_Recep.ConfigStringOK)
                    {
                        estadoActual = CNC_Estados.Configurado;
                        return true;
                    }
                    else if (recep == CNC_Mensajes_Recep.ConfigStringBAD)
                    {
                        return false;
                    }
                    else
                    {
                        return false;
                    }


                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw (new Exception("EnviarConfiguracion: " + ex.Message));
            }


        }

        private void PedirEstadoCNC()
        {
            try
            {
                //conectamos y pedimos estatus
                //CNC_Mensajes_Send.Status;
                string recep = "";

                switch (Convert.ToInt32(recep.Trim()))
                {
                    case 0: estadoActual = CNC_Estados.SerialPortConectado; break; //SERIALPORTCONNECTED
                    case 1: estadoActual = CNC_Estados.HandShakeRecibido; break; //HANDSHAKERECEIVED
                    case 2: estadoActual = CNC_Estados.Conectado; break; //CNCMATICCONNECTED
                    case 3: estadoActual = CNC_Estados.Configurado; break; //CONFIGURED
                    case 4: estadoActual = CNC_Estados.EsperandoComando; break; //WAITINGCOMMAND
                    case 5: estadoActual = CNC_Estados.ProcesandoComando; break; //PROCESSINGCOMMAND
                    case 8: estadoActual = CNC_Estados.MovimientoLibre; break; //FREEMOVES

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public bool EstablecerConexion()
        {
            try
            {
                this.Label.Text = "Estableciendo conexion...";

                //nos conectamos al puerto
                conectarSerialPort();

                this.Label.Text = "Conexion OK: enviando handshake";
                estadoActual = CNC_Estados.SerialPortConectado;

                //enviar: CNC_Mensajes_Send.HandShake;
                enviar(CNC_Mensajes_Send.HandShake);

                //recibimos respuesta
                string recep = recibir(1000);

                estadoActual = CNC_Estados.HandShakeRecibido;

                //si coincide longitud recibida con la enviada
                if (CNC_Mensajes_Send.HandShake.Length == Convert.ToInt32(recep))
                {
                    //enviar: CNC_Mensajes_Send.HandShakeOk 
                    enviar(CNC_Mensajes_Send.HandShakeOk);

                    estadoActual = CNC_Estados.Conectado;

                    this.Label.Text = "Conexion establecida";

                    return true;
                }
                else
                {
                    //enviar: CNC_Mensajes_Send.HandShakeBad 
                    enviar(CNC_Mensajes_Send.HandShakeBad);

                    estadoActual = CNC_Estados.SerialPortConectado;

                    this.Label.Text = "Error en el handshake";

                    return false;
                }

            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.EstablecerConexion: " + ex.Message));
            }
        }

        public bool PosicionOrigen()
        {
            try
            {
                //recibimos respuesta
                string recep = recibir(1000);

                //si se recibe mensaje de posicion de origen
                if (CNC_Mensajes_Recep.PosicionOrigen == recep)
                {
                    //queda esperando instrucciones
                    estadoActual = CNC_Estados.EsperandoComando;

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.PosicionOrigen: " + ex.Message));
            }

        }

        private void conectarSerialPort()
        {
            try
            {
                Port.OpenConnection(this.PuertoConexion);
                Port.DataReceivedCallback = new Port.DataReceivedCallbackDelegate(leerPuerto);

            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.conectarSerialPort: " + ex.Message));
            }

        }

        private void desconectarSerialPort()
        {
            try
            {
                Port.CloseConnection();

            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.desconectarSerialPort: " + ex.Message));
            }

        }

        private void enviar(string texto)
        {
            try
            {
                //limpiamos espacios del texto
                texto = texto.Trim();

                Port.Write(texto);
            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.enviar: " + ex.Message));
            }

        }

        private void leerPuerto(string texto)
        {
            try
            {
                //agregamos el mensaje a la cola de mensajes
                colaMensajes.Add(texto.Trim());

                //si estamos transmitiendo, entonces llamamos a la funcion que lee
                if (transmision)
                {
                    //llamamos a la funcion que interpreta la respuesta
                    RespuestaInstruccion();
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.recibir: " + ex.Message));
            }
        }

        public void CargaLoteInstrucciones(List<string> loteInstrucciones)
        {
            this.proximaInstruccion = 0;
            this.loteInstrucciones = loteInstrucciones;
        }

        private string recibir(int tiempoEspera)
        {
            try
            {
                if (tiempoEspera!=0)
                {
                    //esperamos para que llegue la respuesta
                    System.Threading.Thread.Sleep(tiempoEspera);
                }

                string mensaje = "";

                //si hay mensajes
                if (colaMensajes.Count > 0)
                {
                    //levantamos el primer mensaje de la cola
                    mensaje = colaMensajes[0];
                    colaMensajes.RemoveAt(0);
                }

                return mensaje;
            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.conectarSerialPort: " + ex.Message));
            }
        }

        public bool IniciarTransmision()
        {
            try
            {
                if (EnviarInstruccion(loteInstrucciones[proximaInstruccion]))
                {
                    proximaInstruccion++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("IniciarTransmision: " + ex.Message));
            }

        }

        public void ContinuarTransmision()
        {
            try
            {
                //seguimos enviando
                if (proximaInstruccion < loteInstrucciones.Count)
                {
                    if (EnviarInstruccion(loteInstrucciones[proximaInstruccion]))
                    {
                        proximaInstruccion++;

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("ContinuarTransmision: " + ex.Message));
            }

        }

        private bool EnviarInstruccion(string comando)
        {
            try
            {
                //si la maquina esta WAITINGCOMMAND espera comando
                if (estadoActual == CNC_Estados.EsperandoComando)
                {
                    //enviamos el string del comando
                    enviar(comando);

                    this.Label.Text = "Comando enviado...esperando respuesta";

                    //esperamos la respuesta sobre el comando
                    string recep = recibir(1000);

                    if (recep == CNC_Mensajes_Recep.ComandoSoportado)
                    {
                        //establecemos el estado en transmision
                        this.transmision = true;
                        
                        //seteamos la maquina en PROCESSINGCOMMAND
                        estadoActual = CNC_Estados.ProcesandoComando;

                        this.Label.Text = "Procesando Comando...";

                        return true;
                    }
                    else
                    {
                        //validamos el mensaje
                        if (recep == CNC_Mensajes_Recep.ComandoNoSoportado)
                        {
                            this.Label.Text = "Error: Comando No Soportado";
                        }
                        else if (recep == CNC_Mensajes_Recep.ComandoErroneo)
                        {
                            this.Label.Text = "Error: Comando Erroneo";
                        }

                        return false;
                    }

                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw (new Exception("EnviarInstruccion: " + ex.Message));
            }
        }

        private void RespuestaInstruccion()
        {
            try
            {
                //si estamos en transmision
                if (transmision)
                {
                    //leemos la respuesta
                    string recep = recibir(0);

                    //vuelve a estado de WAITINGCOMMAND
                    estadoActual = CNC_Estados.EsperandoComando;

                    //salimos de estado de transmision
                    transmision = false;

                    if (recep == CNC_Mensajes_Recep.ParadaEmergencia)
                    {
                        this.Label.Text = "Se ha detenido la maquina manualmente";
                    }
                    if (recep == CNC_Mensajes_Recep.SensorFinCarrera)
                    {
                        this.Label.Text = "Se ha llegado al fin de un eje";
                    }

                    if (recep == CNC_Mensajes_Recep.ComandoEjecutado)
                    {
                        this.Label.Text = "Instruccion ejecutada OK";

                        //seguimos enviando las instrucciones del lote
                        ContinuarTransmision();
                    }
                }

            }
            catch (Exception ex)
            {
                throw (new Exception("EnviarInstruccion: " + ex.Message));
            }
        }
    }
}
