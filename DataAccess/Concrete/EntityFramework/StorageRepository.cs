
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class StorageRepository : EfEntityRepositoryBase<Storage, ProjectDbContext>, IStorageRepository
    {
        public StorageRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<bool> IsExistStorageByUserIdAndProductId(int userId, int productId)
        {
            bool exists = await Context.Storages.Where(s=>s.CreatedUserId == userId && s.ProductId == productId && s.isDeleted == false && s.IsReady ==true).AnyAsync();
            return exists;
        }

    }
}
