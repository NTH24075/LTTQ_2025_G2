using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DAL
{
    public class FacultyDAL
    {
        public DataTable GetAll() => DataProvider.Instance.ExecuteQuery("SELECT faculty_id, facultyName FROM Faculty ORDER BY faculty_id");
        public string GetNameById(int? id)
        {
            if (!id.HasValue) return string.Empty;
            string sql = "SELECT facultyName FROM Faculty WHERE faculty_id = @id";
            object o = DataProvider.Instance.ExecuteScalar(sql, new object[] { id.Value });
            return o?.ToString() ?? string.Empty;
        }
        public List<(int Id, string Name)> GetAllFaculties()
        {
            string q = "SELECT faculty_id, facultyName FROM Faculty ORDER BY facultyName";
            DataTable dt = DataProvider.Instance.ExecuteQuery(q);
            var list = new List<(int, string)>();
            foreach (DataRow r in dt.Rows)
                list.Add((Convert.ToInt32(r["faculty_id"]), r["facultyName"].ToString()));
            return list;
        }
    }
}
