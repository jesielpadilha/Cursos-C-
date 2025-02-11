﻿using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public class CatalogoService : Service, ICatalogoService
    {
        private readonly HttpClient _httpClient;

        public CatalogoService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CatalogoUrl);
        }

        public async Task<PagedViewModel<ProdutoViewModel>> ObterTodos(int pageSize, int pageInddex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos?ps={pageSize}&page={pageInddex}&q={query}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedViewModel<ProdutoViewModel>>(response);
        }

        public async Task<ProdutoViewModel> ObterPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<ProdutoViewModel>(response);
        }
    }
}