using ClearTask.Components.Models;
using ClearTask.Components.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ClearTask.Components.Services
{
    public class ProposalService : IProposalService
    {
        private readonly IDbContextFactory<PurchaseContext> purchaseContextFactory;

        public ProposalService(IDbContextFactory<PurchaseContext> purchaseContextFactory)
        {
            this.purchaseContextFactory = purchaseContextFactory;
        }

        public async Task<List<Proposal>> GetAllProposals()
        {
            using var context = purchaseContextFactory.CreateDbContext();
            return await context.Proposals.Where(p => p.Status != ProposalStatus.Deleted).ToListAsync();
        }

        public async Task CreateProposal(Proposal proposal)
        {
            using var context = purchaseContextFactory.CreateDbContext();
            int lastNumber = await context.Proposals
                .Where(p => p.CreationDate.Year == DateTime.Now.Year && p.Status != ProposalStatus.Deleted)
                .OrderByDescending(p => p.Number)
                .Select(p => p.Number)
                .FirstOrDefaultAsync();

            proposal.Number = lastNumber + 1;
            proposal.CreationDate = DateTime.Now;
            proposal.Status = ProposalStatus.Created;

            context.Proposals.Add(proposal);
            await context.SaveChangesAsync();
        }

        public async Task DeleteProposal(int id)
        {
            using var context = purchaseContextFactory.CreateDbContext();
            var proposal = await context.Proposals.FindAsync(id);
            if (proposal != null)
            {
                proposal.Status = ProposalStatus.Deleted;
                await context.SaveChangesAsync();
            }
        }

        public async Task<Proposal> GetProposalById(int id)
        {
            using var context = purchaseContextFactory.CreateDbContext();
            return await context.Proposals.FindAsync(id);
        }
    }
}