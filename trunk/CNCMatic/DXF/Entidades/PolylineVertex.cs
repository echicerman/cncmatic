using System;
using System.Collections.Generic;
using DXF.Objetos;
using DXF.Utils;

namespace DXF.Entidades
{
    public class PolylineVertex :
        DxfObjeto,
        IVertex
    {
        #region private fields

        protected const EntidadTipo TIPO = EntidadTipo.PolylineVertex;
        protected VertexTypeFlags flags;
        protected Vector2f location;
        protected float beginThickness;
        protected float endThickness;
        protected float bulge;
        //protected AciColor color;
        //protected Layer layer;
        //protected LineType lineType;
        //protected Dictionary<ApplicationRegistry, XData> xData;

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <c>PolylineVertex</c> class.
        /// </summary>
        public PolylineVertex()
            : base(DxfCodigoObjeto.Vertex)
        {
            this.flags = VertexTypeFlags.PolylineVertex;
            this.location = Vector2f.Zero;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            this.bulge = 0.0f;
            this.beginThickness = 0.0f;
            this.endThickness = 0.0f;
        }

        /// <summary>
        /// Initializes a new instance of the <c>PolylineVertex</c> class.
        /// </summary>
        /// <param name="location">Polyline <see cref="Vector2f">vertex</see> coordinates.</param>
        public PolylineVertex(Vector2f location)
            : base(DxfCodigoObjeto.Vertex)
        {
            this.flags = VertexTypeFlags.PolylineVertex;
            this.location = location;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            this.bulge = 0.0f;
            this.beginThickness = 0.0f;
            this.endThickness = 0.0f;
        }

        /// <summary>
        /// Initializes a new instance of the <c>PolylineVertex</c> class.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public PolylineVertex(float x, float y)
            : base(DxfCodigoObjeto.Vertex)
        {
            this.flags = VertexTypeFlags.PolylineVertex;
            this.location = new Vector2f(x, y);
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
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

    /// <summary>
    /// Defines the vertex type.
    /// </summary>
    [Flags]
    public enum VertexTypeFlags
    {
        /// <summary>
        /// 2d polyline vertex
        /// </summary>
        PolylineVertex = 0,
        /// <summary>
        /// Extra vertex created by curve-fitting
        /// </summary>
        CurveFittingExtraVertex = 1,
        /// <summary>
        /// Curve-fit tangent defined for this vertex.
        /// A curve-fit tangent direction of 0 may be omitted from DXF output but is significant if this bit is set,
        /// </summary>
        CurveFitTangent = 2,
        /// <summary>
        /// Not used
        /// </summary>
        NotUsed = 4,
        /// <summary>
        /// Spline vertex created by spline-fitting
        /// </summary>
        SplineVertexFromSplineFitting = 8,
        /// <summary>
        /// Spline frame control point
        /// </summary>
        SplineFrameControlPoint = 16,
        /// <summary>
        /// 3D polyline vertex
        /// </summary>
        Polyline3dVertex = 32,
        /// <summary>
        /// 3D polygon mesh
        /// </summary>
        Polygon3dMesh = 64,
        /// <summary>
        /// Polyface mesh vertex
        /// </summary>
        PolyfaceMeshVertex = 128
    }

    /// <summary>
    /// Represents a generic vertex.
    /// </summary>
    internal interface IVertex :
        IEntidadObjeto
    {
        /// <summary>
        /// Gets the Vertex type.
        /// </summary>
        VertexTypeFlags Flags { get; }
    }
}
