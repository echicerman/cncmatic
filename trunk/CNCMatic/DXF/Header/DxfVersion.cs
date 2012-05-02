using DXF.Utils;
namespace DXF.Header
{
    /// <summary>
    /// Define la version del archivo DXF.
    /// </summary>
    public enum DxfVersion
    {
        [StringValue("AC1009")]AutoCad12,
        [StringValue("AC1012")]AutoCad13,
        [StringValue("AC1014")]AutoCad14,
        [StringValue("AC1015")]AutoCad2000,
        [StringValue("AC1018")]AutoCad2004,
        [StringValue("AC1021")]AutoCad2007,
        [StringValue("AC1024")]AutoCad2010,
    }
}
