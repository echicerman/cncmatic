using System;
using System.Collections.Generic;
//using DXF.Tables;
using DXF.Objetos;
using Configuracion;

namespace DXF.Entidades
{
    /// <summary>
    /// Representa una <see cref="DXF.Entidades.IEntidadObjeto">entidad</see> linea.
    /// </summary>
    public class Linea :
        DxfObjeto,
        IEntidadObjeto
    {
        #region propiedades privadas

        private const EntidadTipo TIPO = EntidadTipo.Linea;
        private Vector3f puntoInicio;
        private Vector3f puntoFin;
        //private float thickness;
        //private AciColor color;
        //private Layer layer;
        //private LineType lineType;
        private Vector3f normal;
        //private Dictionary<ApplicationRegistry, XData> xData;

        #endregion

        #region constructores

        /// <summary>
        /// Inicializa una nueva instancia de la clase <c>linea</c>.
        /// </summary>
        /// <param name="puntoInicio"><see cref="Vector3f">Punto inicial</see>de la linea</param>
        /// <param name="puntoFin">Line <see cref="Vector3f">Punto final</see>de la linea</param>
        public Linea(Vector3f puntoInicio, Vector3f puntoFin)
            : base(DxfCodigoObjeto.Linea)
        {
            this.puntoInicio = puntoInicio;
            this.puntoFin = puntoFin;
            //this.thickness = 0.0f;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            this.normal = Vector3f.UnitarioZ;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <c>linea</c>.
        /// </summary>
        public Linea()
            : base(DxfCodigoObjeto.Linea)
        {
            this.puntoInicio = Vector3f.Nulo;
            this.puntoFin = Vector3f.Nulo;
            //this.thickness = 0.0f;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            this.normal = Vector3f.UnitarioZ;
        }

        #endregion

        #region propiedades publicas

        /// <summary>
        /// Obtiene o establece el <see cref="Dxf.Objetos.Vector3f">punto de inicio</see> de la linea.
        /// </summary>
        public Vector3f PuntoInicio
        {
            get { return this.puntoInicio; }
            set { this.puntoInicio = value; }
        }

        /// <summary>
        /// Obtiene o establece el <see cref="Dxf.Objetos.Vector3f">punto de fin</see> de la linea.
        /// </summary>
        public Vector3f PuntoFinal
        {
            get { return this.puntoFin; }
            set { this.puntoFin = value; }
        }

        /// <summary>
        /// Gets or sets the line thickness.
        /// </summary>
        //public float Thickness
        //{
        //    get { return this.thickness; }
        //    set { this.thickness = value; }
        //}

        /// <summary>
        /// Obtiene o establece la <see cref="Dxf.Objetos.Vector3f">normal</see> de la linea.
        /// </summary>
        public Vector3f Normal
        {
            get { return this.normal; }
            set
            {
                if (Vector3f.Nulo == value)
                    throw new ArgumentNullException("valor", "La normal no puede ser un vector nulo");
                value.Normalize();
                this.normal = value;
            }
        }

        #endregion

        #region IEntidadObjeto miembros

        /// <summary>
        /// Obtiene el <see cref="Dxf.Entidades.EntidadTipo">tipo</see> de la entidad.
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
            if (this.puntoInicio.Z > config.MaxZ || this.puntoInicio.Z < 0)
            {
                return false;
            }

            if (this.puntoFin.X > config.MaxX || this.puntoFin.X < 0)
            {
                return false;
            }
            if (this.puntoFin.Y > config.MaxY || this.puntoFin.Y < 0)
            {
                return false;
            }
            if (this.puntoFin.Z > config.MaxZ || this.puntoFin.Z < 0)
            {
                return false;
            }

            return resultado;
        }
        #endregion

        #region overrides

        /// <summary>
        /// Convierte el tipo de la instancia a string
        /// </summary>
        /// <returns>El tipo en string</returns>
        public override string ToString()
        {
            return TIPO.ToString();
        }

        #endregion

    }
}
