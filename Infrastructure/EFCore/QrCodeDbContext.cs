using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.EFCore
{
    public class QrCodeDbContext : DbContext
    {
        public DbSet<QrCode> QrCodes { get; set; }
        public QrCodeDbContext(DbContextOptions<QrCodeDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QrCode>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(e => e.BytesQrCode).IsRequired();
                entity.Property(e => e.DateOfCreation).IsRequired();
            });
        }
    }
}
