
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
    public class CustomerRepository : EfEntityRepositoryBase<Customer, ProjectDbContext>, ICustomerRepository
    {
        public CustomerRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Customer>> GetListByUserId(int userid)
        {
            var list = await Context.Customers.Where(x => x.CreatedUserId == userid && x.isDeleted == false).ToListAsync();
            return list;
        }
    }
}
