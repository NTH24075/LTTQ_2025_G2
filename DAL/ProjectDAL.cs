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
        public List<ProjectListItemDTO> SearchProjectsByTimeAndKeyword(
            DateTime? from, DateTime? to, string keyword)
        {
            // Khung truy vấn
            string query = @"
                    SELECT 
                        p.project_id,
                        p.[name]            AS ProjectName,
                        p.[description]     AS [Description],
                        s.semesterName      AS SemesterName,
                        s.startDate         AS SemesterStart,
                        s.endDate           AS SemesterEnd,
                        t.teamName          AS TeamName,
                        th.teacherName      AS TeacherName
                    FROM Project p
                    LEFT JOIN Semester s ON s.semester_id = p.semester_id
                    LEFT JOIN Team t     ON t.project_id   = p.project_id
                    LEFT JOIN Teacher th ON th.teacher_id  = t.teacher_id
                    WHERE 1 = 1";

            var paramList = new List<object>();

            // Từ khóa
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query += @"
  AND (p.[name] LIKE @kw OR t.teamName LIKE @kw OR th.teacherName LIKE @kw)";
                paramList.Add("%" + keyword + "%");
            }

            // Bộ lọc theo mốc thời gian của Milestone.dueDate (ưu tiên vì cụ thể)
            if (from.HasValue && to.HasValue)
            {
                query += @"
  AND EXISTS (
        SELECT 1 FROM Milestone m
        WHERE m.project_id = p.project_id
          AND m.dueDate BETWEEN @from AND @to
  )";
                paramList.Add(from.Value);
                paramList.Add(to.Value);
            }
            else if (from.HasValue) // chỉ từ ngày
            {
                query += @"
  AND EXISTS (
        SELECT 1 FROM Milestone m
        WHERE m.project_id = p.project_id
          AND m.dueDate >= @from
  )";
                paramList.Add(from.Value);
            }
            else if (to.HasValue) // chỉ đến ngày
            {
                query += @"
                        AND EXISTS (
                            SELECT 1 FROM Milestone m
                            WHERE m.project_id = p.project_id
                                AND m.dueDate <= @to
                          )";
                paramList.Add(to.Value);
            }

            // Order cho dễ nhìn
            query += @"
ORDER BY 
    COALESCE(s.startDate, '1900-01-01'),
    p.project_id";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query, paramList.ToArray());

            var result = new List<ProjectListItemDTO>();
            foreach (DataRow r in dt.Rows)
            {
                result.Add(new ProjectListItemDTO
                {
                    ProjectId = Convert.ToInt64(r["project_id"]),
                    ProjectName = r["ProjectName"]?.ToString(),
                    Description = r["Description"]?.ToString(),
                    SemesterName = r["SemesterName"]?.ToString(),
                    SemesterStart = r["SemesterStart"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["SemesterStart"]),
                    SemesterEnd = r["SemesterEnd"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["SemesterEnd"]),
                    TeamName = r["TeamName"]?.ToString(),
                    TeacherName = r["TeacherName"]?.ToString()
                });
            }

            return result;
        }
    }
}
