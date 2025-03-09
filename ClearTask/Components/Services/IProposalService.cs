using ClearTask.Components.Models;
using System.Threading.Tasks;

namespace ClearTask.Components.Services
{
    public interface IProposalService
    {
        Task<List<Proposal>> GetAllProposals();
        Task CreateProposal(Proposal proposal);
        Task DeleteProposal(int id);
        Task<Proposal> GetProposalById(int id);
        Task UpdateProposal(Proposal proposal);
        Task CreateProposalMaterial(ProposalMaterial material); 
        Task UpdateProposalMaterial(ProposalMaterial material);
        //Task DeleteProposalMaterial(int id); // Если нужно удалить материал
    }
}