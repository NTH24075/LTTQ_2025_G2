using System;
using System.Collections.Generic;
using System.Data;

namespace LTTQ_G2_2025.DAL
{
    public class StatisticsDAL
    {
        private static readonly Lazy<StatisticsDAL> _ins = new Lazy<StatisticsDAL>(() => new StatisticsDAL());
        public static StatisticsDAL Instance { get { return _ins.Value; } }

        // --- SINH VIÊN ---
        public DataTable GetStudentList(DateTime? from, DateTime? to)
        {
            string sql = @"
SELECT 
    s.student_id      AS [ID],
    s.studentCode     AS [Mã SV],
    s.studentName     AS [Họ tên],
    s.email           AS [Email],
    s.phoneNumber     AS [SĐT],
    c.clazzName       AS [Lớp],
    f.facultyName     AS [Khoa],
    CONVERT(varchar(10), xa.dob, 23) AS [Ngày sinh]
FROM Student s
LEFT JOIN Class   c ON c.clazz_id   = s.clazz_id
LEFT JOIN Faculty f ON f.faculty_id = c.faculty_id
CROSS APPLY (
    SELECT COALESCE(
        TRY_CONVERT(date, s.dateOfBirth, 23),
        TRY_CONVERT(date, s.dateOfBirth, 103),
        TRY_CONVERT(date, s.dateOfBirth, 105)
    ) AS dob
) xa
WHERE 1=1
  AND (@from IS NULL OR xa.dob >= @from)
  AND (@to   IS NULL OR xa.dob <= @to)
ORDER BY s.studentName;";

            var p = new Dictionary<string, object>
            {
                { "@from", from.HasValue ? (object)from.Value : DBNull.Value },
                { "@to",   to.HasValue   ? (object)to.Value   : DBNull.Value }
            };
            return DataProvider.Instance.ExecuteQuery(sql, p);
        }

        // --- GIẢNG VIÊN ---
        public DataTable GetTeacherList(DateTime? from, DateTime? to)
        {
            string sql = @"
SELECT 
    t.teacher_id    AS [ID],
    t.teacherCode   AS [Mã GV],
    t.teacherName   AS [Họ tên],
    t.email         AS [Email],
    t.phoneNumber   AS [SĐT],
    f.facultyName   AS [Khoa],
    d.degreeName    AS [Học vị],
    CONVERT(varchar(10), xa.dob, 23) AS [Ngày sinh]
FROM Teacher t
LEFT JOIN Faculty f ON f.faculty_id = t.faculty_id
LEFT JOIN Degree  d ON d.degree_id  = t.degree_id
CROSS APPLY (
    SELECT COALESCE(
        TRY_CONVERT(date, t.dateOfBirth, 23),
        TRY_CONVERT(date, t.dateOfBirth, 103),
        TRY_CONVERT(date, t.dateOfBirth, 105)
    ) AS dob
) xa
WHERE 1=1
  AND (@from IS NULL OR xa.dob >= @from)
  AND (@to   IS NULL OR xa.dob <= @to)
ORDER BY t.teacherName;";

            var p = new Dictionary<string, object>
            {
                { "@from", from.HasValue ? (object)from.Value : DBNull.Value },
                { "@to",   to.HasValue   ? (object)to.Value   : DBNull.Value }
            };
            return DataProvider.Instance.ExecuteQuery(sql, p);
        }

        // --- NHÓM ---
        public DataTable GetTeamList(DateTime? from, DateTime? to)
        {
            string sql = @"
SELECT
    tm.team_id      AS [TeamID],
    tm.teamName     AS [Tên nhóm],
    t.teacherName   AS [Giảng viên],
    f.facultyName   AS [Khoa],
    p.[name]        AS [Đề tài],
    s.semesterName  AS [Học kỳ],
    s.startDate     AS [Bắt đầu kỳ],
    s.endDate       AS [Kết thúc kỳ]
FROM Team tm
LEFT JOIN Teacher  t ON t.teacher_id  = tm.teacher_id
LEFT JOIN Faculty  f ON f.faculty_id  = t.faculty_id
LEFT JOIN Project  p ON p.project_id  = tm.project_id
LEFT JOIN Semester s ON s.semester_id = p.semester_id
WHERE 1=1
  AND (@from IS NULL OR s.startDate >= @from)
  AND (@to   IS NULL OR s.endDate   <= @to)
ORDER BY tm.team_id DESC;";

            var p = new Dictionary<string, object>
            {
                { "@from", from.HasValue ? (object)from.Value : DBNull.Value },
                { "@to",   to.HasValue   ? (object)to.Value   : DBNull.Value }
            };
            return DataProvider.Instance.ExecuteQuery(sql, p);
        }

        // --- ĐỀ TÀI ---
        public DataTable GetProjectList(DateTime? from, DateTime? to)
        {
            string sql = @"
SELECT 
    p.project_id       AS [ProjectID],
    p.[name]           AS [Tên đề tài],
    s.semesterName     AS [Học kỳ],
    p.projectStatus    AS [Trạng thái],
    COUNT(DISTINCT pr.progress_report_id) AS [Số báo cáo],
    MIN(m.dueDate)     AS [Mốc sớm nhất],
    MAX(m.dueDate)     AS [Mốc muộn nhất]
FROM Project p
LEFT JOIN Semester s ON s.semester_id = p.semester_id
LEFT JOIN ProgressReport pr ON pr.project_id = p.project_id
LEFT JOIN Milestone m ON m.project_id = p.project_id
WHERE 1=1
  AND (@from IS NULL OR s.startDate >= @from)
  AND (@to   IS NULL OR s.endDate   <= @to)
GROUP BY p.project_id, p.[name], s.semesterName, p.projectStatus
ORDER BY p.project_id DESC;";

            var p = new Dictionary<string, object>
            {
                { "@from", from.HasValue ? (object)from.Value : DBNull.Value },
                { "@to",   to.HasValue   ? (object)to.Value   : DBNull.Value }
            };
            return DataProvider.Instance.ExecuteQuery(sql, p);
        }
    }
}
