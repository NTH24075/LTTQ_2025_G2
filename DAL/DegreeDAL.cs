using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DAL
{
    internal class DegreeDAL
    {
        public DataTable GetAll()
        {
            string sql = "SELECT degree_id, degreeName FROM Degree ORDER BY degree_id";
            return DataProvider.Instance.ExecuteQuery(sql);
        }
    }
}
