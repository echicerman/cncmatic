using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Configuracion;
using VirtualSerial;
using CommandPreprocessor;
using SafeControls;

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
        //se dirige a la "Posicion de Origen", retorna el mensaje
        //y pasa a esperar la configuracion
        public static string Conectado = "CNCMATICCONNECTED";

        //READYTOCONFIGURE: espera los parametros de configuracion
        public static string EsperandoConfig = "READYTOCONFIGURE";

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

        //EMERGENCYSTOP: estado interno del CNC
        public static string ParadaEmergencia = "EMERGENCYSTOP";

        //LIMITSENSOR: estado interno del CNC
        public static string SensorFinCarrera = "LIMITSENSOR";
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
        public static string MovimientoLibre = "FM:";

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

        //Pausa: un M00
        public static string G_Pausa = "M00";

        //Stop: un M02
        public static string G_Stop = "M02";

        //PosicionDeOrigen, se dirige al 0,0,0 (absoluto) de la maquina
        public static string PosicionDeOrigen = "origin";
    }

    /// <summary>
    /// Conjunto de mensajes utilizados para la comunicación desde el CNC al CNCMatic
    /// </summary>
    public struct CNC_Mensajes_Recep
    {
        //(-(-(-(HANDSHAKERECEIVED)-)-)-)
        //Maquina conectada, pasa a CNCMATICCONNECTED
        public static string MaquinaConectada = "MC";
        //Maquina no conectada, vuelve a SERIALPORTCONNECTED
        public static string MaquinaNoConectada = "ERR:MNC";


        //(-(-(-(CNCMATICCONNECTED)-)-)-)
        //Configuracion OK
        public static string ConfigStringOK = "CFGOK";
        //Configuracion BAD
        public static string ConfigStringBAD = "ERR:CFGE";

        //(-(-(-(CONFIGURED)-)-)-)
        //Posicion Origen
        public static string PosicionOrigen = "PO";

        //(-(-(-(WAITINGCOMMAND)-)-)-)
        //Comando Soportado
        public static string ComandoSoportado = "CMDS";
        //Comando No Soportado
        public static string ComandoNoSoportado = "ERR:CMDNS";
        //Comando Erroneo
        public static string ComandoErroneo = "ERR:CMDE";

        //(-(-(-(PROCESINGCOMMAND)-)-)-)
        //Comando terminado OK
        public static string ComandoEjecutado = "CMDDONE";
        //Comando terminado por un fin de de carrera en un eje
        public static string SensorFinCarrera = "ERR:SFC"; //<<-- tambien aplica para cuando la maquina esta en FREEMOVES
        //Comando terminado por una parada de emergencia por hardware
        public static string ParadaEmergencia = "ERR:PE";

        //(-(-(-(FREEMOVES)-)-)-)
        //FREEMOVE: responde OK al cambio de estado
        public static string MovimientoLibreOK = "CNCFM";
        //Error Freemoves: responde error si recibe un sentido de avance/retroceso invalido
        public static string MovimientoLibreMAL = "ERR:CNCFM";

        //(-(-(-(MENSAJES VARIOS)-)-)-)-)
        //RESET: cuando termine de procesar el comando actual pone en estado 
        //de SERIALPORTCONNECTED y responde
        public static string Reset = "CNCR";

        //STOP: cuando termine de procesar el comando actual pone en estado 
        //de SERIALPORTCONNECTED y responde
        public static string Stop = "CNCSFM";

        //STATUS: cuando termine de procesar el comando actual devuelve el id 
        //del status actual 
        public static string Status = "CNCS";

        //PP: si recibe freemoves o origin y la maquina esta pausada (luego de un M00) responde PP
        public static string PPausado = "ERR:PP";
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
            set
            {
                this.configuracion = value;

                //actualizamos la configuracion del commandpreproccesor
                ConfigurarCommandPreprocessor();
            }
        }

        private List<string> colaMensajes = new List<string>();

        private List<string> loteInstrucciones = new List<string>();
        private int proximaInstruccion;

        private List<string> loteInstruccionesTemp = new List<string>();
        private int proximaInstruccionTemp;

        public Position PosicionActual { get; set; }

        public SafeControls.SafeToolStripStatusLabel Label { get; set; }
        public SafeControls.SafeToolStripStatusLabel LblPosicionActual { get; set; }
        public SafeControls.SafeToolStripProgressBar BarraProgreso { get; set; }

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

        //esta propiedad establece si la maquina recibio configuracion
        private bool configurada = false;

        private bool EnviarConfiguracion()
        {
            try
            {
                //si la maquina esta READYTOCOFIGURE espera la configuracion
                if (estadoActual == CNC_Estados.EsperandoConfig)
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

        //private void PedirEstadoCNC()
        //{
        //    try
        //    {
        //        //conectamos y pedimos estatus
        //        //CNC_Mensajes_Send.Status;
        //        string recep = "";

        //        switch (Convert.ToInt32(recep.Trim()))
        //        {
        //            case 0: estadoActual = CNC_Estados.SerialPortConectado; break; //SERIALPORTCONNECTED
        //            case 1: estadoActual = CNC_Estados.HandShakeRecibido; break; //HANDSHAKERECEIVED
        //            case 2: estadoActual = CNC_Estados.Conectado; break; //CNCMATICCONNECTED
        //            case 3: estadoActual = CNC_Estados.Configurado; break; //CONFIGURED
        //            case 4: estadoActual = CNC_Estados.EsperandoComando; break; //WAITINGCOMMAND
        //            case 5: estadoActual = CNC_Estados.ProcesandoComando; break; //PROCESSINGCOMMAND
        //            case 8: estadoActual = CNC_Estados.MovimientoLibre; break; //FREEMOVES

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (new Exception("CNC.PedirEstadoCNC: " + ex.Message));
        //    }

        //}

        public bool EstablecerConexion()
        {
            try
            {
                this.Label.Text = "Estableciendo conexión con puerto USB...";

                //nos conectamos al puerto
                conectarSerialPort();

                //si la maquina quedo en espera, enviamos el reset para inicializar la secuencia
                if (this.estadoActual == CNC_Estados.EsperandoComando)
                {
                    enviar(CNC_Mensajes_Send.Reset);
                }
                else
                {
                    this.Label.Text = "Conexión OK (1/3): enviando handshake...";
                    this.BarraProgreso.Value = 25;

                    this.estadoActual = CNC_Estados.SerialPortConectado;

                    //enviar: CNC_Mensajes_Send.HandShake;
                    enviar(CNC_Mensajes_Send.HandShake);
                }
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
                    Port.Label = this.Label;
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

                //si pedimos ir al inicio absoluto
                if (this.UltimoMensajeSend == CNC_Mensajes_Send.PosicionDeOrigen)
                {
                    string recep = recibir();

                    if (recep == CNC_Mensajes_Recep.PosicionOrigen)
                    {
                        actualizarPosicionActual("X0 Y0 Z0");
                    }

                    if (recep == CNC_Mensajes_Recep.PPausado)
                    {
                        //no puede irse al origen porque la maquina esta pausada
                        this.Label.Text = "Error: maquina en estado de movimiento libre";
                    }

                    return;
                }

                //si pedimos el estado actual
                if (this.UltimoMensajeSend == CNC_Mensajes_Send.Status)
                {
                    string recep = recibir();

                    //abrimos el comando recibido, por ejemplo:
                    //"CNCS:4"
                    string respuesta = recep.Split(':')[0];
                    int numero = Convert.ToInt32(recep.Split(':')[1]);

                    switch (numero)
                    {
                        case 0: this.estadoActual = CNC_Estados.SerialPortConectado; break;
                        case 1: this.estadoActual = CNC_Estados.HandShakeRecibido; break;
                        case 2: this.estadoActual = CNC_Estados.Conectado; break;
                        case 3: this.estadoActual = CNC_Estados.EsperandoConfig; break;
                        case 4: this.estadoActual = CNC_Estados.EsperandoComando; break;
                        case 5: this.estadoActual = CNC_Estados.ProcesandoComando; break;
                        case 6: this.estadoActual = CNC_Estados.SensorFinCarrera; break;
                        case 7: this.estadoActual = CNC_Estados.ParadaEmergencia; break;
                        case 8: this.estadoActual = CNC_Estados.MovimientoLibre; break;
                    }

                    return;
                }

                //si pedimos ponernos en FREEMOVES "FM:+Z"
                if (this.UltimoMensajeSend.StartsWith(CNC_Mensajes_Send.MovimientoLibre))
                {
                    //leemos el mensaje
                    string recep = recibir();

                    if (recep == CNC_Mensajes_Recep.MovimientoLibreOK)
                    {
                        //ponemos la maquina en FREEMOVES
                        this.estadoActual = CNC_Estados.MovimientoLibre;
                    }

                    //no se reconocio el sentido de avance/retroceso
                    if (recep == CNC_Mensajes_Recep.MovimientoLibreMAL)
                    {
                        //mantiene el estado?
                        if (this.UltimoMensajeSend.Split(':').Count() == 2)
                        {
                            this.Label.Text = "Error: " + this.UltimoMensajeSend.Split(':')[1] + " no es un sentido válido";
                        }
                        else
                        {
                            this.Label.Text = "Error: sentido informado no válido";
                        }
                    }

                    if (recep == CNC_Mensajes_Recep.PPausado)
                    {
                        //no puede ponerse en freemoves porque la maquina esta pausada
                        this.Label.Text = "Error: maquina en estado de movimiento libre";
                    }

                    return;
                }

                //si pedimos hacer un RESET
                if (this.UltimoMensajeSend == CNC_Mensajes_Send.Reset)
                {
                    //leemos respuesta
                    string recep = recibir();

                    //si acepto el reset
                    if (recep == CNC_Mensajes_Recep.Reset)
                    {
                        this.configurada = false;

                        this.Label.Text = "CNC restarted";
                        estadoActual = CNC_Estados.SerialPortConectado;
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

                        //queda en estado READYTOCONFIGURE
                        this.estadoActual = CNC_Estados.EsperandoConfig;
                    }

                    //se llego al fin de carreras
                    if (respuesta == CNC_Mensajes_Recep.SensorFinCarrera)
                    {
                        //vemos que hacemos...
                        this.Label.Text = "Fin de Carrera alcanzado";

                        //queda en estado READYTOCONFIGURE
                        this.estadoActual = CNC_Estados.EsperandoConfig;
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
                    }
                    else
                    {
                        //enviar: CNC_Mensajes_Send.HandShakeBad 
                        enviar(CNC_Mensajes_Send.HandShakeBad);

                        estadoActual = CNC_Estados.SerialPortConectado;

                        this.Label.Text = "Conexión (1/3): error en el handshake. Intente nuevamente.";
                    }

                    return;

                }

                //HANDSHAKERECEIVED
                if (this.estadoActual == CNC_Estados.HandShakeRecibido)
                {
                    //leemos
                    string recep = recibir();

                    //si recibio correcto el ok, la maquina se conecta
                    if (recep == CNC_Mensajes_Recep.MaquinaConectada)
                    {
                        estadoActual = CNC_Estados.Conectado;

                        this.Label.Text = "Conexión OK (1/3): handshake recibido ok...";

                        this.BarraProgreso.Value = 50;
                    }
                    else if (recep == CNC_Mensajes_Recep.MaquinaNoConectada)
                    {
                        estadoActual = CNC_Estados.SerialPortConectado;

                        this.Label.Text = "Conexión (1/3): error en el handshake. Intente nuevamente.";
                    }

                    return;
                }

                //CNCMATICCONNECTED
                if (estadoActual == CNC_Estados.Conectado)
                {
                    //leemos la respuesta
                    string recep = recibir();

                    //si llego a la PO
                    if (recep == CNC_Mensajes_Recep.PosicionOrigen)
                    {
                        //nos paramos en el origen
                        actualizarPosicionActual("X0 Y0 Z0");

                        //pasamos a READYTOCONFIGURE
                        this.estadoActual = CNC_Estados.EsperandoConfig;

                        //enviamos la configuracion
                        if (EnviarConfiguracion())
                        {
                            this.Label.Text = "Conexión OK (2/3): enviando configuración...";

                            this.BarraProgreso.Value = 75;
                        }
                        else
                        {
                            this.Label.Text = "Conexión (2/3): error enviando configuración. Intente nuevamente.";
                        }
                    }


                    return;
                }

                //READYTOCONFIGURE
                if (estadoActual == CNC_Estados.EsperandoConfig)
                {
                    //leemos la respuesta
                    string recep = recibir();

                    //vemos el estado de la respuesta
                    if (recep == CNC_Mensajes_Recep.ConfigStringOK)
                    {
                        this.configurada = true;

                        estadoActual = CNC_Estados.EsperandoComando;

                        this.Label.Text = "Conexión OK (3/3): Conexión establecida";

                        this.BarraProgreso.Value = 100;

                    }
                    else if (recep == CNC_Mensajes_Recep.ConfigStringBAD)
                    {
                        //seguimos en el mismo estado...
                        this.Label.Text = "Conexión (3/3): error en la configuracion. Intente nuevamente.";
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

                        if (this.UltimoMensajeSend == CNC_Mensajes_Send.G_Pausa)
                            this.Label.Text = "Pausando Transmisión...";
                        else if (this.UltimoMensajeSend == CNC_Mensajes_Send.G_Stop)
                            this.Label.Text = "Deteniendo Ejecución...";
                        else
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


                    //abrimos el comando recibido, por ejemplo:
                    //"CMDDONE_X12 Y28 Z4"
                    string respuesta = recep.Split('_')[0];

                    if (respuesta == CNC_Mensajes_Recep.ComandoEjecutado)
                    {
                        string posicion = recep.Split('_')[1];

                        //actualizamos la posicion actual del CNC
                        actualizarPosicionActual(posicion);

                        //volvemos a esperar comando
                        estadoActual = CNC_Estados.EsperandoComando;

                        if (this.UltimoMensajeSend == CNC_Mensajes_Send.G_Pausa)
                            this.Label.Text = "Transmision Pausada";

                        //caso especial al recibir un M02
                        if (this.UltimoMensajeSend == CNC_Mensajes_Send.G_Stop)
                        {
                            this.Label.Text = "Ejecución Detenida";

                            //queda en CNCMATICCONNECTED
                            this.estadoActual = CNC_Estados.Conectado;

                            //no continuamos con la ejecucion...
                            return;
                        }

                        if (!this.pausarTransmision)
                            //continuamos la transmision
                            this.Transmision();
                    }
                    else if (respuesta == CNC_Mensajes_Recep.ParadaEmergencia)
                    {
                        string posicion = recep.Split('_')[1];

                        //actualizamos la posicion actual del CNC
                        actualizarPosicionActual(posicion);

                        //volvemos a esperar comando
                        estadoActual = CNC_Estados.EsperandoComando;

                        //vemos que hacemos...
                        this.Label.Text = "Parada de Emergencia";
                    }
                    else if (respuesta == CNC_Mensajes_Recep.SensorFinCarrera)
                    {
                        string posicion = recep.Split('_')[1];

                        //actualizamos la posicion actual del CNC
                        actualizarPosicionActual(posicion);

                        //volvemos a esperar comando
                        estadoActual = CNC_Estados.EsperandoComando;

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

            ////propiedades de la barra
            //this.BarraProgreso.Maximum = this.loteInstrucciones.Count;
            //this.BarraProgreso.Minimum = 0;
            //this.BarraProgreso.Step = 1;
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

        public bool IniciarTransmision()
        {
            try
            {
                this.Label.Text = "Iniciando transmisión de movimientos...";

                //aca actualizamos el cero de la pieza, con la posicion actual
                PrepararCommandPreprocessor();

                //iniciamos la transmision
                this.Transmision();

                return true;
            }
            catch (Exception ex)
            {
                throw (new Exception("IniciarTransmision: " + ex.Message));
            }
        }

        public bool ReanudarTransmision()
        {
            try
            {
                this.Label.Text = "Reiniciando transmisión de movimientos...";

                this.pausarTransmision = false;

                //iniciamos la transmision
                this.Transmision();

                return true;
            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.ReanudarTransmision: " + ex.Message));
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
                ConfigurarCommandPreprocessor();

                //le pasamos al commandprocessor la posicion de referencia (0 de la pieza)
                CommandPreprocessor.CommandPreprocessor.GetInstance().ReferencePosition = this.PosicionActual;
            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.PrepararCommandPreprocessor: " + ex.Message));
            }
        }

        private void ConfigurarCommandPreprocessor()
        {

            try
            {
                //le pasamos la configuracion necesaria
                CommandPreprocessor.Configuration.absoluteProgamming = (this.configuracion.TipoProg == "abs");
                CommandPreprocessor.Configuration.millimetersProgramming = (this.configuracion.UnidadMedida == "mm");
                CommandPreprocessor.Configuration.defaultFeedrate = Convert.ToDouble(this.configuracion.VelocidadMovimiento);
                CommandPreprocessor.Configuration.millimetersCurveSection = Convert.ToDouble(this.configuracion.LargoSeccion);
            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.ConfigurarCommandPreprocessor: " + ex.Message));
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
                        //actualizamos la barra
                        this.BarraProgreso.Value = (100 / loteInstrucciones.Count) * (this.proximaInstruccion + 1);
                        //this.BarraProgreso.Value = 33;
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
                else if (loteInstrucciones.Count == proximaInstruccion)
                {
                    if (loteInstruccionesTemp.Count == proximaInstruccionTemp)
                        return false;
                    else return true;
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
            try
            {
                //enviamos la pausa M00
                enviar(CNC_Mensajes_Send.G_Pausa);

                //establecemos en pausa
                this.pausarTransmision = true;
            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.PausarTransmision: " + ex.Message));
            }
        }

        public void Detener()
        {
            try
            {

                if (
                    this.estadoActual == CNC_Estados.EsperandoComando ||
                    this.estadoActual == CNC_Estados.ProcesandoComando
                  )
                {
                    //enviamos M02 mientras este en ejecucion
                    enviar(CNC_Mensajes_Send.G_Stop);
                }
                else
                {
                    this.estadoActual = CNC_Estados.SerialPortConectado;
                }

            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.Detener: " + ex.Message));
            }
        }

        public void Reiniciar()
        {
            try
            {
                //enviamos reset
                enviar(CNC_Mensajes_Send.Reset);
            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.Reiniciar: " + ex.Message));
            }
        }

        public void Desconectar()
        {
            try
            {
                this.desconectarSerialPort();

            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.Desconectar: " + ex.Message));
            }
        }

        public void IrAlInicio()
        {
            try
            {
                //nos conectamos al puerto
                conectarSerialPort();

                //enviamos la peticion para ir al origen
                enviar(CNC_Mensajes_Send.PosicionDeOrigen);

            }
            catch (Exception ex)
            {
                throw (new Exception("CNC.PausarTransmision: " + ex.Message));
            }

        }

        public void EnviarMovimientoLibre(string comando)
        {
            try
            {
                //nos conectamos al puerto
                conectarSerialPort();

                //MovimientoLibre = "FM:"
                //comando = "+Z"
                enviar(CNC_Mensajes_Send.MovimientoLibre + comando);

                //si la maquina no esta en movimiento libre, nos ponemos en FREEMOVES
                //if (this.estadoActual != CNC_Estados.MovimientoLibre)
                //{
                //guardamos el proximo movimiento para cuando recibamos el ok sobre el modo FREEMOVE
                //this.movimientoLibrePendiente = comando;

                //iniciamos movimiento libre
                //IniciarMovimientoLibre();


                //}
                //else
                //{
                //    //como ya estamos en FREEMOVES enviamos directamente
                //    enviar(comando);
                //}
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
                //if (this.estadoActual == CNC_Estados.MovimientoLibre)
                //{
                //como ya estamos en FREEMOVES enviamos directamente
                enviar(CNC_Mensajes_Send.Stop);
                //}

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
