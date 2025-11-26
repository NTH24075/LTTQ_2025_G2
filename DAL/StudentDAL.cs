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
        public StudentDetailViewDTO GetStudentDetailByAccountId(long accountId)
        {
            string query = @"
                SELECT TOP 1 
                    s.student_id,
                    s.studentName,
                    s.studentCode,
                    s.dateOfBirth,
                    s.email,
                    s.phoneNumber,
                    s.studentAddress,
                    s.img,
                    s.account_id,
                    f.facultyName
                FROM Student s
                LEFT JOIN Class c ON c.clazz_id = s.clazz_id
                LEFT JOIN Faculty f ON f.faculty_id = c.faculty_id
                WHERE s.account_id = @id";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { accountId });

            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];

            return new StudentDetailViewDTO
            {
                StudentId = Convert.ToInt64(row["student_id"]),
                StudentName = row["studentName"].ToString(),
                StudentCode = row["studentCode"].ToString(),
                DateOfBirth = row["dateOfBirth"].ToString(),
                Email = row["email"].ToString(),
                PhoneNumber = row["phoneNumber"].ToString(),
                StudentAddress = row["studentAddress"].ToString(),
                Img = row["img"].ToString(),
                AccountId = Convert.ToInt64(row["account_id"]),
                FacultyName = row["facultyName"].ToString()
            };
        }
        public List<StudentViewDTO> GetAllStudents()
        {
            string query = @"
        SELECT 
            s.student_id,
            s.studentName,
            s.studentCode,
            s.email,
            s.account_id,
            s.phoneNumber,
            f.facultyName,
            c.clazzName
        FROM Student s
        LEFT JOIN Class c ON c.clazz_id = s.clazz_id
        LEFT JOIN Faculty f ON f.faculty_id = c.faculty_id
        WHERE s.flagDelete = 0";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            List<StudentViewDTO> list = new List<StudentViewDTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new StudentViewDTO()
                {
                    AccountId = Convert.ToInt64(row["account_id"]),
                    StudentId = Convert.ToInt64(row["student_id"]),
                    StudentName = row["studentName"].ToString(),
                    StudentCode = row["studentCode"].ToString(),
                    Email = row["email"].ToString(),
                    PhoneNumber = row["phoneNumber"].ToString(),
                    facultyName = row["facultyName"].ToString(),
                    className = row["clazzName"].ToString()
                });
            }

            return list;
        }

        public List<StudentViewDTO> SearchStudents(string keyword)
        {
            string kw = "%" + (keyword ?? string.Empty).Trim() + "%";

            string query = @"
                    SELECT 
                        s.student_id,
                        s.studentName,
                        s.studentCode,
                        s.email,
                        s.account_id,
                        s.phoneNumber,
                        f.facultyName,
                        c.clazzName
                    FROM Student s
                    LEFT JOIN Class c   ON c.clazz_id   = s.clazz_id
                    LEFT JOIN Faculty f ON f.faculty_id = c.faculty_id
                    WHERE s.flagDelete = 0
                      AND (
                            s.studentName LIKE @kw1 OR
                            s.studentCode LIKE @kw2 OR
                            s.email       LIKE @kw3 OR
                            s.phoneNumber LIKE @kw4
                          );";

            // TRUYỀN ĐÚNG 4 THAM SỐ (tương ứng @kw1..@kw4)
            DataTable dt = DataProvider.Instance.ExecuteQuery(
                query,
                new object[] { kw, kw, kw, kw }
            );

            var list = new List<StudentViewDTO>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new StudentViewDTO
                {
                    StudentId = Convert.ToInt64(row["student_id"]),
                    StudentName = row["studentName"]?.ToString(),
                    StudentCode = row["studentCode"]?.ToString(),
                    Email = row["email"]?.ToString(),
                    PhoneNumber = row["phoneNumber"]?.ToString(),
                    facultyName = row["facultyName"]?.ToString(),
                    className = row["clazzName"]?.ToString(),
                    // account_id có thể NULL → nhớ check DBNull
                    AccountId = row["account_id"] == DBNull.Value ? (long?)null : Convert.ToInt64(row["account_id"])
                });
            }
            return list;
        }
           
        public bool UpdateStudentBasic(StudentDTO dto)
        {
            string sql = @"
                    UPDATE Student
                    SET
                        studentName    = @name,
                        studentCode    = @code,
                        dateOfBirth    = @dob,
                        email          = @mail,
                        phoneNumber    = @phone,
                        studentAddress = @addr,
                        img            = @img
                    WHERE student_id   = @id";

            int rows = DataProvider.Instance.ExecuteNonQuery(sql, new object[]
            {
                dto.StudentName,
                dto.StudentCode,
                dto.DateOfBirth,
                dto.Email,
                dto.PhoneNumber,
                dto.StudentAddress,
                (object)dto.Img ?? DBNull.Value,
                dto.StudentId
            });

            return rows > 0;
        }

        public bool IsStudentCodeExists(string code)
        {
            string q = "SELECT COUNT(*) FROM Student WHERE studentCode = @c";
            int c = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(q, new object[] { code }));
            return c > 0;
        }

        public bool IsStudentEmailExists(string email)
        {
            string q = "SELECT COUNT(*) FROM Student WHERE email = @e";
            int c = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(q, new object[] { email }));
            return c > 0;
        }

        public bool IsPhoneExists(string phone)
        {
            string q = "SELECT COUNT(*) FROM Student WHERE phoneNumber = @p";
            int c = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(q, new object[] { phone }));
            return c > 0;
        }

        public long InsertStudent(StudentDTO s)
        {
            string q = @"
                    INSERT INTO Student(
                        studentName, studentCode, dateOfBirth, email, phoneNumber,
                        studentGender, studentAddress, img, flagDelete,
                        account_id, clazz_id, team_id
                    ) VALUES(
                        @name, @code, @dob, @mail, @phone,
                        @gender, @addr, @img, 0,
                        @acc, @clazz, NULL
                    );
                    SELECT SCOPE_IDENTITY();";

            object o = DataProvider.Instance.ExecuteScalar(q, new object[]
            {
                s.StudentName, s.StudentCode, s.DateOfBirth, s.Email, s.PhoneNumber,
                s.StudentGender, s.StudentAddress, s.Img ?? (object)DBNull.Value,
                s.AccountId.HasValue ? (object)s.AccountId.Value : DBNull.Value,
                s.ClazzId.HasValue ? (object)s.ClazzId.Value : DBNull.Value
            });

            if (o == null || o == DBNull.Value) throw new Exception("Thêm Student thất bại.");
            return Convert.ToInt64(o);
        }
    }
}

