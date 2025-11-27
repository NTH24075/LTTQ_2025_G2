using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DAL
{
    public class ClassDAL
    {
        public long GetClassIdByName(string className)
        {
            string q = "SELECT class_id FROM Class WHERE className = @name";
            object o = DataProvider.Instance.ExecuteScalar(q, new object[] { className });
            if (o == null || o == DBNull.Value) return 0;
            return Convert.ToInt64(o);
        }
    }
}
