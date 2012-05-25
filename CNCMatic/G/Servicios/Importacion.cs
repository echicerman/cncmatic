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
        public void leeGfile(string path)
        {
            try
            {
                FileStream fs = File.OpenRead(path);
                
            }
            catch (Exception e)
            {
                throw new Exception (e.Message);
            }

            
        }
    }
}
