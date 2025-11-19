using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class RoleDTO
    {
        public RoleDTO() { }

        public RoleDTO(int roleId, string roleName)
        {
            RoleId = roleId;
            RoleName = roleName;
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
