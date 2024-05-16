using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Domain.Services.Contracts.IGenericServices
{
    public interface IGenericServices<TEntity> where TEntity : BaseEntity
    {
    }
}
