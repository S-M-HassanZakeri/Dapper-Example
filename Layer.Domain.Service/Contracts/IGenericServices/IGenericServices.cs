using Layer.Common.ErrorExaptation;
using Layer.Domian.Entities.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Domain.Service.Contracts.IGenericServices
{
    public interface IGenericServices<TEntity> where TEntity : BaseEntity
    {
        Task<Tuple<IEnumerable<TEntity>, ResultStatusOpration>> GetAllAsync();
        Task<Tuple<TEntity, ResultStatusOpration>> GetByIdAsync(int id);
        Task<Tuple<ResultStatusOpration>> InsertAsync(TEntity entity);
        Task<Tuple<ResultStatusOpration>> UpdateAsync(TEntity entity);
        Task<Tuple<ResultStatusOpration>> DeleteAsync(int id);
    }
}
