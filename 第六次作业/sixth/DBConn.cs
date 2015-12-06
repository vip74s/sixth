using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace sixth
{
    class DBConn
    {
        //连接字符串
        public static string connStr = "Data Source=.;Initial Catalog=repair;Integrated Security=True;Pooling=False";
        public static SqlConnection conn = new SqlConnection(connStr);
    }
}
