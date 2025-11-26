using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DAL
{
    public class TeacherDAL
    {
        private TeacherViewDetailDTO MapRow(DataRow row)
        {
            return new TeacherViewDetailDTO
            {
                TeacherId      = Convert.ToInt64(row["teacher_id"]),
                TeacherCode    = row["teacherCode"].ToString(),
                TeacherName    = row["teacherName"].ToString(),
                DateOfBirth    = row["dateOfBirth"].ToString(),
                Email          = row["email"].ToString(),
                PhoneNumber    = row["phoneNumber"].ToString(),
                TeacherGender  = row["teacherGender"] != DBNull.Value && Convert.ToBoolean(row["teacherGender"]),
                TeacherAddress = row["teacherAddress"].ToString(),
                Img            = row["img"].ToString(),
                FlagDelete     = row["flagDelete"] != DBNull.Value && Convert.ToBoolean(row["flagDelete"]),
                FacultyId      = row["faculty_id"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["faculty_id"]),
                DegreeId       = row["degree_id"]  == DBNull.Value ? (int?)null : Convert.ToInt32(row["degree_id"]),
                AccountId      = row["account_id"] == DBNull.Value ? (long?)null : Convert.ToInt64(row["account_id"]),

                // Join fields (có thể null)
                FacultyName    = row.Table.Columns.Contains("facultyName") && row["facultyName"] != DBNull.Value ? row["facultyName"].ToString() : null,
                DegreeName     = row.Table.Columns.Contains("degreeName")  && row["degreeName"]  != DBNull.Value ? row["degreeName"].ToString()  : null
            };
        }

        public List<TeacherViewDetailDTO> GetAll()
        {
            string sql = @"
                SELECT 
                    t.teacher_id, t.teacherCode, t.teacherName, t.dateOfBirth, t.email, t.phoneNumber,
                    t.teacherGender, t.teacherAddress, t.img, t.flagDelete, 
                    t.faculty_id, t.degree_id, t.account_id,
                    f.facultyName, d.degreeName
                FROM Teacher t
                LEFT JOIN Faculty f ON f.faculty_id = t.faculty_id
                LEFT JOIN Degree  d ON d.degree_id  = t.degree_id
                ORDER BY t.teacherName";

            DataTable dt = DataProvider.Instance.ExecuteQuery(sql);
            var list = new List<TeacherViewDetailDTO>();
            foreach (DataRow r in dt.Rows) list.Add(MapRow(r));
            return list;
        }

        public TeacherViewDetailDTO GetById(long teacherId)
        {
            string sql = @"
                SELECT 
                    t.teacher_id, t.teacherCode, t.teacherName, t.dateOfBirth, t.email, t.phoneNumber,
                    t.teacherGender, t.teacherAddress, t.img, t.flagDelete, 
                    t.faculty_id, t.degree_id, t.account_id,
                    f.facultyName, d.degreeName
                FROM Teacher t
                LEFT JOIN Faculty f ON f.faculty_id = t.faculty_id
                LEFT JOIN Degree  d ON d.degree_id  = t.degree_id
                WHERE t.teacher_id = @id";

            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new object[] { teacherId });
            if (dt.Rows.Count == 0) return null;
            return MapRow(dt.Rows[0]);
        }
        public bool UpdateBasicInfo(TeacherDTO dto)
        {
            string sql = @"
            UPDATE Teacher
            SET teacherCode    = @code,
                teacherName    = @name,
                dateOfBirth    = @dob,
                email          = @em,
                phoneNumber    = @ph,
                teacherAddress = @addr,
                degree_id      = @deg
            WHERE teacher_id   = @id";

            int n = DataProvider.Instance.ExecuteNonQuery(sql, new object[] {
            dto.TeacherCode ?? (object)DBNull.Value,
            dto.TeacherName ?? (object)DBNull.Value,
            dto.DateOfBirth ?? (object)DBNull.Value,
            dto.Email ?? (object)DBNull.Value,
            dto.PhoneNumber ?? (object)DBNull.Value,
            dto.TeacherAddress ?? (object)DBNull.Value,
            dto.DegreeId.HasValue ? (object)dto.DegreeId.Value : DBNull.Value,
            dto.TeacherId
        });
            return n > 0;
        }
        public TeacherViewDetailDTO GetByAccountId(long accountId)
        {
            string sql = @"
                SELECT 
                    t.teacher_id, t.teacherCode, t.teacherName, t.dateOfBirth, t.email, t.phoneNumber,
                    t.teacherGender, t.teacherAddress, t.img, t.flagDelete, 
                    t.faculty_id, t.degree_id, t.account_id,
                    f.facultyName, d.degreeName
                FROM Teacher t
                LEFT JOIN Faculty f ON f.faculty_id = t.faculty_id
                LEFT JOIN Degree  d ON d.degree_id  = t.degree_id
                WHERE t.account_id = @id";

            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new object[] { accountId });
            if (dt.Rows.Count == 0) return null;
            return MapRow(dt.Rows[0]);
        }

        
        public bool Insert(TeacherViewDetailDTO t)
        {
            string sql = @"
                INSERT INTO Teacher
                (teacherCode, teacherName, dateOfBirth, email, phoneNumber, teacherGender, 
                 teacherAddress, img, flagDelete, faculty_id, degree_id, account_id)
                VALUES
                (@code, @name, @dob, @mail, @phone, @gender, 
                 @addr, @img, @flag, @fid, @did, @acc)";

            int n = DataProvider.Instance.ExecuteNonQuery(sql, new object[]
            {
                t.TeacherCode, t.TeacherName, t.DateOfBirth, t.Email, t.PhoneNumber, t.TeacherGender,
                t.TeacherAddress, t.Img, t.FlagDelete, (object)t.FacultyId ?? DBNull.Value,
                (object)t.DegreeId ?? DBNull.Value, (object)t.AccountId ?? DBNull.Value
            });
            return n > 0;
        }

        public bool Update(TeacherViewDetailDTO t)
        {
            string sql = @"
                UPDATE Teacher
                   SET teacherCode    = @code,
                       teacherName    = @name,
                       dateOfBirth    = @dob,
                       email          = @mail,
                       phoneNumber    = @phone,
                       teacherGender  = @gender,
                       teacherAddress = @addr,
                       img            = @img,
                       flagDelete     = @flag,
                       faculty_id     = @fid,
                       degree_id      = @did,
                       account_id     = @acc
                 WHERE teacher_id     = @id";

            int n = DataProvider.Instance.ExecuteNonQuery(sql, new object[]
            {
                t.TeacherCode, t.TeacherName, t.DateOfBirth, t.Email, t.PhoneNumber, t.TeacherGender,
                t.TeacherAddress, t.Img, t.FlagDelete, (object)t.FacultyId ?? DBNull.Value,
                (object)t.DegreeId ?? DBNull.Value, (object)t.AccountId ?? DBNull.Value,
                t.TeacherId
            });
            return n > 0;
        }

        public bool Delete(long teacherId)
        {
            string sql = "DELETE FROM Teacher WHERE teacher_id = @id";
            int n = DataProvider.Instance.ExecuteNonQuery(sql, new object[] { teacherId });
            return n > 0;
        }
    
        public TeacherViewDetailDTO GetTeacherDTOByAccountId(long accountId)
        {
            string query = @"
                SELECT TOP 1
                    t.teacher_id,
                    t.teacherCode,
                    t.teacherName,
                    t.dateOfBirth,
                    t.email,
                    t.phoneNumber,
                    t.teacherAddress,
                    t.img,
                    t.account_id,
                    f.facultyName
                FROM Teacher t
                LEFT JOIN Faculty f ON f.faculty_id = t.faculty_id
                WHERE t.account_id = @id";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { accountId });
            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];

            return new TeacherViewDetailDTO
            {
                TeacherId = Convert.ToInt64(row["teacher_id"]),
                TeacherCode = row["teacherCode"].ToString(),
                TeacherName = row["teacherName"].ToString(),
                DateOfBirth = row["dateOfBirth"].ToString(),
                Email = row["email"].ToString(),
                PhoneNumber = row["phoneNumber"].ToString(),
                TeacherAddress = row["teacherAddress"].ToString(),
                Img = row["img"].ToString(),
                AccountId = Convert.ToInt64(row["account_id"]),
                FacultyName = row["facultyName"].ToString()
            };
        }
        public List<TeacherViewDetailDTO> Search(string keyword, int? facultyId, int? degreeId)
        {
            string sql = @"
        SELECT 
            t.teacher_id, t.teacherCode, t.teacherName, t.dateOfBirth, t.email, t.phoneNumber,
            t.teacherGender, t.teacherAddress, t.img, t.flagDelete, 
            t.faculty_id, t.degree_id, t.account_id,
            f.facultyName, d.degreeName
        FROM Teacher t
        LEFT JOIN Faculty f ON f.faculty_id = t.faculty_id
        LEFT JOIN Degree  d ON d.degree_id  = t.degree_id
        WHERE 1 = 1";

            var parameters = new List<object>();

            // Tìm kiếm theo teachercode, teachername, email, phone
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += @" AND (
                    t.teacherCode  LIKE @kw1 OR 
                    t.teacherName  LIKE @kw2 OR 
                    t.email        LIKE @kw3 OR 
                    t.phoneNumber  LIKE @kw4
                 )";
                string like = $"%{keyword.Trim()}%";
                parameters.Add(like); // @kw1
                parameters.Add(like); // @kw2
                parameters.Add(like); // @kw3
                parameters.Add(like); // @kw4
            }

            // Lọc theo Khoa (ComboBox)
            if (facultyId.HasValue)
            {
                sql += " AND t.faculty_id = @fid";
                parameters.Add(facultyId.Value); // @fid
            }

            // (Nếu sau này bạn có combobox học vị)
            if (degreeId.HasValue)
            {
                sql += " AND t.degree_id = @did";
                parameters.Add(degreeId.Value); // @did
            }

            sql += " ORDER BY t.teacherName";

            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, parameters.ToArray());

            var list = new List<TeacherViewDetailDTO>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new TeacherViewDetailDTO
                {
                    TeacherId = Convert.ToInt64(row["teacher_id"]),
                    TeacherCode = row["teacherCode"].ToString(),
                    TeacherName = row["teacherName"].ToString(),
                    DateOfBirth = row["dateOfBirth"].ToString(),
                    Email = row["email"].ToString(),
                    PhoneNumber = row["phoneNumber"].ToString(),
                    TeacherGender = row["teacherGender"] != DBNull.Value && Convert.ToBoolean(row["teacherGender"]),
                    TeacherAddress = row["teacherAddress"].ToString(),
                    Img = row["img"].ToString(),
                    FlagDelete = row["flagDelete"] != DBNull.Value && Convert.ToBoolean(row["flagDelete"]),
                    FacultyId = row["faculty_id"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["faculty_id"]),
                    DegreeId = row["degree_id"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["degree_id"]),
                    AccountId = row["account_id"] == DBNull.Value ? (long?)null : Convert.ToInt64(row["account_id"]),
                    FacultyName = row.Table.Columns.Contains("facultyName") ? row["facultyName"].ToString() : null,
                    DegreeName = row.Table.Columns.Contains("degreeName") ? row["degreeName"].ToString() : null
                });
            }
            return list;
        }
        public bool InsertTeacher(TeacherDTO dto)
        {
            // DB schema:
            // teacherCode, teacherName, dateOfBirth (varchar), email, phoneNumber,
            // teacherGender (bit), teacherAddress, img, flagDelete (bit),
            // faculty_id (int), degree_id (int), account_id (bigint, unique, có thể null)

            string query = @"
                INSERT INTO Teacher
                    (teacherCode, teacherName, dateOfBirth, email, phoneNumber,
                     teacherGender, teacherAddress, img, flagDelete,
                     faculty_id, degree_id, account_id)
                VALUES
                    (@teacherCode, @teacherName, @dateOfBirth, @email, @phoneNumber,
                     @teacherGender, @teacherAddress, @img, @flagDelete,
                     @faculty_id, @degree_id, @account_id)";

            int result = DataProvider.Instance.ExecuteNonQuery(
                query,
                new object[]
                {
                    dto.TeacherCode,
                    dto.TeacherName,
                    dto.DateOfBirth,
                    dto.Email,
                    dto.PhoneNumber,
                    dto.TeacherGender,
                    dto.TeacherAddress,
                    dto.Img ?? "",
                    dto.FlagDelete,
                    (object)dto.FacultyId ?? System.DBNull.Value,
                    (object)dto.DegreeId ?? System.DBNull.Value,
                    (object)dto.AccountId ?? System.DBNull.Value
                });

            return result > 0;
        }
    }

}
