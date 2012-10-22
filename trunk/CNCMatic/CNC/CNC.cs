using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Configuracion;
using VirtualSerial;
using CommandPreprocessor;

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
                    instancia.PosicionActual = new Position();
                    instancia.PosicionActual.X = 0;
                    instancia.PosicionActual.Y = 0;
                    instancia.PosicionActual.Z = 0;
                }
                return instancia;
            }
        }

        private XML_Config configuracion;
        public XML_Config Configuracion
        {
            get { return this.configuracion; }
            set { this.configuracion = value; }
        }

        private List<string> colaMensajes = new List<string>();

        private List<string> loteInstrucciones = new List<string>();
        private int proximaInstruccion;

        private List<string> loteInstruccionesTemp = new List<string>();
        private int proximaInstruccionTemp;

        public Position PosicionActual { get; set; }

        public System.Windows.Forms.ToolStripStatusLabel Label { get; set; }
        public System.Windows.Forms.ToolStripStatusLabel LblPosicionActual { get; set; }

        //variable que indica si en una transmision debe pausar el envio de comandos al cnc
        private bool pausarTransmision = false;

        //variable para guardar el primer movimiento libre a enviar luego de ingresar en FREEMOVES
        private string movimientoLibrePendiente;

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

        private bool EnviarConfiguracion()
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
                        GxP = this.configuracion.ConfigMatMot[i].GradosPaso;
                        TamV = this.configuracion.ConfigMatMot[i].TamVuelta;
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

                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.EnviarConfiguracion: " + ex.Message));
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
                throw (new Exception("CNC.PedirEstadoCNC: " + ex.Message));
            }

        }

        public bool EstablecerConexion()
        {
            try
            {
                this.Label.Text = "Estableciendo conexión con puerto USB...";

                //nos conectamos al puerto
                conectarSerialPort();

                this.Label.Text = "Conexión OK (1/3): enviando handshake...";
                estadoActual = CNC_Estados.SerialPortConectado;

                //enviar: CNC_Mensajes_Send.HandShake;
                enviar(CNC_Mensajes_Send.HandShake);

                return true;

            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.EstablecerConexion: " + ex.Message));
            }
        }

        private void conectarSerialPort()
        {
            try
            {
                //sino esta conectado en el puerto conectamos
                if (!Port.Conectado(this.PuertoConexion))
                {
                    Port.OpenConnection(this.PuertoConexion);
                    Port.DataReceivedCallback = new Port.DataReceivedCallbackDelegate(atenderPuerto);
                }
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

                //guardamos el ultimo mensaje enviado
                this.UltimoMensajeSend = texto;

                Port.Write(texto);
            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.enviar: " + ex.Message));
            }

        }

        private void atenderPuerto(string texto)
        {
            try
            {
                //agregamos el mensaje a la cola de mensajes
                colaMensajes.Add(texto.Trim());

                //********************************************************************
                //--------------------------MENSAJES----------------------------------
                //********************************************************************

                //si pedimos la posicion actual
                if (this.UltimoMensajeSend == CNC_Mensajes_Send.PosicionActual)
                {
                    string recep = recibir();

                    //actualizamos la posicion actual del CNC
                    actualizarPosicionActual(recep);

                    return;
                }

                //si pedimos ponernos en FREEMOVES
                if (this.UltimoMensajeSend == CNC_Mensajes_Send.MovimientoLibre)
                {
                    //leemos el mensaje
                    string recep = recibir();

                    if (recep == CNC_Mensajes_Recep.MovimientoLibreOK)
                    {
                        //ponemos la maquina en FREEMOVES
                        this.estadoActual = CNC_Estados.MovimientoLibre;

                        if (this.movimientoLibrePendiente != "")
                        {
                            //enviamos la ultima instruccion de movimiento libre
                            enviar(this.movimientoLibrePendiente);
                            this.movimientoLibrePendiente = "";
                        }
                    }

                    return;
                }

                //********************************************************************
                //--------------------------ESTADOS-----------------------------------
                //********************************************************************

                //FREEMOVES
                if (this.estadoActual == CNC_Estados.MovimientoLibre)
                {
                    //leemos el mensaje
                    string recep = recibir();

                    //abrimos el comando recibido, por ejemplo:
                    //"CNCSFM_X12 Y28 Z4"
                    string respuesta = recep.Split('_')[0];

                    if (respuesta == CNC_Mensajes_Recep.Stop)
                    {
                       string posicion = recep.Split('_')[1];

                        //actualizamos la posicion actual del CNC
                        actualizarPosicionActual(posicion);
                        
                        //vuelve a serial SERIALPORTCONNECTED
                        this.estadoActual = CNC_Estados.SerialPortConectado;
                    }

                    return;
                }

                //SERIALPORTCONNECTED
                if (this.estadoActual == CNC_Estados.SerialPortConectado)
                {
                    //pasamos la maquina a HANDSHAKERECEIVED
                    estadoActual = CNC_Estados.HandShakeRecibido;

                    string recep = recibir();

                    //si coincide longitud recibida con la enviada
                    if (CNC_Mensajes_Send.HandShake.Length == Convert.ToInt32(recep))
                    {
                        //enviar: CNC_Mensajes_Send.HandShakeOk 
                        enviar(CNC_Mensajes_Send.HandShakeOk);

                        estadoActual = CNC_Estados.Conectado;

                        //enviamos la configuracion
                        if (EnviarConfiguracion())
                        {
                            this.Label.Text = "Conexión OK (2/3): enviando configuración...";
                        }

                    }
                    else
                    {
                        //enviar: CNC_Mensajes_Send.HandShakeBad 
                        enviar(CNC_Mensajes_Send.HandShakeBad);

                        estadoActual = CNC_Estados.SerialPortConectado;

                        this.Label.Text = "Error en el handshake. Intente nuevamente.";

                    }

                    return;

                }

                //CNCMATICCONNECTED
                if (estadoActual == CNC_Estados.Conectado)
                {
                    //leemos la respuesta
                    string recep = recibir();

                    //vemos el estado de la respuesta
                    if (recep == CNC_Mensajes_Recep.ConfigStringOK)
                    {
                        estadoActual = CNC_Estados.Configurado;

                        this.Label.Text = "Conexión OK (3/3): finalizando conexión...";

                    }
                    else if (recep == CNC_Mensajes_Recep.ConfigStringBAD)
                    {
                        //seguimos en el mismo estado...
                        this.Label.Text = "Error en la configuracion. Intente nuevamente.";
                    }

                    return;
                }

                //CONFIGURED
                if (estadoActual == CNC_Estados.Configurado)
                {
                    //leemos la respuesta
                    string recep = recibir();

                    //si llego a la PO
                    if (recep == CNC_Mensajes_Recep.PosicionOrigen)
                    {
                        this.Label.Text = "Conexión establecida";

                        //queda esperando instrucciones
                        estadoActual = CNC_Estados.EsperandoComando;

                        //aca tendriamos que pedir la posicion del cero de la pieza??
                        
                        //aca actualizamos el cero de la pieza
                        PrepararCommandPreprocessor();

                        //iniciamos la transmision
                        this.Transmision();

                    }

                    return;
                }

                //WAITINGCOMMAND
                if (estadoActual == CNC_Estados.EsperandoComando)
                {
                    string recep = recibir();

                    if (recep == CNC_Mensajes_Recep.ComandoSoportado)
                    {
                        //establecemos el estado en transmision
                        //this.transmision = true;

                        //seteamos la maquina en PROCESSINGCOMMAND
                        estadoActual = CNC_Estados.ProcesandoComando;

                        this.Label.Text = "Procesando Comando...";

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

                    }

                    return;
                }

                //PROCESSINGCOMMAND
                if (estadoActual == CNC_Estados.ProcesandoComando)
                {
                    string recep = recibir();

                    //volvemos a esperar comando
                    estadoActual = CNC_Estados.EsperandoComando;

                    //abrimos el comando recibido, por ejemplo:
                    //"CMDDONE_X12 Y28 Z4"
                    string respuesta = recep.Split('_')[0];
                    string posicion = recep.Split('_')[1];

                    //actualizamos la posicion actual del CNC
                    actualizarPosicionActual(posicion);

                    if (respuesta == CNC_Mensajes_Recep.ComandoEjecutado && !this.pausarTransmision)
                    {
                        //continuamos la transmision
                        this.Transmision();

                    }
                    else if (respuesta == CNC_Mensajes_Recep.ParadaEmergencia)
                    {
                        //vemos que hacemos...
                        this.Label.Text = "Parada de Emergencia";
                    }
                    else if (respuesta == CNC_Mensajes_Recep.SensorFinCarrera)
                    {
                        //vemos que hacemos...
                        this.Label.Text = "Fin de Carrera alcanzado";
                    }

                    return;
                }

            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.atenderPuerto: " + ex.Message));
            }
        }

        private void actualizarPosicionActual(string posicionStr)
        {
            try
            {
                //obtenemos los pasos de cada eje
                int pasosX = Convert.ToInt32(posicionStr.Split(' ')[0].Remove(0, 1));
                int pasosY = Convert.ToInt32(posicionStr.Split(' ')[1].Remove(0, 1));
                int pasosZ = Convert.ToInt32(posicionStr.Split(' ')[2].Remove(0, 1));

                decimal GxP, TamV, Valor;

                //para cada eje

                for (int i = 0; i < 3; i++)
                {
                    GxP = this.configuracion.ConfigMatMot[i].GradosPaso;
                    TamV = this.configuracion.ConfigMatMot[i].TamVuelta;
                    Valor = 360 / (GxP * TamV);

                    switch (i)
                    {
                        case 0: this.PosicionActual.X = Convert.ToDouble(pasosX / Valor); break;
                        case 1: this.PosicionActual.Y = Convert.ToDouble(pasosY / Valor); break;
                        case 2: this.PosicionActual.Z = Convert.ToDouble(pasosZ / Valor); break;
                    }
                }

                //actualizamos la posicion actual en el formulario
                LblPosicionActual.Text = "X" + string.Format("{0:0.00}", this.PosicionActual.X) +
                                        " Y" + string.Format("{0:0.00}", this.PosicionActual.Y) +
                                        " Z" + string.Format("{0:0.00}", this.PosicionActual.Z);
            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.actualizarPosicionActual: " + ex.Message));
            }

        }

        public void CargaLoteInstrucciones(List<string> loteInstrucciones)
        {
            //lote global de instrucciones
            this.proximaInstruccion = 0;
            this.loteInstrucciones = loteInstrucciones;

            //lote temporal para cada preprocesamiento de las instrucciones globales
            this.loteInstruccionesTemp = new List<string>();
            this.proximaInstruccionTemp = 0;
        }

        private string recibir()
        {
            try
            {
                string mensaje = "";

                //si hay mensajes
                if (colaMensajes.Count > 0)
                {
                    //levantamos el primer mensaje de la cola
                    mensaje = colaMensajes[0];
                    colaMensajes.RemoveAt(0);
                }

                //guardamos el ultimo mensaje recibido
                this.UltimoMensajeRecep = mensaje;

                return mensaje;
            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.recibir: " + ex.Message));
            }
        }

        private bool Transmision()
        {
            try
            {
                //preparamos el siguiente comando temporal para enviar
                if (PrepararSiguienteComando())
                {
                    if (EnviarInstruccion(loteInstruccionesTemp[proximaInstruccionTemp]))
                    {
                        proximaInstruccionTemp++;
                        return true;
                    }
                    else
                    {
                        this.Label.Text = "ERROR ENVIO";
                        return false;
                    }
                }
                else
                {
                    this.Label.Text = "Fin del procesamiento";
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("Transmision: " + ex.Message));
            }

        }

        private void PrepararCommandPreprocessor()
        {
            try
            {
                //le pasamos al commandprocessor la posicion de referencia (0 de la pieza)
                CommandPreprocessor.CommandPreprocessor.GetInstance().ReferencePosition = this.PosicionActual;
            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.PrepararCommandPreprocessor: " + ex.Message));
            }
        }
        
        private bool PrepararSiguienteComando()
        {
            try
            {
                //seguimos mientras haya instrucciones globales
                if (loteInstrucciones.Count > proximaInstruccion)
                {
                    //si llegamos a enviar todas las temporales, procesamos el siguiente global
                    if (loteInstruccionesTemp.Count == proximaInstruccionTemp)
                    {
                        //tomamos el proximo comando global
                        string sgteComando = loteInstrucciones[proximaInstruccion];

                        //le pasamos al commandprocessor la posicion actual
                        CommandPreprocessor.CommandPreprocessor.GetInstance().CurrentPosition = this.PosicionActual;

                        //cargamos el lote de instrucciones temp
                        this.loteInstruccionesTemp = CommandPreprocessor.CommandPreprocessor.GetInstance().ProcessCommand(sgteComando);
                        this.proximaInstruccionTemp = 0;

                        //pasamos a la siguiente global
                        this.proximaInstruccion++;
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.PrepararSiguienteComando: " + ex.Message));
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

                    this.Label.Text = "Comando enviado. Esperando respuesta...";

                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.EnviarInstruccion: " + ex.Message));
            }
        }

        public void PausarTransmision()
        {
            this.pausarTransmision = true;

        }

        public void EnviarMovimientoLibre(string comando)
        {
            try
            {
                //si la maquina no esta en movimiento libre, nos ponemos en FREEMOVES
                if (this.estadoActual != CNC_Estados.MovimientoLibre)
                {
                    //guardamos el proximo movimiento para cuando recibamos el ok sobre el modo FREEMOVE
                    this.movimientoLibrePendiente = comando;

                    //iniciamos movimiento libre
                    IniciarMovimientoLibre();


                }
                else
                {
                    //como ya estamos en FREEMOVES enviamos directamente
                    enviar(comando);
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.EnviarMovimientoLibre: " + ex.Message));
            }


        }

        public void DetenerMovimientoLibre()
        {
            try
            {
                //si la maquina esta en movimiento libre enviamos el STOP
                if (this.estadoActual == CNC_Estados.MovimientoLibre)
                {
                    //como ya estamos en FREEMOVES enviamos directamente
                    enviar(CNC_Mensajes_Send.Stop);
                }

            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.DetenerMovimientoLibre: " + ex.Message));
            }


        }

        private void IniciarMovimientoLibre()
        {
            try
            {
                //nos conectamos al puerto
                conectarSerialPort();

                //enviamos la peticion para ponernos en movimiento libre
                enviar(CNC_Mensajes_Send.MovimientoLibre);
            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.EnviarMovimientoLibre: " + ex.Message));
            }
        }
    }
}
