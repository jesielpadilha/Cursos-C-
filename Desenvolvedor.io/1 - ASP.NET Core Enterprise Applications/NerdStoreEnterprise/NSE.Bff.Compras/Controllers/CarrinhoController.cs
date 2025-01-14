﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Bff.Compras.Models;
using NSE.Bff.Compras.Services;
using NSE.Bff.Compras.Services.gRPC;
using NSE.WebAPI.Core.Controllers;

namespace NSE.Bff.Compras.Controllers
{
    [Authorize]
    public class CarrinhoController : MainController
    {
        private readonly ICarrinhoService _carrinhoService;
        private readonly ICarrinhoGrpcService _carrinhoGrpcService;
        private readonly ICatalogoService _catalogoService;
        private readonly IPedidoService _pedidoService;

        public CarrinhoController(ICarrinhoService carrinhoService,
            ICatalogoService catalogoService,
            IPedidoService pedidoService, 
            ICarrinhoGrpcService carrinhoGrpcService
        )
        {
            _carrinhoService = carrinhoService;
            _catalogoService = catalogoService;
            _pedidoService = pedidoService;
            _carrinhoGrpcService = carrinhoGrpcService;
        }

        [HttpGet("compras/carrinho")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse(await _carrinhoGrpcService.ObterCarrinho());
        }

        [HttpGet("compras/carrinho-quantidade")]
        public async Task<int> ObterQuantidadeCarrinho()
        {
            var carrinho = await _carrinhoGrpcService.ObterCarrinho();
            return carrinho?.Itens.Sum(c => c.Quantidade) ?? 0;
        }

        [HttpPost("compras/carrinho")]
        public async Task<IActionResult> AdicionarItemCarrinho(ItemCarrinhoDTO itemProduto)
        {
            var produto = await _catalogoService.ObterPorId(itemProduto.ProdutoId);

            await ValidarItemCarrinho(produto, itemProduto.Quantidade);
            if (!OperacaoValida()) return CustomResponse();

            itemProduto.Nome = produto.Nome;
            itemProduto.Valor = produto.Valor;
            itemProduto.Imagem = produto.Imagem;

            return CustomResponse(await _carrinhoService.AdicionarItemCarrinho(itemProduto));
        }

        [HttpPut("compras/carrinho/{produtoId}")]
        public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, ItemCarrinhoDTO itemProduto)
        {
            var produto = await _catalogoService.ObterPorId(produtoId);

            await ValidarItemCarrinho(produto, itemProduto.Quantidade);
            if (!OperacaoValida()) return CustomResponse();

            itemProduto.Nome = produto.Nome;
            itemProduto.Valor = produto.Valor;
            itemProduto.Imagem = produto.Imagem;

            return CustomResponse(await _carrinhoService.AtualizarItemCarrinho(produtoId, itemProduto));
        }

        [HttpDelete("compras/carrinho/{produtoId}")]
        public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
        {
            var produto = await _catalogoService.ObterPorId(produtoId);
            if (produto == null)
            {
                AdicionarErroProcessamento("Produto inexistente!");
                return CustomResponse();
            }

            return CustomResponse(await _carrinhoService.RemoverItemCarrinho(produtoId));
        }

        [HttpPost]
        [Route("compras/carrinho/aplicar-voucher")]
        public async Task<IActionResult> AplicarVoucher([FromBody] string voucherCodigo)
        {
            var voucher = await _pedidoService.ObterVoucherPorCodigo(voucherCodigo);
            if (voucher is null)
            {
                AdicionarErroProcessamento("Voucher inválido ou não encontrado!");
                return CustomResponse();
            }

            var resposta = await _carrinhoService.AplicarVoucherCarrinho(voucher);
            return CustomResponse(resposta);
        }

        private async Task ValidarItemCarrinho(ItemProdutoDTO produto, int quantidade)
        {
            if (produto == null)
            {
                AdicionarErroProcessamento("Produto enexistente!");
                return;
            }

            if (quantidade < 1)
            {
                AdicionarErroProcessamento($"Escolha ao menos uma unidade do produto {produto.Nome}");
                return;
            }

            var carrinho = await _carrinhoService.ObterCarrinho();
            var itemCarrinho = carrinho.Itens.FirstOrDefault(p => p.ProdutoId == produto.Id);

            if (itemCarrinho != null && itemCarrinho.Quantidade + quantidade > produto.QuantidadeEstoque)
            {
                AdicionarErroProcessamento($"O produto {produto.Nome} possui {produto.QuantidadeEstoque}  {(produto.QuantidadeEstoque > 1 ? "unidades" : "unidade")}  em estoque, você selecionou {quantidade}");
                return;
            }

            if (quantidade > produto.QuantidadeEstoque)
                AdicionarErroProcessamento($"O produto {produto.Nome} possui {produto.QuantidadeEstoque}  {(produto.QuantidadeEstoque > 1 ? "unidades" : "unidade")}  em estoque, você selecionou {quantidade}");
        }
    }
}
