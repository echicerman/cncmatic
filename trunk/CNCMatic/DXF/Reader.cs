using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
//using DXF.Blocks;
using DXF.Entidades;
using DXF.Objetos;
using DXF.Header;
using DXF.Utils;
//using DXF.Tables;
//using Attribute = DXF.Entidades.Attribute;

namespace DXF
{
    /// <summary>
    /// Lector DXF
    /// </summary>
    internal sealed class DxfReader
    {
        #region propiedades privadas

        private readonly string archivo;
        private int fileLine;
        private bool isFileOpen;
        private Stream entrada;
        private StreamReader reader;

        //encabezado
        private List<string> comentarios;
        private DxfVersion version;
        private string handleSeed;

        //entidades
        private List<Arco> arcos;
        private List<Circulo> circulos;
        private List<Punto> puntos;
        private List<Elipse> elipses;
        //private List<Face3d> faces3d;
        //private List<Solid> solids;
        //private List<Insert> inserts;
        private List<Linea> lineas;
        private List<IPolilinea> polilineas;
        //private List<Text> texts;

        //tables
        //private Dictionary<string, ApplicationRegistry> appIds;
        //private Dictionary<string, Layer> layers;
        //private Dictionary<string, LineType> lineTypes;
        //private Dictionary<string, TextStyle> textStyles;

        //blocks
        //private Dictionary<string, Block> blocks;

        #endregion

        #region constructors

        public DxfReader(string archivo)
        {
            this.archivo = archivo;
        }

        #endregion

        #region propiedades publicas

        public bool IsFileOpen
        {
            get { return this.isFileOpen; }
        }

        public List<string> Comentarios
        {
            get { return this.comentarios; }
        }

        #endregion

        #region public header properties

        public string HandleSeed
        {
            get { return this.handleSeed; }
        }

        public DxfVersion Version
        {
            get { return this.version; }
        }

        #endregion

        #region propiedades publicas entidades

        public List<Arco> Arcos
        {
            get { return this.arcos; }
        }

        public List<Circulo> Circulos
        {
            get { return this.circulos; }
        }

        public List<Elipse> Elipses
        {
            get { return this.elipses; }
        }

        public List<Punto> Puntos
        {
            get { return this.puntos; }
        }

        //public List<Face3d> Faces3d
        //{
        //    get { return this.faces3d; }
        //}

        //public List<Solid> Solids
        //{
        //    get { return this.solids; }
        //}

        public List<Linea> Lineas
        {
            get { return this.lineas; }
        }

        public List<IPolilinea> Polilineas
        {
            get { return this.polilineas; }
        }

        //public List<Insert> Inserts
        //{
        //    get { return this.inserts; }
        //}

        //public List<Text> Texts
        //{
        //    get { return this.texts; }
        //}

        #endregion

        #region public table properties

        //public Dictionary<string, ApplicationRegistry> ApplicationRegistrationIds
        //{
        //    get { return this.appIds; }
        //}

        //public Dictionary<string, Layer> Layers
        //{
        //    get { return this.layers; }
        //}

        //public Dictionary<string, LineType> LineTypes
        //{
        //    get { return this.lineTypes; }
        //}

        //public Dictionary<string, TextStyle> TextStyles
        //{
        //    get { return this.textStyles; }
        //}

        //public Dictionary<string, Block> Blocks
        //{
        //    get { return this.blocks; }
        //}

        #endregion

        #region metodos publicos

        public void Open()
        {
            try
            {
                this.entrada = File.OpenRead(this.archivo);
                this.reader = new StreamReader(this.entrada, Encoding.ASCII);
                this.isFileOpen = true;
            }
            catch (Exception ex)
            {
                throw (new DxfException(this.archivo, "Error al intentar abrir el archivo.", ex));
            }
        }

        public void Close()
        {
            if (this.isFileOpen)
            {
                this.reader.Close();
                this.entrada.Close();
            }
            this.isFileOpen = false;
        }

        public void Read()
        {
            this.comentarios = new List<string>();
            //this.appIds = new Dictionary<string, ApplicationRegistry>();
            //this.layers = new Dictionary<string, Layer>();
            //this.lineTypes = new Dictionary<string, LineType>();
            //this.textStyles = new Dictionary<string, TextStyle>();
            //this.blocks = new Dictionary<string, Block>();

            this.arcos = new List<Arco>();
            this.circulos = new List<Circulo>();
            //this.faces3d = new List<Face3d>();
            this.elipses = new List<Elipse>();
            //this.solids = new List<Solid>();
            //this.inserts = new List<Insert>();
            this.lineas = new List<Linea>();
            this.polilineas = new List<IPolilinea>();
            this.puntos = new List<Punto>();
            //this.texts = new List<Text>();
            this.fileLine = -1;

            ParCodigoValor cod = this.ReadCodePair();
            while (cod.Val != StringCode.EndOfFile)
            {
                if (cod.Val == StringCode.BeginSection)
                {
                    cod = this.ReadCodePair();
                    switch (cod.Val)
                    {
                        case StringCode.HeaderSection:
                            this.ReadHeader();
                            break;
                        case StringCode.ClassesSection:
                        //    this.ReadClasses();
                            break;
                        case StringCode.TablesSection:
                        //    this.ReadTables();
                            break;
                        case StringCode.BlocksSection:
                        //    this.ReadBlocks();
                            break;
                        case StringCode.EntitiesSection:
                            this.ReadEntities();
                            break;
                        case StringCode.ObjectsSection:
                            this.ReadObjects();
                            break;
                        default:
                            throw new InvalidDxfSectionException(cod.Val, this.archivo, "Seccion desconocida" + cod.Val + " linea " + this.fileLine);
                    }
                }
                cod = this.ReadCodePair();
            }
        }

        #endregion

        #region metodos secciones

        private void ReadHeader()
        {
            ParCodigoValor cod = this.ReadCodePair();
            while (cod.Val != StringCode.EndSection)
            {
                if (HeaderVariable.Allowed.ContainsKey(cod.Val))
                {
                    int codeGroup = HeaderVariable.Allowed[cod.Val];
                    string variableName = cod.Val;
                    cod = this.ReadCodePair();
                    if (cod.Cod != codeGroup)
                        throw new DxfHeaderVariableException(variableName, this.archivo, "Invalid variable name and code group convination in line " + this.fileLine);
                    switch (variableName)
                    {
                        case SystemVariable.DabaseVersion:
                            this.version = (DxfVersion)StringEnum.Parse(typeof(DxfVersion), cod.Val);
                            break;
                        case SystemVariable.HandSeed:
                            this.handleSeed = cod.Val;
                            break;

                    }
                }
                cod = this.ReadCodePair();
            }
        }

        //private void ReadClasses()
        //{
        //    ParCodigoValor cod = this.ReadCodePair();
        //    while (cod.Val != StringCode.EndSection)
        //    {
        //        cod = this.ReadCodePair();
        //    }
        //}

        //private void ReadTables()
        //{
        //    ParCodigoValor cod = this.ReadCodePair();
        //    while (cod.Val != StringCode.EndSection)
        //    {
        //        cod = this.ReadCodePair();
        //        switch (cod.Val)
        //        {
        //            case StringCode.ApplicationIDTable:
        //                Debug.Assert(cod.Cod == 2);
        //                this.ReadApplicationsId();
        //                break;
        //            case StringCode.LayerTable:
        //                Debug.Assert(cod.Cod == 2);
        //                this.ReadLayers();
        //                break;
        //            case StringCode.LineTypeTable:
        //                Debug.Assert(cod.Cod == 2);
        //                this.ReadLineTypes();
        //                break;
        //            case StringCode.TextStyleTable:
        //                Debug.Assert(cod.Cod == 2);
        //                this.ReadTextStyles();
        //                break;
        //        }
        //    }
        //}

        //private void ReadApplicationsId()
        //{
        //    ParCodigoValor cod = this.ReadCodePair();
        //    while (cod.Val != StringCode.EndTable)
        //    {
        //        if (cod.Val == StringCode.ApplicationIDTable)
        //        {
        //            Debug.Assert(cod.Cod == 0);
        //            ApplicationRegistry appId = this.ReadApplicationId(ref cod);
        //            this.appIds.Add(appId.Name, appId);
        //        }
        //        else
        //        {
        //            cod = this.ReadCodePair();
        //        }
        //    }
        //}

        //private ApplicationRegistry ReadApplicationId(ref ParCodigoValor cod)
        //{
        //    string appId = string.Empty;
        //    string handle = string.Empty;
        //    cod = this.ReadCodePair();

        //    while (cod.Cod != 0)
        //    {
        //        switch (cod.Cod)
        //        {
        //            case 2:
        //                if (string.IsNullOrEmpty(cod.Val))
        //                {
        //                    throw new DxfInvalidCodeValueEntityException(cod.Cod, cod.Val, this.archivo,
        //                                                                 "Valor no valido " + cod.Val + " en codigo " + cod.Cod + " linea " + this.fileLine);
        //                }
        //                appId = cod.Val;
        //                break;
        //            case 5:
        //                handle = cod.Val;
        //                break;
        //        }
        //        cod = this.ReadCodePair();
        //    }

        //    return new ApplicationRegistry(appId)
        //    {
        //        Handle = handle
        //    };
        //}

        //private void ReadBlocks()
        //{
        //    ParCodigoValor cod = this.ReadCodePair();
        //    while (cod.Val != StringCode.EndSection)
        //    {
        //        switch (cod.Val)
        //        {
        //            case StringCode.BeginBlock:
        //                Block block = this.ReadBlock(ref cod);
        //                this.blocks.Add(block.Name, block);
        //                break;
        //            default:
        //                cod = this.ReadCodePair();
        //                break;
        //        }
        //    }
        //}

        //private Block ReadBlock(ref ParCodigoValor  code)
        //{
        //    Layer layer = null;
        //    string name = string.Empty;
        //    string handle = string.Empty;
        //    Vector3f basePoint = Vector3f.Zero;
        //    List<IEntityObject> entities = new List<IEntityObject>();
        //    Dictionary<string, AttributeDefinition> attdefs = new Dictionary<string, AttributeDefinition>();

        //    code = this.ReadCodePair();
        //    while (code.Value != StringCode.EndBlock)
        //    {
        //        switch (code.Code)
        //        {
        //            case 5:
        //                handle = code.Value;
        //                code = this.ReadCodePair();
        //                break;
        //            case 8:
        //                layer = this.GetLayer(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 2:
        //                name = code.Value;
        //                code = this.ReadCodePair();
        //                break;
        //            case 10:
        //                basePoint.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 20:
        //                basePoint.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 30:
        //                basePoint.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 3:
        //                name = code.Value;
        //                code = this.ReadCodePair();
        //                break;
        //            case 0: // entity
        //                IEntityObject entity;
        //                entity = this.ReadBlockEntity(ref code);
        //                if (entity != null)
        //                    if (entity.Type == EntityType.AttributeDefinition)
        //                        attdefs.Add(((AttributeDefinition)entity).Id, (AttributeDefinition)entity);
        //                    else
        //                        entities.Add(entity);
        //                break;
        //            default:
        //                code = this.ReadCodePair();
        //                break;
        //        }
        //    }

