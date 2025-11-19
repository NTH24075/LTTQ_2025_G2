using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class AccountDTO
    {
        public AccountDTO() { }
        public List<string> Roles { get; set; }
        public AccountDTO(long accountId, string password, string email)
        {
            AccountId = accountId;
            Password = password;
            Email = email;
        }
        public bool HasRole(string roleName)
        {
            return Roles.Any(r => r.Equals(roleName, StringComparison.OrdinalIgnoreCase));
        }
        public string GetPrimaryRole()
        {
            if (HasRole("Admin")) return "Admin";
            if (HasRole("Manager")) return "Manager";
            if (HasRole("User")) return "User";
            return "User"; // Default
        }
        public long AccountId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
