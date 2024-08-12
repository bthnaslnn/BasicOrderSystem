
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class OrderRepository : EfEntityRepositoryBase<Order, ProjectDbContext>, IOrderRepository
    {
        public OrderRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetListByProductId(int productid)
        {
            var list = await Context.Orders.Where(x => x.ProductId == productid && x.isDeleted == false).ToListAsync();
            return list;
        }
    }
}
