using System.ComponentModel.DataAnnotations;

namespace ClearTask.Components.Models
{
    public enum MaterialStatus : byte
    {
        Created = 0,
        Deleted = 1
    }

    public class ProposalMaterial
    {
        public int Id { get; set; }
        public MaterialStatus Status { get; set; } = MaterialStatus.Created;

        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;

        [MaxLength(10)]
        public string MaterialCode { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1;

        public string Comment { get; set; } = string.Empty;

        public int ProposalId { get; set; }
        public Proposal Proposal { get; set; }

        // Метод клонирования
        public ProposalMaterial Clone()
        {
            return new ProposalMaterial
            {
                Id = Id,
                Status = Status,
                Name = Name,
                Code = Code,
                MaterialCode = MaterialCode,
                Quantity = Quantity,
                Comment = Comment,
                ProposalId = ProposalId
            };
        }
    }
}