using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Threading;
//using Dxf.Blocks;
using DXF.Entidades;
using DXF.Header;
using DXF.Objetos;
using Configuracion;
//using Dxf.Tables;
//using Attribute = DXF.Entidades.Attribute;

namespace DXF
{
    /// <summary>
    /// Representa un documento donde leer archivos dxf
    /// </summary>
    /// <remarks>
    /// The dxf object names (application registries, layers, text styles, blocks, line types,...) for AutoCad12 can not contain spaces,
    /// if this situation happens all spaces will be replaced by an underscore character '_'.
    /// </remarks>
    public class DxfDoc
    {
        #region propiedades privadas

        #region encabezado

        private string fileName;
        private DxfVersion version;
        private int handleCount = 100; //we will reserve the first handles for special cases

        #endregion

        #region tablas

        //private Dictionary<string, ApplicationRegistry> appRegisterNames;
        //private readonly Dictionary<string, ViewPort> viewports;
        //private Dictionary<string, Layer> layers;
        //private Dictionary<string, LineType> lineTypes;
        //private Dictionary<string, TextStyle> textStyles;
        //private readonly Dictionary<string, DimensionStyle> dimStyles;

        #endregion

        #region bloques

        //private Dictionary<string, Block> blocks;

        #endregion

        #region entidades

        private readonly Hashtable addedObjects;
        private List<Arco> arcos;
        private List<Circulo> circulos;
        private List<Elipse> elipses;
        //private List<NurbsCurve> nurbsCurves;
        //private List<Solid> solids;
        //private List<Face3d> faces3d;
        //private List<Insert> inserts;
        private List<Linea> lineas;
        private List<Punto> puntos;
        private List<IPolilinea> polilineas;
        //private List<Text> texts;

        #endregion

        #endregion

        #region constructores

        /// <summary>
        ///Inicia una nueva instancia de clase <c>DxfDoc</c>.
        /// </summary>
        public DxfDoc()
        {
            this.addedObjects = new Hashtable(); // keeps track of the added object to avoid duplicates
            //this.version = this.version;
            //this.viewports = new Dictionary<string, ViewPort>();
            //this.layers = new Dictionary<string, Layer>();
            //this.lineTypes = new Dictionary<string, LineType>();
            //this.textStyles = new Dictionary<string, TextStyle>();
            //this.blocks = new Dictionary<string, Block>();
            //this.appRegisterNames = new Dictionary<string, ApplicationRegistry>();
            //this.dimStyles = new Dictionary<string, DimensionStyle>();
            this.arcos = new List<Arco>();
            this.elipses = new List<Elipse>();
            //this.nurbsCurves = new List<NurbsCurve>();
            //this.faces3d = new List<Face3d>();
            //this.solids = new List<Solid>();
            //this.inserts = new List<Insert>();
            this.polilineas = new List<IPolilinea>();
            this.lineas = new List<Linea>();
            this.circulos = new List<Circulo>();
            this.puntos = new List<Punto>();
            //this.texts = new List<Text>();
        }

        #endregion

        #region public properties

        #region header

        /// <summary>
        /// Gets the dxf file <see cref="DxfVersion">version</see>.
        /// </summary>
        public DxfVersion Version
        {
            get { return this.version; }
        }

        /// <summary>
        /// Gets the name of the dxf document, once a file is saved or loaded this field is equals the file name without extension.
        /// </summary>
        public string FileName
        {
            get { return this.fileName; }
        }

        #endregion

        #region table public properties

        /// <summary>
        /// Gets the application registered names.
        /// </summary>
        //public ReadOnlyCollection<ApplicationRegistry> AppRegisterNames
        //{
        //    get
        //    {
        //        List<ApplicationRegistry> list = new List<ApplicationRegistry>();
        //        list.AddRange(this.appRegisterNames.Values);
        //        return list.AsReadOnly();
        //    }
        //}

        /// <summary>
        /// Gets the <see cref="Layer">layer</see> list.
        /// </summary>
        //public ReadOnlyCollection<Layer> Layers
        //{
        //    get
        //    {
        //        List<Layer> list = new List<Layer>();
        //        list.AddRange(this.layers.Values);
        //        return list.AsReadOnly();
        //    }
        //}

