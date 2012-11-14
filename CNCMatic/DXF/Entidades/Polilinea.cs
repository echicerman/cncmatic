using System;
using System.Collections.Generic;
using DXF.Objetos;
using DXF.Utils;
using Configuracion;
//using DXF.Tables;

namespace DXF.Entidades
{
    /// <summary>
    /// Representa una entidad polilinea <see cref="netDxf.Entities.IEntityObject"></see>.
    /// </summary>
    /// <remarks>
    /// The <see cref="netDxf.Entities.LightWeightPolyline">LightWeightPolyline</see> and
    /// the <see cref="netDxf.Entities.Polyline">Polyline</see> are essentially the same entity, they are both here for compatibility reasons.
    /// When a AutoCad12 file is saved all lightweight polylines will be converted to polylines, while for AutoCad2000 and later versions all
    /// polylines will be converted to lightweight polylines.
    /// </remarks>
    public class Polilinea :
        DxfObjeto,
        IPolilinea
    {
        #region propiedades privadas

        //private readonly EndSequence endSequence;
        private const EntidadTipo TIPO = EntidadTipo.Polilinea;
        private List<PolylineVertex> vertexes;
        private bool isClosed;
        private PolylineTypeFlags flags;
        //private Layer layer;
        //private AciColor color;
        //private LineType lineType;
        private Vector3f normal;
        private float elevation;
        //private float thickness;
        //private Dictionary<ApplicationRegistry, XData> xData;

        #endregion

        #region constructores

        /// <summary>
        /// Initializes a new instance of the <c>Polyline</c> class.
        /// </summary>
        /// <param name="vertexes">Polyline vertex list in object coordinates.</param>
        /// <param name="isClosed">Sets if the polyline is closed</param>
        public Polilinea(List<PolylineVertex> vertexes, bool isClosed)
            : base(DxfCodigoObjeto.Polilinea)
        {
            this.vertexes = vertexes;
            this.isClosed = isClosed;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            this.normal = Vector3f.UnitarioZ;
            this.elevation = 0.0f;
            //this.thickness = 0.0f;
            this.flags = isClosed ? PolylineTypeFlags.ClosedPolylineOrClosedPolygonMeshInM : PolylineTypeFlags.OpenPolyline;
            //this.endSequence = new EndSequence();
        }

        /// <summary>
        /// Initializes a new instance of the <c>Polyline</c> class.
        /// </summary>
        /// <param name="vertexes">Polyline <see cref="PolylineVertex">vertex</see> list in object coordinates.</param>
        public Polilinea(List<PolylineVertex> vertexes)
            : base(DxfCodigoObjeto.Polilinea)
        {
            this.vertexes = vertexes;
            this.isClosed = false;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            this.normal = Vector3f.UnitarioZ;
            this.elevation = 0.0f;
            //this.thickness = 0.0f;
            this.flags = PolylineTypeFlags.OpenPolyline;
            //this.endSequence = new EndSequence();
        }

        /// <summary>
        /// Initializes a new instance of the <c>Polyline</c> class.
        /// </summary>
        public Polilinea()
            : base(DxfCodigoObjeto.Polilinea)
        {
            this.vertexes = new List<PolylineVertex>();
            this.isClosed = false;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            this.normal = Vector3f.UnitarioZ;
            this.elevation = 0.0f;
            this.flags = PolylineTypeFlags.OpenPolyline;
            //this.endSequence = new EndSequence();
        }

        #endregion

        #region propiedades publicas

        /// <summary>
        /// Gets or sets the polyline <see cref="netDxf.Entities.PolylineVertex">vertex</see> list.
        /// </summary>
        public List<PolylineVertex> Vertexes
        {
            get { return this.vertexes; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                this.vertexes = value;
            }
        }

        /// <summary>
        /// Gets or sets if the polyline is closed.
        /// </summary>
        public virtual bool IsClosed
        {
            get { return this.isClosed; }
            set
            {
                this.flags |= value ? PolylineTypeFlags.ClosedPolylineOrClosedPolygonMeshInM : PolylineTypeFlags.OpenPolyline;
                this.isClosed = value;
            }
        }

        /// <summary>
        /// Gets or sets the polyline <see cref="netDxf.Vector3f">normal</see>.
        /// </summary>
        public Vector3f Normal
        {
            get { return this.normal; }
            set
            {
                if (Vector3f.Nulo == value)
                    throw new ArgumentNullException("value", "La normal no puede ser el vector nulo");
                value.Normalize();
                this.normal = value;
            }
        }

        /// <summary>
        /// Gets or sets the polyline thickness.
        /// </summary>
        //public float Thickness
        //{
        //    get { return this.thickness; }
        //    set { this.thickness = value; }
        //}

        /// <summary>
        /// Gets or sets the polyline elevation.
        /// </summary>
        public float Elevation
        {
            get { return this.elevation; }
            set { this.elevation = value; }
        }

        //internal EndSequence EndSequence
        //{
        //    get { return this.endSequence; }
        //}

        #endregion

        #region IPolilinea Members

        /// <summary>
        /// Gets the polyline type.
        /// </summary>
        public PolylineTypeFlags Flags
        {
            get { return this.flags; }
        }

        #endregion

        #region IEntidad Members

        /// <summary>
        /// Gets the entity <see cref="netDxf.Entities.EntityType">type</see>.
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
        /// Sets a constant width for all the polyline segments.
        /// </summary>
        /// <param name="width">Polyline width.</param>
        public void SetConstantWidth(float width)
        {
            foreach (PolylineVertex v in this.vertexes)
            {
                v.BeginThickness = width;
                v.EndThickness = width;
            }
        }

        /// <summary>
        /// Converts the polyline in a <see cref="netDxf.Entities.LightWeightPolyline">LightWeightPolyline</see>.
        /// </summary>
        /// <returns>A new instance of <see cref="LightWeightPolyline">LightWeightPolyline</see> that represents the lightweight polyline.</returns>
        //public LightWeightPolyline ToLightWeightPolyline()
        //{
        //    List<LightWeightPolylineVertex> polyVertexes = new List<LightWeightPolylineVertex>();
        //    foreach (PolylineVertex v in this.vertexes)
        //    {
        //        polyVertexes.Add(new LightWeightPolylineVertex(v.Location)
        //        {
        //            BeginThickness = v.BeginThickness,
        //            Bulge = v.Bulge,
        //            EndThickness = v.EndThickness,
        //        }
        //            );
        //    }

        //    return new LightWeightPolyline(polyVertexes, this.isClosed)
        //    {
        //        Color = this.color,
        //        Layer = this.layer,
        //        LineType = this.lineType,
        //        Normal = this.normal,
        //        Elevation = this.elevation,
        //        Thickness = this.thickness,
        //        XData = this.xData
        //    };
        //}

        public bool PerteneceAreaTrabajo(XML_Config config)
        {
            //por defecto estimamos que la figura estara dentro
            bool resultado = true;

            foreach (PolylineVertex v in this.Vertexes)
            {
                //ANALIZAMOS LOS PUNTOS DE INICIO Y FIN:
                if (v.Location.X > config.MaxX || v.Location.X < 0)
                {
                    return false;
                }
                if (v.Location.Y > config.MaxY || v.Location.Y < 0)
                {
                    return false;
                }
                //sacamos la validacion sobre Z para permitir los valores negativos
                //    if (v.Location.Z > config.MaxZ || v.Location.Z < 0)
                //{
                //    return false;
                //}
            }

            return resultado;
        }

        /// <summary>
        /// Obtains a list of vertexes that represent the polyline approximating the curve segments as necessary.
        /// </summary>
        /// <param name="bulgePrecision">Curve segments precision (a value of zero means that no approximation will be made).</param>
        /// <param name="weldThreshold">Tolerance to consider if two new generated vertexes are equal.</param>
        /// <param name="bulgeThreshold">Minimun distance from which approximate curved segments of the polyline.</param>
        /// <returns>The return vertexes are expresed in object coordinate system.</returns>
        public List<Vector2f> PoligonalVertexes(int bulgePrecision, float weldThreshold, float bulgeThreshold)
        {
            List<Vector2f> ocsVertexes = new List<Vector2f>();

            int index = 0;

            foreach (PolylineVertex vertex in this.Vertexes)
            {
                float bulge = vertex.Bulge;
                Vector2f p1;
                Vector2f p2;

                if (index == this.Vertexes.Count - 1)
                {
                    p1 = new Vector2f(vertex.Location.X, vertex.Location.Y);
                    p2 = new Vector2f(this.vertexes[0].Location.X, this.vertexes[0].Location.Y);
                }
                else
                {
                    p1 = new Vector2f(vertex.Location.X, vertex.Location.Y);
                    p2 = new Vector2f(this.vertexes[index + 1].Location.X, this.vertexes[index + 1].Location.Y);
                }

                if (!p1.Equals(p2, weldThreshold))
                {
                    if (bulge == 0 || bulgePrecision == 0)
                    {
                        ocsVertexes.Add(p1);
                    }
                    else
                    {
                        float c = Vector2f.Distance(p1, p2);
                        if (c >= bulgeThreshold)
                        {
                            float s = (c / 2) * Math.Abs(bulge);
                            float r = ((c / 2) * (c / 2) + s * s) / (2 * s);
                            float theta = (float)(4 * Math.Atan(Math.Abs(bulge)));
                            float gamma = (float)((Math.PI - theta) / 2);
                            float phi;

                            if (bulge > 0)
                            {
                                phi = Vector2f.AngleBetween(Vector2f.UnitX, p2 - p1) + gamma;
                            }
                            else
                            {
                                phi = Vector2f.AngleBetween(Vector2f.UnitX, p2 - p1) - gamma;
                            }

                            Vector2f center = new Vector2f((float)(p1.X + r * Math.Cos(phi)), (float)(p1.Y + r * Math.Sin(phi)));
                            Vector2f a1 = p1 - center;
                            float angle = 4 * ((float)(Math.Atan(bulge))) / (bulgePrecision + 1);

                            ocsVertexes.Add(p1);
                            for (int i = 1; i <= bulgePrecision; i++)
                            {
                                Vector2f curvePoint = new Vector2f();
                                Vector2f prevCurvePoint = new Vector2f(this.vertexes[this.vertexes.Count - 1].Location.X, this.vertexes[this.vertexes.Count - 1].Location.Y);
                                curvePoint.X = center.X + (float)(Math.Cos(i * angle) * a1.X - Math.Sin(i * angle) * a1.Y);
                                curvePoint.Y = center.Y + (float)(Math.Sin(i * angle) * a1.X + Math.Cos(i * angle) * a1.Y);

                                if (!curvePoint.Equals(prevCurvePoint, weldThreshold) &&
                                    !curvePoint.Equals(p2, weldThreshold))
                                {
                                    ocsVertexes.Add(curvePoint);
                                }
                            }
                        }
                        else
                        {
                            ocsVertexes.Add(p1);
                        }
                    }
                }
                index++;
            }

            return ocsVertexes;
        }

        #endregion

        #region overrides

        /// <summary>
        /// Asigns a handle to the object based in a integer counter.
        /// </summary>
        /// <param name="entityNumber">Number to asign.</param>
        /// <returns>Next avaliable entity number.</returns>
        /// <remarks>
        /// Some objects might consume more than one, is, for example, the case of polylines that will asign
        /// automatically a handle to its vertexes. The entity number will be converted to an hexadecimal number.
        /// </remarks>
        //internal override int AsignHandle(int entityNumber)
        //{
        //    foreach (PolylineVertex v in this.vertexes)
        //    {
        //        entityNumber = v.AsignHandle(entityNumber);
        //    }
        //    entityNumber = this.endSequence.AsignHandle(entityNumber);
        //    return base.AsignHandle(entityNumber);
        //}

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

    public class Polyline3dVertex :
    DxfObjeto,
    IVertex
    {

        #region private fields

        protected const EntidadTipo TIPO = EntidadTipo.Polyline3dVertex;
        protected VertexTypeFlags flags;
        protected Vector3f location;
        //protected AciColor color;
        //protected Layer layer;
        //protected LineType lineType;
        //protected Dictionary<ApplicationRegistry, XData> xData;

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <c><Polyline3dVertex/c> class.
        /// </summary>
        public Polyline3dVertex()
            : base(DxfCodigoObjeto.Vertex)
        {
            this.flags = VertexTypeFlags.Polyline3dVertex;
            this.location = Vector3f.Nulo;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
        }

        /// <summary>
        /// Initializes a new instance of the <c>Polyline3dVertex</c> class.
        /// </summary>
        /// <param name="location">Polyline <see cref="Vector3f">vertex</see> coordinates.</param>
        public Polyline3dVertex(Vector3f location)
            : base(DxfCodigoObjeto.Vertex)
        {
            this.flags = VertexTypeFlags.Polyline3dVertex;
            this.location = location;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
        }

        /// <summary>
        /// Initializes a new instance of the <c>PolylineVertex</c> class.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        public Polyline3dVertex(float x, float y, float z)
            : base(DxfCodigoObjeto.Vertex)
        {
            this.flags = VertexTypeFlags.Polyline3dVertex;
            this.location = new Vector3f(x, y, z);
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the vertex <see cref="netDxf.Vector3f">location</see>.
        /// </summary>
        public Vector3f Location
        {
            get { return this.location; }
            set { this.location = value; }
        }

        #endregion

        #region IEntityObject Members

        /// <summary>
        /// Gets the entity <see cref="netDxf.Entities.EntityType">type</see>.
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

        #region IVertex Members

        /// <summary>
        /// Gets the vertex type.
        /// </summary>
        public VertexTypeFlags Flags
        {
            get { return this.flags; }
        }

        #endregion

        #region overrides

        /// <summary>
        /// Converts the value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            return String.Format("{0} ({1})", TIPO, this.location);
        }

        #endregion
    }

    public class Polyline3d :
        DxfObjeto,
        IPolilinea
    {
        #region private fields

        //private readonly EndSequence endSequence;
        protected const EntidadTipo TIPO = EntidadTipo.Polyline3d;
        protected List<Polyline3dVertex> vertexes;
        protected PolylineTypeFlags flags;
        //protected Layer layer;
        //protected AciColor color;
        //protected LineType lineType;
        //protected Dictionary<ApplicationRegistry, XData> xData;

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <c>Polyline3d</c> class.
        /// </summary>
        /// <param name="vertexes">3d polyline <see cref="Polyline3dVertex">vertex</see> list.</param>
        /// <param name="isClosed">Sets if the polyline is closed</param>
        public Polyline3d(List<Polyline3dVertex> vertexes, bool isClosed)
            : base(DxfCodigoObjeto.Polilinea)
        {
            this.flags = isClosed ? PolylineTypeFlags.ClosedPolylineOrClosedPolygonMeshInM | PolylineTypeFlags.Polyline3D : PolylineTypeFlags.Polyline3D;
            this.vertexes = vertexes;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            //this.endSequence = new EndSequence();
        }

        /// <summary>
        /// Initializes a new instance of the <c>Polyline3d</c> class.
        /// </summary>
        /// <param name="vertexes">3d polyline <see cref="Polyline3dVertex">vertex</see> list.</param>
        public Polyline3d(List<Polyline3dVertex> vertexes)
            : base(DxfCodigoObjeto.Polilinea)
        {
            this.flags = PolylineTypeFlags.Polyline3D;
            this.vertexes = vertexes;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            //this.endSequence = new EndSequence();
        }

        /// <summary>
        /// Initializes a new instance of the <c>Polyline3d</c> class.
        /// </summary>
        public Polyline3d()
            : base(DxfCodigoObjeto.Polilinea)
        {
            this.flags = PolylineTypeFlags.Polyline3D;
            this.vertexes = new List<Polyline3dVertex>();
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            //this.endSequence = new EndSequence();
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the polyline <see cref="netDxf.Entities.Polyline3dVertex">vertex</see> list.
        /// </summary>
        public List<Polyline3dVertex> Vertexes
        {
            get { return this.vertexes; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                this.vertexes = value;
            }
        }

        //internal EndSequence EndSequence
        //{
        //    get { return this.endSequence; }
        //}

        #endregion

        #region IPolyline Members

        /// <summary>
        /// Gets the polyline type.
        /// </summary>
        public PolylineTypeFlags Flags
        {
            get { return this.flags; }
        }

        #endregion

        #region IEntityObject Members

        /// <summary>
        /// Gets the entity <see cref="netDxf.Entities.EntityType">type</see>.
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

        #region overrides

        /// <summary>
        /// Asigns a handle to the object based in a integer counter.
        /// </summary>
        /// <param name="entityNumber">Number to asign.</param>
        /// <returns>Next avaliable entity number.</returns>
        /// <remarks>
        /// Some objects might consume more than one, is, for example, the case of polylines that will asign
        /// automatically a handle to its vertexes. The entity number will be converted to an hexadecimal number.
        /// </remarks>
        //internal override int AsignHandle(int entityNumber)
        //{
        //    foreach (Polyline3dVertex v in this.vertexes)
        //    {
        //        entityNumber = v.AsignHandle(entityNumber);
        //    }
        //    entityNumber = this.endSequence.AsignHandle(entityNumber);

        //    return base.AsignHandle(entityNumber);
        //}

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

    /// <summary>
    /// Represents a polyline <see cref="netDxf.Entities.IEntityObject">entity</see>.
    /// </summary>
    /// <remarks>
    /// The <see cref="netDxf.Entities.LightWeightPolyline">LightWeightPolyline</see> and
    /// the <see cref="netDxf.Entities.Polyline">Polyline</see> are essentially the same entity, they are both here for compatibility reasons.
    /// When a AutoCad12 file is saved all lightweight polylines will be converted to polylines, while for AutoCad2000 and later versions all
    /// polylines will be converted to lightweight polylines.
    /// </remarks>
    public class LightWeightPolyline :
        DxfObjeto,
        IPolilinea
    {
        #region private fields

        private const EntidadTipo TIPO = EntidadTipo.LightWeightPolyline;
        private List<LightWeightPolylineVertex> vertexes;
        private bool isClosed;
        private PolylineTypeFlags flags;
        //private Layer layer;
        //private AciColor color;
        //private LineType lineType;
        private Vector3f normal;
        private float elevation;
        private float thickness;
        //private Dictionary<ApplicationRegistry, XData> xData;

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <c>Polyline</c> class.
        /// </summary>
        /// <param name="vertexes">Polyline <see cref="LightWeightPolylineVertex">vertex</see> list in object coordinates.</param>
        /// <param name="isClosed">Sets if the polyline is closed</param>
        public LightWeightPolyline(List<LightWeightPolylineVertex> vertexes, bool isClosed)
            : base(DxfCodigoObjeto.LightWeightPolyline)
        {
            this.vertexes = vertexes;
            this.isClosed = isClosed;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            this.normal = Vector3f.UnitarioZ;
            this.elevation = 0.0f;
            this.thickness = 0.0f;
            this.flags = isClosed ? PolylineTypeFlags.ClosedPolylineOrClosedPolygonMeshInM : PolylineTypeFlags.OpenPolyline;
        }

        /// <summary>
        /// Initializes a new instance of the <c>Polyline</c> class.
        /// </summary>
        /// <param name="vertexes">Polyline <see cref="LightWeightPolylineVertex">vertex</see> list in object coordinates.</param>
        public LightWeightPolyline(List<LightWeightPolylineVertex> vertexes)
            : base(DxfCodigoObjeto.LightWeightPolyline)
        {
            this.vertexes = vertexes;
            this.isClosed = false;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            this.normal = Vector3f.UnitarioZ;
            this.elevation = 0.0f;
            this.thickness = 0.0f;
            this.flags = PolylineTypeFlags.OpenPolyline;
        }

        /// <summary>
        /// Initializes a new instance of the <c>Polyline</c> class.
        /// </summary>
        public LightWeightPolyline()
            : base(DxfCodigoObjeto.LightWeightPolyline)
        {
            this.vertexes = new List<LightWeightPolylineVertex>();
            this.isClosed = false;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            this.normal = Vector3f.Nulo;
            this.elevation = 0.0f;
            this.flags = PolylineTypeFlags.OpenPolyline;
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the polyline <see cref="netDxf.Entities.PolylineVertex">vertex</see> list.
        /// </summary>
        public List<LightWeightPolylineVertex> Vertexes
        {
            get { return this.vertexes; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                this.vertexes = value;
            }
        }

        /// <summary>
        /// Gets or sets if the polyline is closed.
        /// </summary>
        public virtual bool IsClosed
        {
            get { return this.isClosed; }
            set
            {
                this.flags |= value ? PolylineTypeFlags.ClosedPolylineOrClosedPolygonMeshInM : PolylineTypeFlags.OpenPolyline;
                this.isClosed = value;
            }
        }

        /// <summary>
        /// Gets or sets the polyline <see cref="netDxf.Vector3f">normal</see>.
        /// </summary>
        public Vector3f Normal
        {
            get { return this.normal; }
            set
            {
                if (Vector3f.Nulo == value)
                    throw new ArgumentNullException("value", "The normal can not be the zero vector");
                value.Normalize();
                this.normal = value;
            }
        }

        /// <summary>
        /// Gets or sets the polyline thickness.
        /// </summary>
        public float Thickness
        {
            get { return this.thickness; }
            set { this.thickness = value; }
        }

        /// <summary>
        /// Gets or sets the polyline elevation.
        /// </summary>
        public float Elevation
        {
            get { return this.elevation; }
            set { this.elevation = value; }
        }

        #endregion

        #region IPolyline Members

        /// <summary>
        /// Gets the polyline type.
        /// </summary>
        public PolylineTypeFlags Flags
        {
            get { return this.flags; }
        }

        #endregion

        #region IEntityObject Members

        /// <summary>
        /// Gets the entity <see cref="netDxf.Entities.EntityType">type</see>.
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

        #region public methods

        /// <summary>
        /// Sets a constant width for all the polyline segments.
        /// </summary>
        /// <param name="width">Polyline width.</param>
        public void SetConstantWidth(float width)
        {
            foreach (LightWeightPolylineVertex v in this.vertexes)
            {
                v.BeginThickness = width;
                v.EndThickness = width;
            }
        }

        /// <summary>
        /// Converts the lightweight polyline in a <see cref="Polyline">Polyline</see>.
        /// </summary>
        /// <returns>A new instance of <see cref="Polyline">Polyline</see> that represents the lightweight polyline.</returns>
        public Polilinea ToPolilinea()
        {
            List<PolylineVertex> polyVertexes = new List<PolylineVertex>();

            foreach (LightWeightPolylineVertex v in this.vertexes)
            {
                polyVertexes.Add(new PolylineVertex(v.Location)
                {
                    BeginThickness = v.BeginThickness,
                    Bulge = v.Bulge,
                    //Color = this.Color,
                    EndThickness = v.EndThickness,
                    //Layer = this.Layer,
                    //LineType = this.LineType,
                }
                    );
            }

            return new Polilinea(polyVertexes, this.isClosed)
            {
                //Color = this.color,
                //Layer = this.layer,
                //LineType = this.lineType,
                Normal = this.normal,
                Elevation = this.elevation,
                //Thickness = this.thickness,
                //XData = this.xData
            };
        }

        #region metodos publicos

        public bool PerteneceAreaTrabajo(XML_Config config)
        {
            //por defecto estimamos que la figura estara dentro
            bool resultado = true;

            foreach (LightWeightPolylineVertex v in this.Vertexes)
            {
                //ANALIZAMOS LOS PUNTOS DE INICIO Y FIN:
                if (v.Location.X > config.MaxX || v.Location.X < 0)
                {
                    return false;
                }
                if (v.Location.Y > config.MaxY || v.Location.Y < 0)
                {
                    return false;
                }
                //sacamos la validacion sobre Z para permitir los valores negativos
                //if (v.Location.Z > config.MaxZ || v.Location.Z < 0)
                //{
                //    return false;
                //}
            }

            return resultado;
        }
        #endregion


        /// <summary>
        /// Obtains a list of vertexes that represent the polyline approximating the curve segments as necessary.
        /// </summary>
        /// <param name="bulgePrecision">Curve segments precision (a value of zero means that no approximation will be made).</param>
        /// <param name="weldThreshold">Tolerance to consider if two new generated vertexes are equal.</param>
        /// <param name="bulgeThreshold">Minimun distance from which approximate curved segments of the polyline.</param>
        /// <returns>The return vertexes are expresed in object coordinate system.</returns>
        public List<Vector2f> PoligonalVertexes(int bulgePrecision, float weldThreshold, float bulgeThreshold)
        {
            List<Vector2f> ocsVertexes = new List<Vector2f>();

            int index = 0;

            foreach (LightWeightPolylineVertex vertex in this.Vertexes)
            {
                float bulge = vertex.Bulge;
                Vector2f p1;
                Vector2f p2;

                if (index == this.Vertexes.Count - 1)
                {
                    p1 = new Vector2f(vertex.Location.X, vertex.Location.Y);
                    p2 = new Vector2f(this.vertexes[0].Location.X, this.vertexes[0].Location.Y);
                }
                else
                {
                    p1 = new Vector2f(vertex.Location.X, vertex.Location.Y);
                    p2 = new Vector2f(this.vertexes[index + 1].Location.X, this.vertexes[index + 1].Location.Y);
                }

                if (!p1.Equals(p2, weldThreshold))
                {
                    if (bulge == 0 || bulgePrecision == 0)
                    {
                        ocsVertexes.Add(p1);
                    }
                    else
                    {
                        float c = Vector2f.Distance(p1, p2);
                        if (c >= bulgeThreshold)
                        {
                            float s = (c / 2) * Math.Abs(bulge);
                            float r = ((c / 2) * (c / 2) + s * s) / (2 * s);
                            float theta = (float)(4 * Math.Atan(Math.Abs(bulge)));
                            float gamma = (float)((Math.PI - theta) / 2);
                            float phi;

                            if (bulge > 0)
                            {
                                phi = Vector2f.AngleBetween(Vector2f.UnitX, p2 - p1) + gamma;
                            }
                            else
                            {
                                phi = Vector2f.AngleBetween(Vector2f.UnitX, p2 - p1) - gamma;
                            }

                            Vector2f center = new Vector2f((float)(p1.X + r * Math.Cos(phi)), (float)(p1.Y + r * Math.Sin(phi)));
                            Vector2f a1 = p1 - center;
                            float angle = 4 * ((float)(Math.Atan(bulge))) / (bulgePrecision + 1);

                            ocsVertexes.Add(p1);
                            for (int i = 1; i <= bulgePrecision; i++)
                            {
                                Vector2f curvePoint = new Vector2f();
                                Vector2f prevCurvePoint = new Vector2f(this.vertexes[this.vertexes.Count - 1].Location.X, this.vertexes[this.vertexes.Count - 1].Location.Y);
                                curvePoint.X = center.X + (float)(Math.Cos(i * angle) * a1.X - Math.Sin(i * angle) * a1.Y);
                                curvePoint.Y = center.Y + (float)(Math.Sin(i * angle) * a1.X + Math.Cos(i * angle) * a1.Y);

                                if (!curvePoint.Equals(prevCurvePoint, weldThreshold) &&
                                    !curvePoint.Equals(p2, weldThreshold))
                                {
                                    ocsVertexes.Add(curvePoint);
                                }
                            }
                        }
                        else
                        {
                            ocsVertexes.Add(p1);
                        }
                    }
                }
                index++;
            }

            return ocsVertexes;
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

    public class LightWeightPolylineVertex
    {
        #region private fields

        protected const EntidadTipo TIPO = EntidadTipo.LightWeightPolylineVertex;
        protected Vector2f location;
        protected float beginThickness;
        protected float endThickness;
        protected float bulge;

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <c>LightWeightPolylineVertex</c> class.
        /// </summary>
        public LightWeightPolylineVertex()
        {
            this.location = Vector2f.Zero;
            this.bulge = 0.0f;
            this.beginThickness = 0.0f;
            this.endThickness = 0.0f;
        }

        /// <summary>
        /// Initializes a new instance of the <c>LightWeightPolylineVertex</c> class.
        /// </summary>
        /// <param name="location">Lightweight polyline <see cref="netDxf.Vector2f">vertex</see> coordinates.</param>
        public LightWeightPolylineVertex(Vector2f location)
        {
            this.location = location;
            this.bulge = 0.0f;
            this.beginThickness = 0.0f;
            this.endThickness = 0.0f;
        }

        /// <summary>
        /// Initializes a new instance of the <c>LightWeightPolylineVertex</c> class.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public LightWeightPolylineVertex(float x, float y)
        {
            this.location = new Vector2f(x, y);
            this.bulge = 0.0f;
            this.beginThickness = 0.0f;
            this.endThickness = 0.0f;
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the polyline vertex <see cref="netDxf.Vector2f">location</see>.
        /// </summary>
        public Vector2f Location
        {
            get { return this.location; }
            set { this.location = value; }
        }

        /// <summary>
        /// Gets or sets the light weight polyline begin thickness.
        /// </summary>
        public float BeginThickness
        {
            get { return this.beginThickness; }
            set { this.beginThickness = value; }
        }

        /// <summary>
        /// Gets or sets the light weight polyline end thickness.
        /// </summary>
        public float EndThickness
        {
            get { return this.endThickness; }
            set { this.endThickness = value; }
        }

        /// <summary>
        /// Gets or set the light weight polyline bulge. Accepted values range from -1 to 1.
        /// </summary>
        /// <remarks>
        /// The bulge is the tangent of one fourth the included angle for an arc segment, 
        /// made negative if the arc goes clockwise from the start point to the endpoint. 
        /// A bulge of 0 indicates a straight segment, and a bulge of 1 is a semicircle.
        /// </remarks>
        public float Bulge
        {
            get { return this.bulge; }
            set
            {
                if (this.bulge < -1.0 || this.bulge > 1.0f)
                {
                    throw new ArgumentOutOfRangeException("value", value, "The bulge must be a value between minus one and plus one");
                }
                this.bulge = value;
            }
        }

        /// <summary>
        /// Gets the entity <see cref="netDxf.Entities.EntityType">type</see>.
        /// </summary>
        public EntidadTipo Tipo
        {
            get { return TIPO; }
        }

        #endregion

        #region overrides

        /// <summary>
        /// Converts the value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            return String.Format("{0} ({1})", TIPO, this.location);
        }

        #endregion
    }
}
