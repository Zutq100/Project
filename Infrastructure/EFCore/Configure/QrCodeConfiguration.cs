using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EFCore.Configure
{
    public class QrCodeConfiguration : IEntityTypeConfiguration<QrCode>
    {
        public void Configure(EntityTypeBuilder<QrCode> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(e => e.BytesQrCode).IsRequired();
            builder.Property(e => e.DateOfCreation).IsRequired();
        }
    }
}
