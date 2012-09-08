using System;
using System.Collections.Generic;
using DXF.Objetos;
using DXF.Utils;
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
        IEntidadObjeto
    {

    }
}