        //    // read the end bloc object until a new element is found
        //    code = this.ReadCodePair();
        //    string endBlockHandle = string.Empty;
        //    Layer endBlockLayer = layer;
        //    while (code.Code != 0)
        //    {
        //        switch (code.Code)
        //        {
        //            case 5:
        //                endBlockHandle = code.Value;
        //                code = this.ReadCodePair();
        //                break;
        //            case 8:
        //                endBlockLayer = this.GetLayer(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            default:
        //                code = this.ReadCodePair();
        //                break;
        //        }
        //    }
        //    Block block = new Block(name)
        //    {
        //        BasePoint = basePoint,
        //        Layer = layer,
        //        Entities = entities,
        //        Attributes = attdefs,
        //        Handle = handle,
        //    };
        //    block.End.Handle = endBlockHandle;
        //    block.End.Layer = endBlockLayer;

        //    return block;
        //}

        //private IEntityObject ReadBlockEntity(ref ParCodigoValor code)
        //{
        //    IEntityObject entity = null;

        //    switch (code.Value)
        //    {
        //        case DxfObjectCode.Arc:
        //            entity = this.ReadArc(ref code);
        //            break;
        //        case DxfObjectCode.Circle:
        //            entity = this.ReadCircle(ref code);
        //            break;
        //        case DxfObjectCode.Face3D:
        //            entity = this.ReadFace3D(ref code);
        //            break;
        //        case DxfObjectCode.Solid:
        //            entity = this.ReadSolid(ref code);
        //            break;
        //        case DxfObjectCode.Insert:
        //            code = this.ReadCodePair();
        //            break;
        //        case DxfObjectCode.Line:
        //            entity = this.ReadLine(ref code);
        //            break;
        //        case DxfObjectCode.LightWeightPolyline:
        //            entity = this.ReadLightWeightPolyline(ref code);
        //            break;
        //        case DxfObjectCode.Polyline:
        //            entity = this.ReadPolyline(ref code);
        //            break;
        //        case DxfObjectCode.Text:
        //            entity = this.ReadText(ref code);
        //            break;
        //        case DxfObjectCode.AttributeDefinition:
        //            entity = this.ReadAttributeDefinition(ref code);
        //            break;
        //        default:
        //            ReadUnknowEntity(ref code);
        //            //code = this.ReadCodePair();
        //            break;
        //    }

        //    return entity;
        //}

        //private AttributeDefinition ReadAttributeDefinition(ref ParCodigoValor code)
        //{
        //    string handle = string.Empty;
        //    string id = string.Empty;
        //    string text = string.Empty;
        //    object value = null;
        //    AttributeFlags flags = AttributeFlags.Visible;
        //    Vector3f firstAlignmentPoint = Vector3f.Zero;
        //    Vector3f secondAlignmentPoint = Vector3f.Zero;
        //    Layer layer = Layer.Default;
        //    AciColor color = AciColor.ByLayer;
        //    LineType lineType = LineType.ByLayer;
        //    TextStyle style = TextStyle.Default;
        //    float height = 0;
        //    float widthFactor = 0;
        //    int horizontalAlignment = 0;
        //    int verticalAlignment = 0;
        //    float rotation = 0;
        //    Vector3f normal = Vector3f.UnitZ;

        //    code = this.ReadCodePair();
        //    while (code.Code != 0)
        //    {
        //        switch (code.Code)
        //        {
        //            case 5:
        //                handle = code.Value;
        //                break;
        //            case 2:
        //                id = code.Value;
        //                break;
        //            case 3:
        //                text = code.Value;
        //                break;
        //            case 1:
        //                value = code.Value;
        //                break;
        //            case 8: //layer code
        //                layer = this.GetLayer(code.Value);
        //                break;
        //            case 62: //aci color code
        //                color = new AciColor(short.Parse(code.Value));
        //                break;
        //            case 6: //type line code
        //                lineType = this.GetLineType(code.Value);
        //                break;
        //            case 70:
        //                flags = (AttributeFlags)int.Parse(code.Value);
        //                break;
        //            case 10:
        //                firstAlignmentPoint.X = float.Parse(code.Value);
        //                break;
        //            case 20:
        //                firstAlignmentPoint.Y = float.Parse(code.Value);
        //                break;
        //            case 30:
        //                firstAlignmentPoint.Z = float.Parse(code.Value);
        //                break;
        //            case 11:
        //                secondAlignmentPoint.X = float.Parse(code.Value);
        //                break;
        //            case 21:
        //                secondAlignmentPoint.Y = float.Parse(code.Value);
        //                break;
        //            case 31:
        //                secondAlignmentPoint.Z = float.Parse(code.Value);
        //                break;
        //            case 7:
        //                style = this.GetTextStyle(code.Value);
        //                break;
        //            case 40:
        //                height = float.Parse(code.Value);
        //                break;
        //            case 41:
        //                widthFactor = float.Parse(code.Value);
        //                break;
        //            case 50:
        //                rotation = float.Parse(code.Value);
        //                break;
        //            case 72:
        //                horizontalAlignment = int.Parse(code.Value);
        //                break;
        //            case 74:
        //                verticalAlignment = int.Parse(code.Value);
        //                break;
        //            case 210:
        //                normal.X = float.Parse(code.Value);
        //                break;
        //            case 220:
        //                normal.Y = float.Parse(code.Value);
        //                break;
        //            case 230:
        //                normal.Z = float.Parse(code.Value);
        //                break;
        //        }

        //        code = this.ReadCodePair();
        //    }

        //    TextAlignment alignment = ObtainAlignment(horizontalAlignment, verticalAlignment);

        //    return new AttributeDefinition(id)
        //    {
        //        BasePoint = (alignment == TextAlignment.BaselineLeft ? firstAlignmentPoint : secondAlignmentPoint),
        //        Normal = normal,
        //        Alignment = alignment,
        //        Text = text,
        //        Value = value,
        //        Flags = flags,
        //        Layer = layer,
        //        Color = color,
        //        LineType = lineType,
        //        Style = style,
        //        Height = height,
        //        WidthFactor = widthFactor,
        //        Rotation = rotation,
        //        Handle = handle
        //    };
        //}

        //private Attribute ReadAttribute(Block block, ref ParCodigoValor code)
        //{
        //    string handle = string.Empty;
        //    AttributeDefinition attdef = null;
        //    Layer layer = Layer.Default;
        //    AciColor color = AciColor.ByLayer;
        //    LineType lineType = LineType.ByLayer;
        //    Object value = null;
        //    code = this.ReadCodePair();
        //    while (code.Code != 0)
        //    {
        //        switch (code.Code)
        //        {
        //            case 5:
        //                handle = code.Value;
        //                break;
        //            case 2:
        //                attdef = block.Attributes[code.Value];
        //                break;
        //            case 1:
        //                value = code.Value;
        //                break;
        //            case 8: //layer code
        //                layer = this.GetLayer(code.Value);
        //                break;
        //            case 62: //aci color code
        //                color = new AciColor(short.Parse(code.Value));
        //                break;
        //            case 6: //type line code
        //                lineType = this.GetLineType(code.Value);
        //                break;
        //        }
        //        code = this.ReadCodePair();
        //    }

        //    return new Attribute(attdef)
        //    {
        //        Color = color,
        //        Layer = layer,
        //        LineType = lineType,
        //        Value = value,
        //        Handle = handle
        //    };
        //}

        private void ReadEntities()
        {
            ParCodigoValor  cod = this.ReadCodePair();
            while (cod.Val != StringCode.EndSection)
            {
                IEntidadObjeto  entidad;
                switch (cod.Val)
                {
                    case DxfCodigoObjeto.Arco:
                        entidad = this.ReadArco(ref cod);
                        this.arcos.Add((Arco)entidad);
                        break;
                    case DxfCodigoObjeto.Circulo:
                        entidad = this.ReadCirculo(ref cod);
                        this.circulos.Add((Circulo)entidad);
                        break;
                    case DxfCodigoObjeto.Punto:
                        entidad = this.ReadPunto(ref cod);
                        this.puntos.Add((Punto)entidad);
                        break;
                    case DxfCodigoObjeto.Elipse:
                        entidad = this.ReadElipse(ref cod);
                        this.elipses.Add((Elipse)entidad);
                        break;
                    //case DxfObjectCode.Face3D:
                    //    entity = this.ReadFace3D(ref code);
                    //    this.faces3d.Add((Face3d)entity);
                    //    break;
                    //case DxfObjectCode.Solid:
                    //    entity = this.ReadSolid(ref code);
                    //    this.solids.Add((Solid)entity);
                    //    break;
                    //case DxfObjectCode.Insert:
                    //    entity = this.ReadInsert(ref code);
                    //    this.inserts.Add((Insert)entity);
                    //    break;
                    case DxfCodigoObjeto.Linea:
                        entidad = this.ReadLinea(ref cod);
                        this.lineas.Add((Linea)entidad);
                        break;
                    case DxfCodigoObjeto.LightWeightPolyline:
                        entidad = this.ReadLightWeightPolyline(ref cod);
                        this.polilineas.Add((IPolilinea)entidad);
                        break;
                    case DxfCodigoObjeto.Polilinea:
                        entidad = this.ReadPolilinea(ref cod);
                        this.polilineas.Add((IPolilinea)entidad);
                        break;
                    //case DxfObjectCode.Text:
                    //    entity = this.ReadText(ref code);
                    //    this.texts.Add((Text)entity);
                    //    break;
                    //case DxfObjectCode.Esfera:
                    //    entity = this.ReadEsfera(ref code);
                    //    this.texts.Add((Text)entity);
                    //    break;
                    default:
                        ReadUnknowEntity(ref cod);
                        //code = this.ReadCodePair();
                        break;
                }
            }
        }

        private void ReadObjects()
        {
            ParCodigoValor  cod = this.ReadCodePair();
            while (cod.Val != StringCode.EndSection)
            {
                cod = this.ReadCodePair();
            }
        }

        #endregion

        #region layer methods

        //private void ReadLayers()
        //{
        //    ParCodigoValor  cod = this.ReadCodePair();
        //    while (cod.Val != StringCode.EndTable)
        //    {
        //        if (cod.Val == StringCode.LayerTable)
        //        {
        //            Debug.Assert(cod.Cod == 0);
        //            Layer capa = this.ReadLayer(ref cod);
        //            this.layers.Add(capa.Name, capa);
        //        }
        //        else
        //        {
        //            cod = this.ReadCodePair();
        //        }
        //    }
        //}

        //private Layer ReadLayer(ref ParCodigoValor  cod)
        //{
        //    string handle = string.Empty;
        //    string name = string.Empty;
        //    bool isVisible = true;
        //    //AciColor color = null;
        //    //LineType lineType = null;

        //    cod = this.ReadCodePair();

        //    while (cod.Cod != 0)
        //    {
        //        switch (cod.Cod)
        //        {
        //            case 5:
        //                handle = cod.Val;
        //                break;
        //            case 2:
        //                if (string.IsNullOrEmpty(cod.Val))
        //                {
        //                    throw new DxfInvalidCodeValueEntityException(cod.Cod, cod.Val, this.archivo,
        //                                                                 "Valor no valido " + cod.Val + " en codigo " + cod.Cod + " linea " + this.fileLine);
        //                }
        //                name = cod.Val;
        //                break;
        //            case 62:
        //                short index;
        //                if (short.TryParse(cod.Val, out index))
        //                {
        //                    if (index < 0)
        //                    {
        //                        isVisible = false;
        //                        index = Math.Abs(index);
        //                    }
        //                    if (index > 256)
        //                    {
        //                        throw new DxfInvalidCodeValueEntityException(cod.Cod, cod.Val, this.archivo,
        //                                                                 "Valor no valido " + cod.Val + " en codigo " + cod.Cod + " linea " + this.fileLine);
        //                    }
        //                }
        //                else
        //                {
        //                    throw new DxfInvalidCodeValueEntityException(cod.Cod, cod.Val, this.archivo,
        //                                                                 "Valor no valido " + cod.Val + " en codigo " + cod.Cod + " linea " + this.fileLine);
        //                }

