using ClearTask.Components.Models;


namespace ClearTask.Components.Models
{
    public enum ProposalStatus : byte
    {
        Created = 0,
        Approved = 1,
        Deleted = 2
    }

    public class Proposal
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        // Вычисляемое поле FullNumber
        public string FullNumber => $"{CreationDate.Year.ToString("yy")}/{Number:D4}";

        public ProposalStatus Status { get; set; } = ProposalStatus.Created;

        // Вычисляемое поле TextStatus
        public string TextStatus => Status switch
        {
            ProposalStatus.Created => "Создана",
            ProposalStatus.Approved => "Утверждена",
            ProposalStatus.Deleted => "Удалена",
            _ => "Неизвестный статус"
        };

        public string Division { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;

        // Связь с материалами
        public List<ProposalMaterial> Materials { get; set; } = new();

        // Метод клонирования внутри класса Proposal
        public Proposal Clone()
        {
            return new Proposal
            {
                Id = Id,
                Number = Number,
                CreationDate = CreationDate,
                Status = Status,
                Author = Author,
                Division = Division,
                Materials = Materials?.Select(m => m.Clone()).ToList() ?? new List<ProposalMaterial>()
            };
        }
    }
}