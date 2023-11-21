
namespace EasyPlc.Application;

public class AddressAddInput : PlcAddress
{
    /// <summary>
    /// PLC ID
    /// </summary>
    [Required(ErrorMessage = "PlcId不能为空")]
    public override long PlcId { get; set; }

    /// <summary>
    /// PLC ID
    /// </summary>
    [Required(ErrorMessage = "资源Id不能为空")]
    public override long ResourceId { get; set; }
}
public class AddressEditInput : AddressAddInput
{
    [Required(ErrorMessage = "Id不能为空")]
    public override long Id { get; set; }
}
public class AddressSortInput
{
    public List<SortColumn> Columns { get; set; }
}
public class SortColumn
{
    [Required(ErrorMessage = "PlcId不能为空")]
    public long PlcId { get; set; }
    [Required(ErrorMessage = "Sort不能为空")]
    public int Sort { get; set; }
}
