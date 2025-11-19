using LTTQ_G2_2025.DAL;
using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.BLL
{
    public class AccountBLL
    {
        private AccountDAL _accountDAL;

        public AccountBLL()
        {
            _accountDAL = new AccountDAL();
        }

        public bool CheckAccountInDatabase(AccountDTO user)
        {
            return _accountDAL.CheckAccountInDatabase(user);
        }

        public bool CheckUsernameInDatabase(string username)
        {
            return _accountDAL.CheckUsernameInDatabase(username);
        }

        public bool InsertAccountInDatabase(AccountDTO user)
        {
            return _accountDAL.AddAccount(user);
        }

        public AccountDTO LoginAccount(string username, string password)
        {
            return _accountDAL.GetAccountByLogin(username, password);
        }

        // Lấy danh sách roles của account
        public List<string> GetRolesByAccountId(long accountId)
        {
            return _accountDAL.GetRolesByAccountId(accountId);
        }

        // Thêm role cho account
        public bool AssignRoleToAccount(long accountId, int roleId)
        {
            return _accountDAL.AddRoleToAccount(accountId, roleId);
        }

        // Xóa role khỏi account
        public bool RemoveRoleFromAccount(long accountId, int roleId)
        {
            return _accountDAL.RemoveRoleFromAccount(accountId, roleId);
        }
    }
}