        //                color = new AciColor(short.Parse(cod.Val));
        //                break;
        //            case 6:
        //                if (string.IsNullOrEmpty(cod.Val))
        //                {
        //                    throw new DxfInvalidCodeValueEntityException(cod.Cod, cod.Val, this.archivo,
        //                                                                 "Valor no valido " + cod.Val + " en codigo " + cod.Cod + " linea " + this.fileLine);
        //                }
        //                lineType = this.GetLineType(cod.Val);
        //                break;
        //        }


        //        cod = this.ReadCodePair();
        //    }

        //    return new Layer(name)
        //    {
        //        Color = color,
        //        LineType = lineType,
        //        IsVisible = isVisible,
        //        Handle = handle
        //    };
        //}

        #endregion

        #region line type methods

        //private void ReadLineTypes()
        //{
        //    CodeValuePair code = this.ReadCodePair();
        //    while (code.Value != StringCode.EndTable)
        //    {
        //        if (code.Value == StringCode.LineTypeTable)
        //        {
        //            Debug.Assert(code.Code == 0); //el código 0 indica el inicio de una nueva capa
        //            LineType tl = this.ReadLineType(ref code);
        //            this.lineTypes.Add(tl.Name, tl);
        //        }
        //        else
        //        {
        //            code = this.ReadCodePair();
        //        }
        //    }
        //}

        //private LineType ReadLineType(ref CodeValuePair code)
        //{
        //    string handle = string.Empty;
        //    string name = string.Empty;
        //    string description = string.Empty;
        //    var segments = new List<float>();

        //    code = this.ReadCodePair();

        //    while (code.Code != 0)
        //    {
        //        switch (code.Code)
        //        {
        //            case 5:
        //                handle = code.Value;
        //                break;
        //            case 2:
        //                if (string.IsNullOrEmpty(code.Value))
        //                {
        //                    throw new DxfInvalidCodeValueEntityException(code.Code, code.Value, this.file,
        //                                                                 "Invalid value " + code.Value + " in code " + code.Code + " line " + this.fileLine);
        //                }
        //                name = code.Value;
        //                break;
        //            case 3: //descripción del tipo de línea
        //                description = code.Value;
        //                break;
        //            case 73:
        //                //number of segments (not needed)
        //                break;
        //            case 40:
        //                //length of the line type segments (not needed)
        //                break;
        //            case 49:
        //                segments.Add(float.Parse(code.Value));
        //                break;
        //        }
        //        code = this.ReadCodePair();
        //    }

        //    return new LineType(name)
        //    {
        //        Description = description,
        //        Segments = segments,
        //        Handle = handle
        //    };
        //}

        #endregion

        #region text style methods

        //private void ReadTextStyles()
        //{
        //    CodeValuePair code = this.ReadCodePair();
        //    while (code.Value != StringCode.EndTable)
        //    {
        //        if (code.Value == StringCode.TextStyleTable)
        //        {
        //            Debug.Assert(code.Code == 0); //el código 0 indica el inicio de una nueva capa
        //            TextStyle style = this.ReadTextStyle(ref code);
        //            this.textStyles.Add(style.Name, style);
        //        }
        //        else
        //        {
        //            code = this.ReadCodePair();
        //        }
        //    }
        //}

        //private TextStyle ReadTextStyle(ref ParCodigoValor code)
        //{
        //    string handle = string.Empty;
        //    string name = string.Empty;
        //    string font = string.Empty;
        //    bool isVertical = false;
        //    bool isBackward = false;
        //    bool isUpsideDown = false;
        //    float height = 0.0f;
        //    float widthFactor = 0.0f;
        //    float obliqueAngle = 0.0f;

        //    code = this.ReadCodePair();

        //    //leer los datos mientras no encontramos el código 0 que indicaría el final de la capa
        //    while (code.Code != 0)
        //    {
        //        switch (code.Code)
        //        {
        //            case 5:
        //                handle = code.Value;
        //                break;
        //            case 2:
        //                if (string.IsNullOrEmpty(code.Value))
        //                {
        //                    throw new DxfInvalidCodeValueEntityException(code.Code, code.Value, this.file,
        //                                                                 "Invalid value " + code.Value + " in code " + code.Code + " line " + this.fileLine);
        //                }
        //                name = code.Value;
        //                break;
        //            case 3:
        //                if (string.IsNullOrEmpty(code.Value))
        //                {
        //                    throw new DxfInvalidCodeValueEntityException(code.Code, code.Value, this.file,
        //                                                                 "Invalid value " + code.Value + " in code " + code.Code + " line " + this.fileLine);
        //                }
        //                font = code.Value;
        //                break;

        //            case 70:
        //                if (int.Parse(code.Value) == 4)
        //                {
        //                    isVertical = true;
        //                }
        //                break;
        //            case 71:
        //                //orientación texto (normal)
        //                if (int.Parse(code.Value) == 6)
        //                {
        //                    isBackward = true;
        //                    isUpsideDown = true;
        //                }
        //                else if (int.Parse(code.Value) == 2)
        //                {
        //                    isBackward = true;
        //                }
        //                else if (int.Parse(code.Value) == 4)
        //                {
        //                    isUpsideDown = true;
        //                }
        //                break;
        //            case 40:
        //                height = float.Parse(code.Value);
        //                break;
        //            case 41:
        //                widthFactor = float.Parse(code.Value);
        //                break;
        //            case 42:
        //                //last text height used (not aplicable)
        //                break;
        //            case 50:
        //                obliqueAngle = (float.Parse(code.Value));
        //                break;
        //        }
        //        code = this.ReadCodePair();
        //    }

        //    return new TextStyle(name, font)
        //    {
        //        Height = height,
        //        IsBackward = isBackward,
        //        IsUpsideDown = isUpsideDown,
        //        IsVertical = isVertical,
        //        ObliqueAngle = obliqueAngle,
        //        WidthFactor = widthFactor,
        //        Handle = handle
        //    };
        //}

        #endregion

        #region entity methods

        private Arco ReadArco(ref ParCodigoValor cod)
        {
            var arco = new Arco();
            Vector3f centro = Vector3f.Nulo;
            Vector3f normal = Vector3f.UnitarioZ;
            //Dictionary<ApplicationRegistry, XData> xData = new Dictionary<ApplicationRegistry, XData>();
            cod = this.ReadCodePair();
            while (cod.Cod != 0)
            {
                switch (cod.Cod)
                {
                    case 5:
                        arco.Handle = cod.Val;
                        cod = this.ReadCodePair();
                        break;
                    //case 8: //layer code
                    //    arco.Layer = this.GetLayer(cod.Val);
                    //    cod = this.ReadCodePair();
                    //    break;
                    //case 62: //aci color code
                    //    arco.Color = new AciColor(short.Parse(code.Value));
                    //    code = this.ReadCodePair();
                    //    break;
                    //case 6: //type line code
                    //    arc.LineType = this.GetLineType(code.Value);
                    //    code = this.ReadCodePair();
                    //    break;
                    case 10:
                        centro.X = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 20:
                        centro.Y = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 30:
                        centro.Z = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 40:
                        arco.Radio = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 50:
                        arco.AnguloInicio = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 51:
                        arco.AnguloFin = float.Parse(cod.Val);
                        cod= this.ReadCodePair();
                        break;
                    //case 39:
                    //    arc.Thickness = float.Parse(code.Value);
                    //    code = this.ReadCodePair();
                    //    break;
                    case 210:
                        normal.X = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 220:
                        normal.Y = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 230:
                        normal.Z = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    //case 1001:
                    //    XData xDataItem = this.ReadXDataRecord(code.Value, ref code);
                    //    xData.Add(xDataItem.ApplicationRegistry, xDataItem);
                    //    break;
                    default:
                        if (cod.Cod >= 1000 && cod.Cod <= 1071)
                            throw new DxfInvalidCodeValueEntityException(cod.Cod, cod.Val,
                                                                         this.archivo, "The extended data of an entity must start with the application registry code " + this.fileLine);
                        cod = this.ReadCodePair();
                        break;
                }
            }

            //arc.XData = xData;
            arco.Centro = centro;
            arco.Normal = normal;


            Vector3f PuntoInicio = Vector3f.Nulo;
            Vector3f PuntoFin = Vector3f.Nulo;

            PuntoInicio.X = arco.Centro.X + arco.Radio * (float)Math.Cos(MathHelper.DegToRad * arco.AnguloInicio);
            PuntoInicio.Y = arco.Centro.Y + arco.Radio * (float)Math.Sin(MathHelper.DegToRad * arco.AnguloInicio);
            //por ahora queda en duro
            PuntoInicio.Z = 0;

            PuntoFin.X = arco.Centro.X + arco.Radio * (float)Math.Cos(MathHelper.DegToRad * arco.AnguloFin);
            PuntoFin.Y = arco.Centro.Y + arco.Radio * (float)Math.Sin(MathHelper.DegToRad * arco.AnguloFin);
            //por ahora queda en duro
            PuntoFin.Z = 0;

            arco.PuntoInicio = PuntoInicio;
            arco.PuntoFin = PuntoFin;

            return arco;
        }

        //private double toRadian(float degreeAngle)
        //{
        //    double radian, degree;
        //    degree = Convert.ToDouble(degreeAngle);
        //    radian = (degree * Math.PI) / 180;
        //    return radian;
        //}


        private Circulo ReadCirculo(ref ParCodigoValor cod)
        {
            var circulo = new Circulo();
            Vector3f centro = Vector3f.Nulo;
            Vector3f normal = Vector3f.UnitarioZ;
            //Dictionary<ApplicationRegistry, XData> xData = new Dictionary<ApplicationRegistry, XData>();

            cod = this.ReadCodePair();
            while (cod.Cod != 0)
            {
                switch (cod.Cod)
                {
                    case 5:
                        circulo.Handle = cod.Val;
                        cod = this.ReadCodePair();
                        break;
                    //case 8: //layer code
                    //    circle.Layer = this.GetLayer(code.Value);
                    //    code = this.ReadCodePair();
                    //    break;
                    //case 62: //aci color code
                    //    circle.Color = new AciColor(short.Parse(code.Value));
                    //    code = this.ReadCodePair();
                    //    break;
                    //case 6: //type line code
                    //    circle.LineType = this.GetLineType(code.Value);
                    //    code = this.ReadCodePair();
                    //    break;
                    case 10:
                        centro.X = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 20:
                        centro.Y = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 30:
                        centro.Z = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 40:
                        circulo.Radio = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    //case 39:
                    //    circulo.Thickness = float.Parse(cod.Val);
                    //    cod = this.ReadCodePair();
                    //    break;
                    case 210:
                        normal.X = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 220:
                        normal.Y = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 230:
                        normal.Z = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    //case 1001:
                    //    XData xDataItem = this.ReadXDataRecord(code.Value, ref code);
                    //    xData.Add(xDataItem.ApplicationRegistry, xDataItem);
                    //    break;
                    default:
                        if (cod.Cod >= 1000 && cod.Cod <= 1071)
                            throw new DxfInvalidCodeValueEntityException(cod.Cod, cod.Val, this.archivo,
                                                                         "The extended data of an entity must start with the application registry code " + this.fileLine);
                        cod = this.ReadCodePair();
                        break;
                }
            }

            //circulo.XData = xData;
            circulo.Centro = centro;
            circulo.Normal = normal;
            return circulo;
        }

