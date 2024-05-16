using Layer.Domain.Service.Contracts.IGenericServices;
using Layer.Domian.Entities.DB.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Domain.Service.Contracts.IUserService
{
    public interface IUserService : IGenericServices<UserEntity>
    {
    }
}
