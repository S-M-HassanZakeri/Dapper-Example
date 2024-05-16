using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Domain.Contract.Context
{
    public class DBContext
    {
        public IDbConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        public IDbConnection GetDbContext(string connectionString)
        {
            return GetConnection(connectionString);
        }
    }
}
