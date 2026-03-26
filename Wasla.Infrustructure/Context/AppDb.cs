using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Wasla.Data.Entite;

namespace Wasla.Infrustructure.Context
{
    public class AppDb:IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }


        public AppDb(DbContextOptions<AppDb> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("public");

            builder.Entity<User>().HasIndex(u => u.PhoneNumber).IsUnique();
            builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            builder.Entity<User>().HasIndex(u => u.Id);
           
            builder.Entity<Wallet>()
                .HasMany(w => w.Transactions)
                .WithOne(r => r.Wallet)
                .HasForeignKey(r => r.WalletId) 
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Proposal>()
                .HasOne(p => p.Contract)
                .WithOne(c => c.Proposal)
                .HasForeignKey<Contract>(c => c.ProposalId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Review>()
                .HasOne(r => r.Contract)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.ContractId)
                .OnDelete(DeleteBehavior.Cascade); 
            builder.Entity<WalletTransaction>()
                .HasOne(t => t.Wallet)
                .WithMany(w => w.Transactions)
                .HasForeignKey(t => t.WalletId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Contract>()
                .HasMany(c => c.Transactions)
                .WithOne(t => t.Contract)
                .HasForeignKey(t => t.ContractId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<RefreshToken>(entity =>
            {
                entity.HasOne(rt => rt.User)
                      .WithMany()
                      .HasForeignKey(rt => rt.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Contract>(entity =>
            {
                // Client Relationship
                entity.HasOne(c => c.Client)
                      .WithMany()
                      .HasForeignKey(c => c.ClientId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Freelancer Relationship
                entity.HasOne(c => c.Freelancer)
                      .WithMany()
                      .HasForeignKey(c => c.FreelancerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Review>(entity =>
            {
                // Reviewer
                entity.HasOne(r => r.Reviewer)
                      .WithMany()
                      .HasForeignKey(r => r.ReviewerId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Reviewee
                entity.HasOne(r => r.Reviewee)
                      .WithMany()
                      .HasForeignKey(r => r.RevieweeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            base.OnModelCreating(builder);
        }
    }
}
