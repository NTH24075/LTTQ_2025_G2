using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class AccountViewDTO
    {
        public AccountViewDTO() { }

        public AccountViewDTO(long accountId, string email, string roleName,string displayName)
        {
            AccountId = accountId;
            Email = email;
            RoleName = roleName;
            DisplayName = displayName;

        }

        public long AccountId { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string DisplayName { get; set; }
    }
}
