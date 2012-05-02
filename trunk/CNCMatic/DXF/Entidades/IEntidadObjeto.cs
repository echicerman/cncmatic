using System.Collections.Generic;
//using DXF.Tables;

namespace DXF.Entidades
{

    /// <summary>
    /// Define el tipo de entidad
    /// </summary>
    public enum EntidadTipo
    {
        /// <summary>
        /// linea
        /// </summary>
        Linea,

        /// <summary>
        /// linea multiple
        /// </summary>
        Multilinea,

        ///// <summary>
        ///// 3d polyline .
        ///// </summary>
        //Polyline3d,

        ///// <summary>
        ///// lightweight polyline.
        ///// </summary>
        //LightWeightPolyline,

        ///// <summary>
        ///// polyface mesh.
        ///// </summary>
        //PolyfaceMesh,

        /// <summary>
        /// circulo
        /// </summary>
        Circulo,

        /// <summary>
        /// nurbs curve
        /// </summary>
        //NurbsCurve,

        /// <summary>
        /// elipse
        /// </summary>
        Elipse,

        /// <summary>
        /// punto
        /// </summary>
        Punto,

        /// <summary>
        /// arco
        /// </summary>
        Arco,

        /// <summary>
        /// text string.
        /// </summary>
        //Text,

        /// <summary>
        /// 3d face.
        /// </summary>
        //Face3D,

        /// <summary>
        /// solid.
        /// </summary>
        //Solid,

        /// <summary>
        /// block insertion.
        /// </summary>
        //Insert,

        /// <summary>
        /// hatch.
        /// </summary>
        //Hatch,

        /// <summary>
        /// atributo
        /// </summary>
        Atributo,

        /// <summary>
        /// definicion de atributo
        /// </summary>
        AtributoDefinicion,

        /// <summary>
        /// lightweight polyline vertex.
        /// </summary>
        //LightWeightPolylineVertex,

        /// <summary>
        /// polyline vertex.
        /// </summary>
        //PolylineVertex,

        /// <summary>
        /// polyline 3d vertex.
        /// </summary>
       // Polyline3dVertex,

        /// <summary>
        /// polyface mesh vertex.
        /// </summary>
        //PolyfaceMeshVertex,

        /// <summary>
        /// polyface mesh face.
        /// </summary>
        //PolyfaceMeshFace,

        /// <summary>
        /// dim.
        /// </summary>
        //Dimension,

        /// <summary>
        /// A generi Vertex
        /// </summary>
        //Vertex
    }

    /// <summary>
    /// Representa una entidad generica
    /// </summary>
    public interface IEntidadObjeto
    {
        /// <summary>
        /// Obtiene la entidad <see cref="EntidadTipo">type</see>.
        /// </summary>
        EntidadTipo Tipo { get; }

        /// <summary>
        /// Gets or sets the entity <see cref="AciColor">color</see>.
        /// </summary>
        //AciColor Color { get; set; }

        /// <summary>
        /// Gets or sets the entity <see cref="Layer">layer</see>.
        /// </summary>
        //Layer Layer { get; set; }

        /// <summary>
        /// Gets or sets the entity <see cref="LineType">line type</see.
        /// </summary>
        //LineType LineType { get; set; }

        /// <summary>
        /// Gets or sets the entity <see cref="XData">extended data</see.
        /// </summary>
        //Dictionary<ApplicationRegistry, XData> XData { get; set; }
    }
}