        //private Circle ReadEsfera(ref ParCodigoValor code)
        //{
        //    var circle = new Circle();
        //    Vector3f center = Vector3f.Zero;
        //    Vector3f normal = Vector3f.UnitZ;
        //    Dictionary<ApplicationRegistry, XData> xData = new Dictionary<ApplicationRegistry, XData>();

        //    code = this.ReadCodePair();
        //    while (code.Code != 0)
        //    {
        //        switch (code.Code)
        //        {
        //            case 5:
        //                circle.Handle = code.Value;
        //                code = this.ReadCodePair();
        //                break;
        //            case 8: //layer code
        //                circle.Layer = this.GetLayer(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 62: //aci color code
        //                circle.Color = new AciColor(short.Parse(code.Value));
        //                code = this.ReadCodePair();
        //                break;
        //            case 6: //type line code
        //                circle.LineType = this.GetLineType(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 10:
        //                center.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 20:
        //                center.Y = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 30:
        //                center.Z = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 40:
        //                circle.Radius = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 39:
        //                circle.Thickness = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 210:
        //                normal.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 220:
        //                normal.Y = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 230:
        //                normal.Z = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 1001:
        //                XData xDataItem = this.ReadXDataRecord(code.Value, ref code);
        //                xData.Add(xDataItem.ApplicationRegistry, xDataItem);
        //                break;
        //            default:
        //                if (code.Code >= 1000 && code.Code <= 1071)
        //                    throw new DxfInvalidCodeValueEntityException(code.Code, code.Value, this.file,
        //                                                                 "The extended data of an entity must start with the application registry code " + this.fileLine);
        //                code = this.ReadCodePair();
        //                break;
        //        }
        //    }

        //    circle.XData = xData;
        //    circle.Center = center;
        //    circle.Normal = normal;
        //    return circle;
        //}

        private Elipse ReadElipse(ref ParCodigoValor code)
        {
            var ellipse = new Elipse();
            Vector3f center = Vector3f.Nulo;
            Vector3f axisPoint = Vector3f.Nulo;
            Vector3f normal = Vector3f.UnitarioZ;
            float ratio = 0;
            //Dictionary<ApplicationRegistry, XData> xData = new Dictionary<ApplicationRegistry, XData>();

            code = this.ReadCodePair();
            while (code.Cod != 0)
            {
                switch (code.Cod)
                {
                    case 5:
                        ellipse.Handle = code.Val;
                        code = this.ReadCodePair();
                        break;
                    //case 8: //layer code
                    //    ellipse.Layer = this.GetLayer(code.Val);
                    //    code = this.ReadCodePair();
                    //    break;
                    //case 62: //aci color code
                    //    ellipse.Color = new AciColor(short.Parse(code.Val));
                    //    code = this.ReadCodePair();
                    //    break;
                    //case 6: //type line code
                    //    ellipse.LineType = this.GetLineType(code.Val);
                    //    code = this.ReadCodePair();
                    //    break;
                    case 10:
                        center.X = float.Parse(code.Val);
                        code = this.ReadCodePair();
                        break;
                    case 20:
                        center.Y = float.Parse(code.Val);
                        code = this.ReadCodePair();
                        break;
                    case 30:
                        center.Z = float.Parse(code.Val);
                        code = this.ReadCodePair();
                        break;
                    case 11:
                        axisPoint.X = float.Parse(code.Val);
                        code = this.ReadCodePair();
                        break;
                    case 21:
                        axisPoint.Y = float.Parse(code.Val);
                        code = this.ReadCodePair();
                        break;
                    case 31:
                        axisPoint.Z = float.Parse(code.Val);
                        code = this.ReadCodePair();
                        break;
                    case 40:
                        ratio = float.Parse(code.Val);
                        code = this.ReadCodePair();
                        break;
                    case 41:
                        ellipse.AnguloInicio = (float)(double.Parse(code.Val) * MathHelper.RadToDeg);
                        code = this.ReadCodePair();
                        break;
                    case 42:
                        ellipse.AnguloFin = (float)(double.Parse(code.Val) * MathHelper.RadToDeg);
                        code = this.ReadCodePair();
                        break;
                    case 210:
                        normal.X = float.Parse(code.Val);
                        code = this.ReadCodePair();
                        break;
                    case 220:
                        normal.Y = float.Parse(code.Val);
                        code = this.ReadCodePair();
                        break;
                    case 230:
                        normal.Z = float.Parse(code.Val);
                        code = this.ReadCodePair();
                        break;
                    //case 1001:
                    //    XData xDataItem = this.ReadXDataRecord(code.Value, ref code);
                    //    xData.Add(xDataItem.ApplicationRegistry, xDataItem);
                    //    break;
                    default:
                        if (code.Cod >= 1000 && code.Cod <= 1071)
                            throw new DxfInvalidCodeValueEntityException(code.Cod, code.Val, this.archivo,
                                                                         "The extended data of an entity must start with the application registry code " + this.fileLine);
                        code = this.ReadCodePair();
                        break;
                }
            }

            Vector3d ocsAxisPoint = MathHelper.Transform((Vector3d)axisPoint,
                                                         (Vector3d)normal,
                                                         MathHelper.CoordinateSystem.World,
                                                         MathHelper.CoordinateSystem.Object);
            double rotacion = (float)Vector2d.AngleBetween(Vector2d.UnitarioX, new Vector2d(ocsAxisPoint.X, ocsAxisPoint.Y));
            
            ellipse.EjeMayor = 2 * axisPoint.Modulus();
            ellipse.EjeMenor = ellipse.EjeMayor * ratio;
            ellipse.Rotacion = (float)(rotacion * MathHelper.RadToDeg);
            ellipse.Centro = center;
            ellipse.Normal = normal;
            //ellipse.XData = xData;
            return ellipse;
        }

        private Punto ReadPunto(ref ParCodigoValor cod)
        {
            var punto = new Punto();
            Vector3f ubicacion = Vector3f.Nulo;
            Vector3f normal = Vector3f.UnitarioZ;
            //Dictionary<ApplicationRegistry, XData> xData = new Dictionary<ApplicationRegistry, XData>();

            cod = this.ReadCodePair();
            while (cod.Cod != 0)
            {
                switch (cod.Cod)
                {
                    case 5:
                        punto.Handle = cod.Val;
                        cod = this.ReadCodePair();
                        break;
                    //case 8: //layer code
                    //    punto.Layer = this.GetLayer(cod.Val);
                    //    cod = this.ReadCodePair();
                    //    break;
                    //case 62: //aci color code
                    //    point.Color = new AciColor(short.Parse(code.Value));
                    //    code = this.ReadCodePair();
                    //    break;
                    //case 6: //type line code
                    //    point.LineType = this.GetLineType(code.Value);
                    //    code = this.ReadCodePair();
                    //    break;
                    case 10:
                        ubicacion.X = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 20:
                        ubicacion.Y = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 30:
                        ubicacion.Z = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    //case 39:
                    //    punto.Thickness = float.Parse(cod.Val);
                    //    cod = this.ReadCodePair();
                    //    break;
                    case 210:
                        normal.X = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 220:
                        normal.Y = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 230:
                        normal.Z = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    //case 1001:
                    //    XData xDataItem = this.ReadXDataRecord(code.Value, ref code);
                    //    xData.Add(xDataItem.ApplicationRegistry, xDataItem);
                    //    break;
                    default:
                        if (cod.Cod >= 1000 && cod.Cod <= 1071)
                            throw new DxfInvalidCodeValueEntityException(cod.Cod, cod.Val, this.archivo,
                                                                         "The extended data of an entity must start with the application registry code " + this.fileLine);
                        cod = this.ReadCodePair();
                        break;
                }
            }

            //point.XData = xData;
            punto.Ubicacion = ubicacion;
            punto.Normal = normal;
            return punto;
        }

        //private Face3d ReadFace3D(ref CodeValuePair code)
        //{
        //    var face = new Face3d();
        //    Vector3f v0 = Vector3f.Zero;
        //    Vector3f v1 = Vector3f.Zero;
        //    Vector3f v2 = Vector3f.Zero;
        //    Vector3f v3 = Vector3f.Zero;
        //    Dictionary<ApplicationRegistry, XData> xData = new Dictionary<ApplicationRegistry, XData>();

        //    code = this.ReadCodePair();
        //    while (code.Code != 0)
        //    {
        //        switch (code.Code)
        //        {
        //            case 5:
        //                face.Handle = code.Value;
        //                code = this.ReadCodePair();
        //                break;
        //            case 8: //layer code
        //                face.Layer = this.GetLayer(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 62: //aci color code
        //                face.Color = new AciColor(short.Parse(code.Value));
        //                code = this.ReadCodePair();
        //                break;
        //            case 6: //type line code
        //                face.LineType = this.GetLineType(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 10:
        //                v0.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 20:
        //                v0.Y = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 30:
        //                v0.Z = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 11:
        //                v1.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 21:
        //                v1.Y = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 31:
        //                v1.Z = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 12:
        //                v2.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 22:
        //                v2.Y = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 32:
        //                v2.Z = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 13:
        //                v3.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 23:
        //                v3.Y = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 33:
        //                v3.Z = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 70:
        //                face.EdgeFlags = (EdgeFlags)(int.Parse(code.Value));
        //                code = this.ReadCodePair();
        //                break;
        //            case 1001:
        //                XData xDataItem = this.ReadXDataRecord(code.Value, ref code);
        //                xData.Add(xDataItem.ApplicationRegistry, xDataItem);
        //                break;
        //            default:
        //                if (code.Code >= 1000 && code.Code <= 1071)
        //                    throw new DxfInvalidCodeValueEntityException(code.Code, code.Value, this.file,
        //                                                                 "The extended data of an entity must start with the application registry code " + this.fileLine);

        //                code = this.ReadCodePair();
        //                break;
        //        }
        //    }

        //    face.FirstVertex = v0;
        //    face.SecondVertex = v1;
        //    face.ThirdVertex = v2;
        //    face.FourthVertex = v3;
        //    face.XData = xData;
        //    return face;
        //}

        //private Solid ReadSolid(ref CodeValuePair code)
        //{
        //    var solid = new Solid();
        //    Vector3f v0 = Vector3f.Zero;
        //    Vector3f v1 = Vector3f.Zero;
        //    Vector3f v2 = Vector3f.Zero;
        //    Vector3f v3 = Vector3f.Zero;
        //    Vector3f normal = Vector3f.UnitZ;
        //    Dictionary<ApplicationRegistry, XData> xData = new Dictionary<ApplicationRegistry, XData>();

