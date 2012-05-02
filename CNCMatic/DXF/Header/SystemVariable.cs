using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXF.Header
{
    /// <summary>
    /// Variables string del sistema
    /// </summary>
    public static class SystemVariable
    {
        /// <summary>
        /// La version de la database de AUTOCAD
        /// </summary>
        public const string DabaseVersion = "$ACADVER";

        /// <summary>
        /// Proximo handle disponible (esta variable debe estar presente en la seccion del header)
        /// </summary>
        public const string HandSeed = "$HANDSEED";
    }
}
