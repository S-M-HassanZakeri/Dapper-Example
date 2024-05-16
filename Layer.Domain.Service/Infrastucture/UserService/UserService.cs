using Layer.Domain.Contract.Contracts;
using Layer.Domain.Service.Contracts.IUserService;
using Layer.Domain.Service.Infrastucture.GenericServices;
using Layer.Domian.Entities.DB.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Domain.Service.Infrastucture.UserService
{
    public class UserService : GenericServices<UserEntity>, IUserService
    {
        public UserService(IGenericRepository<UserEntity> repo) : base(repo)
        {
        }
    }
}
