using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace G.Servicios
{
    public class Importacion
    {
        /// <summary>
        /// Funcion que lee un archivo G plano
        /// </summary>
        /// <param name="path">Ruta absoluta de ubicacion del archivo G</param>
        public List<string> leeGfile(string path)
        {
            try
            {

                string linea = "";
                List<string> lineas = new List<string>();

                //FileStream fs = File.OpenRead(path);
                StreamReader sr = new StreamReader(path);
                while (!sr.EndOfStream)
                {
                    linea = sr.ReadLine();
                    if (ComandoValido(linea))
                        lineas.Add(linea);

                }
                sr.Close();
                return lineas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        /// <summary>
        /// Metodo que establece si la instruccion es valida o no
        /// </summary>
        /// <param name="comando">Instruccion en G leida a validar</param>
        /// <returns>true si el comando paso las validaciones, false en caso contrario</returns>
        private bool ComandoValido(string comando)
        {
            //que no sea linea en blanco
            if (comando == "")
                return false;

            //que solo inicie con g o m
            if (!comando.StartsWith("g", true, null)
                && !comando.StartsWith("m", true, null))
                return false;
                        
            //todo OK
            return true;
        }
    }
}
