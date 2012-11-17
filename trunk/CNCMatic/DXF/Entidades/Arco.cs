using System;
using System.Collections.Generic;
using DXF.Objetos;
using DXF.Utils;
using Configuracion;
//using DXF.Tables;

namespace DXF.Entidades
{
    /// <summary>
    /// Representa un arco circular <see cref="DXF.Entidades.IEntidadObjeto"></see>
    /// </summary>
    public class Arco :
        DxfObjeto,
        IEntidadObjeto
    {
        #region propiedades privadas

        private const EntidadTipo TIPO = EntidadTipo.Arco;
        private Vector3f centro;
        private float radio;
        private float anguloInicio;
        private float anguloFin;
        private Vector3f puntoInicio;
        private Vector3f puntoFin;
        //private float thickness;
        private Vector3f normal;
        private char sentido;
        private bool invertido;
        //private AciColor color;
        // private Layer layer;
        // private LineType lineType;
        // private Dictionary<ApplicationRegistry, XData> xData;

        #endregion

        #region constructores

        /// <summary>
        /// Inicializa una nueva instancia de la clase <c>Arco</c>
        /// </summary>
        /// <param name="centro">Arco <see cref="DXF.Objetos.Vector3f">centro</see> en coordenadas</param>
        /// <param name="radio">Arco radio</param>
        /// <param name="anguloInicio">Angulo de inicio del arco en grados</param>
        /// <param name="anguloFin">Angulo de fin del arco en grados</param>
        /// <remarks>La coordenada Z del centro representa la elevacion sobre la normal.</remarks>
        public Arco(Vector3f centro, float radio, float anguloInicio, float anguloFin)
            : base(DxfCodigoObjeto.Arco)
        {
            this.centro = centro;
            this.radio = radio;
            this.anguloInicio = anguloInicio;
            this.anguloFin = anguloFin;
            //this.thickness = 0.0f;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            this.normal = Vector3f.UnitarioZ;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <c>Arco</c>
        /// </summary>
        public Arco() :
            base(DxfCodigoObjeto.Arco)
        {
            this.centro = Vector3f.Nulo;
            this.radio = 0.0f;
            this.anguloInicio = 0.0f;
            this.anguloFin = 0.0f;
            //this.thickness = 0.0f;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            this.normal = Vector3f.UnitarioZ;
        }

        #endregion

        #region propiedades publicas

        /// <summary>
        /// Obtiene o establece el <see cref="Dxf.Objetos.Vector3f">centro</see> del arco.
        /// </summary>
        /// <remarks>La coordinada Z del centro representa la elevacion del arco sobre la normal.</remarks>
        public Vector3f Centro
        {
            get { return this.centro; }
            set { this.centro = value; }
        }

        /// <summary>
        /// Obtiene o establece el <see cref="netDxf.startPoint">punto de inicio</see>del arco.
        /// </summary>
        /// <remarks>The .</remarks>
        public Vector3f PuntoInicio
        {
            get { return this.puntoInicio; }
            set { this.puntoInicio = value; }
        }

        /// <summary>
        /// Obtiene o establece el <see cref="netDxf.endPoint">punto de fin</see>del arco.
        /// </summary>
        /// <remarks>The .</remarks>
        public Vector3f PuntoFin
        {
            get { return this.puntoFin; }
            set { this.puntoFin = value; }
        }

        /// <summary>
        /// Obtiene o establece el radio del arco.
        /// </summary>
        public float Radio
        {
            get { return this.radio; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("valor", value.ToString());
                this.radio = value;
            }
        }

        /// <summary>
        /// Obtiene o establece el angulo de inicio del arco.
        /// </summary>
        public float AnguloInicio
        {
            get { return this.anguloInicio; }
            set { this.anguloInicio = value; }
        }

        /// <summary>
        /// Obtiene o establece el angulo de fin del arco.
        /// </summary>
        public float AnguloFin
        {
            get { return this.anguloFin; }
            set { this.anguloFin = value; }
        }

        public char Sentido
        {
            get { return this.sentido; }
            set { this.sentido = value; }
        }

        /// <summary>
        /// Gets or sets the arc thickness.
        /// </summary>
        //public float Thickness
        //{
        //    get { return this.thickness; }
        //    set { this.thickness = value; }
        //}

        /// <summary>
        /// Obtiene o establece la <see cref="Dxf.Objetos.Vector3f">normal</see> del arco.
        /// </summary>
        public Vector3f Normal
        {
            get { return this.normal; }
            set
            {
                if (Vector3f.Nulo == value)
                    throw new ArgumentNullException("valor", "The normal can not be the zero vector");
                value.Normalize();
                this.normal = value;
            }
        }

        public float MinimoX
        {
            get { return this.centro.X - radio; }

        }
        public float MaximoX
        {
            get { return this.centro.X + radio; }

        }
        public float MinimoY
        {
            get { return this.centro.Y - radio; }
        }
        public float MaximoY
        {
            get { return this.centro.Y + radio; }

        }
        #endregion

        #region IEntidadObjeto miembros

        /// <summary>
        /// Obtiene el <see cref="Dxf.Entidades.EntidadTipo">tipo</see> de entidad.
        /// </summary>
        public EntidadTipo Tipo
        {
            get { return TIPO; }
        }

        /// <summary>
        /// Gets or sets the entity <see cref="netDxf.AciColor">color</see>.
        /// </summary>
        //public AciColor Color
        //{
        //    get { return this.color; }
        //    set
        //    {
        //        if (value == null)
        //            throw new ArgumentNullException("value");
        //        this.color = value;
        //    }
        //}

        /// <summary>
        /// Gets or sets the entity <see cref="netDxf.Tables.Layer">layer</see>.
        /// </summary>
        //public Layer Layer
        //{
        //    get { return this.layer; }
        //    set
        //    {
        //        if (value == null)
        //            throw new ArgumentNullException("value");
        //        this.layer = value;
        //    }
        //}

        /// <summary>
        /// Gets or sets the entity <see cref="netDxf.Tables.LineType">line type</see>.
        /// </summary>
        //public LineType LineType
        //{
        //    get { return this.lineType; }
        //    set
        //    {
        //        if (value == null)
        //            throw new ArgumentNullException("value");
        //        this.lineType = value;
        //    }
        //}

        /// <summary>
        /// Gets or sets the entity <see cref="netDxf.XData">extende data</see>.
        /// </summary>
        //public Dictionary<ApplicationRegistry, XData> XData
        //{
        //    get { return this.xData; }
        //    set { this.xData = value; }
        //}

        public Vector3f PInicial
        {
            get { return this.puntoInicio; }
            set { this.puntoInicio = value; }
        }

        public Vector3f PFinal
        {
            get { return this.puntoFin; }
            set { this.puntoFin = value; }
        }
        public void InvertirPuntos()
        {
            //invierto los puntos
            Vector3f ptoTemp = this.puntoInicio;
            this.puntoInicio = this.puntoFin;
            this.puntoFin = ptoTemp;

            //e invierto el sentido del arco
            if (this.sentido == 'A') this.sentido = 'H';
            else this.sentido = 'A';
            
            this.invertido = true;
        }
        public bool Invertido
        { get { return this.invertido; } set { this.invertido = value; } }
        #endregion

        #region metodos publicos

        /// <summary>
        /// Converts the arc in a list of vertexes.
        /// </summary>
        /// <param name="precision">Number of vertexes generated.</param>
        /// <returns>A list vertexes that represents the arc expresed in object coordinate system.</returns>
        public List<Vector2f> PoligonalVertexes(int precision)
        {
            if (precision < 2)
                throw new ArgumentOutOfRangeException("precision", precision, "The arc precision must be greater or equal to two");

            List<Vector2f> ocsVertexes = new List<Vector2f>();
            float start = (float)(this.anguloInicio * MathHelper.DegToRad);
            float end = (float)(this.anguloFin * MathHelper.DegToRad);
            float angle = (end - start) / precision;

            for (int i = 0; i <= precision; i++)
            {
                float sine = (float)(this.radio * Math.Sin(start + angle * i));
                float cosine = (float)(this.radio * Math.Cos(start + angle * i));
                ocsVertexes.Add(new Vector2f(cosine + this.centro.X, sine + this.centro.Y));
            }

            return ocsVertexes;
        }

        /// <summary>
        /// Converts the arc in a list of vertexes.
        /// </summary>
        /// <param name="precision">Number of vertexes generated.</param>
        /// <param name="weldThreshold">Tolerance to consider if two new generated vertexes are equal.</param>
        /// <returns>A list vertexes that represents the arc expresed in object coordinate system.</returns>
        public List<Vector2f> PoligonalVertexes(int precision, float weldThreshold)
        {
            if (precision < 2)
                throw new ArgumentOutOfRangeException("precision", precision, "The arc precision must be greater or equal to two");

            List<Vector2f> ocsVertexes = new List<Vector2f>();
            float start = (float)(this.anguloInicio * MathHelper.DegToRad);
            float end = (float)(this.anguloFin * MathHelper.DegToRad);

            if (2 * this.radio >= weldThreshold)
            {
                float angulo = (end - start) / precision;
                Vector2f prevPoint;
                Vector2f firstPoint;

                float sine = (float)(this.radio * Math.Sin(start));
                float cosine = (float)(this.radio * Math.Cos(start));
                firstPoint = new Vector2f(cosine + this.centro.X, sine + this.centro.Y);
                ocsVertexes.Add(firstPoint);
                prevPoint = firstPoint;

                for (int i = 1; i <= precision; i++)
                {
                    sine = (float)(this.radio * Math.Sin(start + angulo * i));
                    cosine = (float)(this.radio * Math.Cos(start + angulo * i));
                    Vector2f point = new Vector2f(cosine + this.centro.X, sine + this.centro.Y);

                    if (!point.Equals(prevPoint, weldThreshold) && !point.Equals(firstPoint, weldThreshold))
                    {
                        ocsVertexes.Add(point);
                        prevPoint = point;
                    }
                }
            }

            return ocsVertexes;
        }

        public bool PerteneceAreaTrabajo(XML_Config config)
        {
            //por defecto estimamos que la figura estara dentro
            bool resultado = true;


            //ANALIZAMOS LOS PUNTOS DE INICIO Y FIN:
            if (this.puntoInicio.X > config.MaxX || this.puntoInicio.X < 0)
            {
                return false;
            }
            if (this.puntoInicio.Y > config.MaxY || this.puntoInicio.Y < 0)
            {
                return false;
            }
            
            //sacamos la validacion sobre Z para permitir los valores negativos
            //if (this.puntoInicio.Z > config.MaxZ || this.puntoInicio.Z < 0)
            //{
            //    return false;
            //}

            if (this.puntoFin.X > config.MaxX || this.puntoFin.X < 0)
            {
                return false;
            }
            if (this.puntoFin.Y > config.MaxY || this.puntoFin.Y < 0)
            {
                return false;
            }
            //if (this.puntoFin.Z > config.MaxZ || this.puntoFin.Z < 0)
            //{
            //    return false;
            //}

            //CASO 1 - CUADRANTE DEL INICIO = 1
            if (this.CuadranteInicio() == 1)
            {
                switch (this.CuadranteFin())
                {
                    case 1: //como los dos estan en el cuadrante 1
                        if (this.anguloInicio > this.anguloFin)
                        {
                            //hay que calcular todos los maximos y minimos
                            if (this.MaximoY > config.MaxY || this.MinimoY < 0 || this.MinimoX < 0 || this.MaximoX > config.MaxX)
                            {
                                return false;
                            } break;
                        } break;
                    case 2:
                        //hay que comparar con el maximo en y
                        if (this.MaximoY > config.MaxY)
                        {
                            return false;
                        } break;
                    case 3:
                        //hay que comparar con el maximo en y, minimo en X
                        if (this.MaximoY > config.MaxY || this.MinimoX < 0)
                        {
                            return false;
                        } break;
                    case 4:
                        //hay que comparar con el maximo y minimo en y, minimo en X
                        if (this.MaximoY > config.MaxY || this.MinimoY < 0 || this.MinimoX < 0)
                        {
                            return false;
                        } break;
                    default:
                        return false;
                }
            }


            //CASO 2 - CUADRANTE DEL INICIO = 2
            if (this.CuadranteInicio() == 2)
            {
                switch (this.CuadranteFin())
                {
                    case 1:
                        //hay que comparar con el maximo y minimo en x, maximo en X
                        if (this.MaximoX > config.MaxX || this.MinimoX < 0 || this.MaximoY > config.MaxY)
                        {
                            return false;
                        } break;
                    case 2://como los dos estan en el cuadrante 2
                        if (this.anguloInicio > this.anguloFin)
                        {
                            //hay que calcular todos los maximos y minimos
                            if (this.MaximoY > config.MaxY || this.MinimoY < 0 || this.MinimoX < 0 || this.MaximoX > config.MaxX)
                            {
                                return false;
                            } break;
                        } break;
                    case 3:
                        //hay que comparar con el minimo en X
                        if (this.MinimoX < 0)
                        {
                            return false;
                        } break;
                    case 4:
                        //hay que comparar con el minimo en y, minimo en X
                        if (this.MinimoY < 0 || this.MinimoX < 0)
                        {
                            return false;
                        } break;
                    default:
                        return false;
                }
            }

            //CASO 3 - CUADRANTE DEL INICIO = 3
            if (this.CuadranteInicio() == 3)
            {
                switch (this.CuadranteFin())
                {
                    case 1:
                        //hay que comparar con el minimo en y, maximo en X
                        if (this.MaximoX > config.MaxX || this.MinimoY < 0)
                        {
                            return false;
                        } break;
                    case 2:
                        //hay que comparar con el minimo en y, maximo en X
                        if (this.MaximoX > config.MaxX || this.MinimoY < 0 || this.MaximoY > config.MaxY)
                        {
                            return false;
                        } break;
                    case 3://como los dos estan en el cuadrante 3
                        if (this.anguloInicio > this.anguloFin)
                        {
                            //hay que calcular todos los maximos y minimos
                            if (this.MaximoY > config.MaxY || this.MinimoY < 0 || this.MinimoX < 0 || this.MaximoX > config.MaxX)
                            {
                                return false;
                            } break;
                        } break;
                    case 4:
                        //hay que comparar con el minimo en y
                        if (this.MinimoY < 0)
                        {
                            return false;
                        } break;
                    default:
                        return false;
                }
            }

            //CASO 4 - CUADRANTE DEL INICIO = 4
            if (this.CuadranteInicio() == 4)
            {
                switch (this.CuadranteFin())
                {
                    case 1:
                        //hay que comparar con el maximo en X
                        if (this.MaximoX > config.MaxX)
                        {
                            return false;
                        } break;
                    case 2:
                        //hay que comparar con el maximo en y, maximo en X
                        if (this.MaximoX > config.MaxX || this.MaximoY > config.MaxY)
                        {
                            return false;
                        } break;
                    case 3:
                        //hay que comparar con el maximo en y, maximo y minimo en X
                        if (this.MaximoX > config.MaxX || this.MaximoX < 0 || this.MaximoY > config.MaxY)
                        {
                            return false;
                        } break;
                    case 4:
                        //como los dos estan en el cuadrante 4
                        if (this.anguloInicio > this.anguloFin)
                        {
                            //hay que calcular todos los maximos y minimos
                            if (this.MaximoY > config.MaxY || this.MinimoY < 0 || this.MinimoX < 0 || this.MaximoX > config.MaxX)
                            {
                                return false;
                            } break;
                        } break;
                    default:
                        return false;
                }
            }

            return resultado;
        }

        private int CuadranteInicio()
        {

            if (this.anguloInicio >= 0 && this.AnguloInicio < 90)
            {
                return 1;
            }
            else if (this.anguloInicio >= 90 && this.AnguloInicio < 180)
            {
                return 2;
            }
            else if (this.anguloInicio >= 180 && this.AnguloInicio < 270)
            {
                return 3;
            }
            else
            {
                return 4;
            }


        }
        private int CuadranteFin()
        {

            if (this.anguloFin >= 0 && this.anguloFin < 90)
            {
                return 1;
            }
            else if (this.anguloFin >= 90 && this.anguloFin < 180)
            {
                return 2;
            }
            else if (this.anguloFin >= 180 && this.anguloFin < 270)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }


        #endregion

        #region overrides

        /// <summary>
        /// Converts the value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            return TIPO.ToString();
        }

        #endregion

    }

}
