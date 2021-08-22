using System;
using System.Collections.Generic;
using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Outputs;
using BaltaStore.Domain.StoreContext.Entites;
using BaltaStore.Domain.StoreContext.Handlers;
using BaltaStore.Domain.StoreContext.Queries;
using BaltaStore.Domain.StoreContext.Repositories;
using BaltaStore.Domain.StoreContext.VelueObjects;
using BaltaStore.Shared.Commands;
using Microsoft.AspNetCore.Mvc;

namespace BaltaStore.Api.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository CustomerRepository;
        private readonly CustomerHandler _handler;

        public CustomerController(ICustomerRepository customerRepository, CustomerHandler handler)
        {
            CustomerRepository = customerRepository;
            _handler = handler;
        }

        [Route("v1/customers")]
        [HttpGet]
        [ResponseCache(Duration = 60)]
        public List<ListCustomerQueryResult> Get()
        {
            var customers = (List<ListCustomerQueryResult>)CustomerRepository.Get();
            return customers;
        }

        [Route("v1/customers/{id}")]
        [HttpGet]
        public ListCustomerQueryResult GetById(Guid id)
        {
            return CustomerRepository.Get(id);
        }

        [Route("v1/customers/{id}/orders")]
        [HttpGet]
        public List<ListCustomerOrdersQueryResult> GetOrders(Guid id)
        {
            return (List<ListCustomerOrdersQueryResult>)CustomerRepository.GetOrders(id);
        }

        [Route("v1/customers")]
        [HttpPost]
        public ICommandResult Post([FromBody] CreateCustomerCommand command)
        {
            var customer = _handler.Handle(command);
            return customer;
        }

        [Route("v1/customers")]
        [HttpPut]
        public Customer Put([FromBody] CreateCustomerCommand command)
        {
            var customer = new Customer(
                new Name(command.FirstName, command.LastName),
                new Document(command.Document),
                new Email(command.Email),
                command.Phone
            );
            return customer;
        }

        [Route("v1/customers/{id}")]
        [HttpDelete]
        public object Delete(Guid id)
        {
            return new { message = "Cliente deletado com sucesso!" };
        }
    }
}