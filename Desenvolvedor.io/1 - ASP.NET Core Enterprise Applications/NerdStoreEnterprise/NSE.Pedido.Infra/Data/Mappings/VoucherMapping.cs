using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Pedido.Domain.Vouchers;

namespace NSE.Pedido.Infra.Data.Mappings
{
    public class VoucherMapping : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Codigo)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(x => x.ValorDesconto)
                .HasPrecision(18, 2);

            builder.Property(x => x.Percentual)
                .HasPrecision(18, 2);

            builder.ToTable("Vouchers");
        }
    }
}
