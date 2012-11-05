using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Timers;
using System.Windows.Forms;
using System.Threading;

namespace VirtualSerial
{
    public static class Port
    {
        public delegate void DataReceivedCallbackDelegate(string text);

        #region Private Properties
        private static SerialPort virtualPort = null;
        private static bool connected = false;
        private static DataReceivedCallbackDelegate callback;
        #endregion

        #region Getters & Setters
        public static SerialPort VirtualPort
        {
            get
            {
                if (virtualPort != null)// && virtualPort.IsOpen)
                {
                    return virtualPort;
                }
                else
                {
                    throw new Exception("La máquina no está conectada.");
                }
            }
            set { virtualPort = value; }
        }
        public static bool Connected
        {
            get { return connected; }
            set { connected = value; }
        }
        public static DataReceivedCallbackDelegate DataReceivedCallback
        {
            get { return callback; }
            set { callback = value; }
        }
        #endregion

        /// <summary>
        /// Limpia los buffers de IN/OUT y Cierra la conexion
        /// </summary>
        public static void CloseConnection()
        {
            try
            {
                VirtualPort.DiscardInBuffer();
                VirtualPort.DiscardOutBuffer();
            }
            catch {}
            finally
            {
                VirtualPort.Close();
                Connected = false;
            }
        }

        public static void Connect(string portName)
        {
            try
            {
                VirtualPort = new SerialPort(portName);
                virtualPort.Open(); // ojo que TIENE que usar la property directamente y no el metodo

                VirtualPort.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw ex;
            }            
        }

        private static void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                DataReceivedCallback(VirtualPort.ReadExisting());
            }
            catch(Exception ex)
            {
                CloseConnection();
                throw ex;
            }
        }

        /// <summary>
        /// Escribe en el puerto la cadena recibida, en caso de error cierra la conexion
        /// </summary>
        /// <param name="text"></param>
        public static void Write(string text)
        {
            try
            {
                VirtualPort.Write(text);
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw ex;
            }
        }
    }
}
