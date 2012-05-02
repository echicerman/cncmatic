using System;
using System.Collections.Generic;

namespace DXF.Header
{
    /// <summary>
    /// Define la version de DXF.
    /// </summary>
    internal class HeaderVariable
    {
        public const int NAME_CODE_GROUP = 9;
        public static readonly Dictionary<string, int> Allowed = InitializeSystemVariables();
        private readonly string nombre;
        private readonly int codeGroup;
        private readonly object valor;

        public HeaderVariable(string nombre, object valor)
        {
            if (!Allowed.ContainsKey(nombre))
                throw new ArgumentOutOfRangeException("nombre", nombre, "Nombre Variable " + nombre + " no definido.");
            this.codeGroup = Allowed[nombre];
            this.nombre = nombre;
            this.valor = valor;
        }

        public string Nombre
        {
            get { return this.nombre; }
        }

        public int CodeGroup
        {
            get { return this.codeGroup; }
        }

        public object Valor
        {
            get { return this.valor; }
        }

        public override string ToString()
        {
            return String.Format("{0} : {1}", this.nombre, this.valor);
        }

        private static Dictionary<string, int> InitializeSystemVariables()
        {
            return new Dictionary<string, int>
                       {
                           {SystemVariable.DabaseVersion, 1},
                           {SystemVariable.HandSeed, 5}
                       };
        }
    }
}
