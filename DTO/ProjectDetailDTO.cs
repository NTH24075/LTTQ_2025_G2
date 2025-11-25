using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class ProjectDetailDTO
    {
        public long ProjectId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }
        public string Description { get; set; }
        public bool ProjectStatus { get; set; }
        public int? SemesterId { get; set; }

        // Thêm 3 thuộc tính mở rộng
        public string TeamName { get; set; }
        public string TeacherName { get; set; }
        public string SemesterName { get; set; }
    }
}
