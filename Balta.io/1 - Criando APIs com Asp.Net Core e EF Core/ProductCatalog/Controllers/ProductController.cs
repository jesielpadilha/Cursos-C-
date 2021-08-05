using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
using ProductCatalog.Repositories;
using ProductCatalog.ViewModels.ProductViewModel;

namespace ProductCatalog.Controllers
{
  public class ProductController : Controller
  {
    private readonly ProductRepository _repository;

    public ProductController(ProductRepository repository)
    {
      _repository = repository;
    }

    [Route("v1/products")]
    [HttpGet]
    [ResponseCache(Duration = 5)]
    public IEnumerable<ListProductViewModel> Get()
    {
      return _repository.Get();
    }

    [Route("v1/products/{id}")]
    [HttpGet]
    public Product Get(int id)
    {
      return _repository.Get(id);
    }

    [Route("v1/products")]
    [HttpPost]
    public ResultViewModel Post([FromBody] EditorProductViewModel model)
    {
      var validationResult = ShowErrorMessageValidation(model, "Não foi possível cadastrar o produto!");
      if (validationResult != null)
        return validationResult;

      var product = new Product
      {
        Title = model.Title,
        Description = model.Description,
        Price = model.Price,
        CategoryId = model.CategoryId,
        Image = model.Image,
        Quantity = model.Quantity,
        CreateDate = DateTime.Now,
        LastUpdateDate = DateTime.Now,
      };

      _repository.Save(product);

      return new ResultViewModel
      {
        Success = true,
        Message = "Produto cadastrado com sucesso!",
        Data = product
      };
    }

    [Route("v1/products/")]
    [HttpPut]
    public ResultViewModel Put([FromBody] EditorProductViewModel model)
    {
      var validationResult = ShowErrorMessageValidation(model, "Não foi possível atualizar o produto!");
      if (validationResult != null)
        return validationResult;

      var product = _repository.Find(model.Id);
      product.Title = model.Title;
      product.Description = model.Description;
      product.Price = model.Price;
      product.CategoryId = model.CategoryId;
      product.Image = model.Image;
      product.Quantity = model.Quantity;
      product.LastUpdateDate = DateTime.Now;

      _repository.Update(product);

      return new ResultViewModel
      {
        Success = true,
        Message = "Produto alterado com sucesso!",
        Data = product
      };
    }
    private ResultViewModel ShowErrorMessageValidation(EditorProductViewModel model, string message)
    {
      if (model == null)
        return null;

      model.Validate();
      if (model.IsValid)
        return null;

      return new ResultViewModel
      {
        Success = false,
        Message = message,
        Data = model.Notifications
      };
    }

  }
}