        /// <summary>
        /// Gets the <see cref="LineType">linetype</see> list.
        /// </summary>
        //public ReadOnlyCollection<LineType> LineTypes
        //{
        //    get
        //    {
        //        List<LineType> list = new List<LineType>();
        //        list.AddRange(this.lineTypes.Values);
        //        return list.AsReadOnly();
        //    }
        //}

        /// <summary>
        /// Gets the <see cref="TextStyle">text style</see> list.
        /// </summary>
        //public ReadOnlyCollection<TextStyle> TextStyles
        //{
        //    get
        //    {
        //        List<TextStyle> list = new List<TextStyle>();
        //        list.AddRange(this.textStyles.Values);
        //        return list.AsReadOnly();
        //    }
        //}

        /// <summary>
        /// Gets the <see cref="Block">block</see> list.
        /// </summary>
        //public ReadOnlyCollection<Block> Blocks
        //{
        //    get
        //    {
        //        List<Block> list = new List<Block>();
        //        list.AddRange(this.blocks.Values);
        //        return list.AsReadOnly();
        //    }
        //}

        #endregion

        #region entities public properties

        /// <summary>
        /// Gets the <see cref="netDxf.Entities.Arc">arc</see> list.
        /// </summary>
        public ReadOnlyCollection<Arco> Arcos
        {
            get { return this.arcos.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="netDxf.Entities.Ellipse">ellipse</see> list.
        /// </summary>
        public ReadOnlyCollection<Elipse> Elipses
        {
            get { return this.elipses.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="netDxf.Entities.NurbsCurve">NURBS Curve</see> list.
        /// </summary>
        //public ReadOnlyCollection<NurbsCurve> NurbsCurves
        //{
        //    get { return this.nurbsCurves.AsReadOnly(); }
        //}

        /// <summary>
        /// Gets the <see cref="netDxf.Entities.Circle">circle</see> list.
        /// </summary>
        public ReadOnlyCollection<Circulo> Circulos
        {
            get { return this.circulos.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="netDxf.Entities.Face3d">3d face</see> list.
        /// </summary>
        //public ReadOnlyCollection<Face3d> Faces3d
        //{
        //    get { return this.faces3d.AsReadOnly(); }
        //}

        /// <summary>
        /// Gets the <see cref="netDxf.Entities.Solid">solid</see> list.
        /// </summary>
        //public ReadOnlyCollection<Solid> Solids
        //{
        //    get { return this.solids.AsReadOnly(); }
        //}

        /// <summary>
        /// Gets the <see cref="netDxf.Entities.Insert">insert</see> list.
        /// </summary>
        //public ReadOnlyCollection<Insert> Inserts
        //{
        //    get { return this.inserts.AsReadOnly(); }
        //}

        /// <summary>
        /// Gets the <see cref="netDxf.Entities.Line">line</see> list.
        /// </summary>
        public ReadOnlyCollection<Linea> Lineas
        {
            get { return this.lineas.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="netDxf.Entities.IPolyline">polyline</see> list.
        /// </summary>
        /// <remarks>
        /// The polyline list contains all entities that are considered polylines in the dxf, they are:
        /// <see cref="Polyline">polylines</see>, <see cref="Polyline3d">3d polylines</see> and <see cref="PolyfaceMesh">polyface meshes</see>
        /// </remarks>
        public ReadOnlyCollection<IPolilinea> Polilineas
        {
            get { return this.polilineas.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="netDxf.Entities.Point">point</see> list.
        /// </summary>
        public ReadOnlyCollection<Punto> Puntos
        {
            get { return this.puntos.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the <see cref="netDxf.Entities.Text">text</see> list.
        /// </summary>
        //public ReadOnlyCollection<Text> Texts
        //{
        //    get { return this.texts.AsReadOnly(); }
        //}

        #endregion

        #endregion

        #region public table methods

        /// <summary>
        /// Gets a text style from the the table.
        /// </summary>
        /// <param name="name">TextStyle name</param>
        /// <returns>TextStyle.</returns>
        //public TextStyle GetTextStyle(string name)
        //{
        //    return this.textStyles[name];
        //}

        /// <summary>
        /// Determines if a specified text style exists in the table.
        /// </summary>
        /// <param name="textStyle">Text style to locate.</param>
        /// <returns>True if the specified text style exists or false in any other case.</returns>
        //public bool ContainsTextStyle(TextStyle textStyle)
        //{
        //    return this.textStyles.ContainsKey(textStyle.Name);
        //}

        /// <summary>
        /// Gets a block from the the table.
        /// </summary>
        /// <param name="name">Block name</param>
        /// <returns>Block.</returns>
        //public Block GetBlock(string name)
        //{
        //    return this.blocks[name];
        //}

        /// <summary>
        /// Determines if a specified block exists in the table.
        /// </summary>
        /// <param name="block">Block to locate.</param>
        /// <returns>True if the specified block exists or false in any other case.</returns>
        //public bool ContainsBlock(Block block)
        //{
        //    return this.blocks.ContainsKey(block.Name);
        //}

        /// <summary>
        /// Gets a line type from the the table.
        /// </summary>
        /// <param name="name">LineType name</param>
        /// <returns>LineType.</returns>
        //public LineType GetLineType(string name)
        //{
        //    return this.lineTypes[name];
        //}

        /// <summary>
        /// Determines if a specified line type exists in the table.
        /// </summary>
        /// <param name="lineType">Line type to locate.</param>
        /// <returns>True if the specified line type exists or false in any other case.</returns>
        //public bool ContainsLineType(LineType lineType)
        //{
        //    return this.lineTypes.ContainsKey(lineType.Name);
        //}

        /// <summary>
        /// Gets a layer from the the table.
        /// </summary>
        /// <param name="name">Layer name</param>
        /// <returns>Layer.</returns>
        //public Layer GetLayer(string name)
        //{
        //    return this.layers[name];
        //}

        /// <summary>
        /// Determines if a specified layer exists in the table.
        /// </summary>
        /// <param name="layer">Layer to locate.</param>
        /// <returns>True if the specified layer exists or false in any other case.</returns>
        //public bool ContainsLayer(Layer layer)
        //{
        //    return this.layers.ContainsKey(layer.Name);
        //}

        #endregion

        #region public methods

        /// <summary>
        /// Adds a new <see cref="IEntityObject">entity</see> to the document.
        /// </summary>
        /// <param name="entity">An <see cref="IEntityObject">entity</see></param>
        //public void AddEntity(IEntidadObjeto entidad)
        //{
        //    // check if the entity has not been added to the document
        //    if (this.addedObjects.ContainsKey(entidad))
        //        throw new ArgumentException("The entity " + entidad.Tipo + " object has already been added to the document.", "entity");

        //    this.addedObjects.Add(entidad, entidad);

        //    if (entidad.XData != null)
        //    {
        //        foreach (ApplicationRegistry appReg in entidad.XData.Keys)
        //        {
        //            if (!this.appRegisterNames.ContainsKey(appReg.Name))
        //            {
        //                this.appRegisterNames.Add(appReg.Name, appReg);
        //            }
        //        }
        //    }

        //    if (!this.layers.ContainsKey(entidad.Layer.Name))
        //    {
        //        if (!this.lineTypes.ContainsKey(entidad.Layer.LineType.Name))
        //        {
        //            this.lineTypes.Add(entidad.Layer.LineType.Name, entidad.Layer.LineType);
        //        }
        //        this.layers.Add(entidad.Layer.Name, entidad.Layer);
        //    }

        //    if (!this.lineTypes.ContainsKey(entidad.LineType.Name))
        //    {
        //        this.lineTypes.Add(entidad.LineType.Name, entidad.LineType);
        //    }

        //    switch (entidad.Tipo)
        //    {
        //        case EntidadTipo.Arco:
        //            this.arcos.Add((Arco)entidad);
        //            break;
        //        case EntidadTipo.Circulo:
        //            this.circulos.Add((Circulo)entidad);
        //            break;
        //        //case EntidadTipo.Ellipse:
        //        //    this.ellipses.Add((Ellipse)entidad);
        //        //    break;
        //        //case EntidadTipo.NurbsCurve:
        //        //    throw new NotImplementedException("Nurbs curves not avaliable at the moment.");
        //        //    this.nurbsCurves.Add((NurbsCurve)entidad);
        //        //    break;
        //        case EntidadTipo.Punto:
        //            this.puntos.Add((Punto)entidad);
        //            break;
        //        //case EntidadTipo.Face3D:
        //        //    this.faces3d.Add((Face3d)entidad);
        //        //    break;
        //        //case EntidadTipo.Solid:
        //        //    this.solids.Add((Solid)entidad);
        //        //    break;
        //        //case EntidadTipo.Insert:
        //        //    // if the block definition has already been added, we do not need to do anything else
        //        //    if (!this.blocks.ContainsKey(((Insert)entidad).Block.Name))
        //        //    {
        //        //        this.blocks.Add(((Insert)entidad).Block.Name, ((Insert)entidad).Block);

        //        //        if (!this.layers.ContainsKey(((Insert)entidad).Block.Layer.Name))
        //        //        {
        //        //            this.layers.Add(((Insert)entidad).Block.Layer.Name, ((Insert)entidad).Block.Layer);
        //        //        }

        //        //        //for new block definitions configure its entities
        //        //        foreach (IEntidadObjeto blockEntity in ((Insert)entidad).Block.Entities)
        //        //        {
        //        //            // check if the entity has not been added to the document
        //        //            if (this.addedObjects.ContainsKey(blockEntity))
        //        //                throw new ArgumentException("The entity " + blockEntity.Type +
        //        //                                            " object of the block " + ((Insert)entidad).Block.Name +
        //        //                                            " has already been added to the document.", "entity");
        //        //            this.addedObjects.Add(blockEntity, blockEntity);

        //        //            if (!this.layers.ContainsKey(blockEntity.Layer.Name))
        //        //            {
        //        //                this.layers.Add(blockEntity.Layer.Name, blockEntity.Layer);
        //        //            }
        //        //            if (!this.lineTypes.ContainsKey(blockEntity.LineType.Name))
        //        //            {
        //        //                this.lineTypes.Add(blockEntity.LineType.Name, blockEntity.LineType);
        //        //            }
        //        //        }
        //        //        //for new block definitions configure its attributes
        //        //        foreach (Attribute attribute in ((Insert)entidad).Attributes)
        //        //        {
        //        //            if (!this.layers.ContainsKey(attribute.Layer.Name))
        //        //            {
        //        //                this.layers.Add(attribute.Layer.Name, attribute.Layer);
        //        //            }
        //        //            if (!this.lineTypes.ContainsKey(attribute.LineType.Name))
        //        //            {
        //        //                this.lineTypes.Add(attribute.LineType.Name, attribute.LineType);
        //        //            }

        //        //            AttributeDefinition attDef = attribute.Definition;
        //        //            if (!this.layers.ContainsKey(attDef.Layer.Name))
        //        //            {
        //        //                this.layers.Add(attDef.Layer.Name, attDef.Layer);
        //        //            }

        //        //            if (!this.lineTypes.ContainsKey(attDef.LineType.Name))
        //        //            {
        //        //                this.lineTypes.Add(attDef.LineType.Name, attDef.LineType);
        //        //            }

        //        //            if (!this.textStyles.ContainsKey(attDef.Style.Name))
        //        //            {
        //        //                this.textStyles.Add(attDef.Style.Name, attDef.Style);
        //        //            }
        //        //        }
        //        //    }

        //        //    this.inserts.Add((Insert)entidad);
        //        //    break;
        //        case EntidadTipo.Linea:
        //            this.lineas.Add((Linea)entidad);
        //            break;
        //        //case EntidadTipo.LightWeightPolyline:
        //        //    this.polylines.Add((IPolyline)entidad);
        //        //    break;
        //        //case EntidadTipo.Polyline:
        //        //    this.polylines.Add((IPolyline)entidad);
        //        //    break;
        //        //case EntidadTipo.Polyline3d:
        //        //    this.polylines.Add((IPolyline)entidad);
        //        //    break;
        //        //case EntidadTipo.PolyfaceMesh:
        //        //    this.polylines.Add((IPolyline)entidad);
        //        //    break;
        //        //case EntidadTipo.Text:
        //        //    if (!this.textStyles.ContainsKey(((Text)entidad).Style.Name))
        //        //    {
        //        //        this.textStyles.Add(((Text)entidad).Style.Name, ((Text)entidad).Style);
        //        //    }
        //        //    this.texts.Add((Text)entidad);
        //        //    break;
        //        //case EntidadTipo.Vertex:
        //        //    throw new ArgumentException("The entity " + entity.Type + " is only allowed as part of another entity", "entity");

        //        //case EntidadTipo.PolylineVertex:
        //        //    throw new ArgumentException("The entity " + entity.Type + " is only allowed as part of another entity", "entity");

        //        //case EntidadTipo.Polyline3dVertex:
        //        //    throw new ArgumentException("The entity " + entity.Type + " is only allowed as part of another entity", "entity");

        //        //case EntidadTipo.PolyfaceMeshVertex:
        //        //    throw new ArgumentException("The entity " + entity.Type + " is only allowed as part of another entity", "entity");

        //        //case EntidadTipo.PolyfaceMeshFace:
        //        //    throw new ArgumentException("The entity " + entity.Type + " is only allowed as part of another entity", "entity");

        //        //case EntidadTipo.AttributeDefinition:
        //        //    throw new ArgumentException("The entity " + entity.Type + " is only allowed as part of another entity", "entity");

        //        //case EntidadTipo.Attribute:
        //        //    throw new ArgumentException("The entity " + entity.Type + " is only allowed as part of another entity", "entity");

        //        default:
        //            throw new NotImplementedException("La entidad " + entidad.Tipo + " no esta implementada o es desconocida");
        //    }

        //}

        /// <summary>
        /// Carga un archivo DXF.
        /// </summary>
        /// <param name="archivo">Nombre del archivo.</param>
        public void Cargar(string archivo)
        {
            if (!File.Exists(archivo))
                throw new FileNotFoundException("No se puede encontrar el archivo " + archivo + ".", archivo);

            this.fileName = Path.GetFileNameWithoutExtension(archivo);

            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            DxfReader dxfReader = new DxfReader(archivo);
            dxfReader.Open();
            dxfReader.Read();
            dxfReader.Close();

            //header information
            this.version = dxfReader.Version;
            this.handleCount = Convert.ToInt32(dxfReader.HandleSeed, 16);

            //tables information
            //this.appRegisterNames = dxfReader.ApplicationRegistrationIds;
            //this.layers = dxfReader.Layers;
            //this.lineTypes = dxfReader.LineTypes;
            //this.textStyles = dxfReader.TextStyles;
            //this.blocks = dxfReader.Blocks;

            //entities information
            this.arcos = dxfReader.Arcos;
            this.circulos = dxfReader.Circulos;
            this.elipses = dxfReader.Elipses;
            this.puntos = dxfReader.Puntos;
            //this.faces3d = dxfReader.Faces3d;
            //this.solids = dxfReader.Solids;
            this.polilineas = dxfReader.Polilineas;
            this.lineas = dxfReader.Lineas;
            //this.inserts = dxfReader.Inserts;
            //this.texts = dxfReader.Texts;

            Thread.CurrentThread.CurrentCulture = cultureInfo;


        }

        public bool AnalizarFiguras()
        {
            //hay que cambiar esto...
            XML_Config config = new XML_Config();
            config.MaxX = 100;
            config.MaxY = 100;
            config.MaxZ = 100;

            //analizamos los arcos del documento
            foreach (Arco arco in this.arcos)
            {
                if (!arco.PerteneceAreaTrabajo(config))
                    return false;
            }

            //analizamos los puntos del documento
            foreach (Punto punto in this.puntos)
            {
                if (!punto.PerteneceAreaTrabajo(config))
                    return false;
            }

            //analizamos las lineas del documento
            foreach (Linea linea in this.lineas)
            {
                if (!linea.PerteneceAreaTrabajo(config))
                    return false;
            }

            //analizamos las polilineas del documento
            foreach (IPolilinea polilinea in this.polilineas)
            {
                if (polilinea.Tipo == EntidadTipo.Polilinea)
                {
                    if (!((Polilinea)polilinea).PerteneceAreaTrabajo(config))
                        return false;
                }
                if (polilinea.Tipo == EntidadTipo.LightWeightPolyline)
                {
                    if (!((LightWeightPolyline)polilinea).PerteneceAreaTrabajo(config))
                        return false;
                }
            }

            //analizamos los circulos del documento
            foreach (Circulo circulo in this.circulos)
            {
                if (!circulo.PerteneceAreaTrabajo(config))
                    return false;
            }


            return true;
        }

        #endregion
    }
}
