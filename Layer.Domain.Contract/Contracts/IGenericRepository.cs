using Dapper;
using Layer.Common.ErrorExaptation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Domain.Contract.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<Tuple<List<TEntity>, ResultStatusOpration>> FindAllAsync();
        Task<Tuple<TEntity, ResultStatusOpration>> FindByIdAsync(int id);
        Task<Tuple<ResultStatusOpration>> CreateAsync(TEntity entity);
        Task<Tuple<ResultStatusOpration>> UpdateAsync(TEntity entity);
        Task<Tuple<ResultStatusOpration>> DeleteAsync(int id);
        Task<Tuple<List<TEntity>, ResultStatusOpration>> GetFilterAll(Expression<Func<TEntity, bool>> filter);
        Task<Tuple<TEntity, ResultStatusOpration>> GetFilter(Expression<Func<TEntity, bool>> filter);
        Task<Tuple<List<TEntity>, ResultStatusOpration>> GetQueryAll(string query);
        Task<int> GetStoredProcedure(string storedProcedure, DynamicParameters dynamicParameters);

    }
}