        //    code = this.ReadCodePair();
        //    while (code.Code != 0)
        //    {
        //        switch (code.Code)
        //        {
        //            case 5:
        //                solid.Handle = code.Value;
        //                code = this.ReadCodePair();
        //                break;
        //            case 8: //layer code
        //                solid.Layer = this.GetLayer(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 62: //aci color code
        //                solid.Color = new AciColor(short.Parse(code.Value));
        //                code = this.ReadCodePair();
        //                break;
        //            case 6: //type line code
        //                solid.LineType = this.GetLineType(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 10:
        //                v0.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 20:
        //                v0.Y = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 30:
        //                v0.Z = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 11:
        //                v1.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 21:
        //                v1.Y = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 31:
        //                v1.Z = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 12:
        //                v2.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 22:
        //                v2.Y = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 32:
        //                v2.Z = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 13:
        //                v3.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 23:
        //                v3.Y = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 33:
        //                v3.Z = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 70:
        //                solid.Thickness = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 210:
        //                normal.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 220:
        //                normal.Y = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 230:
        //                normal.Z = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 1001:
        //                XData xDataItem = this.ReadXDataRecord(code.Value, ref code);
        //                xData.Add(xDataItem.ApplicationRegistry, xDataItem);
        //                break;
        //            default:
        //                if (code.Code >= 1000 && code.Code <= 1071)
        //                    throw new DxfInvalidCodeValueEntityException(code.Code, code.Value, this.file,
        //                                                                 "The extended data of an entity must start with the application registry code " + this.fileLine);

        //                code = this.ReadCodePair();
        //                break;
        //        }
        //    }

        //    solid.FirstVertex = v0;
        //    solid.SecondVertex = v1;
        //    solid.ThirdVertex = v2;
        //    solid.FourthVertex = v3;
        //    solid.Normal = normal;
        //    solid.XData = xData;
        //    return solid;
        //}

        //private Insert ReadInsert(ref CodeValuePair code)
        //{
        //    string handle = string.Empty;
        //    Vector3f basePoint = Vector3f.Zero;
        //    Vector3f normal = Vector3f.UnitZ;
        //    Vector3f scale = new Vector3f(1, 1, 1);
        //    float rotation = 0.0f;
        //    Block block = null;
        //    Layer layer = Layer.Default;
        //    AciColor color = AciColor.ByLayer;
        //    LineType lineType = LineType.ByLayer;
        //    List<Attribute> attributes = new List<Attribute>();
        //    Dictionary<ApplicationRegistry, XData> xData = new Dictionary<ApplicationRegistry, XData>();

        //    code = this.ReadCodePair();
        //    while (code.Code != 0)
        //    {
        //        switch (code.Code)
        //        {
        //            case 5:
        //                handle = code.Value;
        //                code = this.ReadCodePair();
        //                break;
        //            case 2:
        //                block = this.GetBlock(code.Value);
        //                if (block == null)
        //                    throw new DxfEntityException(DxfObjectCode.Insert, this.file, "Block " + code.Value + " not defined line " + this.fileLine);
        //                code = this.ReadCodePair();
        //                break;
        //            case 8: //layer code
        //                layer = this.GetLayer(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 62: //aci color code
        //                color = new AciColor(short.Parse(code.Value));
        //                code = this.ReadCodePair();
        //                break;
        //            case 6: //type line code
        //                lineType = this.GetLineType(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 10:
        //                basePoint.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 20:
        //                basePoint.Y = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 30:
        //                basePoint.Z = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 41:
        //                scale.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 42:
        //                scale.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 43:
        //                scale.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 50:
        //                rotation = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 210:
        //                normal.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 220:
        //                normal.Y = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 230:
        //                normal.Z = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 1001:
        //                XData xDataItem = this.ReadXDataRecord(code.Value, ref code);
        //                xData.Add(xDataItem.ApplicationRegistry, xDataItem);
        //                break;
        //            default:
        //                if (code.Code >= 1000 && code.Code <= 1071)
        //                    throw new DxfInvalidCodeValueEntityException(code.Code, code.Value, this.file,
        //                                                                 "The extended data of an entity must start with the application registry code " + this.fileLine);

        //                code = this.ReadCodePair();
        //                break;
        //        }
        //    }

        //    // if there are attributes
        //    string endSequenceHandle = string.Empty;
        //    Layer endSequenceLayer = Layer.Default;
        //    if (code.Value == DxfObjectCode.Attribute)
        //    {
        //        while (code.Value != StringCode.EndSequence)
        //        {
        //            if (code.Value == DxfObjectCode.Attribute)
        //            {
        //                Debug.Assert(code.Code == 0);
        //                Attribute attribute = this.ReadAttribute(block, ref code);
        //                attributes.Add(attribute);
        //            }
        //        }
        //        // read the end end sequence object until a new element is found
        //        code = this.ReadCodePair();
        //        while (code.Code != 0)
        //        {
        //            switch (code.Code)
        //            {
        //                case 5:
        //                    endSequenceHandle = code.Value;
        //                    code = this.ReadCodePair();
        //                    break;
        //                case 8:
        //                    endSequenceLayer = this.GetLayer(code.Value);
        //                    code = this.ReadCodePair();
        //                    break;
        //                default:
        //                    code = this.ReadCodePair();
        //                    break;
        //            }
        //        }
        //    }

        //    Insert insert = new Insert(block)
        //    {
        //        Color = color,
        //        Layer = layer,
        //        LineType = lineType,
        //        InsertionPoint = basePoint,
        //        Rotation = rotation,
        //        Scale = scale,
        //        Normal = normal,
        //        Handle = handle
        //    };

        //    insert.EndSequence.Handle = endSequenceHandle;
        //    insert.EndSequence.Layer = endSequenceLayer;
        //    insert.Attributes.Clear();
        //    insert.Attributes.AddRange(attributes);
        //    insert.XData = xData;

        //    return insert;
        //}

        private Linea ReadLinea(ref ParCodigoValor cod)
        {
            var linea = new Linea();
            Vector3f inicio = Vector3f.Nulo;
            Vector3f fin = Vector3f.Nulo;
            Vector3f normal = Vector3f.UnitarioZ ;
            //Dictionary<ApplicationRegistry, XData> xData = new Dictionary<ApplicationRegistry, XData>();

            cod = this.ReadCodePair();
            while (cod.Cod != 0)
            {
                switch (cod.Cod)
                {
                    case 5:
                        linea.Handle = cod.Val;
                        cod = this.ReadCodePair();
                        break;
                    //case 8: //layer code
                    //    linea.Layer = this.GetLayer(cod.Val);
                    //    cod = this.ReadCodePair();
                    //    break;
                    //case 62: //aci color code
                    //    linea.Color = new AciColor(short.Parse(cod.Val));
                    //    cod = this.ReadCodePair();
                    //    break;
                    //case 6: //type line code
                    //    linea.LineType = this.GetLineType(cod.Val);
                    //    cod = this.ReadCodePair();
                    //    break;
                    case 10:
                        inicio.X = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 20:
                        inicio.Y = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 30:
                        inicio.Z = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 11:
                        fin.X = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 21:
                        fin.Y = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 31:
                        fin.Z = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    //case 39:
                    //    linea.Thickness = float.Parse(cod.Val);
                    //    cod = this.ReadCodePair();
                    //    break;
                    case 210:
                        normal.X = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 220:
                        normal.Y = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 230:
                        normal.Z = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    //case 1001:
                    //    XData xDataItem = this.ReadXDataRecord(cod.Val, ref cod);
                    //    xData.Add(xDataItem.ApplicationRegistry, xDataItem);
                    //    break;
                    default:
                        if (cod.Cod >= 1000 && cod.Cod <= 1071)
                            throw new DxfInvalidCodeValueEntityException(cod.Cod, cod.Val, this.archivo,
                                                                         "The extended data of an entity must start with the application registry code " + this.fileLine);

                        cod = this.ReadCodePair();
                        break;
                }
            }

            linea.PuntoInicio  = inicio;
            linea.PuntoFinal = fin;
            linea.Normal = normal;
            //linea.XData = xData;

            return linea;
        }

        //private LightWeightPolyline ReadLightWeightPolyline(ref CodeValuePair code)
        //{
        //    var pol = new LightWeightPolyline();
        //    //int numVertexes;
        //    float constantWidth = 0.0F;
        //    LightWeightPolylineVertex v = new LightWeightPolylineVertex();
        //    float vX = 0.0f;
        //    Vector3f normal = Vector3f.UnitZ;
        //    Dictionary<ApplicationRegistry, XData> xData = new Dictionary<ApplicationRegistry, XData>();

        //    code = this.ReadCodePair();

        //    while (code.Code != 0)
        //    {
        //        switch (code.Code)
        //        {
        //            case 5:
        //                pol.Handle = code.Value;
        //                code = this.ReadCodePair();
        //                break;
        //            case 8:
        //                pol.Layer = this.GetLayer(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 62:
        //                pol.Color = new AciColor(short.Parse(code.Value));
        //                code = this.ReadCodePair();
        //                break;
        //            case 6:
        //                pol.LineType = this.GetLineType(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 38:
        //                pol.Elevation = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 39:
        //                pol.Thickness = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 43:
        //                constantWidth = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 70:
        //                if (int.Parse(code.Value) == 0)
        //                {
        //                    pol.IsClosed = false;
        //                }
        //                else if (int.Parse(code.Value) == 1)
        //                {
        //                    pol.IsClosed = true;
        //                }
        //                code = this.ReadCodePair();
        //                break;
        //            case 90:
        //                //numVertexes = int.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 10:
        //                v = new LightWeightPolylineVertex
        //                {
        //                    BeginThickness = constantWidth,
        //                    EndThickness = constantWidth
        //                };
        //                vX = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 20:
        //                float vY = float.Parse(code.Value);
        //                v.Location = new Vector2f(vX, vY);
        //                pol.Vertexes.Add(v);
        //                code = this.ReadCodePair();
        //                break;
        //            case 40:
        //                v.BeginThickness = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 41:
        //                v.EndThickness = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 42:
        //                v.Bulge = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 210:
        //                normal.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 220:
        //                normal.Y = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 230:
        //                normal.Z = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 1001:
        //                XData xDataItem = this.ReadXDataRecord(code.Value, ref code);
        //                xData.Add(xDataItem.ApplicationRegistry, xDataItem);
        //                break;
        //            default:
        //                if (code.Code >= 1000 && code.Code <= 1071)
        //                    throw new DxfInvalidCodeValueEntityException(code.Code, code.Value, this.file,
        //                                                                 "The extended data of an entity must start with the application registry code " + this.fileLine);

        //                code = this.ReadCodePair();
        //                break;
        //        }
        //    }

        //    pol.Normal = normal;
        //    pol.XData = xData;

        //    return pol;
        //}

        //private IPolyline ReadPolyline(ref CodeValuePair code)
        //{
        //    string handle = string.Empty;
        //    Layer layer = Layer.Default;
        //    AciColor color = AciColor.ByLayer;
        //    LineType lineType = LineType.ByLayer;
        //    PolylineTypeFlags flags = PolylineTypeFlags.OpenPolyline;
        //    float elevation = 0.0f;
        //    float thickness = 0.0f;
        //    Vector3f normal = Vector3f.UnitZ;
        //    List<Vertex> vertexes = new List<Vertex>();
        //    Dictionary<ApplicationRegistry, XData> xData = new Dictionary<ApplicationRegistry, XData>();
        //    //int numVertexes = -1;
        //    //int numFaces = -1;

