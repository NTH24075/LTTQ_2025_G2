using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DAL
{
    public class RoleDAL
    {
        public List<RoleDTO> GetAllRoles()
        {
            string query = "SELECT role_id, roleName FROM Role";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            List<RoleDTO> list = new List<RoleDTO>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new RoleDTO
                {
                    RoleId = Convert.ToInt32(row["role_id"]),
                    RoleName = row["roleName"].ToString()
                });
            }
            return list;
        }
    }
}
