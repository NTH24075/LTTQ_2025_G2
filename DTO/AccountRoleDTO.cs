using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class AccountRoleDTO
    {
        public AccountRoleDTO() { }

        public AccountRoleDTO(long accountRoleId, int roleId, long accountId)
        {
            AccountRoleId = accountRoleId;
            RoleId = roleId;
            AccountId = accountId;
        }

        public long AccountRoleId { get; set; }
        public int RoleId { get; set; }
        public long AccountId { get; set; }
    }
}
