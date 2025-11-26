using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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
            st.stageName                              AS [Giai đoạn],
            m.milestone_id                            AS [Mã mốc],
            CONVERT(NVARCHAR(MAX), m.milestoneName)   AS [Tên mốc],
            CONVERT(NVARCHAR(MAX), m.milestoneDescription) AS [Mô tả],
            m.percentWeight                           AS [Trọng số (%)],
            m.deadline                                AS [Deadline],
            pr.reportDate                             AS [Ngày nộp],
            CONVERT(NVARCHAR(MAX), pr.reportDescription) AS [Mô tả nộp],
            CASE 
                WHEN pr.reportDate IS NULL THEN N'Chưa nộp'
                WHEN pr.reportDate <= m.deadline THEN N'Đúng hạn'
                ELSE N'Trễ hạn'
            END                                       AS [Trạng thái]
        FROM Milestone m
        INNER JOIN Stage st ON st.stage_id = m.stage_id      -- <== LIÊN KẾT VỚI STAGE
        INNER JOIN Team t   ON t.project_id = m.project_id
        LEFT JOIN ProgressReport pr
               ON pr.milestone_id = m.milestone_id
              AND pr.team_id      = t.team_id
        WHERE m.project_id = @pid
          AND t.teacher_id = @tid
  "; 

    return DataProvider.Instance.ExecuteQuery(
        query,
        new object[] { projectId, teacherId }
    );
        }
        public DataTable GetStageCompletionStatus(long projectId, long teacherId)
        {
            string query = @"
SELECT DISTINCT
    st.stage_id  AS [StageId],
    st.stageName AS [Giai đoạn],
    CASE 
        WHEN EXISTS (
            SELECT 1 
            FROM ProgressReport pr
            WHERE pr.project_id = m.project_id
              AND pr.stage_id   = st.stage_id
        ) 
        THEN N'Đã hoàn thành'
        ELSE N'Chưa hoàn thành'
    END AS [Trạng thái]
FROM Stage st
INNER JOIN Milestone m ON m.stage_id = st.stage_id
INNER JOIN Team t      ON t.project_id = m.project_id
WHERE t.project_id = @pid
  AND t.teacher_id = @tid
ORDER BY st.stage_id";

            return DataProvider.Instance.ExecuteQuery(
                query,
                new object[] { projectId, teacherId }   // 2 param, 2 @
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
        public List<InforStage> GetStagesByProjectId(long projectId)
        {
            var stages = new List<InforStage>();

            string query = @"
        SELECT 
            p.project_id,
            p.name AS projectName,

            s.stage_id,
            s.stageName,

            m.milestone_id,
            m.milestoneName,
            m.milestoneDescription,
            m.weightPercent,
            m.dueDate,
                 
            pr.progress_report_id,
            pr.progressReportName,
            pr.progressReportContent,
            pr.progressReportFile,
            pr.progressReportPercent
        FROM Project p
        LEFT JOIN (
            SELECT DISTINCT stage_id, project_id FROM Milestone
            UNION
            SELECT DISTINCT stage_id, project_id FROM ProgressReport
        ) sp
               ON sp.project_id = p.project_id
        LEFT JOIN Stage s
               ON s.stage_id = sp.stage_id
        LEFT JOIN Milestone m
               ON m.stage_id = s.stage_id
              AND m.project_id = p.project_id
        LEFT JOIN ProgressReport pr
               ON pr.stage_id = s.stage_id
              AND pr.project_id = p.project_id
        WHERE p.project_id = @projectId;";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { projectId });

            foreach (DataRow row in dt.Rows)
            {
                var stage = new InforStage
                {
                    ProjectId = row["project_id"] is DBNull ? 0 : Convert.ToInt64(row["project_id"]),
                    StageId = row["stage_id"] is DBNull ? 0 : Convert.ToInt32(row["stage_id"]),
                    StageName = row["stageName"] as string,

                    MilestoneId = row["milestone_id"] is DBNull ? 0 : Convert.ToInt64(row["milestone_id"]),
                    MilestoneName = row["milestoneName"] as string,
                    MilestoneDescription = row["milestoneDescription"] as string,
                    WeightPercent = row["weightPercent"] is DBNull ? (int?)null : Convert.ToInt32(row["weightPercent"]),
                    DueDate = row["dueDate"] is DBNull ? (DateTime?)null : Convert.ToDateTime(row["dueDate"]),

                    ProgressReportId = row["progress_report_id"] is DBNull ? 0 : Convert.ToInt64(row["progress_report_id"]),
                    ProgressReportName = row["progressReportName"] as string,
                    ProgressReportContent = row["progressReportContent"] as string,
                    ProgressReportFile = row["progressReportFile"] as string,
                    ProgressReportPercent = row["progressReportPercent"] is DBNull ? (int?)null : Convert.ToInt32(row["progressReportPercent"])
                };

                stages.Add(stage);
            }

            return stages;
        }
        public bool SaveEvaluationForMilestone(long projectId, long milestoneId, decimal score, string comment)
        {
            const long councilId = 1;

            try
            {
                // 1. Kiểm tra đã có bản ghi chưa
                string sqlCheck = @"
            SELECT COUNT(*) 
            FROM Evaluation
            WHERE project_id  = @project_id
              AND milestone_id = @milestone_id
              AND council_id   = @council_id";

                object result = DataProvider.Instance.ExecuteScalar(
                    sqlCheck,
                    new object[] { projectId, milestoneId, councilId }  // 3 tham số, 3 @
                );

                int count = 0;
                if (result != null && result != DBNull.Value)
                    count = Convert.ToInt32(result);

                int rows;

                if (count > 0)
                {
                    // 2a. ĐÃ có -> UPDATE
                    string sqlUpdate = @"
                UPDATE Evaluation
                SET totalScore     = @score,
                    comment        = @comment,
                    evaluationDate = GETDATE()
                WHERE project_id   = @project_id
                  AND milestone_id = @milestone_id
                  AND council_id   = @council_id;";

                    rows = DataProvider.Instance.ExecuteNonQuery(
                        sqlUpdate,
                        new object[] { projectId, milestoneId, councilId, score, comment } // 5 tham số, 5 @
                    );
                }
                else
                {
                    // 2b. Chưa có -> INSERT
                    string sqlInsert = @"
                INSERT INTO Evaluation
                    (project_id, milestone_id, council_id, totalScore, comment, evaluationDate)
                VALUES
                    (@project_id, @milestone_id, @council_id, @score, @comment, GETDATE());";

                    rows = DataProvider.Instance.ExecuteNonQuery(
                        sqlInsert,
                        new object[] { projectId, milestoneId, councilId, score, comment } // 5 tham số, 5 @
                    );
                }

                return rows > 0;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL khi lưu điểm milestone: " + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public DataRow GetProjectHeader(long projectId, long teacherId)
        {
            string sql = @"
        SELECT TOP 1
            p.project_id,
            p.name        AS projectName,
            t.teacherName AS teacherName
        FROM Project p
        INNER JOIN Team tm ON tm.project_id = p.project_id
        INNER JOIN Teacher t ON t.teacher_id = tm.teacher_id
        WHERE p.project_id = @pid
          AND t.teacher_id = @tid;";

            DataTable dt = DataProvider.Instance.ExecuteQuery(
                sql,
                new object[] { projectId, teacherId } // @pid, @tid
            );

            if (dt.Rows.Count == 0) return null;
            return dt.Rows[0];
        }
        public DataRow GetMilestoneEvaluation(long projectId, long milestoneId)
        {
            const long councilId = 1; // giống chỗ SaveEvaluationForMilestone

            string sql = @"
        SELECT TOP 1
            m.weightPercent        AS weightPercent,
            m.milestoneDescription AS milestoneDescription,
            ev.totalScore          AS totalScore,
            ev.comment             AS comment
        FROM Milestone m
        LEFT JOIN Evaluation ev
               ON ev.project_id   = m.project_id
              AND ev.milestone_id = m.milestone_id
              AND ev.council_id   = @council_id
        WHERE m.project_id   = @pid
          AND m.milestone_id = @mid;";

            // THỨ TỰ tham số phải khớp với @ trong SQL:
            // @council_id, @pid, @mid
            DataTable dt = DataProvider.Instance.ExecuteQuery(
                sql,
                new object[] { councilId, projectId, milestoneId }
            );

            if (dt.Rows.Count == 0) return null;
            return dt.Rows[0];
        }
        public bool UpdateMilestoneProgress(long milestoneId, int? weightPercent, string milestoneDescription)
        {
            string sql = @"
        UPDATE Milestone
        SET weightPercent = @weightPercent,
            milestoneDescription = @milestoneDescription
        WHERE milestone_id = @mid";

            // chú ý thứ tự parameters: @weightPercent, @milestoneDescription, @mid
            object pWeight = (object)weightPercent ?? DBNull.Value;
            object pDesc = (object)(milestoneDescription ?? string.Empty);

            int rows = DataProvider.Instance.ExecuteNonQuery(
                sql,
                new object[] { pWeight, pDesc, milestoneId }
            );

            return rows > 0;
        }
    }

}
