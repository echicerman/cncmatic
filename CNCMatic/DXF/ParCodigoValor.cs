namespace DXF
{
    /// <summary>
    /// Representa el minimo elemento de informacion en un archivo dxf
    /// </summary>
    internal struct ParCodigoValor
    {
        private readonly int cod;
        private readonly string val;

        /// <summary>
        /// Inicializar una nueva instancia de la clase <c>ParCodigoValor</c>.
        /// </summary>
        /// <param name="cod">Codigo DXF</param>
        /// <param name="value">Valor para el codigo.</param>
        public ParCodigoValor(int cod, string val)
        {
            this.cod = cod;
            this.val = val;
        }

        /// <summary>
        /// Obtiene el codigo DXF
        /// </summary>
        public int Cod
        {
            get { return this.cod; }
        }

        /// <summary>
        /// Obtiene el valor
        /// </summary>
        public string Val
        {
            get { return this.val; }
        }
    }
}
