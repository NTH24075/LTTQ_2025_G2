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
        public bool UpdatePassword(long accountId, string newPassword)
        {
            return _accountDAL.UpdatePassword(accountId, newPassword);
        }
        public List<AccountViewDTO> GetAllAccounts()
        {
            return _accountDAL.GetAllAccounts();
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

       
        public List<AccountViewDTO> SearchAccounts(string emailKeyword, string roleName)
        {
            return _accountDAL.SearchAccounts(emailKeyword, roleName);
        }
        public List<AccountViewDTO> GetAllAccountsForView()
        {
            return _accountDAL.GetAccountListForView();
        }
        public bool UpdateAccount(long accountId, string newEmail, string newPassword, int newRoleId)
        {
            return _accountDAL.UpdateAccount(accountId, newEmail, newPassword, newRoleId);
        }
        public AccountEditDTO GetAccountDetailById(long accountId)
        {
            return _accountDAL.GetAccountDetailById(accountId);
        }
    }
}
