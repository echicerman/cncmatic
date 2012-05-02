﻿using System;
using System.Collections.Generic;
//using DXF.Tables;
using DXF.Objetos;

namespace DXF.Entidades
{
    /// <summary>
    /// Representa una <see cref="DXF.Entidades.IEntidadObjeto">entidad</see> punto.
    /// </summary>
    public class Punto :
        DxfObjeto,
        IEntidadObjeto
    {
        #region propiedades privadas

        private const EntidadTipo TIPO = EntidadTipo.Punto;
        private Vector3f ubicacion;
        //private float thickness;
        //private Layer layer;
        //private AciColor color;
        //private LineType lineType;
        private Vector3f normal;
        //private Dictionary<ApplicationRegistry, XData> xData;

        #endregion

        #region constructores

        /// <summary>
        /// Inicializa una nueva instancia de la clase <c>Punto</c>.
        /// </summary>
        /// <param name="ubicacion">Punto de <see cref="Vector3f">ubicacion</see>.</param>
        public Punto(Vector3f ubicacion)
            : base(DxfCodigoObjeto.Punto)
        {
            this.ubicacion = ubicacion;
            //this.thickness = 0.0f;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            this.normal = Vector3f.UnitarioZ ;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <c>Punto</c>.
        /// </summary>
        public Punto()
            : base(DxfCodigoObjeto.Punto)
        {
            this.ubicacion = Vector3f.Nulo;
            //this.thickness = 0.0f;
            //this.layer = Layer.Default;
            //this.color = AciColor.ByLayer;
            //this.lineType = LineType.ByLayer;
            this.normal = Vector3f.UnitarioZ;
        }

        #endregion

        #region propiedades publicas

        /// <summary>
        /// Obtiene o establece el punto de <see cref="netDxf.Vector3f">ubicacion</see>.
        /// </summary>
        public Vector3f Ubicacion
        {
            get { return this.ubicacion; }
            set { this.ubicacion = value; }
        }

        /// <summary>
        /// Gets or sets the point thickness.
        /// </summary>
        //public float Thickness
        //{
        //    get { return this.thickness; }
        //    set { this.thickness = value; }
        //}

        /// <summary>
        /// Obtiene o establece el punto de la <see cref="DXF.Objetos.Vector3f">normal</see>.
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
