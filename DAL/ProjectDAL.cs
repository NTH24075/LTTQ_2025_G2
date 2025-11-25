using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DAL
{
    public class ProjectDAL
    {
        private ProjectDTO MapRow(DataRow r)
        {
            return new ProjectDTO
            {
                ProjectId = Convert.ToInt64(r["project_id"]),
                Name = r["name"].ToString(),
                Content = r["content"].ToString(),
                Img = r["img"].ToString(),
                Description = r["description"].ToString(),
                ProjectStatus = r["projectStatus"] != DBNull.Value && Convert.ToBoolean(r["projectStatus"]),
                SemesterId = r["semester_id"] == DBNull.Value ? (int?)null : Convert.ToInt32(r["semester_id"]),
                //TeamName = r.Table.Columns.Contains("teamName") ? r["teamName"].ToString() : null,
                //TeacherName = r.Table.Columns.Contains("teacherName") ? r["teacherName"].ToString() : null,
                //SemesterName = r.Table.Columns.Contains("semesterName") ? r["semesterName"].ToString() : null
            };
        }

        /// Kiểm tra Team đã gán project chưa
        public bool TeamHasProject(long teamId)
        {
            string sql = "SELECT project_id FROM Team WHERE team_id = @tid";
            object v = DataProvider.Instance.ExecuteScalar(sql, new object[] { teamId });
            return v != null && v != DBNull.Value;
        }

        

        /// Tạo Project mới và gán vào Team
        public bool CreateProjectAndAttachToTeam(
            long teamId,
            string name, string content, string img, string description,
            bool status, int? semesterId)
        {
            // 1) Tạo project mới
            string insertSql = @"
                INSERT INTO Project([name],[content],img,[description],projectStatus,semester_id)
                VALUES(@n,@c,@i,@d,@st,@sem);
                SELECT SCOPE_IDENTITY();";

            object newIdObj = DataProvider.Instance.ExecuteScalar(insertSql, new object[]
            { name, content, img, description, status, (object)semesterId ?? DBNull.Value });

            if (newIdObj == null) return false;
            long newProjectId = Convert.ToInt64(newIdObj);

            // 2) Gán vào team
            string updateSql = "UPDATE Team SET project_id = @pid WHERE team_id = @tid";
            int n = DataProvider.Instance.ExecuteNonQuery(updateSql, new object[] { newProjectId, teamId });
            return n > 0;
        }

        public long? GetTeamIdByStudentAccount(long accountId)
        {
            string sql = "SELECT s.team_id FROM Student s WHERE s.account_id = @acc";
            object v = DataProvider.Instance.ExecuteScalar(sql, new object[] { accountId });
            if (v == null || v == DBNull.Value) return null;
            return Convert.ToInt64(v);
        }
        public ProjectDTO GetProjectByStudentAccount(long accountId)
        {
            string query = @"
                SELECT TOP 1 
                    p.project_id,
                    p.name,
                    p.content,
                    p.img,
                    p.description,
                    p.projectStatus,
                    p.semester_id
                FROM Account a
                JOIN Student s ON s.account_id = a.account_id
                LEFT JOIN Team t ON t.team_id = s.team_id
                LEFT JOIN Project p ON p.project_id = t.project_id
                WHERE a.account_id = @accountId";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { accountId });

            if (dt.Rows.Count == 0 || dt.Rows[0]["project_id"] == DBNull.Value)
                return null; // sinh viên chưa có project

            DataRow row = dt.Rows[0];

            return new ProjectDTO
            {
                ProjectId = Convert.ToInt64(row["project_id"]),
                Name = row["name"].ToString(),
                Content = row["content"].ToString(),
                Img = row["img"].ToString(),
                Description = row["description"].ToString(),
                ProjectStatus = Convert.ToBoolean(row["projectStatus"]),
                SemesterId = row["semester_id"] == DBNull.Value ? null : (int?)Convert.ToInt32(row["semester_id"])

            };
        }
        /// Lấy Project theo Team
        public ProjectDTO GetProjectByTeamId(long teamId)
        {
            string sql = @"
                SELECT TOP 1
                    p.project_id, p.[name], p.[content], p.img, p.[description],
                    p.projectStatus, p.semester_id,
                    t.teamName,
                    te.teacherName,
                    se.semesterName
                FROM Team t
                LEFT JOIN Project p  ON p.project_id = t.project_id
                LEFT JOIN Teacher te ON te.teacher_id = t.teacher_id
                LEFT JOIN Semester se ON se.semester_id = p.semester_id
                WHERE t.team_id = @tid";

            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new object[] { teamId });

            if (dt.Rows.Count == 0) return null;          // không có team
            if (dt.Rows[0]["project_id"] == DBNull.Value)  // team chưa có project
                return null;

            return MapRow(dt.Rows[0]);
        }



        public ProjectDetailDTO GetProjectWithDetailByStudentAccount(long accountId)
        {
            string query = @"
            SELECT TOP 1 
                p.project_id,
                p.name,
                p.content,
                p.img,
                p.description,
                p.projectStatus,
                p.semester_id,

                t.teamName,
                te.teacherName,
                se.semesterName

            FROM Account a
            JOIN Student s ON s.account_id = a.account_id
            LEFT JOIN Team t ON t.team_id = s.team_id
            LEFT JOIN Teacher te ON te.teacher_id = t.teacher_id
            LEFT JOIN Project p ON p.project_id = t.project_id
            LEFT JOIN Semester se ON se.semester_id = p.semester_id
            WHERE a.account_id = @accountId";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { accountId });

            if (dt.Rows.Count == 0 || dt.Rows[0]["project_id"] == DBNull.Value)
                return null;

            DataRow row = dt.Rows[0];

            return new ProjectDetailDTO
            {
                ProjectId = Convert.ToInt64(row["project_id"]),
                Name = row["name"].ToString(),
                Content = row["content"].ToString(),
                Img = row["img"].ToString(),
                Description = row["description"].ToString(),
                ProjectStatus = Convert.ToBoolean(row["projectStatus"]),
                SemesterId = row["semester_id"] == DBNull.Value
                                ? null
                                : (int?)Convert.ToInt32(row["semester_id"]),

                TeamName = row["teamName"].ToString(),
                TeacherName = row["teacherName"].ToString(),
                SemesterName = row["semesterName"].ToString()
            };
        }

    }
}
