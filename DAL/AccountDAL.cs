using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTTQ_G2_2025.DTO;

namespace LTTQ_G2_2025.DAL
{
    public class AccountDAL
    {
        // Kiểm tra tài khoản trong database
        public bool CheckAccountInDatabase(AccountDTO user)
        {
            string query = "SELECT COUNT(*) FROM account WHERE email = @email AND password = @password";
            object result = DataProvider.Instance.ExecuteScalar(query, new object[] { user.Email, user.Password });
            return Convert.ToInt32(result) > 0;
        }

        // Kiểm tra username có tồn tại không
        public bool CheckUsernameInDatabase(string username)
        {
            string query = "SELECT COUNT(*) FROM account WHERE email = @email";
            object result = DataProvider.Instance.ExecuteScalar(query, new object[] { username });
            return Convert.ToInt32(result) > 0;
        }

        // Thêm tài khoản mới
        public bool AddAccount(AccountDTO user)
        {
            string query = "INSERT INTO account (email, password) VALUES (@username, @password)";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { user.Email, user.Password });
            return result > 0;
        }

        // Lấy thông tin account kèm roles
        public AccountDTO GetAccountByLogin(string username, string password)
        {
            string query = @"SELECT account_id, email, password, Email 
                           FROM account 
                           WHERE email = @email AND password = @password";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { username, password });

            if (data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                AccountDTO account = new AccountDTO(
                    Convert.ToInt64(row["account_id"]),
                    row["password"].ToString(),
                    row["email"].ToString()
                );

                // Lấy danh sách roles của account
                account.Roles = GetRolesByAccountId(account.AccountId);

                return account;
            }
            return null;
        }

        // Lấy danh sách roles của một account
        public List<string> GetRolesByAccountId(long accountId)
        {
            string query = @"SELECT R.RoleName 
                           FROM ROLE R
                           INNER JOIN ACCOUNTROLE AR ON R.Role_Id = AR.Role_Id
                           WHERE AR.Account_role_Id = @accountId";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { accountId });

            List<string> roles = new List<string>();
            foreach (DataRow row in data.Rows)
            {
                roles.Add(row["RoleName"].ToString());
            }

            return roles;
        }

        // Lấy AccountId theo username
        public long GetAccountIdByUsername(string username)
        {
            string query = "SELECT account_id FROM account WHERE email = @email";
            object result = DataProvider.Instance.ExecuteScalar(query, new object[] { username });
            return result != null ? Convert.ToInt64(result) : 0;
        }

        // Thêm role cho account
        public bool AddRoleToAccount(long accountId, int role_Id)
        {
            string query = "INSERT INTO ACCOUNTROLE (Account_role_Id, Role_Id) VALUES (@accountId, @role_Id)";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { accountId, role_Id });
            return result > 0;
        }

        // Xóa role khỏi account
        public bool RemoveRoleFromAccount(long accountId, int role_Id)
        {
            string query = "DELETE FROM ACCOUNTROLE WHERE Account_role_Id = @accountId AND Role_Id = @role_Id";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { accountId, role_Id });
            return result > 0;
        }
    }
}
