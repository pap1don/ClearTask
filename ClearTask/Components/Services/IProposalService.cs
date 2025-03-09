using ClearTask.Components.Models;

namespace ClearTask.Components.Services
{
    public interface IProposalService
    {
        Task<List<Proposal>> GetAllProposals();
        Task CreateProposal(Proposal proposal);
        Task DeleteProposal(int id);
        Task<Proposal> GetProposalById(int id);
    }
}