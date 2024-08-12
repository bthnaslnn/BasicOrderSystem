
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
namespace DataAccess.Abstract
{
    public interface ICustomerRepository : IEntityRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetListByUserId(int userid);
    }
}