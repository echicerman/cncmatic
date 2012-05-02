using System;

namespace DXF.Objetos
{
    /// <summary>
    /// Clase de base de todos los objetos dxf
    /// </summary>
    public class DxfObjeto
    {
        #region propiedades privadas

        private readonly string codNombre;
        private string handle;

        #endregion

        #region constructores

        /// <summary>
        /// Inicializa una nueva instancia de la clase <c>DxfObjeto</c>
        /// </summary>
        public DxfObjeto(string codNombre)
        {
            this.codNombre = codNombre;
        }

        #endregion

        #region propiedades publicas

        /// <summary>
        /// Obtiene el tipo string de la entidad dxf
        /// </summary>
        public string CodNombre
        {
            get { return this.codNombre; }
        }

        /// <summary>
        /// Obtiene o establece el handle del objeto dxf
        /// </summary>
        public string Handle
        {
            get { return this.handle; }
            internal set { this.handle = value; }
        }

        #endregion

        #region metodos publicos

        /// <summary>
        /// Asigna un handle al objeto basado en un contador int
        /// </summary>
        /// <param name="entidadNro">Numero a asignar</param>
        /// <returns>Proximo numero de entidad disponible</returns>
        /// <remarks>
        /// Algunos objetos pueden consumir mas de uno, x ej, el caso de las multipleslineas que asignan
        /// un handle a sus vertices.
        /// </remarks>
        internal virtual int AsignarHandle(int entidadNro)
        {
            this.handle = Convert.ToString(entidadNro, 16);
            return entidadNro + 1;
        }

        #endregion
    }
}
