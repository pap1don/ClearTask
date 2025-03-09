using System.ComponentModel.DataAnnotations;

namespace ClearTask.Components.Models
{
    public class Proposal
    {
        public int id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Message { get; set; }

    }
}