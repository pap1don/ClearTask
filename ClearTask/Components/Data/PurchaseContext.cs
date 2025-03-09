using System.Collections.Generic;
using ClearTask.Components.Models;
using Microsoft.EntityFrameworkCore;

namespace ClearTask.Components.Data
{
    public class PurchaseContext : DbContext
    {
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<ProposalMaterial> ProposalMaterials { get; set; }
        public PurchaseContext(DbContextOptions<PurchaseContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proposal>()
                .HasMany(p => p.Materials)
                .WithOne(pm => pm.Proposal)
                .HasForeignKey(pm => pm.ProposalId);
        }
    }

}
