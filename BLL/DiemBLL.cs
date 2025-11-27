using LTTQ_G2_2025.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.BLL
{
    internal class DiemBLL
    {
        private readonly long _teacherId;
        private readonly DiemDAL _diemDAL = new DiemDAL();

        public DiemBLL(long teacherId)
        {
            _teacherId = teacherId;
        }

        /// <summary>
        /// Lấy danh sách project theo trạng thái + keyword cho tab Điểm
        /// </summary>
        public DataTable GetProjects(int filterStatus, string keyword)
        {
            return _diemDAL.GetProjectsForScoreTab(_teacherId, filterStatus, keyword);
        }
        public DataTable GetProjectsForScoreTab(int filterStatus, string keyword)
        {
            return _diemDAL.GetProjectsForScoreTab(_teacherId, filterStatus, keyword);
        }
        public DataTable GetScoreMatrixByProject(long projectId)
        {
            DataTable raw = _diemDAL.GetScoreRowsByProject(projectId, _teacherId);

            // Không có dữ liệu
            if (raw == null || raw.Rows.Count == 0)
                return raw;

            // Lấy danh sách milestone (id + name, sort theo id)
            var milestones = raw.AsEnumerable()
                .Where(r => !r.IsNull("milestone_id"))
                .Select(r => new
                {
                    Id = r.Field<long>("milestone_id"),
                    Name = r.Field<string>("milestoneName")
                })
                .Distinct()
                .OrderBy(x => x.Id)
                .ToList();

            // Tạo DataTable kết quả
            DataTable result = new DataTable();
            result.Columns.Add("Mã SV", typeof(string));
            result.Columns.Add("Họ tên", typeof(string));

            // Mỗi milestone 1 cột
            foreach (var m in milestones)
            {
                result.Columns.Add(m.Name, typeof(decimal));
            }

            // Cột điểm trung bình
            result.Columns.Add("Điểm TB", typeof(decimal));

            // Group theo sinh viên
            var groups = raw.AsEnumerable()
                .GroupBy(r => new
                {
                    StudentId = r.Field<long>("student_id"),
                    MaSV = r.Field<string>("studentCode"),
                    HoTen = r.Field<string>("studentName")
                });

            foreach (var g in groups)
            {
                DataRow row = result.NewRow();
                row["Mã SV"] = g.Key.MaSV;
                row["Họ tên"] = g.Key.HoTen;

                decimal sum = 0;
                int count = 0;
                bool missingAny = false;

                foreach (var m in milestones)
                {
                    // Tìm dòng tương ứng milestone này
                    var r = g.FirstOrDefault(x => !x.IsNull("milestone_id") &&
                                                  x.Field<long>("milestone_id") == m.Id);

                    if (r == null || r.IsNull("totalScore"))
                    {
                        // chưa chấm -> để trống
                        missingAny = true;
                        continue;
                    }

                    decimal score = r.Field<decimal>("totalScore");
                    row[m.Name] = score;
                    sum += score;
                    count++;
                }

                // Nếu tất cả milestone của sv đều đã có điểm -> tính ĐTB
                if (!missingAny && count > 0)
                {
                    row["Điểm TB"] = Math.Round(sum / count, 2);
                }
                // nếu còn thiếu ít nhất 1 mốc: để trống Điểm TB (DBNull)

                result.Rows.Add(row);
            }

            return result;
        }
        public DataTable GetScoreExportForTeacher()
        {
            return _diemDAL.GetScoreExportForTeacher(_teacherId);
        }
    }
}
