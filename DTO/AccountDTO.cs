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
        public string Roles { get; set; }
        public AccountDTO(long accountId, string password, string email)
        {
            AccountId = accountId;
            Password = password;
            Email = email;
        }
        public AccountDTO(long accountId, string email)
        {
            AccountId = accountId;
            Email = email;
        }
        
        public long AccountId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int roleid { get; set; }
    }
}
