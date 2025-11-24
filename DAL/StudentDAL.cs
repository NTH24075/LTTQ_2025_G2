using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DAL
{
    public class StudentDAL
    {
        public StudentDTO GetStudentByAccountId(long accountId)
        {
            string query = @"
                SELECT TOP 1 
                    student_id,
                    studentName,
                    studentCode,
                    dateOfBirth,
                    email,
                    phoneNumber,
                    studentAddress,
                    img,
                    account_id
                FROM Student
                WHERE account_id = @id";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { accountId });

            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];

            return new StudentDTO
            {
                StudentId = Convert.ToInt64(row["student_id"]),
                StudentName = row["studentName"].ToString(),
                StudentCode = row["studentCode"].ToString(),
                DateOfBirth = row["dateOfBirth"].ToString(),
                Email = row["email"].ToString(),
                PhoneNumber = row["phoneNumber"].ToString(),
                StudentAddress = row["studentAddress"].ToString(),
                Img = row["img"].ToString(),
                AccountId = Convert.ToInt64(row["account_id"])
            };
        }
    }
}