        //    code = this.ReadCodePair();

        //    while (code.Code != 0)
        //    {
        //        switch (code.Code)
        //        {
        //            case 5:
        //                handle = code.Value;
        //                code = this.ReadCodePair();
        //                break;
        //            case 8:
        //                layer = this.GetLayer(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 62:
        //                color = new AciColor(short.Parse(code.Value));
        //                code = this.ReadCodePair();
        //                break;
        //            case 6:
        //                lineType = this.GetLineType(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 30:
        //                elevation = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 39:
        //                thickness = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 70:
        //                flags = (PolylineTypeFlags)(int.Parse(code.Value));
        //                code = this.ReadCodePair();
        //                break;
        //            case 71:
        //                //this field might not exist for polyface meshes, we cannot depend on it
        //                //numVertexes = int.Parse(code.Value); code = this.ReadCodePair();
        //                code = this.ReadCodePair();
        //                break;
        //            case 72:
        //                //this field might not exist for polyface meshes, we cannot depend on it
        //                //numFaces  = int.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 210:
        //                normal.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 220:
        //                normal.Y = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 230:
        //                normal.Z = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 1001:
        //                XData xDataItem = this.ReadXDataRecord(code.Value, ref code);
        //                xData.Add(xDataItem.ApplicationRegistry, xDataItem);
        //                break;
        //            default:
        //                if (code.Code >= 1000 && code.Code <= 1071)
        //                    throw new DxfInvalidCodeValueEntityException(code.Code, code.Value, this.file,
        //                                                                 "The extended data of an entity must start with the application registry code " + this.fileLine);

        //                code = this.ReadCodePair();
        //                break;
        //        }
        //    }

        //    //begin to read the vertex list
        //    if (code.Value != DxfObjectCode.Vertex)
        //        throw new DxfEntityException(DxfObjectCode.Polyline, this.file, "Vertex not found in line " + this.fileLine);
        //    while (code.Value != StringCode.EndSequence)
        //    {
        //        if (code.Value == DxfObjectCode.Vertex)
        //        {
        //            Debug.Assert(code.Code == 0);
        //            Vertex vertex = this.ReadVertex(ref code);
        //            vertexes.Add(vertex);
        //        }
        //    }

        //    // read the end end sequence object until a new element is found
        //    if (code.Value != StringCode.EndSequence)
        //        throw new DxfEntityException(DxfObjectCode.Polyline, this.file, "End sequence entity not found in line " + this.fileLine);
        //    code = this.ReadCodePair();
        //    string endSequenceHandle = string.Empty;
        //    Layer endSequenceLayer = layer;
        //    while (code.Code != 0)
        //    {
        //        switch (code.Code)
        //        {
        //            case 5:
        //                endSequenceHandle = code.Value;
        //                code = this.ReadCodePair();
        //                break;
        //            case 8:
        //                endSequenceLayer = this.GetLayer(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            default:
        //                code = this.ReadCodePair();
        //                break;
        //        }
        //    }

        //    IPolyline pol;
        //    bool isClosed = false;

        //    if ((flags & PolylineTypeFlags.ClosedPolylineOrClosedPolygonMeshInM) == PolylineTypeFlags.ClosedPolylineOrClosedPolygonMeshInM)
        //    {
        //        isClosed = true;
        //    }

        //    //to avoid possible error between the vertex type and the polyline type
        //    //the polyline type will decide which information to use from the read vertex
        //    if ((flags & PolylineTypeFlags.Polyline3D) == PolylineTypeFlags.Polyline3D)
        //    {
        //        List<Polyline3dVertex> polyline3dVertexes = new List<Polyline3dVertex>();
        //        foreach (Vertex v in vertexes)
        //        {
        //            Polyline3dVertex vertex = new Polyline3dVertex
        //            {
        //                Color = v.Color,
        //                Layer = v.Layer,
        //                LineType = v.LineType,
        //                Location = v.Location,
        //                Handle = v.Handle
        //            };
        //            vertex.XData = v.XData;
        //            polyline3dVertexes.Add(vertex);
        //        }

              
        //        pol = new Polyline3d(polyline3dVertexes, isClosed)
        //        {
        //            Handle = handle
        //        };
        //        ((Polyline3d)pol).EndSequence.Handle = endSequenceHandle;
        //        ((Polyline3d)pol).EndSequence.Layer = endSequenceLayer;
        //    }
        //    else if ((flags & PolylineTypeFlags.PolyfaceMesh) == PolylineTypeFlags.PolyfaceMesh)
        //    {
        //        //the vertex list created contains vertex and face information
        //        List<PolyfaceMeshVertex> polyfaceVertexes = new List<PolyfaceMeshVertex>();
        //        List<PolyfaceMeshFace> polyfaceFaces = new List<PolyfaceMeshFace>();
        //        foreach (Vertex v in vertexes)
        //        {
        //            if ((v.Flags & (VertexTypeFlags.PolyfaceMeshVertex | VertexTypeFlags.Polygon3dMesh)) == (VertexTypeFlags.PolyfaceMeshVertex | VertexTypeFlags.Polygon3dMesh))
        //            {
        //                PolyfaceMeshVertex vertex = new PolyfaceMeshVertex
        //                {
        //                    Color = v.Color,
        //                    Layer = v.Layer,
        //                    LineType = v.LineType,
        //                    Location = v.Location,
        //                    Handle = v.Handle
        //                };
        //                vertex.XData = xData;
        //                polyfaceVertexes.Add(vertex);
        //            }
        //            else if ((v.Flags & (VertexTypeFlags.PolyfaceMeshVertex)) == (VertexTypeFlags.PolyfaceMeshVertex))
        //            {
        //                PolyfaceMeshFace vertex = new PolyfaceMeshFace
        //                {
        //                    Color = v.Color,
        //                    Layer = v.Layer,
        //                    LineType = v.LineType,
        //                    VertexIndexes = v.VertexIndexes,
        //                    Handle = v.Handle
        //                };
        //                vertex.XData = xData;
        //                polyfaceFaces.Add(vertex);
        //            }

                   
        //        }
        //        pol = new PolyfaceMesh(polyfaceVertexes, polyfaceFaces)
        //        {
        //            Handle = handle
        //        };
        //        ((PolyfaceMesh)pol).EndSequence.Handle = endSequenceHandle;
        //        ((PolyfaceMesh)pol).EndSequence.Layer = endSequenceLayer;
        //    }
        //    else
        //    {
        //        List<PolylineVertex> polylineVertexes = new List<PolylineVertex>();
        //        foreach (Vertex v in vertexes)
        //        {
        //            PolylineVertex vertex = new PolylineVertex
        //            {
        //                Location = new Vector2f(v.Location.X, v.Location.Y),
        //                BeginThickness = v.BeginThickness,
        //                Bulge = v.Bulge,
        //                Color = v.Color,
        //                EndThickness = v.EndThickness,
        //                Layer = v.Layer,
        //                LineType = v.LineType,
        //                Handle = v.Handle
        //            };
        //            vertex.XData = xData;

                   
        //            polylineVertexes.Add(vertex);
        //        }

        //        pol = new Polyline(polylineVertexes, isClosed)
        //        {
        //            Thickness = thickness,
        //            Elevation = elevation,
        //            Normal = normal,
        //            Handle = handle
        //        };
        //        ((Polyline)pol).EndSequence.Handle = endSequenceHandle;
        //        ((Polyline)pol).EndSequence.Layer = endSequenceLayer;
        //    }

        //    pol.Color = color;
        //    pol.Layer = layer;
        //    pol.LineType = lineType;
        //    pol.XData = xData;

        //    return pol;
        //}

        //private Text ReadText(ref ParCodigoValor  code)
        //{
        //    var text = new Text();

        //    Vector3f firstAlignmentPoint = Vector3f.Zero;
        //    Vector3f secondAlignmentPoint = Vector3f.Zero;
        //    Vector3f normal = Vector3f.UnitZ;
        //    int horizontalAlignment = 0;
        //    int verticalAlignment = 0;
        //    Dictionary<ApplicationRegistry, XData> xData = new Dictionary<ApplicationRegistry, XData>();

        //    code = this.ReadCodePair();
        //    while (code.Code != 0)
        //    {
        //        switch (code.Code)
        //        {
        //            case 5:
        //                text.Handle = code.Value;
        //                code = this.ReadCodePair();
        //                break;
        //            case 1:
        //                text.Value = code.Value;
        //                code = this.ReadCodePair();
        //                break;
        //            case 8: //layer code
        //                text.Layer = this.GetLayer(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 62: //aci color code
        //                text.Color = new AciColor(short.Parse(code.Value));
        //                code = this.ReadCodePair();
        //                break;
        //            case 6: //type line code
        //                text.LineType = this.GetLineType(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 10:
        //                firstAlignmentPoint.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 20:
        //                firstAlignmentPoint.Y = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 30:
        //                firstAlignmentPoint.Z = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 11:
        //                secondAlignmentPoint.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 21:
        //                secondAlignmentPoint.Y = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 31:
        //                secondAlignmentPoint.Z = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 40:
        //                text.Height = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 41:
        //                text.WidthFactor = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 50:
        //                text.Rotation = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 51:
        //                text.ObliqueAngle = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 7:
        //                text.Style = this.GetTextStyle(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 72:
        //                horizontalAlignment = int.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 73:
        //                verticalAlignment = int.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 210:
        //                normal.X = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 220:
        //                normal.Y = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 230:
        //                normal.Z = float.Parse(code.Value);
        //                code = this.ReadCodePair();
        //                break;
        //            case 1001:
        //                XData xDataItem = this.ReadXDataRecord(code.Value, ref code);
        //                xData.Add(xDataItem.ApplicationRegistry, xDataItem);
        //                break;
        //            default:
        //                if (code.Code >= 1000 && code.Code <= 1071)
        //                    throw new DxfInvalidCodeValueEntityException(code.Code, code.Value, this.file,
        //                                                                 "The extended data of an entity must start with the application registry code " + this.fileLine);

        //                code = this.ReadCodePair();
        //                break;
        //        }
        //    }

        //    TextAlignment alignment = ObtainAlignment(horizontalAlignment, verticalAlignment);

        //    text.BasePoint = alignment == TextAlignment.BaselineLeft ? firstAlignmentPoint : secondAlignmentPoint;
        //    text.Normal = normal;
        //    text.Alignment = alignment;
        //    text.XData = xData;

        //    return text;
        //}

        //private Vertex ReadVertex(ref ParCodigoValor  cod)
        //{
        //    string handle = string.Empty;
        //    Layer layer = Layer.Default;
        //    AciColor color = AciColor.ByLayer;
        //    LineType lineType = LineType.ByLayer;
        //    Vector3f location = new Vector3f();
        //    Dictionary<ApplicationRegistry, XData> xData = new Dictionary<ApplicationRegistry, XData>();
        //    float endThickness = 0.0f;
        //    float beginThickness = 0.0f;
        //    float bulge = 0.0f;
        //    List<int> vertexIndexes = new List<int>();
        //    VertexTypeFlags flags = VertexTypeFlags.PolylineVertex;

        //    cod = this.ReadCodePair();

