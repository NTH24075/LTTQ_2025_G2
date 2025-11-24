using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace LTTQ_G2_2025.DAL
{
    public class AccountDAL
    {
        // Kiểm tra tài khoản trong database
        public bool CheckAccountInDatabase(AccountDTO user)
        {
            string query = "SELECT COUNT(*) FROM Account WHERE email = @email AND password = @password";
            object result = DataProvider.Instance.ExecuteScalar(query, new object[] { user.Email, user.Password });
            return Convert.ToInt32(result) > 0;
        }

        // Kiểm tra username có tồn tại không
        public bool CheckUsernameInDatabase(string username)
        {
            string query = "SELECT COUNT(*) FROM Account WHERE email = @email";
            object result = DataProvider.Instance.ExecuteScalar(query, new object[] { username });
            return Convert.ToInt32(result) > 0;
        }

        // Thêm tài khoản mới
        public bool AddAccount(AccountDTO user)
        {
            string query = "INSERT INTO Account (email, password) VALUES (@username, @password)";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { user.Email, user.Password });
            return result > 0;
        }

        // Lấy thông tin account kèm roles
        public AccountDTO GetAccountByLogin(string username, string password)
        {
            string query = @"
                SELECT account_id, email, password, role_id
                FROM Account 
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

                // nếu AccountDTO có thuộc tính Roles (List<string>)
                account.Roles = GetRolesByAccountId(account.AccountId);

                return account;
            }
            return null;
        }

        public string GetRolesByAccountId(long accountId)
        {
            string query = @"
                SELECT R.roleName
                FROM Account A
                INNER JOIN Role R ON A.role_id = R.role_id
                WHERE A.account_id = @accountId";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { accountId });

            string roles = "";
            foreach (DataRow row in data.Rows)
            {
                roles=(row["roleName"].ToString());
            }
            return roles;
        }
        public AccountEditDTO GetAccountDetailById(long accountId)
        {
            string query = @"
                            SELECT TOP 1 
                                a.account_id,
                                a.email,
                                a.[password],
                                r.role_id,
                                r.roleName,
                                ISNULL(t.teacherName, ISNULL(s.studentName, ad.name)) AS DisplayName
                            FROM Account a
                            JOIN Role r ON r.role_id = a.role_id
                            LEFT JOIN Teacher t ON t.account_id = a.account_id
                            LEFT JOIN Student s ON s.account_id = a.account_id
                            LEFT JOIN Admin ad  ON ad.account_id = a.account_id
                            WHERE a.account_id = @id";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { accountId });

            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];

            return new AccountEditDTO
            {
                AccountId = Convert.ToInt64(row["account_id"]),
                Email = row["email"].ToString(),
                Name = row["DisplayName"].ToString(),
                Password = row["password"].ToString(),
                RoleId = Convert.ToInt32(row["role_id"]),
                RoleName = row["roleName"].ToString()
            };
        }
        // Lấy AccountId theo username
        public long GetAccountIdByUsername(string username)
        {
            string query = "SELECT account_id FROM Account WHERE email = @email";
            object result = DataProvider.Instance.ExecuteScalar(query, new object[] { username });
            return result != null ? Convert.ToInt64(result) : 0;
        }

        public List<AccountViewDTO> GetAllAccounts()
        {
            string query = @"
                SELECT  
                    a.account_id,
                    a.email,
                    r.roleName,
                    ISNULL(t.teacherName, ISNULL(s.studentName, ad.name)) AS DisplayName
                FROM Account a
                LEFT JOIN Role r    ON r.role_id = a.role_id
                LEFT JOIN Teacher t ON t.account_id = a.account_id
                LEFT JOIN Student s ON s.account_id = a.account_id
                LEFT JOIN Admin ad  ON ad.account_id = a.account_id";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            List<AccountViewDTO> list = new List<AccountViewDTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new AccountViewDTO(
                    Convert.ToInt64(row["account_id"]),
                    row["email"].ToString(),
                    row["roleName"].ToString(),
                    row["DisplayName"].ToString()
                ));
            }
            return list;
        }

        public bool UpdateAccount(long accountId, string newEmail, string newPassword, int newRoleId)
        {
            // 1. Cập nhật Email + Password
            string q1 = "UPDATE Account SET email = @email, [password] = @password role_id = @newRoleId WHERE account_id = @id";
            int result =DataProvider.Instance.ExecuteNonQuery(q1, new object[] { newEmail, newPassword, accountId, newRoleId });

            // 2. Xóa hết role cũ

            // 3. Thêm role mới
            

            return result > 0;
        }
        public bool UpdatePassword(long accountId, string newPassword)
        {
            string query = "UPDATE Account SET password = @password WHERE account_id = @accountId";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[]
            {
                newPassword,
                accountId
            });

            return result > 0;
        }

        public List<AccountViewDTO> SearchAccounts(string emailKeyword, string roleName)
        {
            string query = @"
                SELECT 
                    a.account_id,
                    a.email,
                    r.roleName,
                    ISNULL(t.teacherName, ISNULL(s.studentName, ad.name)) AS DisplayName
                FROM Account a
                LEFT JOIN Role r    ON r.role_id = a.role_id
                LEFT JOIN Teacher t ON t.account_id = a.account_id
                LEFT JOIN Student s ON s.account_id = a.account_id
                LEFT JOIN Admin ad  ON ad.account_id = a.account_id
                WHERE 1 = 1";

            var parameters = new List<object>();

            if (!string.IsNullOrEmpty(emailKeyword))
            {
                query += " AND a.email LIKE @email";
                parameters.Add("%" + emailKeyword + "%");
            }

            if (!string.IsNullOrEmpty(roleName))
            {
                query += " AND r.roleName = @roleName";
                parameters.Add(roleName);
            }

            DataTable dt = DataProvider.Instance.ExecuteQuery(query, parameters.ToArray());

            List<AccountViewDTO> list = new List<AccountViewDTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new AccountViewDTO(
                    Convert.ToInt64(row["account_id"]),
                    row["email"].ToString(),
                    row["roleName"].ToString(),
                    row["DisplayName"].ToString()
                ));
            }

            return list;
        }

        public List<AccountViewDTO> GetAccountListForView()
        {
            string query = @"
                SELECT 
                    a.account_id,
                    a.email,
                    r.roleName,
                    ISNULL(t.teacherName, ISNULL(s.studentName, ad.name)) AS DisplayName
                FROM Account a
                LEFT JOIN Role r    ON r.role_id = a.role_id
                LEFT JOIN Teacher t ON t.account_id = a.account_id
                LEFT JOIN Student s ON s.account_id = a.account_id
                LEFT JOIN Admin ad  ON ad.account_id = a.account_id";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            List<AccountViewDTO> list = new List<AccountViewDTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new AccountViewDTO(
                    Convert.ToInt64(row["account_id"]),
                    row["email"].ToString(),
                    row["roleName"].ToString(),
                    row["DisplayName"].ToString()
                ));
            }

            return list;
        }
    }
}
