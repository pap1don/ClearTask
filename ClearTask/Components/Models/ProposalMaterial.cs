using ClearTask.Components.Models;
using System.ComponentModel.DataAnnotations;

namespace ClearTask.Components.Models;
public enum MaterialStatus : byte
{
    Created = 0,
    Deleted = 1
}
public class ProposalMaterial
{
  
    public int Id { get; set; }
    public MaterialStatus Status { get; set; } = MaterialStatus.Created;

    // Вычисляемое поле TextStatus
    public string TextStatus => Status switch
    {
        MaterialStatus.Created => "Создан",
        MaterialStatus.Deleted => "Удален",
        _ => "Неизвестный статус"
    };

    public string Name { get; set; }
    public string Code { get; set; }

    [MaxLength(10)]
    public string MaterialCode { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    public string Comment { get; set; }

    public int ProposalId { get; set; }
    public Proposal Proposal { get; set; }
}