        //    while (cod.Cod != 0)
        //    {
        //        switch (cod.Cod)
        //        {
        //            case 5:
        //                handle = cod.Val;
        //                cod = this.ReadCodePair();
        //                break;
        //            case 8:
        //                layer = this.GetLayer(cod.Val);
        //                cod = this.ReadCodePair();
        //                break;
        //            case 62:
        //                color = new AciColor(short.Parse(cod.Val));
        //                cod = this.ReadCodePair();
        //                break;
        //            case 6:
        //                lineType = this.GetLineType(code.Val);
        //                cod = this.ReadCodePair();
        //                break;
        //            case 10:
        //                location.X = float.Parse(cod.Val);
        //                cod = this.ReadCodePair();
        //                break;
        //            case 20:
        //                location.Y = float.Parse(cod.Val);
        //                cod = this.ReadCodePair();
        //                break;
        //            case 30:
        //                location.Z = float.Parse(cod.Val);
        //                cod = this.ReadCodePair();
        //                break;
        //            case 40:
        //                beginThickness = float.Parse(cod.Val);
        //                cod = this.ReadCodePair();
        //                break;
        //            case 41:
        //                endThickness = float.Parse(cod.Val);
        //                cod = this.ReadCodePair();
        //                break;
        //            case 42:
        //                bulge = float.Parse(cod.Val);
        //                cod = this.ReadCodePair();
        //                break;
        //            case 70:
        //                flags = (VertexTypeFlags)int.Parse(cod.Val);
        //                cod = this.ReadCodePair();
        //                break;
        //            case 71:
        //                vertexIndexes.Add(int.Parse(cod.Val));
        //                cod = this.ReadCodePair();
        //                break;
        //            case 72:
        //                vertexIndexes.Add(int.Parse(cod.Val));
        //                cod = this.ReadCodePair();
        //                break;
        //            case 73:
        //                vertexIndexes.Add(int.Parse(cod.Val));
        //                cod = this.ReadCodePair();
        //                break;
        //            case 74:
        //                vertexIndexes.Add(int.Parse(cod.Val));
        //                cod = this.ReadCodePair();
        //                break;
        //            case 1001:
        //                XData xDataItem = this.ReadXDataRecord(cod.Val, ref cod);
        //                xData.Add(xDataItem.ApplicationRegistry, xDataItem);
        //                break;
        //            default:
        //                if (cod.Cod >= 1000 && cod.Cod <= 1071)
        //                    throw new DxfInvalidCodeValueEntityException(cod.Cod, cod.Val, this.archivo,
        //                                                                 "The extended data of an entity must start with the application registry code " + this.fileLine);

        //                cod = this.ReadCodePair();
        //                break;
        //        }
        //    }

        //    return new Vertex
        //    {
        //        Flags = flags,
        //        Location = location,
        //        BeginThickness = beginThickness,
        //        Bulge = bulge,
        //        Color = color,
        //        EndThickness = endThickness,
        //        Layer = layer,
        //        LineType = lineType,
        //        VertexIndexes = vertexIndexes.ToArray(),
        //        XData = xData,
        //        Handle = handle
        //    };

            
        //}

        private LightWeightPolyline ReadLightWeightPolyline(ref ParCodigoValor cod)
        {
            var pol = new LightWeightPolyline();
            //int numVertexes;
            float constantWidth = 0.0F;
            LightWeightPolylineVertex v = new LightWeightPolylineVertex();
            float vX = 0.0f;
            Vector3f normal = Vector3f.UnitarioZ;
            //Dictionary<ApplicationRegistry, XData> xData = new Dictionary<ApplicationRegistry, XData>();

            cod = this.ReadCodePair();

            while (cod.Cod != 0)
            {
                switch (cod.Cod)
                {
                    case 5:
                        pol.Handle = cod.Val;
                        cod = this.ReadCodePair();
                        break;
                    //case 8:
                    //    pol.Layer = this.GetLayer(code.Value);
                    //    code = this.ReadCodePair();
                    //    break;
                    //case 62:
                    //    pol.Color = new AciColor(short.Parse(code.Value));
                    //    code = this.ReadCodePair();
                    //    break;
                    //case 6:
                    //    pol.LineType = this.GetLineType(code.Value);
                    //    code = this.ReadCodePair();
                    //    break;
                    case 38:
                        pol.Elevation = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 39:
                        pol.Thickness = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 43:
                        constantWidth = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 70:
                        if (int.Parse(cod.Val) == 0)
                        {
                            pol.IsClosed = false;
                        }
                        else if (int.Parse(cod.Val) == 1)
                        {
                            pol.IsClosed = true;
                        }
                        cod = this.ReadCodePair();
                        break;
                    case 90:
                        //numVertexes = int.Parse(code.Value);
                        cod = this.ReadCodePair();
                        break;
                    case 10:
                        v = new LightWeightPolylineVertex
                        {
                            BeginThickness = constantWidth,
                            EndThickness = constantWidth
                        };
                        vX = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 20:
                        float vY = float.Parse(cod.Val);
                        v.Location = new Vector2f(vX, vY);
                        pol.Vertexes.Add(v);
                        cod = this.ReadCodePair();
                        break;
                    case 40:
                        v.BeginThickness = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 41:
                        v.EndThickness = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 42:
                        v.Bulge = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 210:
                        normal.X = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 220:
                        normal.Y = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 230:
                        normal.Z = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    //case 1001:
                    //    XData xDataItem = this.ReadXDataRecord(cod.Val, ref cod);
                    //    xData.Add(xDataItem.ApplicationRegistry, xDataItem);
                    //    break;
                    default:
                        if (cod.Cod >= 1000 && cod.Cod <= 1071)
                            throw new DxfInvalidCodeValueEntityException(cod.Cod, cod.Val, this.archivo,
                                                                         "The extended data of an entity must start with the application registry code " + this.fileLine);

                        cod = this.ReadCodePair();
                        break;
                }
            }

            pol.Normal = normal;
            //pol.XData = xData;

            return pol;
        }

        private IPolilinea ReadPolilinea(ref ParCodigoValor cod)
        {
            string handle = string.Empty;
            //Layer layer = Layer.Default;
            //AciColor color = AciColor.ByLayer;
            //LineType lineType = LineType.ByLayer;
            PolylineTypeFlags flags = PolylineTypeFlags.OpenPolyline;
            float elevation = 0.0f;
            float thickness = 0.0f;
            Vector3f normal = Vector3f.UnitarioZ;
            List<Vertex> vertexes = new List<Vertex>();
            //Dictionary<ApplicationRegistry, XData> xData = new Dictionary<ApplicationRegistry, XData>();
            //int numVertexes = -1;
            //int numFaces = -1;

            cod = this.ReadCodePair();

            while (cod.Cod != 0)
            {
                switch (cod.Cod)
                {
                    case 5:
                        handle = cod.Val;
                        cod = this.ReadCodePair();
                        break;
                    //case 8:
                    //    layer = this.GetLayer(cod.Value);
                    //    cod = this.ReadCodePair();
                    //    break;
                    //case 62:
                    //    color = new AciColor(short.Parse(code.Value));
                    //    code = this.ReadCodePair();
                    //    break;
                    //case 6:
                    //    lineType = this.GetLineType(code.Value);
                    //    code = this.ReadCodePair();
                    //    break;
                    case 30:
                        elevation = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 39:
                        thickness = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 70:
                        flags = (PolylineTypeFlags)(int.Parse(cod.Val));
                        cod = this.ReadCodePair();
                        break;
                    case 71:
                        //this field might not exist for polyface meshes, we cannot depend on it
                        //numVertexes = int.Parse(code.Value); code = this.ReadCodePair();
                        cod = this.ReadCodePair();
                        break;
                    case 72:
                        //this field might not exist for polyface meshes, we cannot depend on it
                        //numFaces  = int.Parse(code.Value);
                        cod = this.ReadCodePair();
                        break;
                    case 210:
                        normal.X = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 220:
                        normal.Y = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 230:
                        normal.Z = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    //case 1001:
                    //    XData xDataItem = this.ReadXDataRecord(code.Value, ref code);
                    //    xData.Add(xDataItem.ApplicationRegistry, xDataItem);
                    //    break;
                    default:
                        if (cod.Cod >= 1000 && cod.Cod <= 1071)
                            throw new DxfInvalidCodeValueEntityException(cod.Cod, cod.Val, this.archivo,
                                                                         "The extended data of an entity must start with the application registry code " + this.fileLine);

                        cod = this.ReadCodePair();
                        break;
                }
            }

            //begin to read the vertex list
            if (cod.Val != DxfCodigoObjeto.Vertex)
                throw new DxfEntityException(DxfCodigoObjeto.Polilinea, this.archivo, "Vertex not found in line " + this.fileLine);
            while (cod.Val != StringCode.EndSequence)
            {
                if (cod.Val == DxfCodigoObjeto.Vertex)
                {
                    Debug.Assert(cod.Cod == 0);
                    Vertex vertex = this.ReadVertex(ref cod);
                    vertexes.Add(vertex);
                }
            }

            // read the end end sequence object until a new element is found
            if (cod.Val != StringCode.EndSequence)
                throw new DxfEntityException(DxfCodigoObjeto.Polilinea, this.archivo, "End sequence entity not found in line " + this.fileLine);
            cod = this.ReadCodePair();
            string endSequenceHandle = string.Empty;
            //Layer endSequenceLayer = layer;
            while (cod.Cod != 0)
            {
                switch (cod.Cod)
                {
                    case 5:
                        endSequenceHandle = cod.Val;
                        cod = this.ReadCodePair();
                        break;
                    //case 8:
                    //    endSequenceLayer = this.GetLayer(code.Value);
                    //    code = this.ReadCodePair();
                    //    break;
                    default:
                        cod = this.ReadCodePair();
                        break;
                }
            }

            IPolilinea pol;
            bool isClosed = false;

            if ((flags & PolylineTypeFlags.ClosedPolylineOrClosedPolygonMeshInM) == PolylineTypeFlags.ClosedPolylineOrClosedPolygonMeshInM)
            {
                isClosed = true;
            }

            //to avoid possible error between the vertex type and the polyline type
            //the polyline type will decide which information to use from the read vertex
            if ((flags & PolylineTypeFlags.Polyline3D) == PolylineTypeFlags.Polyline3D)
            {
                List<Polyline3dVertex> polyline3dVertexes = new List<Polyline3dVertex>();
                foreach (Vertex v in vertexes)
                {
                    Polyline3dVertex vertex = new Polyline3dVertex
                    {
                        //Color = v.Color,
                        //Layer = v.Layer,
                        //LineType = v.LineType,
                        Location = v.Location,
                        Handle = v.Handle
                    };
                    //vertex.XData = v.XData;
                    polyline3dVertexes.Add(vertex);
                }

                ////posible error avoidance, the polyline is marked as polyline3d code:(70,8) but the vertex is marked as PolylineVertex code:(70,0)
                //if (v.Type == EntityType.PolylineVertex)
                //{
                //    Polyline3dVertex polyline3dVertex = new Polyline3dVertex(((PolylineVertex)v).Location.X, ((PolylineVertex)v).Location.Y,0);
                //    polyline3dVertexes.Add(polyline3dVertex);
                //}
                //else
                //{
                //    polyline3dVertexes.Add((Polyline3dVertex)v);
                //}
                //}
                pol = new Polyline3d(polyline3dVertexes, isClosed)
                {
                    Handle = handle
                };
                //((Polyline3d)pol).EndSequence.Handle = endSequenceHandle;
                //((Polyline3d)pol).EndSequence.Layer = endSequenceLayer;
            }
            else if ((flags & PolylineTypeFlags.PolyfaceMesh) == PolylineTypeFlags.PolyfaceMesh)
            {
                //the vertex list created contains vertex and face information
                List<PolyfaceMeshVertex> polyfaceVertexes = new List<PolyfaceMeshVertex>();
                List<PolyfaceMeshFace> polyfaceFaces = new List<PolyfaceMeshFace>();
                foreach (Vertex v in vertexes)
                {
                    if ((v.Flags & (VertexTypeFlags.PolyfaceMeshVertex | VertexTypeFlags.Polygon3dMesh)) == (VertexTypeFlags.PolyfaceMeshVertex | VertexTypeFlags.Polygon3dMesh))
                    {
                        PolyfaceMeshVertex vertex = new PolyfaceMeshVertex
                        {
                            //Color = v.Color,
                            //Layer = v.Layer,
                            //LineType = v.LineType,
                            Location = v.Location,
                            Handle = v.Handle
                        };
                        //vertex.XData = xData;
                        polyfaceVertexes.Add(vertex);
                    }
                    else if ((v.Flags & (VertexTypeFlags.PolyfaceMeshVertex)) == (VertexTypeFlags.PolyfaceMeshVertex))
                    {
                        PolyfaceMeshFace vertex = new PolyfaceMeshFace
                        {
                            //Color = v.Color,
                            //Layer = v.Layer,
                            //LineType = v.LineType,
                            VertexIndexes = v.VertexIndexes,
                            Handle = v.Handle
                        };
                        //vertex.XData = xData;
                        polyfaceFaces.Add(vertex);
                    }

                    //if (v.Type == EntityType.PolyfaceMeshVertex)
                    //{
                    //    polyfaceVertexes.Add((PolyfaceMeshVertex) v);
                    //}
                    //else if (v.Type == EntityType.PolyfaceMeshFace)
                    //{
                    //    polyfaceFaces.Add((PolyfaceMeshFace) v);
                    //}
                    //else
                    //{
                    //    throw new EntityDxfException(v.Type.ToString(), this.file, "Error in vertex type.");
                    //}
                }
                pol = new PolyfaceMesh(polyfaceVertexes, polyfaceFaces)
                {
                    Handle = handle
                };
                //((PolyfaceMesh)pol).EndSequence.Handle = endSequenceHandle;
                //((PolyfaceMesh)pol).EndSequence.Layer = endSequenceLayer;
            }
            else
            {
                List<PolylineVertex> polylineVertexes = new List<PolylineVertex>();
                foreach (Vertex v in vertexes)
                {
                    PolylineVertex vertex = new PolylineVertex
                    {
                        Location = new Vector2f(v.Location.X, v.Location.Y),
                        BeginThickness = v.BeginThickness,
                        Bulge = v.Bulge,
                        //Color = v.Color,
                        EndThickness = v.EndThickness,
                        //Layer = v.Layer,
                        //LineType = v.LineType,
                        Handle = v.Handle
                    };
                    //vertex.XData = xData;

                    ////posible error avoidance, the polyline is marked as polyline code:(70,0) but the vertex is marked as Polyline3dVertex code:(70,32)
                    //if (v.Type==EntityType.Polyline3dVertex)
                    //{
                    //    PolylineVertex polylineVertex = new PolylineVertex(((Polyline3dVertex)v).Location.X, ((Polyline3dVertex)v).Location.Y);
                    //    polylineVertexes.Add(polylineVertex);
                    //}
                    //else
                    //{
                    //    polylineVertexes.Add((PolylineVertex) v);
                    //}
                    polylineVertexes.Add(vertex);
                }

                pol = new Polilinea(polylineVertexes, isClosed)
                {
                    //Thickness = thickness,
                    Elevation = elevation,
                    Normal = normal,
                    Handle = handle
                };
                //((Polilinea)pol).EndSequence.Handle = endSequenceHandle;
                //((Polilinea)pol).EndSequence.Layer = endSequenceLayer;
            }

            //pol.Color = color;
            //pol.Layer = layer;
            //pol.LineType = lineType;
            //pol.XData = xData;

            return pol;
        }

