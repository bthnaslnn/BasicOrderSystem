
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
namespace DataAccess.Abstract
{
    public interface IOrderRepository : IEntityRepository<Order>
    {
        Task<IEnumerable<Order>> GetListByProductId(int productid);
    }
}