﻿using NSE.Pagamentos.API.Models;
using System.Threading.Tasks;

namespace NSE.Pagamentos.API.Facede
{
    public interface IPagamentoFacade
    {
        Task<Transacao> AutorizarPagamento(Pagamento pagamento);
        Task<Transacao> CapturarPagamento(Transacao transacao);
        Task<Transacao> CancelarAutorizacao(Transacao transacao);
    }
}