namespace G.Objetos
{
    /// <summary>
    /// Clase estatica que contiene los codigos ISO de los movimientos
    /// de fresado
    /// </summary>
    public static class MovesCodes
    {
        /// <summary>
        /// G00-Avance Rapido
        /// </summary>
        public static string avance = "G00";
        /// <summary>
        /// G01-Movimiento Lineal
        /// </summary>
        public static string lineal = "G01";

        /// <summary>
        /// G02-Circulo/helice en sentido horario
        /// </summary>
        public static string circuloHorario = "G02";

        /// <summary>
        /// G03-Circulo/helice en sentido antihorario
        /// </summary>
        public static string circuloAntihorario = "G03";

        /// <summary>
        /// G04-Tiempo de espera en seg o vueltas de cabezal
        /// </summary>
        public static string tiempoEspera = "G04";
        
        /// <summary>
        /// G17-Seleccion de plano XY
        /// </summary>
        public static string planoXY = "G17";

        /// <summary>
        /// G18-Seleccion de plano XY
        /// </summary>
        public static string planoZX = "G18";

        /// <summary>
        /// G19-Seleccion de plano XY
        /// </summary>
        public static string planoYZ = "G19";

        /// <summary>
        /// M00-Parada del programa
        /// </summary>
        public static string parada = "M00";

        /// <summary>
        /// M02-Fin del programa
        /// </summary>
        public static string fin = "M02";

    }
}
