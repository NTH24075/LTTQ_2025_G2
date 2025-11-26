using System;
using System.Collections.Generic;
using System.Data;
using LTTQ_G2_2025.DTO;

namespace LTTQ_G2_2025.DAL
{
    public class TeamDAL
    {
        // Vẫn giữ thứ tự: keyword, from, to như DAL hiện tại của bạn
        public List<TeamViewDTO> SearchTeams(string keyword, DateTime? from, DateTime? to)
        {
            string sql = @"
SELECT
    t.team_id,
    t.teamName,
    ISNULL(tch.teacherName,  N'') AS teacherName,
    ISNULL(f.facultyName,    N'') AS facultyName,
    ISNULL(p.[name],         N'') AS projectName,
    ISNULL(s.semesterName,   N'') AS semesterName
FROM Team t
LEFT JOIN Teacher  tch ON tch.teacher_id  = t.teacher_id
LEFT JOIN Faculty  f   ON f.faculty_id    = tch.faculty_id
LEFT JOIN Project  p   ON p.project_id    = t.project_id
LEFT JOIN Semester s   ON s.semester_id   = p.semester_id
WHERE 1 = 1";

            var parameters = new List<object>();

            // Tìm theo keyword (teamName, teacherName, facultyName, projectName, semesterName)
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += @"
    AND (
        t.teamName      LIKE @kw1 OR
        tch.teacherName LIKE @kw2 OR
        f.facultyName   LIKE @kw3 OR
        p.[name]        LIKE @kw4 OR
        s.semesterName  LIKE @kw5
    )";
                string like = $"%{keyword.Trim()}%";
                // thêm đúng số lần tham số xuất hiện
                parameters.Add(like); // @kw1
                parameters.Add(like); // @kw2
                parameters.Add(like); // @kw3
                parameters.Add(like); // @kw4
                parameters.Add(like); // @kw5
            }

            // Lọc theo thời gian (nếu có)
            if (from.HasValue)
            {
                sql += " AND s.startDate >= @from";
                parameters.Add(from.Value); // @from
            }
            if (to.HasValue)
            {
                sql += " AND s.endDate <= @to";
                parameters.Add(to.Value);   // @to
            }

            sql += " ORDER BY t.team_id DESC";

            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, parameters.ToArray());

            var list = new List<TeamViewDTO>();
            foreach (DataRow r in dt.Rows)
            {
                list.Add(new TeamViewDTO
                {
                    TeamId = Convert.ToInt64(r["team_id"]),
                    TeamName = r["teamName"]?.ToString() ?? "",
                    TeacherName = r["teacherName"]?.ToString() ?? "",
                    FacultyName = r["facultyName"]?.ToString() ?? "",
                    ProjectName = r["projectName"]?.ToString() ?? "",
                    SemesterName = r["semesterName"]?.ToString() ?? ""
                });
            }
            return list;
        }
    }
}
