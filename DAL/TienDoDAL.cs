using System;
using System.Data;

namespace LTTQ_G2_2025.DAL
{
    /// <summary>
    /// DAL cho tab Tiến độ của giảng viên
    /// Teacher -> Team -> Project -> Milestone -> Submission -> Evaluation
    /// </summary>
    public class TienDoDAL
    {
        #region 1. Dự án đã duyệt của teacher

        /// <summary>
        /// Lấy danh sách đồ án đã được duyệt (projectStatus = 1)
        /// của 1 giảng viên (thông qua Team.teacher_id)
        /// </summary>
        public DataTable GetApprovedProjectsByTeacher(long teacherId)
        {
            string query = @"
                SELECT 
                    p.project_id AS [Mã đồ án],
                    CONVERT(NVARCHAR(MAX), p.name) AS [Tên đồ án],
                    N'Đã duyệt' AS [Trạng thái],
                    p.semester_id AS [Học kỳ]
                FROM Project p
                INNER JOIN Team t ON t.project_id = p.project_id
                WHERE p.projectStatus = 1
                  AND t.teacher_id = @tid";

            return DataProvider.Instance.ExecuteQuery(
                query,
                new object[] { teacherId }
            );
        }

        #endregion

        #region 2. Thông tin nhóm & thành viên

        /// <summary>
        /// Lấy danh sách sinh viên trong nhóm của 1 đồ án
        /// (projectId + teacherId dùng để đảm bảo đúng giảng viên)
        /// </summary>
        public DataTable GetTeamMembersByProject(long projectId, long teacherId)
        {
            string query = @"
        SELECT 
            s.student_id                     AS [IdSV],      -- ẩn trên grid, dùng nội bộ
            s.studentCode                    AS [Mã SV],
            CONVERT(NVARCHAR(MAX), s.studentName) AS [Họ tên],
            c.className                      AS [Lớp],
            s.email                          AS [Email],
            t.teamName                       AS [Nhóm]
        FROM Team t
        INNER JOIN Student s ON s.team_id = t.team_id
        LEFT JOIN Class c    ON c.class_id = s.class_id
        WHERE t.project_id = @pid
          AND t.teacher_id = @tid
          AND s.flagDelete = 0";

            return DataProvider.Instance.ExecuteQuery(
                query,
                new object[] { projectId, teacherId }
            );
        }

        /// <summary>
        /// Lấy thông tin nhóm (1 hàng) từ project
        /// (trong trường hợp bạn cần team_id để dùng tiếp)
        /// </summary>
        public DataRow GetTeamByProject(long projectId, long teacherId)
        {
            string query = @"
                SELECT TOP 1
                    t.team_id,
                    t.teamName,
                    t.teacher_id,
                    t.project_id
                FROM Team t
                WHERE t.project_id = @pid
                  AND t.teacher_id = @tid";

            DataTable dt = DataProvider.Instance.ExecuteQuery(
                query,
                new object[] { projectId, teacherId }
            );

            if (dt.Rows.Count == 0) return null;
            return dt.Rows[0];
        }

        #endregion

        #region 3. Milestone & tiến độ nộp bài

        /// <summary>
        /// Lấy danh sách các milestone của 1 đồ án
        /// </summary>
        public DataTable GetMilestonesByProject(long projectId)
        {
            string query = @"
                SELECT 
                    m.milestone_id            AS [Mã mốc],
                    CONVERT(NVARCHAR(MAX), m.milestoneName) AS [Tên mốc],
                    m.deadline               AS [Deadline],
                    m.weight                 AS [Trọng số],
                    m.status                 AS [Trạng tháiRaw]
                FROM Milestone m
                WHERE m.project_id = @pid
                ORDER BY m.deadline";

            return DataProvider.Instance.ExecuteQuery(
                query,
                new object[] { projectId }
            );
        }

        /// <summary>
        /// Lấy tiến độ nộp bài của từng mốc đối với 1 team
        /// (dựa vào Submission so với deadline)
        /// </summary>
        public DataTable GetMilestoneProgressByTeam(long projectId, long teacherId)
        {
            string query = @"
                SELECT 
                    m.milestone_id                        AS [Mã mốc],
                    CONVERT(NVARCHAR(MAX), m.milestoneName) AS [Tên mốc],
                    m.deadline                           AS [Deadline],
                    sub.submissionDate                   AS [Ngày nộp],
                    CASE 
                        WHEN sub.submissionDate IS NULL THEN N'Chưa nộp'
                        WHEN sub.submissionDate <= m.deadline THEN N'Đúng hạn'
                        ELSE N'Trễ hạn'
                    END                                  AS [Trạng thái nộp]
                FROM Milestone m
                INNER JOIN Team t ON t.project_id = m.project_id
                LEFT JOIN Submission sub 
                       ON sub.milestone_id = m.milestone_id 
                      AND sub.team_id = t.team_id
                WHERE m.project_id = @pid
                  AND t.teacher_id = @tid
                ORDER BY m.deadline";

            return DataProvider.Instance.ExecuteQuery(
                query,
                new object[] { projectId, teacherId }
            );
        }

        #endregion

        #region 4. Chấm điểm (Evaluation)

        /// <summary>
        /// Lấy bảng điểm theo từng milestone và từng sinh viên
        /// của 1 project (teacher xác nhận bằng teacherId)
        /// </summary>
        public DataTable GetEvaluationsByProject(long projectId, long teacherId)
        {
            string query = @"
                SELECT 
                    s.student_id                      AS [Mã SV],
                    s.studentCode                     AS [Mã số],
                    CONVERT(NVARCHAR(MAX), s.studentName) AS [Họ tên],
                    m.milestone_id                    AS [Mã mốc],
                    CONVERT(NVARCHAR(MAX), m.milestoneName) AS [Mốc],
                    ev.evaluation_id                  AS [Mã đánh giá],
                    ev.score                          AS [Điểm],
                    CONVERT(NVARCHAR(MAX), ev.comment) AS [Nhận xét]
                FROM Team t
                INNER JOIN Student s   ON s.team_id = t.team_id
                INNER JOIN Milestone m ON m.project_id = t.project_id
                LEFT JOIN Evaluation ev 
                       ON ev.student_id   = s.student_id
                      AND ev.milestone_id = m.milestone_id
                WHERE t.project_id  = @pid
                  AND t.teacher_id  = @tid
                  AND s.flagDelete = 0
                ORDER BY s.studentName, m.deadline";

            return DataProvider.Instance.ExecuteQuery(
                query,
                new object[] { projectId, teacherId }
            );
        }

        /// <summary>
        /// Thêm mới hoặc cập nhật điểm cho 1 sinh viên tại 1 milestone.
        /// Nếu đã tồn tại Evaluation thì UPDATE, chưa có thì INSERT.
        /// </summary>
        public int InsertOrUpdateEvaluation(long milestoneId, long studentId, decimal score, string comment)
        {
            string query = @"
                IF EXISTS (
                    SELECT 1 FROM Evaluation
                    WHERE milestone_id = @mid AND student_id = @sid
                )
                BEGIN
                    UPDATE Evaluation
                    SET score   = @score,
                        comment = @comment
                    WHERE milestone_id = @mid AND student_id = @sid;
                END
                ELSE
                BEGIN
                    INSERT INTO Evaluation (milestone_id, student_id, score, comment)
                    VALUES (@mid, @sid, @score, @comment);
                END";

            return DataProvider.Instance.ExecuteNonQuery(
                query,
                new object[] { milestoneId, studentId, score, comment }
            );
        }

        #endregion
    }
}
