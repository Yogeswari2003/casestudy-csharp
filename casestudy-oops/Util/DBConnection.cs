using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace casestudy_oops.Util
{
    public class DBConnection
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(PropertyUtil.GetConnectionString());
        }
    }
}
