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
            return await context.Proposals
                .Where(p => p.Status != ProposalStatus.Deleted)
                .ToListAsync() ?? new List<Proposal>();
        }

        //public async Task CreateProposal(Proposal proposal)
        //{
        //    using var context = purchaseContextFactory.CreateDbContext();

        //    // Получаем максимальный номер за текущий год или 0, если записей нет
        //    var lastNumber = await context.Proposals
        //        .Where(p => p.CreationDate.Year == DateTime.Now.Year && p.Status != ProposalStatus.Deleted)
        //        .Select(p => (int?)p.Number) // Приводим к nullable, чтобы MaxAsync вернул int?
        //        .MaxAsync() ?? 0;

        //    proposal.Number = lastNumber + 1;
        //    proposal.CreationDate = DateTime.Now;
        //    proposal.Status = ProposalStatus.Created;

        //    context.Proposals.Add(proposal);
        //    await context.SaveChangesAsync();
        //}

        public async Task CreateProposal(Proposal proposal)
        {
            using var context = purchaseContextFactory.CreateDbContext();

            // Генерация номера
            var lastNumber = await context.Proposals
                .Where(p => p.CreationDate.Year == DateTime.Now.Year && p.Status != ProposalStatus.Deleted)
                .MaxAsync(p => (int?)p.Number) ?? 0;

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
            return await context.Proposals
                .Include(p => p.Materials.Where(m => m.Status != MaterialStatus.Deleted))
                .FirstOrDefaultAsync(p => p.Id == id && p.Status != ProposalStatus.Deleted);
        }

        public async Task UpdateProposal(Proposal proposal)
        {
            using var context = purchaseContextFactory.CreateDbContext();
            context.Proposals.Update(proposal);
            await context.SaveChangesAsync();
        }

        public async Task CreateProposalMaterial(ProposalMaterial material)
        {
            using var context = purchaseContextFactory.CreateDbContext();
            material.Status = MaterialStatus.Created;
            context.ProposalMaterials.Add(material);
            await context.SaveChangesAsync();
        }

        public async Task UpdateProposalMaterial(ProposalMaterial material)
        {
            using var context = purchaseContextFactory.CreateDbContext();
            context.ProposalMaterials.Update(material);
            await context.SaveChangesAsync();
        }
    }
}