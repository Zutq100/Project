using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.EFCore
{
    public class QrCodeDbContext : DbContext
    {
        public DbSet<QrCode> QrCodes { get; set; }
        public QrCodeDbContext(DbContextOptions<QrCodeDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        
    }
}
