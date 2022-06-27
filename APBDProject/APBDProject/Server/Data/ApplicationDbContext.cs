using APBDProject.Server.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProject.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Stock> Stocks{ get; set; }
        public DbSet<ApplicationUserStock> ApplicationUserStocks{ get; set; }
        public DbSet<StockArticle> StockArticles { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Article>(e => 
            {
                e.ToTable("Article");
                e.HasKey(e => e.IdArticle);
                e.Property(e => e.IdentifierName).IsRequired();
                e.Property(e => e.Title).IsRequired();
                e.Property(e => e.Author).IsRequired();
                e.Property(e => e.Description).IsRequired();
                e.Property(e => e.PublishedDate).IsRequired();
            });

            builder.Entity<Price>(e =>
            {
                e.ToTable("Price");
                e.HasKey(e => e.IdPrice);
                e.Property(e => e.IdStock).IsRequired();
                e.Property(e => e.Day).IsRequired();
                e.Property(e => e.Open).IsRequired();
                e.Property(e => e.High).IsRequired();
                e.Property(e => e.Low).IsRequired();
                e.Property(e => e.Close).IsRequired();
                e.Property(e => e.Volume).IsRequired();
                e.HasOne(e => e.Stock).WithMany(e => e.Prices).HasForeignKey(e => e.IdStock).OnDelete(DeleteBehavior.ClientSetNull);
            });

            builder.Entity<Stock>(e =>
            {
                e.ToTable("Stock");
                e.HasKey(e => e.IdStock);
                e.Property(e => e.Ticker).IsRequired();
            });

            builder.Entity<ApplicationUserStock>(e =>
            {
                e.ToTable("ApplicationUser_Stock");
                e.HasKey(e => new { e.Id, e.IdStock });
                e.HasOne(e => e.ApplicationUser).WithMany(e => e.ApplicationUserStocks).HasForeignKey(e => e.Id);
                e.HasOne(e => e.Stock).WithMany(e => e.ApplicationUserStocks).HasForeignKey(e => e.IdStock);
            });

            builder.Entity<StockArticle>(e =>
            {
                e.ToTable("Stock_Article");
                e.HasKey(e => new { e.IdStock, e.IdArticle });
                e.HasOne(e => e.Stock).WithMany(e => e.StockArticles).HasForeignKey(e => e.IdStock);
                e.HasOne(e => e.Article).WithMany(e => e.StockArticles).HasForeignKey(e => e.IdArticle);
            });
        }
    }
}
