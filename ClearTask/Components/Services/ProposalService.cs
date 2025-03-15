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
            var proposals = await context.Proposals
                .Where(p => p.Status != ProposalStatus.Deleted)
                .ToListAsync();

            Console.WriteLine($"Количество заявок после фильтрации: {proposals.Count}");
            foreach (var proposal in proposals)
            {
                Console.WriteLine($"Заявка ID: {proposal.Id}, Статус: {proposal.Status}");
            }

            return proposals ?? new List<Proposal>();
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
            if (proposal == null)
            {
                throw new ArgumentNullException(nameof(proposal), "Заявка не может быть null.");
            }

            Console.WriteLine($"Создание заявки: {proposal.Author}, {proposal.Division}");

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
                try
                {
                    Console.WriteLine($"Заявка найдена. Меняем статус на Deleted.");
                    Console.WriteLine($"Статус заявки перед изменением: {proposal.Status}");
                    proposal.Status = ProposalStatus.Deleted;
                    await context.SaveChangesAsync();
                    Console.WriteLine($"Статус заявки после изменения: {proposal.Status}");
                    Console.WriteLine($"Статус заявки успешно изменен на Deleted.");

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при сохранении изменений: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Ошибка: Заявка с ID {id} не найдена.");
            }
        }

        public async Task<Proposal> GetProposalById(int id)
        {
            using var context = purchaseContextFactory.CreateDbContext();
            var proposal = await context.Proposals
                .Include(p => p.Materials.Where(m => m.Status != MaterialStatus.Deleted))
                .FirstOrDefaultAsync(p => p.Id == id && p.Status != ProposalStatus.Deleted);

            if (proposal == null)
            {
                throw new KeyNotFoundException($"Заявка с ID {id} не найдена.");
            }

            return proposal;
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

        public async Task DeleteProposalMaterial(ProposalMaterial material)
        {
            using var context = purchaseContextFactory.CreateDbContext();
            material.Status = MaterialStatus.Deleted;
            context.ProposalMaterials.Update(material);
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