using System;
using System.Collections.Generic;
using DXF.Objetos;
using DXF.Utils;

namespace DXF.Entidades
{
    /// <summary>
    /// Representa una elipse circular <see cref="DXF.Entidades.IEntidadObjeto"></see>
    /// </summary>
    public class Elipse :
        DxfObjeto,
        IEntidadObjeto
    {

        #region propiedades privadas

        private const EntidadTipo TIPO = EntidadTipo.Elipse;
        private Vector3f centro;
        private float ejeMayor;
        private float ejeMenor;
        private float rotacion;
        private float anguloInicio;
        private float anguloFin;
        //private float thickness;
        //private Layer layer;
        //private AciColor color;
        //private LineType lineType;
        private Vector3f normal;
        //private int curvePoints;
        //private Dictionary<ApplicationRegistry, XData> xData;

        #endregion

        #region constructores

        /// <summary>
        /// Inicializa una nueva instancia de la clase <c>Elipse</c>.
        /// </summary>
        /// <param name="centro">Elipse <see cref="DXF.Objetos.Vector3f">centro</see> en coordenadas</param>
        /// <param name="ejeMayor">Elipse eje mayor.</param>
        /// <param name="ejeMenor">Elipse eje menor.</param>
        /// <remarks>La coordenada Z del centro representa la elevacion sobre la normal.</remarks>
        public Elipse(Vector3f centro, float ejeMayor, float ejeMenor)
            : base(DxfCodigoObjeto.Elipse)
        {
            this.centro = centro;
            this.ejeMayor = ejeMayor;
            this.ejeMenor = ejeMenor;
            this.anguloInicio = 0.0f;
            this.anguloFin = 360.0f;
            this.rotacion = 0.0f;
            //this.curvePoints = 30;
            //this.thickness = 0.0f;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            this.normal = Vector3f.UnitarioZ;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <c>Elipse</c>.
        /// </summary>
        public Elipse()
            : base(DxfCodigoObjeto.Elipse)
        {
            this.centro = Vector3f.Nulo;
            this.ejeMayor = 1.0f;
            this.ejeMenor = 0.5f;
            this.anguloInicio = 0.0f;
            this.anguloFin = 360.0f;
            this.rotacion = 0.0f;
            //this.curvePoints = 30;
            //this.thickness = 0.0f;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            this.normal = Vector3f.UnitarioZ;
        }

        #endregion

        #region propiedades publicas

        /// <summary>
        /// Obtiene o establece el <see cref="Dxf.Objetos.Vector3f">centro</see> de la elipse.
        /// </summary>
        /// <remarks>La coordinada Z del centro representa la elevacion del arco sobre la normal.</remarks>
        public Vector3f Centro
        {
            get { return this.centro; }
            set { this.centro = value; }
        }

        /// <summary>
        /// Obtiene o establece el eje mayor de la elipse
        /// </summary>
        public float EjeMayor
        {
            get { return this.ejeMayor; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("value", value, "El eje mayor debe ser mayor a cero");
                this.ejeMayor = value;
            }
        }

        /// <summary>
        /// Obtiene o establece el eje menor de la elipse
        /// </summary>
        public float EjeMenor
        {
            get { return this.ejeMenor; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("value", value, "El eje menor debe ser mayor a cero");
                this.ejeMenor = value;
            }
        }

        /// <summary>
        /// Obtiene o establece la rotacion de la elipse
        /// </summary>
        public float Rotacion
        {
            get { return this.rotacion; }
            set { this.rotacion = value; }
        }

        /// <summary>
        /// Obtiene o establece el angulo de inicio de la elipse.
        /// </summary>
        /// <remarks><c>AnguloInicio</c> igual a 0 y <c>AnguloFin</c> igual a 360 para una elipse completa.</remarks>
        public float AnguloInicio
        {
            get { return this.anguloInicio; }
            set { this.anguloInicio = value; }
        }

        /// <summary>
        /// Obtiene o establece el angulo de fin de la elipse.
        /// </summary>
        /// <remarks><c>AnguloInicio</c> igual a 0 y <c>AnguloFin</c> igual a 360 para una elipse completa.</remarks>
        public float AnguloFin
        {
            get { return this.anguloFin; }
            set { this.anguloFin = value; }
        }

        /// <summary>
        /// Obtiene o establece la <see cref="Dxf.Objetos.Vector3f">normal</see> de la elipse.
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

        ///// <summary>
        ///// Gets or sets the number of points generated along the ellipse during the conversion to a polyline.
        ///// </summary>
        //public int CurvePoints
        //{
        //    get { return this.curvePoints; }
        //    set { this.curvePoints = value; }
        //}

        ///// <summary>
        ///// Gets or sets the ellipse thickness.
        ///// </summary>
        //public float Thickness
        //{
        //    get { return this.thickness; }
        //    set { this.thickness = value; }
        //}

        /// <summary>
        /// Retorna si la elipse es una elipse completa
        /// </summary>
        public bool EsElipseCompleta
        {
            get { return (this.anguloInicio + this.anguloFin == 360); }
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
        public Vector3f PInicial
        { get { return new Vector3f(); } set { } }


        public Vector3f PFinal
        { get { return new Vector3f(); } set { } }
        public bool Invertido
        { get { return false; } set { } }
        public void InvertirPuntos() { }
        ///// <summary>
        ///// Gets or sets the entity <see cref="netDxf.AciColor">color</see>.
        ///// </summary>
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

        ///// <summary>
        ///// Gets or sets the entity <see cref="netDxf.Tables.Layer">layer</see>.
        ///// </summary>
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

        ///// <summary>
        ///// Gets or sets the entity <see cref="netDxf.Tables.LineType">line type</see>.
        ///// </summary>
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

        ///// <summary>
        ///// Gets or sets the entity <see cref="netDxf.XData">extende data</see>.
        ///// </summary>
        //public Dictionary<ApplicationRegistry, XData> XData
        //{
        //    get { return this.xData; }
        //    set { this.xData = value; }
        //}

        #endregion

        #region metodos publicos

        /// <summary>
        /// Converts the ellipse in a Polyline.
        /// </summary>
        /// <param name="precision">Number of vertexes generated.</param>
        /// <returns>A new instance of <see cref="Polyline">Polyline</see> that represents the ellipse.</returns>
        //public Polyline ToPolyline(int precision)
        //{
        //    List<Vector2f> vertexes = this.PolygonalVertexes(precision);
        //    Vector3d ocsCenter = MathHelper.Transform((Vector3d) this.center,
        //                                              (Vector3d)this.normal, MathHelper.CoordinateSystem.World, MathHelper.CoordinateSystem.Object);
        //    Polyline poly = new Polyline
        //    {
        //        Color = this.color,
        //        Layer = this.layer,
        //        LineType = this.lineType,
        //        Normal = this.normal,
        //        Elevation = (float)ocsCenter.Z,
        //        Thickness=this.thickness
        //    };
        //    poly.XData=this.xData;

        //    foreach (Vector2f v in vertexes)
        //    {
        //        poly.Vertexes.Add(new PolylineVertex((float) (v.X + ocsCenter.X), (float) (v.Y + ocsCenter.Y)));
        //    }
        //    if (this.EsElipseCompleta)
        //        poly.IsClosed = true;

        //    return poly;
        //}

        /// <summary>
        /// Converts the ellipse in a list of vertexes.
        /// </summary>
        /// <param name="precision">Number of vertexes generated.</param>
        /// <returns>A list vertexes that represents the ellipse expresed in object coordinate system.</returns>
        public List<Vector2f> PolygonalVertexes(int precision)
        {
            List<Vector2f> points = new List<Vector2f>();
            float beta = (float) (this.rotacion*MathHelper.DegToRad);
            float sinbeta = (float) Math.Sin(beta);
            float cosbeta = (float) Math.Cos(beta);

            if (this.EsElipseCompleta )
            {
                for (int i = 0; i < 360; i += 360/precision)
                {
                    float alpha = (float) (i*MathHelper.DegToRad);
                    float sinalpha = (float) Math.Sin(alpha);
                    float cosalpha = (float) Math.Cos(alpha);

                    float pointX = 0.5f * (this.ejeMayor *cosalpha*cosbeta - this.ejeMenor*sinalpha*sinbeta);
                    float pointY = 0.5f * (this.ejeMayor * cosalpha * sinbeta + this.ejeMenor * sinalpha * cosbeta);

                    points.Add(new Vector2f(pointX, pointY));
                }
            }
            else
            {
                for (int i = 0; i <= precision; i++)
                {
                    float angle = this.anguloInicio + i*(this.anguloFin - this.anguloInicio)/precision;
                    points.Add(this.PointFromEllipse(angle));
                }
            }
            return points;
        }

        private Vector2f PointFromEllipse(float degrees)
        {
            // Convert the basic input into something more usable
            Vector2f ptCenter = new Vector2f(this.centro.X, this.centro.Y);
            float radians = (float) (degrees*MathHelper.DegToRad);

            // Calculate the radius of the ellipse for the given angle
            float a = this.ejeMayor;
            float b = this.ejeMenor;
            float eccentricity = (float) Math.Sqrt(1 - (b*b)/(a*a));
            float radiusAngle = b/(float) Math.Sqrt(1 - (eccentricity*eccentricity)*Math.Pow(Math.Cos(radians), 2));

            // Convert the radius back to Cartesian coordinates
            return new Vector2f(ptCenter.X + radiusAngle*(float) Math.Cos(radians), ptCenter.Y + radiusAngle*(float) Math.Sin(radians));
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
