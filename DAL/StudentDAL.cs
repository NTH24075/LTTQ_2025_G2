using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DAL
{
    public class StudentDAL
    {
        private static StudentDAL instance;
        public static StudentDAL Instance
        {
            get
            {
                if (instance == null) instance = new StudentDAL();
                return instance;
            }
        }
        private StudentDAL() { }

        public List<StudentDTO> GetTeamMembersByAccountId(long accountId)
        {
            List<StudentDTO> list = new List<StudentDTO>();

            string query = @"
            SELECT s2.*
            FROM Account a
            JOIN Student s1 ON s1.account_id = a.account_id
            JOIN Student s2 ON s2.team_id    = s1.team_id
            WHERE a.account_id = @accountId
              AND s2.flagDelete = 0";

           
            DataTable data = DataProvider.Instance.ExecuteQuery(
                query,
                new object[] { accountId }  
            );

            foreach (DataRow row in data.Rows)
            {
                StudentDTO s = new StudentDTO()
                {
                    StudentId = (long)row["student_id"],
                    StudentName = row["studentName"].ToString(),
                    StudentCode = row["studentCode"].ToString(),
                    DateOfBirth = row["dateOfBirth"].ToString(),
                    Email = row["email"].ToString(),
                    PhoneNumber = row["phoneNumber"].ToString(),
                    StudentGender = (bool)row["studentGender"],
                    StudentAddress = row["studentAddress"].ToString(),
                    Img = row["img"].ToString(),
                    FlagDelete = (bool)row["flagDelete"],
                    AccountId = row["account_id"] == DBNull.Value ? (long?)null : (long)row["account_id"],
                    ClazzId = row["clazz_id"] == DBNull.Value ? (long?)null : (long)row["clazz_id"],
                    TeamId = row["team_id"] == DBNull.Value ? (long?)null : (long)row["team_id"]
                };
                list.Add(s);
            }

            return list;
        }

    }
}
