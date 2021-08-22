using System;
using System.Collections.Generic;
using BaltaStore.Domain.StoreContext.Entites;
using BaltaStore.Domain.StoreContext.Queries;
using BaltaStore.Domain.StoreContext.Repositories;

namespace BaltaStore.Tests.Handlers
{
    public class FaskCustomerRepository : ICustomerRepository
    {
        public bool CheckDocument(string document)
        {
            return false;
        }

        public bool CheckEmail(string email)
        {
            return false;
        }

        public IEnumerable<ListCustomerQueryResult> Get()
        {
            throw new NotImplementedException();
        }

        public ListCustomerQueryResult Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public CustomerOrdersCountResult GetCustomerOrdersCount(string document)
        {
            return new CustomerOrdersCountResult();
        }

        public IEnumerable<ListCustomerOrdersQueryResult> GetOrders(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save(Customer customer)
        {

        }
    }
}