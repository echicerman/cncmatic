using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNC
{
    public struct CNC_Estados
    {
        //SERIALPORTCONNECTED: estado inicial, ni bien se enchufa la maquina
        //se envia cualquier cadena, la maquina cuenta los chars y devuelve ese numero
        public static string SerialPortConectado = "SERIALPORTCONNECTED";

        //HANDSHAKEACKRECEIVED: si recibe "ok", pasa al siguiente estado
        //caso contrario vuelve a SERIALPORTCONNECTED
        public static string HandShakeRecibido = "HANDSHAKEACKRECEIVED";

        //CNCMATICCONNECTED: 
        public static string Conectado = "CNCMATICCONNECTED";

        //CONFIGURED: no espera nada, solo mueve la punta al origen y pone la maquina
        //en ORIGINPOSITION
        public static string Configurado = "CONFIGURED";

        //ORIGINPOSITION: envia la cadena "Posicion de Origen" y se pone
        //en WAITINGCOMMAND
        public static string PosicionDeOrigen = "ORIGINPOSITION";

        //WAITINGCOMMAND: se espera comando que debe iniciar con G o M,
        //sino envía "Error en Comando", sino si esta OK puede enviar "Comando Soportado"
        //o "Comando No Soportado". Si se soporta el comando se pone en PROCESSINGCOMMAND
        //sino se queda en WAITINGCOMMAND
        public static string EsperandoComando = "WAITINGCOMMAND";

        //PROCESSINGCOMMAND: la maquina se esta moviendo, al finalizar vuelve
        //a WAITINGCOMMAND, puede terminar bien: "Comando Ejecutado" o mal
        //"Sensor Fin de Carrera"
        public static string ProcesandoComando = "PROCESSINGCOMMAND";

        //LIMITSENSOR: estado interno del pic, se activa al llegar al limite de un eje
        public static string SensorLimite = "LIMITSENSOR";
        
        //RESET: cuando termine de procesar el comando actual pone en estado 
        //de SERIALPORTCONNECTED
        public static string Reset = "RESET";
    }
    
    public class CNC
    {
        public string EstadoActual { get; set; }
        public string UltimaInstruccion { get; set; }
    }
}
