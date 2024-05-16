using Layer.Common.ErrorExaptation;
using Layer.Domain.Contract.Contracts;
using Layer.Domain.Service.Contracts.IGenericServices;
using Layer.Domian.Entities.DB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using static Layer.Common.Enumerate.Enumerate;

namespace Layer.Domain.Service.Infrastucture.GenericServices
{
    public class GenericServices<TEntity> : IGenericServices<TEntity> where TEntity : BaseEntity
    {
        private readonly IGenericRepository<TEntity> _Repo;

        public GenericServices(IGenericRepository<TEntity> repo)
        {
            this._Repo = repo;
        }

        public async Task<Tuple<ResultStatusOpration>> DeleteAsync(int id)
        {
            ResultStatusOpration resultStatusOpration = new ResultStatusOpration()
            {
                Title = "با موفقیت ثبت انجام شد",
                TypeStatus = ResultStatusEnum.Success
            };
            try
            {
                if (id == null)
                {
                    resultStatusOpration.Title = "اطلاعاتی یافت نشد";
                    resultStatusOpration.TypeStatus = ResultStatusEnum.Warning;
                    return new Tuple<ResultStatusOpration>(resultStatusOpration);
                }
                Tuple<ResultStatusOpration> query = await _Repo.DeleteAsync(id);
                return new Tuple<ResultStatusOpration>(resultStatusOpration);
            }
            catch (Exception Error)
            {
                resultStatusOpration.Title = "خطایی پیش آمد است";
                resultStatusOpration.Message = Error.Message;
                resultStatusOpration.TypeStatus = ResultStatusEnum.Error;
                return new Tuple<ResultStatusOpration>(resultStatusOpration);

            }
        }

        public async Task<Tuple<IEnumerable<TEntity>, ResultStatusOpration>> GetAllAsync()
        {

            ResultStatusOpration resultStatusOpration = new ResultStatusOpration()
            {
                Title = "با موفقیت واکشی انجام شد",
                TypeStatus = ResultStatusEnum.Success
            };
            try
            {
                Tuple<List<TEntity>, ResultStatusOpration> query = await _Repo.FindAllAsync();
                return new Tuple<IEnumerable<TEntity>, ResultStatusOpration>(query.Item1, query.Item2);
            }
            catch (Exception Error)
            {
                resultStatusOpration.Title = "خطایی پیش آمد است";
                resultStatusOpration.Message = Error.Message;
                resultStatusOpration.TypeStatus = ResultStatusEnum.Error;
                return new Tuple<IEnumerable<TEntity>, ResultStatusOpration>(null, resultStatusOpration);
            }
        }

        public async Task<Tuple<TEntity, ResultStatusOpration>> GetByIdAsync(int id)
        {
            ResultStatusOpration resultStatusOpration = new ResultStatusOpration()
            {
                Title = "با موفقیت واکشی انجام شد",
                TypeStatus = ResultStatusEnum.Success
            };
            try
            {
                if (id == null)
                {
                    resultStatusOpration.Title = "اطلاعاتی یافت نشد";
                    resultStatusOpration.TypeStatus = ResultStatusEnum.Warning;
                    return new Tuple<TEntity, ResultStatusOpration>(null, resultStatusOpration);
                }
                Tuple<TEntity, ResultStatusOpration> query = await _Repo.FindByIdAsync(id);
                return new Tuple<TEntity, ResultStatusOpration>(query.Item1, query.Item2);
            }
            catch (Exception Error)
            {
                resultStatusOpration.Title = "خطایی پیش آمد است";
                resultStatusOpration.Message = Error.Message;
                resultStatusOpration.TypeStatus = ResultStatusEnum.Error;
                return new Tuple<TEntity, ResultStatusOpration>(null, resultStatusOpration);

            }
        }

        public async Task<Tuple<ResultStatusOpration>> InsertAsync(TEntity entity)
        {
            ResultStatusOpration resultStatusOpration = new ResultStatusOpration()
            {
                Title = "با موفقیت ثبت انجام شد",
                TypeStatus = ResultStatusEnum.Success
            };
            try
            {
                if (entity == null)
                {

                    resultStatusOpration.Title = "اطلاعاتی یافت نشد";
                    resultStatusOpration.TypeStatus = ResultStatusEnum.Warning;
                    return new Tuple<ResultStatusOpration>(resultStatusOpration);
                }
                Tuple<ResultStatusOpration> query = await _Repo.CreateAsync(entity);
                return new Tuple<ResultStatusOpration>(resultStatusOpration);
            }
            catch (Exception Error)
            {
                resultStatusOpration.Title = "خطایی پیش آمد است";
                resultStatusOpration.Message = Error.Message;
                resultStatusOpration.TypeStatus = ResultStatusEnum.Error;
                return new Tuple<ResultStatusOpration>(resultStatusOpration);

            }
        }

        public async Task<Tuple<ResultStatusOpration>> UpdateAsync(TEntity entity)
        {
            ResultStatusOpration resultStatusOpration = new ResultStatusOpration()
            {
                Title = "با موفقیت ثبت انجام شد",
                TypeStatus = ResultStatusEnum.Success
            };
            try
            {
                if (entity == null)
                {

                    resultStatusOpration.Title = "اطلاعاتی یافت نشد";
                    resultStatusOpration.TypeStatus = ResultStatusEnum.Warning;
                    return new Tuple<ResultStatusOpration>(resultStatusOpration);
                }
                Tuple<ResultStatusOpration> query = await _Repo.UpdateAsync(entity);
                return new Tuple<ResultStatusOpration>(resultStatusOpration);
            }
            catch (Exception Error)
            {
                resultStatusOpration.Title = "خطایی پیش آمد است";
                resultStatusOpration.Message = Error.Message;
                resultStatusOpration.TypeStatus = ResultStatusEnum.Error;
                return new Tuple<ResultStatusOpration>(resultStatusOpration);

            }
        }
    }
}
