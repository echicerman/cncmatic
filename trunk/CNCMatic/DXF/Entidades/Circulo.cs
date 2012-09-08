using System;
using System.Collections.Generic;
//using DXF.Tables;
using DXF.Objetos;
using DXF.Utils;


namespace DXF.Entidades
{
    /// <summary>
    /// Representa una <see cref="DXF.Entidades.IEntidadObjeto">entidad</see> circulo.
    /// </summary>
    public class Circulo :
         DxfObjeto,
        IEntidadObjeto
    {
        #region propiedades privadas

        private const EntidadTipo TIPO = EntidadTipo.Circulo;
        private Vector3f centro;
        private Vector3f inicio;
        private float radio;
        //private float thickness;
        //private Layer layer;
        //private AciColor color;
        //private LineType lineType;
        private Vector3f normal;
        //private Dictionary<ApplicationRegistry, XData> xData;

        #endregion

        #region constructores

        /// <summary>
        /// Inicializa una nueva instancia de la clase <c>Circulo</c>.
        /// </summary>
        /// <param name="centro"><see cref="Vector3f">Centro</see> del circulo en coordenadas</param>
        /// <param name="radio">Radio del circulo.</param>
        /// <remarks>La coordinada Z del centro representa la elevacion sobre la normal.</remarks>
        public Circulo(Vector3f centro, float radio, Vector3f inicio)
            : base(DxfCodigoObjeto.Circulo)
        {
            this.centro = centro;
            this.radio = radio;
            this.inicio = inicio;
            //this.thickness = 0.0f;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            this.normal = Vector3f.UnitarioZ;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <c>Circulo</c>.
        /// </summary>
        public Circulo()
            : base(DxfCodigoObjeto.Circulo)
        {
            this.centro = Vector3f.Nulo;
            this.radio = 1.0f;
            this.inicio = Vector3f.Nulo;
            //this.thickness = 0.0f;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            this.normal = Vector3f.UnitarioZ;
        }

        #endregion

        #region propiedades publicas

        /// <summary>
        /// Obtiene o establece el <see cref="netDxf.Vector3f">centro</see> del circulo.
        /// </summary>
        /// <remarks>La coordinada Z del centro representa la elevacion sobre la normal.</remarks>
        public Vector3f Centro
        {
            get { return this.centro; }
            set
            {
                this.centro = value;
                //recalculamos el punto de inicio del circulo sumando el radio a la coordenada X
                this.inicio = new Vector3f(this.centro.X + this.radio, centro.Y, centro.Z);
            }
        }

        /// <summary>
        /// Obtiene o establece el punto de inicio del circulo
        /// </summary>
        public Vector3f Inicio
        {
            get { return this.inicio; }
            set { this.inicio = value; }
        }

        /// <summary>
        /// Obtiene o establece el radio del circulo.
        /// </summary>
        public float Radio
        {
            get { return this.radio; }
            set { this.radio = value; }
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
        /// Obtiene o establece la <see cref="netDxf.Vector3f">normal</see> del circulo.
        /// </summary>
        public Vector3f Normal
        {
            get { return this.normal; }
            set
            {
                value.Normalize();
                this.normal = value;
            }
        }

        #endregion

        #region IEntidadObjeto Miembros

        /// <summary>
        /// Obtiene el <see cref="netDxf.Entities.EntityType">tipo</see> de la entidad.
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

        #endregion

        #region metodos publicos

        /// <summary>
        /// Convierte el circulo en una lista de vertices.
        /// </summary>
        /// <param name="precision">Numero de vertices generados.</param>
        /// <returns>Una lista de verices que representa el circulo expresado en coordenadas.</returns>
        public List<Vector2f> PoligonalVertices(int precision)
        {
            if (precision < 3)
                throw new ArgumentOutOfRangeException("precision", precision, "La precision del circulo debe ser mayor o igual a tres");

            List<Vector2f> ocsVertices = new List<Vector2f>();

            float angulo = (float)(MathHelper.TwoPI / precision);

            for (int i = 0; i < precision; i++)
            {
                float seno = (float)(this.radio * Math.Sin(MathHelper.HalfPI + angulo * i));
                float coseno = (float)(this.radio * Math.Cos(MathHelper.HalfPI + angulo * i));
                ocsVertices.Add(new Vector2f(coseno + this.centro.X, seno + this.centro.Y));
            }

            return ocsVertices;
        }

        /// <summary>
        /// Convierte el circulo en una lista de vertices.
        /// </summary>
        /// <param name="precision">Numero de vertices generados.</param>
        /// <param name="tolerancia">Tolerancia a considerar para comparar si dos nuevos vertices son iguales.</param>
        /// <returns>Una lista de verices que representa el circulo expresado en coordenadas.</returns>
        public List<Vector2f> PoligonalVertices(int precision, float tolerancia)
        {
            if (precision < 3)
                throw new ArgumentOutOfRangeException("precision", precision, "La precision del circulo debe ser mayor o igual a tres");

            List<Vector2f> ocsVertices = new List<Vector2f>();

            if (2 * this.radio >= tolerancia)
            {
                float angulo = (float)(MathHelper.TwoPI / precision);
                Vector2f antPunto;
                Vector2f primerPunto;

                float seno = (float)(this.radio * Math.Sin(MathHelper.HalfPI * 0.5));
                float coseno = (float)(this.radio * Math.Cos(MathHelper.HalfPI * 0.5));
                primerPunto = new Vector2f(coseno + this.centro.X, seno + this.centro.Y);
                ocsVertices.Add(primerPunto);
                antPunto = primerPunto;

                for (int i = 1; i < precision; i++)
                {
                    seno = (float)(this.radio * Math.Sin(MathHelper.HalfPI + angulo * i));
                    coseno = (float)(this.radio * Math.Cos(MathHelper.HalfPI + angulo * i));
                    Vector2f punto = new Vector2f(coseno + this.centro.X, seno + this.centro.Y);

                    if (!punto.Equals(antPunto, tolerancia) &&
                        !punto.Equals(primerPunto, tolerancia))
                    {
                        ocsVertices.Add(punto);
                        antPunto = punto;
                    }
                }
            }

            return ocsVertices;
        }

        #endregion

        #region overrides

        /// <summary>
        /// Convierte el tipo de la instancia a string
        /// </summary>
        /// <returns>El tipo en string.</returns>
        public override string ToString()
        {
            return TIPO.ToString();
        }

        #endregion
    }
}
