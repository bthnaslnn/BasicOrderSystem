
using System;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
namespace DataAccess.Abstract
{
    public interface IStorageRepository : IEntityRepository<Storage>
    {
        Task<bool> IsExistStorageByUserIdAndProductId(int userId, int productId);
    }
}