        private Vertex ReadVertex(ref ParCodigoValor cod)
        {
            string handle = string.Empty;
            //Layer layer = Layer.Default;
            //AciColor color = AciColor.ByLayer;
            //LineType lineType = LineType.ByLayer;
            Vector3f location = new Vector3f();
            //Dictionary<ApplicationRegistry, XData> xData = new Dictionary<ApplicationRegistry, XData>();
            float endThickness = 0.0f;
            float beginThickness = 0.0f;
            float bulge = 0.0f;
            List<int> vertexIndexes = new List<int>();
            VertexTypeFlags flags = VertexTypeFlags.PolylineVertex;

            cod = this.ReadCodePair();

            while (cod.Cod != 0)
            {
                switch (cod.Cod)
                {
                    case 5:
                        handle = cod.Val;
                        cod = this.ReadCodePair();
                        break;
                    //case 8:
                    //    layer = this.GetLayer(cod.Val);
                    //    code = this.ReadCodePair();
                    //    break;
                    //case 62:
                    //    color = new AciColor(short.Parse(cod.Val));
                    //    code = this.ReadCodePair();
                    //    break;
                    //case 6:
                    //    lineType = this.GetLineType(code.Value);
                    //    code = this.ReadCodePair();
                    //    break;
                    case 10:
                        location.X = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 20:
                        location.Y = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 30:
                        location.Z = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 40:
                        beginThickness = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 41:
                        endThickness = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 42:
                        bulge = float.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 70:
                        flags = (VertexTypeFlags)int.Parse(cod.Val);
                        cod = this.ReadCodePair();
                        break;
                    case 71:
                        vertexIndexes.Add(int.Parse(cod.Val));
                        cod = this.ReadCodePair();
                        break;
                    case 72:
                        vertexIndexes.Add(int.Parse(cod.Val));
                        cod = this.ReadCodePair();
                        break;
                    case 73:
                        vertexIndexes.Add(int.Parse(cod.Val));
                        cod = this.ReadCodePair();
                        break;
                    case 74:
                        vertexIndexes.Add(int.Parse(cod.Val));
                        cod = this.ReadCodePair();
                        break;
                    //case 1001:
                    //    XData xDataItem = this.ReadXDataRecord(code.Value, ref code);
                    //    xData.Add(xDataItem.ApplicationRegistry, xDataItem);
                    //    break;
                    default:
                        if (cod.Cod >= 1000 && cod.Cod <= 1071)
                            throw new DxfInvalidCodeValueEntityException(cod.Cod, cod.Val, this.archivo,
                                                                         "The extended data of an entity must start with the application registry code " + this.fileLine);

                        cod = this.ReadCodePair();
                        break;
                }
            }

            return new Vertex
            {
                Flags = flags,
                Location = location,
                BeginThickness = beginThickness,
                Bulge = bulge,
                //Color = color,
                EndThickness = endThickness,
                //Layer = layer,
                //LineType = lineType,
                VertexIndexes = vertexIndexes.ToArray(),
                //XData = xData,
                Handle = handle
            };

      
        }

        private void ReadUnknowEntity(ref ParCodigoValor  cod)
        {
            cod = this.ReadCodePair();
            while (cod.Cod != 0)
            {
                cod = this.ReadCodePair();
            }
        }

        #endregion

        #region private methods

        //private static TextAlignment ObtainAlignment(int horizontal, int vertical)
        //{
        //    TextAlignment alignment = TextAlignment.BaselineLeft;

        //    if (horizontal == 0 && vertical == 3)
        //        alignment = TextAlignment.TopLeft;

        //    else if (horizontal == 1 && vertical == 3)
        //        alignment = TextAlignment.TopCenter;

        //    else if (horizontal == 2 && vertical == 3)
        //        alignment = TextAlignment.TopRight;

        //    else if (horizontal == 0 && vertical == 2)
        //        alignment = TextAlignment.MiddleLeft;

        //    else if (horizontal == 1 && vertical == 2)
        //        alignment = TextAlignment.MiddleCenter;

        //    else if (horizontal == 2 && vertical == 2)
        //        alignment = TextAlignment.MiddleRight;

        //    else if (horizontal == 0 && vertical == 1)
        //        alignment = TextAlignment.BottomLeft;

        //    else if (horizontal == 1 && vertical == 1)
        //        alignment = TextAlignment.BottomCenter;

        //    else if (horizontal == 2 && vertical == 1)
        //        alignment = TextAlignment.BottomRight;

        //    else if (horizontal == 0 && vertical == 0)
        //        alignment = TextAlignment.BaselineLeft;

        //    if (horizontal == 1 && vertical == 0)
        //        alignment = TextAlignment.BaselineCenter;

        //    else if (horizontal == 2 && vertical == 0)
        //        alignment = TextAlignment.BaselineRight;

        //    return alignment;
        //}

        //private Block GetBlock(string name)
        //{
        //    if (this.blocks.ContainsKey(name))
        //    {
        //        return this.blocks[name];
        //    }
        //    return null;
        //}

        //private Layer GetLayer(string name)
        //{
        //    if (this.layers.ContainsKey(name))
        //    {
        //        return this.layers[name];
        //    }

        //    //just in case the layer has not been defined in the table section
        //    var layer = new Layer(name);
        //    this.layers.Add(layer.Name, layer);
        //    return layer;
        //}

        //private LineType GetLineType(string name)
        //{
        //    if (this.lineTypes.ContainsKey(name))
        //    {
        //        return this.lineTypes[name];
        //    }

        //    //just in case the line type has not been defined in the table section
        //    var lineType = new LineType(name);
        //    this.lineTypes.Add(lineType.Name, lineType);
        //    return lineType;
        //}

        //private TextStyle GetTextStyle(string name)
        //{
        //    if (this.textStyles.ContainsKey(name))
        //    {
        //        return this.textStyles[name];
        //    }

        //    //just in case the text style has not been defined in the table section
        //    var textStyle = new TextStyle(name, "Arial");
        //    this.textStyles.Add(textStyle.Name, textStyle);
        //    return textStyle;
        //}

        private ParCodigoValor ReadCodePair()
        {
            int cod;
            string readCod = this.reader.ReadLine();
            this.fileLine += 1;
            if (!int.TryParse(readCod, out cod))
            {
                throw (new DxfException("Codigo grupo no valido " + readCod + " en linea " + this.fileLine));
            }
            string val = this.reader.ReadLine();
            this.fileLine += 1;
            return new ParCodigoValor(cod, val);
        }

        //private XData ReadXDataRecord(string appId, ref ParCodigoValor cod)
        //{
        //    XData xData = new XData(this.appIds[appId]);
        //    code = this.ReadCodePair();

        //    while (code.Code >= 1000 && code.Code <= 1071)
        //    {
        //        if (code.Code == XDataCode.AppReg)
        //            break;

        //        XDataRecord xDataRecord = new XDataRecord(code.Code, code.Value);
        //        xData.XDataRecord.Add(xDataRecord);
        //        code = this.ReadCodePair();
        //    }

        //    return xData;
        //}

        #endregion
    }
}

