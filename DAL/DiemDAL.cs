using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DAL
{
    internal class DiemDAL
    {
        public DataTable GetProjectsForScoreTab(long teacherId, int filterStatus, string keyword)
        {
            string query = @"
WITH M AS (
    SELECT project_id, COUNT(*) AS totalMilestones
    FROM Milestone
    GROUP BY project_id
),
E AS (
    SELECT project_id, COUNT(DISTINCT milestone_id) AS evalMilestones
    FROM Evaluation
    GROUP BY project_id
)
SELECT 
    p.project_id AS [Mã đồ án],
    CONVERT(NVARCHAR(MAX), p.name) AS [Tên đồ án],
    t.teamName AS [Nhóm],
    ISNULL(m.totalMilestones, 0)   AS [Số mốc],
    ISNULL(e.evalMilestones, 0)    AS [Số mốc đã chấm],
    CASE 
        WHEN ISNULL(e.evalMilestones, 0) = 0 
             THEN N'Chưa chấm'
        WHEN ISNULL(e.evalMilestones, 0) < ISNULL(m.totalMilestones, 0) 
             THEN N'Chưa hoàn thành'
        ELSE N'Đã hoàn thành'
    END AS [Trạng thái chấm]
FROM Project p
JOIN Team t ON t.project_id = p.project_id
LEFT JOIN M m ON m.project_id = p.project_id
LEFT JOIN E e ON e.project_id = p.project_id
WHERE t.teacher_id = @tid
  AND p.projectStatus = 1  
  AND (
        @filter0 = 0
        OR (@filter1 = 1 AND ISNULL(e.evalMilestones, 0) < ISNULL(m.totalMilestones, 0))
        OR (@filter2 = 2 AND ISNULL(e.evalMilestones, 0) >= ISNULL(m.totalMilestones, 0) 
                       AND m.totalMilestones > 0)
      )
  AND (
        @kw0 = '' 
        OR p.name LIKE '%' + @kw1 + '%'
      )
ORDER BY p.project_id;
";

            if (keyword == null)
                keyword = string.Empty;

            // Trong query có 6 parameter: @tid, @filter0, @filter1, @filter2, @kw0, @kw1
            // Nên phải truyền đúng 6 giá trị theo đúng thứ tự xuất hiện
            return DataProvider.Instance.ExecuteQuery(
                query,
                new object[]
                {
                    teacherId,     // @tid
                    filterStatus,  // @filter0
                    filterStatus,  // @filter1
                    filterStatus,  // @filter2
                    keyword,       // @kw0
                    keyword        // @kw1
                });
        }
        public DataTable GetScoreRowsByProject(long projectId, long teacherId)
        {
            string sql = @"
SELECT 
    s.student_id,
    s.studentCode,
    s.studentName,
    m.milestone_id,
    m.milestoneName,
    ev.totalScore
FROM Team t
JOIN Student s 
     ON s.team_id = t.team_id 
    AND s.flagDelete = 0
JOIN Project p 
     ON p.project_id = t.project_id
LEFT JOIN Milestone m 
       ON m.project_id = p.project_id
LEFT JOIN Evaluation ev
       ON ev.project_id  = p.project_id
      AND ev.milestone_id = m.milestone_id
WHERE p.project_id = @pid
  AND t.teacher_id = @tid
ORDER BY s.studentName, m.milestone_id;
";

            return DataProvider.Instance.ExecuteQuery(
                sql,
                new object[] { projectId, teacherId }   // @pid, @tid
            );
        }
        public DataTable GetScoreExportForTeacher(long teacherId)
        {
            string sql = @"
SELECT
    p.project_id,
    CONVERT(NVARCHAR(MAX), p.name)   AS projectName,
    t.teamName,
    ROW_NUMBER() OVER(
        PARTITION BY p.project_id
        ORDER BY s.studentName
    )                                AS sttSV,
    s.studentCode,
    CONVERT(NVARCHAR(MAX), s.studentName) AS studentName,
    ISNULL(AVG(CAST(ev.totalScore AS DECIMAL(5,2))), 0) AS avgScore
FROM Team t
JOIN Project p ON p.project_id = t.project_id
JOIN Student s ON s.team_id = t.team_id AND s.flagDelete = 0
LEFT JOIN Evaluation ev
    ON ev.project_id = p.project_id          -- ✅ chỉ join theo project
    -- KHÔNG dùng ev.student_id nữa vì cột đó không tồn tại
WHERE t.teacher_id    = @tid
  AND p.projectStatus = 1
GROUP BY
    p.project_id, p.name,
    t.teamName,
    s.studentCode, s.studentName
ORDER BY
    p.project_id, t.teamName, s.studentName;";

            return DataProvider.Instance.ExecuteQuery(
                sql,
                new object[] { teacherId }
            );
        }



    }
}
