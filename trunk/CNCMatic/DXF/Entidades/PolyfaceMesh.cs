using System;
using System.Collections.Generic;
using DXF.Objetos;
using DXF.Utils;

namespace DXF.Entidades
{

    public class PolyfaceMesh :
    DxfObjeto,
    IPolilinea
    {
        #region private fields
        //private readonly EndSequence endSequence;
        protected const EntidadTipo TIPO = EntidadTipo.PolyfaceMesh;
        private List<PolyfaceMeshFace> faces;
        private List<PolyfaceMeshVertex> vertexes;
        protected PolylineTypeFlags flags;
        //protected Layer layer;
        //protected AciColor color;
        //protected LineType lineType;
        //protected Dictionary<ApplicationRegistry, XData> xData;
        #endregion

        #region constructurs

        /// <summary>
        /// Initializes a new instance of the <c>PolyfaceMesh</c> class.
        /// </summary>
        /// <param name="vertexes">Polyface mesh <see cref="PolyfaceMeshVertex">vertex</see> list.</param>
        /// <param name="faces">Polyface mesh <see cref="PolyfaceMeshFace">faces</see> list.</param>
        public PolyfaceMesh(List<PolyfaceMeshVertex> vertexes, List<PolyfaceMeshFace> faces)
            : base(DxfCodigoObjeto.Polilinea)
        {
            this.flags = PolylineTypeFlags.PolyfaceMesh;
            this.vertexes = vertexes;
            this.faces = faces;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            //this.endSequence = new EndSequence();
        }

        /// <summary>
        /// Initializes a new instance of the <c>PolyfaceMesh</c> class.
        /// </summary>
        public PolyfaceMesh()
            : base(DxfCodigoObjeto.Polilinea)
        {
            this.flags = PolylineTypeFlags.PolyfaceMesh;
            this.faces = new List<PolyfaceMeshFace>();
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            //this.endSequence = new EndSequence();
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the polyface mesh <see cref="netDxf.Entities.PolyfaceMeshVertex">vertexes</see>.
        /// </summary>
        public List<PolyfaceMeshVertex> Vertexes
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
        /// Gets or sets the polyface mesh <see cref="netDxf.Entities.PolyfaceMeshFace">faces</see>.
        /// </summary>
        public List<PolyfaceMeshFace> Faces
        {
            get { return this.faces; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                this.faces = value;
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
        public Vector3f PInicial
        { get { return new Vector3f(); } set { } }


        public Vector3f PFinal
        { get { return new Vector3f(); } set { } }
        public bool Invertido
        { get { return false; } set { } }
        public void InvertirPuntos() { }
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
        //    entityNumber = this.endSequence.AsignHandle(entityNumber);
        //    foreach (PolyfaceMeshVertex v in this.vertexes)
        //    {
        //        entityNumber = v.AsignHandle(entityNumber);
        //    }
        //    foreach (PolyfaceMeshFace f in this.faces)
        //    {
        //        entityNumber = f.AsignHandle(entityNumber);
        //    }
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
    
    public class PolyfaceMeshVertex :
        DxfObjeto,
        IVertex
    {
        #region private fields

        protected const EntidadTipo TIPO = EntidadTipo.PolyfaceMeshVertex;
        protected VertexTypeFlags flags;
        protected Vector3f location;
        //protected AciColor color;
        //protected Layer layer;
        //protected LineType lineType;
        //protected Dictionary<ApplicationRegistry, XData> xData;

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <c>PolylineVertex</c> class.
        /// </summary>
        public PolyfaceMeshVertex()
            : base(DxfCodigoObjeto.Vertex)
        {
            this.flags = VertexTypeFlags.PolyfaceMeshVertex | VertexTypeFlags.Polygon3dMesh;
            this.location = Vector3f.Nulo;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
        }

        /// <summary>
        /// Initializes a new instance of the <c>PolylineVertex</c> class.
        /// </summary>
        /// <param name="location">Polyface mesh vertex <see cref="Vector3f">location</see>.</param>
        public PolyfaceMeshVertex(Vector3f location)
            : base(DxfCodigoObjeto.Vertex)
        {
            this.flags = VertexTypeFlags.PolyfaceMeshVertex | VertexTypeFlags.Polygon3dMesh;
            this.location = location;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
        }

        /// <summary>
        /// Initializes a new instance of the PolylineVertex class.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        public PolyfaceMeshVertex(float x, float y, float z)
            : base(DxfCodigoObjeto.Vertex)
        {
            this.flags = VertexTypeFlags.PolyfaceMeshVertex | VertexTypeFlags.Polygon3dMesh;
            this.location = new Vector3f(x, y, z);
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the polyface mesh vertex <see cref="netDxf.Vector3f">location</see>.
        /// </summary>
        public Vector3f Location
        {
            get { return this.location; }
            set { this.location = value; }
        }

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

        #region IEntityObject Members

        /// <summary>
        /// Gets the entity <see cref="netDxf.Entities.EntityType">type</see>.
        /// </summary>
        public EntidadTipo Tipo
        {
            get { return TIPO; }
        }
        public Vector3f PInicial
        { get { return new Vector3f(); } set { } }

        public Vector3f PFinal
        { get { return new Vector3f(); } set { } }

        public void InvertirPuntos() { }
        public bool Invertido
        { get { return false; } set { } }
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
        /// Converts the value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            return String.Format("{0} {1}", TIPO, this.location);
        }

        #endregion
    }
    public class PolyfaceMeshFace :
    DxfObjeto,
    IVertex
    {
        #region private fields
        protected const EntidadTipo TIPO = EntidadTipo.PolylineVertex;
        protected VertexTypeFlags flags;
        protected int[] vertexIndexes;
        //protected AciColor color;
        //protected Layer layer;
        //protected LineType lineType;
        //protected Dictionary<ApplicationRegistry, XData> xData;

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <c>PolyfaceMeshFace</c> class.
        /// </summary>
        /// <remarks>
        /// By default the face is made up of three vertexes.
        /// </remarks>
        public PolyfaceMeshFace()
            : base(DxfCodigoObjeto.Vertex)
        {
            this.flags = VertexTypeFlags.PolyfaceMeshVertex;
            this.vertexIndexes = new int[3];
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
        }

        /// <summary>
        /// Initializes a new instance of the <c>PolyfaceMeshFace</c> class.
        /// </summary>
        /// <param name="vertexIndexes">Array of indexes to the vertex list of a polyface mesh that makes up the face.</param>
        public PolyfaceMeshFace(int[] vertexIndexes)
            : base(DxfCodigoObjeto.Vertex)
        {
            if (vertexIndexes == null)
                throw new ArgumentNullException("vertexIndexes");
            if (vertexIndexes.Length > 4)
                throw new ArgumentOutOfRangeException("vertexIndexes", vertexIndexes.Length, "The maximun number of index vertexes in a face is 4");

            this.flags = VertexTypeFlags.PolyfaceMeshVertex;
            this.vertexIndexes = vertexIndexes;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the array of indexes to the vertex list of a polyface mesh that makes up the face.
        /// </summary>
        public int[] VertexIndexes
        {
            get { return this.vertexIndexes; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                if (value.Length > 4)
                    throw new ArgumentOutOfRangeException("value", this.vertexIndexes.Length, "The maximun number of index vertexes in a face is 4");

                this.vertexIndexes = value;
            }
        }

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

        #region IEntityObject Members

        /// <summary>
        /// Gets the entity <see cref="netDxf.Entities.EntityType">type</see>.
        /// </summary>
        public EntidadTipo Tipo
        {
            get { return TIPO; }
        }
        public Vector3f PInicial
        { get { return new Vector3f(); } set { } }


        public Vector3f PFinal
        { get { return new Vector3f(); } set { } }

        public void InvertirPuntos() { }
        public bool Invertido
        { get { return false; } set { } }
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
