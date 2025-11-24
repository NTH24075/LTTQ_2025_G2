using LTTQ_G2_2025.DAL;
using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.BLL
{
    public class RoleBLL
    {
        private readonly RoleDAL _roleDAL = new RoleDAL();

        public List<RoleDTO> GetAllRoles()
        {
            return _roleDAL.GetAllRoles();
        }
    }
}
