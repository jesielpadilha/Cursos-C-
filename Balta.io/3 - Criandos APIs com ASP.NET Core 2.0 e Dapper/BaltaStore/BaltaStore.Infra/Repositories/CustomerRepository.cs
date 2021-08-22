using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BaltaStore.Domain.StoreContext.Entites;
using BaltaStore.Domain.StoreContext.Queries;
using BaltaStore.Domain.StoreContext.Repositories;
using BaltaStore.Infra.StoreContext.DataContexts;
using Dapper;

namespace BaltaStore.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BaltaDataContext _context;

        public CustomerRepository(BaltaDataContext context)
        {
            _context = context;
        }

        public bool CheckDocument(string document)
        {
            return _context.Connection.Query<bool>(
                "spCheckDocument",
                new { Document = document },
                commandType: CommandType.StoredProcedure
            ).FirstOrDefault();
        }

        public bool CheckEmail(string email)
        {
            return _context.Connection.Query<bool>(
               "spCheckEmail",
               new { Email = email },
               commandType: CommandType.StoredProcedure
            ).FirstOrDefault();
        }

        public IEnumerable<ListCustomerQueryResult> Get()
        {
            var query = @"
                select Id, concat(FirstName, ' ' , LastName) as Name, Document, Email from Customer
            ";
            return _context.Connection.Query<ListCustomerQueryResult>(query);
        }

        public ListCustomerQueryResult Get(Guid id)
        {
            var query = @"
                select Id, concat(FirstName, ' ' , LastName) as Name, Document, Email from Customer where Id = @id
            ";
            return _context.Connection.Query<ListCustomerQueryResult>(query, new { id }).FirstOrDefault();
        }

        public IEnumerable<ListCustomerOrdersQueryResult> GetOrders(Guid id)
        {
            var query = @"
               select
                    c.Id,
                    concat(c.FirstName, ' ' , c.LastName) as Name,
                    c.Document,
                    c.Email,
                    oi.Price * oi.Quantity as Total,
                    p.Title
                from Customer c
                    inner join [Order] o on o.CustomerId = c.Id
                    inner join [OrderItem] oi on o.Id = oi.OrderId
                    inner join [Product] p on p.Id = oi.ProductId 
                where c.Id = @id
            ";
            return _context.Connection.Query<ListCustomerOrdersQueryResult>(query, new { id });
        }

        public CustomerOrdersCountResult GetCustomerOrdersCount(string document)
        {
            return _context.Connection.Query<CustomerOrdersCountResult>(
              "spGetCustomerOrdersCount",
              new { Document = document },
              commandType: CommandType.StoredProcedure
           ).FirstOrDefault();
        }

        public void Save(Customer customer)
        {
            try
            {
                using (var transaction = _context.Connection.BeginTransaction())
                {
                    _context.Connection.Execute(
                        "spCreateCustomer",
                        new
                        {
                            Id = customer.Id,
                            FirstName = customer.Name.FirstName,
                            LastName = customer.Name.LastName,
                            Document = customer.Document,
                            Email = customer.Email.Address,
                            Phone = customer.Phone,
                        },
                        commandType: CommandType.StoredProcedure
                    );
                    foreach (var address in customer.Addresses)
                    {
                        _context.Connection.Execute(
                            "spCreateAddress",
                            new
                            {
                                Id = address.Id,
                                CustomerId = customer.Id,
                                Number = address.Number,
                                Complement = address.Complement,
                                District = address.District,
                                City = address.City,
                                State = address.State,
                                Country = address.Country,
                                ZipCode = address.ZipCode,
                                Type = address.Type
                            },
                            commandType: CommandType.StoredProcedure
                        );
                    }
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                Console.Write("Falha ao salvar o cliente:\n", e.Message);
            }
        }
    }
}