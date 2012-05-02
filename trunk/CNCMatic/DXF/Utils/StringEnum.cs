using System;
using System.Collections;
using System.Reflection ;

namespace DXF.Utils
{
    #region Class StringEnum

    /// <summary>
    /// Clase de ayuda para trabajar con enums 'extendidos' usando <see cref="StringValueAttribute"/> atributos.
    /// </summary>
    public class StringEnum
    {
        #region Implementacion instancia

        private readonly Type enumTipo;
        private static readonly Hashtable stringValues = new Hashtable();

        /// <summary>
        /// Crea una nueva instancia <see cref="StringEnum"/> .
        /// </summary>
        /// <param name="enumTipo">Tipo Enum.</param>
        public StringEnum(Type enumTipo)
        {
            if (!enumTipo.IsEnum)
                throw new ArgumentException(String.Format("Supplied type must be an Enum.  Type was {0}", enumTipo));
            this.enumTipo = enumTipo;
        }

        /// <summary>
        /// Obtiene el string asociado con el valor del enum dado.
        /// </summary>
        /// <param name="valueName">Nombre del valor del enum</param>
        /// <returns>Valor String</returns>
        public string GetStringValue(string valueName)
        {
            string stringValue;
            try
            {
                Enum tipo = (Enum)Enum.Parse(this.enumTipo, valueName);
                stringValue = GetStringValue(tipo);
            }
            catch
            {
                return null;
            } 

            return stringValue;
        }

        /// <summary>
        /// Obtiene el string asociado con el valor del enum dado.
        /// </summary>
        /// <returns>Valor Array String</returns>
        public Array GetStringValues()
        {
            ArrayList valores = new ArrayList();
            
            //Buscar el valor string asociado con los campos de este enum
            foreach (FieldInfo fi in this.enumTipo.GetFields())
            {
                //Buscar el valor del enum
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
                if (attrs != null)
                    if (attrs.Length > 0)
                        valores.Add(attrs[0].Valor );
            }

            return valores.ToArray();
        }

        /// <summary>
        /// Obtiene los valores como una lista
        /// </summary>
        /// <returns>IList</returns>
        public IList GetListValues()
        {
            Type underlyingType = Enum.GetUnderlyingType(this.enumTipo);
            ArrayList values = new ArrayList();
            //Buscar el valor string asociado con los campos de este enum
            foreach (FieldInfo fi in this.enumTipo.GetFields())
            {
                //Buscar el valor del enum
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
                if (attrs != null)
                    if (attrs.Length > 0)
                        values.Add(new DictionaryEntry(Convert.ChangeType(Enum.Parse(this.enumTipo, fi.Name), underlyingType), attrs[0].Valor));
            }

            return values;
        }

        /// <summary>
        /// Retorna la existencia del valor en el enum
        /// </summary>
        /// <param name="stringValue">Valor String.</param>
        /// <returns>Existencia del valor string</returns>
        public bool IsStringDefined(string stringValue)
        {
            return Parse(this.enumTipo, stringValue) != null;
        }

        /// <summary>
        /// Retorna la existencia del valor en el enum
        /// </summary>
        /// <param name="stringValue">Valor String.</param>
        /// <param name="ignoreCase">Establece si buscar con mayusculas o no</param>
        /// <returns>Existencia del valor string</returns>
        public bool IsStringDefined(string stringValue, bool ignoreCase)
        {
            return Parse(this.enumTipo, stringValue, ignoreCase) != null;
        }

        /// <summary>
        /// Obtiene el tipo del enum
        /// </summary>
        /// <value></value>
        public Type EnumTipo
        {
            get { return this.EnumTipo; }
        }

        #endregion

        #region Implementacion estatica

        /// <summary>
        /// Obtiene un valor string para un enum
        /// </summary>
        /// <param name="valor">Valor.</param>
        /// <returns>Valor String asociado via a <see cref="StringValueAttribute"/> atributo, o null si no se encuentra.</returns>
        public static string GetStringValue(Enum valor)
        {
            string salida = null;
            Type tipo = valor.GetType();

            if (stringValues.ContainsKey(valor))
                salida = ((StringValueAttribute)stringValues[valor]).Valor;
            else
            {
                //Buscar por un 'StringValueAttribute' en los atributos propios
                FieldInfo fi = tipo.GetField(valor.ToString());
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
                if (attrs != null)
                    if (attrs.Length > 0)
                    {
                        stringValues.Add(valor, attrs[0]);
                        salida = attrs[0].Valor;
                    }
            }
            return salida;
        }

        /// <summary>
        /// Parsea el enum proporcionado y un string para buscar un valor enum asociado (case sensitive).
        /// </summary>
        /// <param name="tipo">Tipo.</param>
        /// <param name="stringValue">Valor String</param>
        /// <returns>Valor enum asociado con el string, o null si no es encontrado</returns>
        public static object Parse(Type type, string stringValue)
        {
            return Parse(type, stringValue, false);
        }

        /// <summary>
        /// Parsea el enum proporcionado y un string para buscar un valor enum asociado.
        /// </summary>
        /// <param name="tipo">Tipo.</param>
        /// <param name="stringValue">Valor String</param>
        /// <param name="ignoreCase">Establece si buscar con mayusculas o no</param>
        /// <returns>Valor enum asociado con el string, o null si no es encontrado</returns>
        public static object Parse(Type tipo, string stringValue, bool ignoreCase)
        {
            object salida = null;
            string enumStringValue = null;

            if (!tipo.IsEnum)
                throw new ArgumentException(String.Format("Supplied type must be an Enum.  Type was {0}", tipo));

            //Buscamos el string en el enum
            foreach (FieldInfo fi in tipo.GetFields())
            {
                //Buscamos en nuestros atributos
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
                if (attrs != null)
                    if (attrs.Length > 0)
                        enumStringValue = attrs[0].Valor;

                //Comprobamos la igualdad
                if (string.Compare(enumStringValue, stringValue, ignoreCase) == 0)
                {
                    salida = Enum.Parse(tipo, fi.Name);
                    break;
                }
            }

            return salida;
        }

        /// <summary>
        /// Retorna la existencia del valor string en el enum
        /// </summary>
        /// <param name="stringValue">valor string</param>
        /// <param name="enumTipo">Tipo de enum</param>
        /// <returns>Si el valor string existe</returns>
        public static bool IsStringDefined(Type enumTipo, string stringValue)
        {
            return Parse(enumTipo, stringValue) != null;
        }

        /// <summary>
        /// Retorna la existencia del valor string en el enum
        /// </summary>
        /// <param name="stringValue">valor string</param>
        /// <param name="enumTipo">Tipo de enum</param>
        /// <param name="ignoreCase">Establece si buscar con mayusculas o no</param>
        /// <returns>Si el valor string existe</returns>
        public static bool IsStringDefined(Type enumTipo, string stringValue, bool ignoreCase)
        {
            return Parse(enumTipo, stringValue, ignoreCase) != null;
        }

        #endregion
    }

    #endregion

    #region Clase StringValueAttribute

    /// <summary>
    /// Clase para almacenar valores string
    /// </summary>
    public class StringValueAttribute : Attribute
    {
        private readonly string valor;

        /// <summary>
        /// Crea una nueva <see cref="StringValueAttribute"/> instancia.
        /// </summary>
        /// <param name="valor">Valor.</param>
        public StringValueAttribute(string valor)
        {
            this.valor = valor;
        }

        /// <summary>
        /// Obtiene el valor
        /// </summary>
        /// <value>string</value>
        public string Valor
        {
            get { return this.valor; }
        }
    }

    #endregion
}
