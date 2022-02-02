using NSE.Pedido.API.Application.DTO;
using NSE.Pedido.Domain.Vouchers;
using System.Threading.Tasks;

namespace NSE.Pedido.API.Application.Queries
{
    public interface IVoucherQueries
    {
        Task<VoucherDTO> ObterVoucherPorCodigo(string codigo);
    }

    public class VoucherQueries : IVoucherQueries
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherQueries(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public async Task<VoucherDTO> ObterVoucherPorCodigo(string codigo)
        {
            var voucher = await _voucherRepository.ObterVoucherPorCodigo(codigo);

            if (voucher == null) return null;

            if (!voucher.ValidoUtilizacao()) return null;

            return new VoucherDTO
            {
                Codigo = voucher.Codigo,
                Percentual = voucher.Percentual,
                ValorDesconto = voucher.ValorDesconto,
                TipoDesconto = (int)voucher.TipoDesconto
            };
        }
    }
}
