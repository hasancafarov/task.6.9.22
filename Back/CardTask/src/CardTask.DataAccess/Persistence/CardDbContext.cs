using CardTask.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTask.DataAccess.Persistence
{
    public class CardDbContext : DbContext
    {
        public CardDbContext()
        {
        }

        public CardDbContext(DbContextOptions<CardDbContext> options)
    : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorAddress> VendorAddresses { get; set; }
        public DbSet<VendorContact> VendorContacts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                                                    .AddJsonFile("appsettings.json", false)
                                                    .Build();
            optionsBuilder.UseNpgsql(builder.GetConnectionString("CardDbConnection"));
        }
        //Model'ların(entity) veritabanında generate edilecek yapıları bu fonksiyonda içerisinde konfigüre edilir
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
