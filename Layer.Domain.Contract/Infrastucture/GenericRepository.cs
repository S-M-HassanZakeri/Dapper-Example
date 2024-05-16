using Dapper;
using Dapper.Contrib.Extensions;
using Layer.Common.ErrorExaptation;
using Layer.Domain.Contract.Context;
using Layer.Domain.Contract.Contracts;
using Layer.Domian.Entities.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using static Layer.Common.Enumerate.Enumerate;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Layer.Domain.Contract.Infrastucture
{
    public class GenericServices<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected IDbConnection DbConnection { get; private set; }
        private readonly DBSetting _dBSetting;

        public GenericServices(DBSetting dbSettings)
        {
            _dBSetting = dbSettings;
            DbConnection = new DBContext().GetDbContext(dbSettings.ConnectionString);
        }

        public async Task<Tuple<List<TEntity>, ResultStatusOpration>> FindAllAsync()
        {
            ResultStatusOpration resultStatusOpration = new ResultStatusOpration()
            {
                Title = "با موفقیت ثبت انجام شد",
                TypeStatus = ResultStatusEnum.Success
            };
            DbConnection.Open();

            try
            {
                var execute = await DbConnection
                    .GetAllAsync<TEntity>();
                return new Tuple<List<TEntity>, ResultStatusOpration>(execute.ToList(), resultStatusOpration);
            }
            catch (Exception ex)
            {
                resultStatusOpration.Title = "خطایی پیش آمد است";
                resultStatusOpration.Message = ex.Message;
                resultStatusOpration.TypeStatus = ResultStatusEnum.Error;
                return new Tuple<List<TEntity>, ResultStatusOpration>(null, resultStatusOpration);
            }
            finally { DbConnection.Close(); }
        }

        public async Task<Tuple<TEntity, ResultStatusOpration>> FindByIdAsync(int id)
        {
            ResultStatusOpration resultStatusOpration = new ResultStatusOpration()
            {
                Title = "با موفقیت ثبت انجام شد",
                TypeStatus = ResultStatusEnum.Success
            };
            DbConnection.Open();

            try
            {
                var execute = await DbConnection
                    .GetAsync<TEntity>(id);
                return new Tuple<TEntity, ResultStatusOpration>(execute, resultStatusOpration);

            }
            catch (Exception ex)
            {
                resultStatusOpration.Title = "خطایی پیش آمد است";
                resultStatusOpration.Message = ex.Message;
                resultStatusOpration.TypeStatus = ResultStatusEnum.Error;
                return new Tuple<TEntity, ResultStatusOpration>(null, resultStatusOpration);
            }
            finally { DbConnection.Close(); }
        }

        public async Task<Tuple<ResultStatusOpration>> CreateAsync(TEntity entity)
        {
            ResultStatusOpration resultStatusOpration = new ResultStatusOpration()
            {
                Title = "با موفقیت ثبت انجام شد",
                TypeStatus = ResultStatusEnum.Success
            };
            DbConnection.Open();
            try
            {
                var inserted = await DbConnection
                .InsertAsync<TEntity>(entity);

                return new Tuple<ResultStatusOpration>(resultStatusOpration);

            }
            catch (Exception ex)
            {
                resultStatusOpration.Title = "خطایی پیش آمد است";
                resultStatusOpration.Message = ex.Message;
                resultStatusOpration.TypeStatus = ResultStatusEnum.Error;
                return new Tuple<ResultStatusOpration>(resultStatusOpration);
            }
            finally { DbConnection.Close(); }
        }

        public async Task<Tuple<ResultStatusOpration>> UpdateAsync(TEntity entity)
        {
            ResultStatusOpration resultStatusOpration = new ResultStatusOpration()
            {
                Title = "با موفقیت ثبت انجام شد",
                TypeStatus = ResultStatusEnum.Success
            };
            DbConnection.Open();

            try
            {
                var execute = await DbConnection
                    .UpdateAsync<TEntity>(entity);
                return new Tuple<ResultStatusOpration>(resultStatusOpration);

            }
            catch (Exception ex)
            {
                resultStatusOpration.Title = "خطایی پیش آمد است";
                resultStatusOpration.Message = ex.Message;
                resultStatusOpration.TypeStatus = ResultStatusEnum.Error;
                return new Tuple<ResultStatusOpration>(resultStatusOpration);
                ;
            }
            finally { DbConnection.Close(); }
        }

        public async Task<Tuple<ResultStatusOpration>> DeleteAsync(int id)
        {
            ResultStatusOpration resultStatusOpration = new ResultStatusOpration()
            {
                Title = "با موفقیت ثبت انجام شد",
                TypeStatus = ResultStatusEnum.Success
            };
            DbConnection.Open();

            try
            {
                var entity = await DbConnection
                    .GetAsync<TEntity>(id);

                if (entity == null)
                {
                    resultStatusOpration.Title = "اطلاعاتی یافت نشد";
                    resultStatusOpration.TypeStatus = ResultStatusEnum.Warning;
                    return new Tuple<ResultStatusOpration>(resultStatusOpration);
                }

                var execute = await DbConnection.DeleteAsync<TEntity>(entity);
                return new Tuple<ResultStatusOpration>(resultStatusOpration);


            }
            catch (Exception ex)
            {
                resultStatusOpration.Title = "خطایی پیش آمد است";
                resultStatusOpration.Message = ex.Message;
                resultStatusOpration.TypeStatus = ResultStatusEnum.Error;
                return new Tuple<ResultStatusOpration>(resultStatusOpration);
            }
            finally { DbConnection.Close(); }
        }

        public async Task<Tuple<TEntity, ResultStatusOpration>> GetFilter(Expression<Func<TEntity, bool>> filter)
        {
            ResultStatusOpration resultStatusOpration = new ResultStatusOpration()
            {
                Title = "با موفقیت ثبت انجام شد",
                TypeStatus = ResultStatusEnum.Success
            };
            DbConnection.Open();
            try
            {
                var data = await DbConnection.GetAllAsync<TEntity>();
                var execute = data.AsQueryable().SingleOrDefault(filter);
                return new Tuple<TEntity, ResultStatusOpration>(execute, resultStatusOpration);
            }
            catch (Exception ex)
            {
                resultStatusOpration.Title = "خطایی پیش آمد است";
                resultStatusOpration.Message = ex.Message;
                resultStatusOpration.TypeStatus = ResultStatusEnum.Error;
                return new Tuple<TEntity, ResultStatusOpration>(null, resultStatusOpration);
            }
            finally { DbConnection.Close(); }
        }

        public async Task<Tuple<List<TEntity>, ResultStatusOpration>> GetFilterAll(Expression<Func<TEntity, bool>> filter)
        {
            ResultStatusOpration resultStatusOpration = new ResultStatusOpration()
            {
                Title = "با موفقیت ثبت انجام شد",
                TypeStatus = ResultStatusEnum.Success
            };
            DbConnection.Open();
            try
            {
                var data = await DbConnection.GetAllAsync<TEntity>();
                var execute = data.AsQueryable().Where(filter).ToList();
                return new Tuple<List<TEntity>, ResultStatusOpration>(execute, resultStatusOpration);

            }
            catch (Exception ex)
            {
                resultStatusOpration.Title = "خطایی پیش آمد است";
                resultStatusOpration.Message = ex.Message;
                resultStatusOpration.TypeStatus = ResultStatusEnum.Error;
                return new Tuple<List<TEntity>, ResultStatusOpration>(null, resultStatusOpration);
            }
            finally { DbConnection.Close(); }
        }

        public async Task<Tuple<List<TEntity>, ResultStatusOpration>> GetQueryAll(string query)
        {
            ResultStatusOpration resultStatusOpration = new ResultStatusOpration()
            {
                Title = "با موفقیت ثبت انجام شد",
                TypeStatus = ResultStatusEnum.Success
            };
            DbConnection.Open();

            try
            {
                var execute = await DbConnection.QueryAsync<TEntity>(query);
                return new Tuple<List<TEntity>, ResultStatusOpration>(execute.ToList(), resultStatusOpration);
            }
            catch (Exception ex)
            {
                resultStatusOpration.Title = "خطایی پیش آمد است";
                resultStatusOpration.Message = ex.Message;
                resultStatusOpration.TypeStatus = ResultStatusEnum.Error;
                return new Tuple<List<TEntity>, ResultStatusOpration>(null, resultStatusOpration);
            }
            finally { DbConnection.Close(); }
        }

        public Task<int> GetStoredProcedure(string storedProcedure, DynamicParameters dynamicParameters)
        {
            throw new NotImplementedException();
        }
    }
}
