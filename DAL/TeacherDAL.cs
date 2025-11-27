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
    public class TeacherDAL
    {
        // 1 - 1
        // teacher -account
        //1-n
        //teacher - team, teacher-class, teacher-notification, teacher-document, teacher-councilmember, 
        //n-1
        // teacher-faculty, teacher-degree, 
        //gian tiep
        //teacher-team-project, Teacher → Team → Student, Teacher → Class → Student, Teacher —< CouncilMember >— Council

        public DataTable GetAllProjects()
        {
            string query = @"
            SELECT 
                project_id AS [Mã đồ án],
                CONVERT(NVARCHAR(MAX), name) AS [Tên đồ án],
                CONVERT(NVARCHAR(MAX), content) AS [Nội dung],
                semester_id AS [Học kỳ]
            FROM Project";

            return DataProvider.Instance.ExecuteQuery(query);
        }
        
        public DataTable GetProjectsByTeacher(long teacherId)
        {
            string query = @"
        SELECT 
            p.project_id AS [Mã đồ án],
            CONVERT(NVARCHAR(MAX), p.name) AS [Tên đồ án],
            CONVERT(NVARCHAR(MAX), p.content) AS [Nội dung],
            p.semester_id AS [Học kỳ]
        FROM Project p
        INNER JOIN Team t ON t.project_id = p.project_id
        WHERE t.teacher_id = @tid";

            return DataProvider.Instance.ExecuteQuery(query, new object[] { teacherId });
        }
        // 0 - chưa duyệt, 1 - đã duyệt, 2 - bị hủy, 3 - tất cả
        public DataTable GetProjectsByTeacherAndStatus(long teacherId, int status)
        {
            string query = @"
        SELECT 
            p.project_id AS [Mã đồ án],
            CONVERT(NVARCHAR(MAX), p.name) AS [Tên đồ án],
            CASE 
                WHEN p.projectStatus = 0 THEN N'Chưa duyệt'
                WHEN p.projectStatus = 1 THEN N'Đã duyệt'
                WHEN p.projectStatus = 2 THEN N'Bị hủy'
            END AS [Trạng thái],
            p.semester_id AS [Học kỳ]
        FROM Project p
        INNER JOIN Team t ON t.project_id = p.project_id
        WHERE t.teacher_id = @tid";

            // status = 3 => tất cả
            if (status != 3)
            {
                query += " AND p.projectStatus = @st";
                return DataProvider.Instance.ExecuteQuery(
                    query,
                    new object[] { teacherId, status }
                );
            }

            return DataProvider.Instance.ExecuteQuery(
                query,
                new object[] { teacherId }
            );
        }
        // Lấy chi tiết đồ án theo projectId
        public DataRow GetProjectDetail(long projectId)
        {
            string query = @"
        SELECT 
            project_id,
            CONVERT(NVARCHAR(MAX), name) AS name,
            CONVERT(NVARCHAR(MAX), content) AS content,
            CONVERT(NVARCHAR(MAX), description) AS description,
            projectStatus,
            semester_id
        FROM Project
        WHERE project_id = @id";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { projectId });

            if (dt.Rows.Count == 0) return null;

            return dt.Rows[0];
        }
        // Cập nhật trạng thái đồ án
        public int UpdateProjectStatus(long projectId, int status)
        {
            string query = "UPDATE Project SET projectStatus = @st WHERE project_id = @id";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { status, projectId });
        }

        // Tìm kiếm đồ án theo tên và trạng thái
        public DataTable SearchProjectsByTeacher(string name, int status, long teacherId)
        {
            string query = @"
        SELECT 
            p.project_id AS [Mã đồ án],
            CONVERT(NVARCHAR(MAX), p.name) AS [Tên đồ án],
            CASE 
                WHEN p.projectStatus = 0 THEN N'Chưa duyệt'
                WHEN p.projectStatus = 1 THEN N'Đã duyệt'
                WHEN p.projectStatus = 2 THEN N'Bị hủy'
            END AS [Trạng thái],
            p.semester_id AS [Học kỳ]
        FROM Project p
        INNER JOIN Team t ON t.project_id = p.project_id
        WHERE t.teacher_id = @tid
          AND p.name LIKE @name";

            if (status != 3)
                query += " AND p.projectStatus = @status";

            if (status == 3)
            {
                return DataProvider.Instance.ExecuteQuery(
                    query,
                    new object[]
                    {
                teacherId,
                "%" + name + "%"
                    });
            }
            else
            {
                return DataProvider.Instance.ExecuteQuery(
                    query,
                    new object[]
                    {
                teacherId,
                "%" + name + "%",
                status
                    });
            }
        }
        public DataTable GetApprovedProjectsByTeacher(long teacherId)
        {
            string query = @"
        SELECT p.project_id AS [Mã đồ án],
               CONVERT(NVARCHAR(MAX), p.name) AS [Tên đồ án]
        FROM Project p
        INNER JOIN Team t ON t.project_id = p.project_id
        WHERE p.projectStatus = 1
          AND t.teacher_id = @tid";

            return DataProvider.Instance.ExecuteQuery(query, new object[] { teacherId });
        }
        public long? GetTeacherIdByAccountId(long accountId)
        {
            string query = @"
        SELECT teacher_id
        FROM Teacher
        WHERE account_id = @accId
          AND flagDelete = 0";

            DataTable dt = DataProvider.Instance.ExecuteQuery(
                query,
                new object[] { accountId }
            );

            if (dt.Rows.Count == 0)
                return null;

            return Convert.ToInt64(dt.Rows[0]["teacher_id"]);
        }
    }
}
