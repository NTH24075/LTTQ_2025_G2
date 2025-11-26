using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class ProjectListItemDTO
    {
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string TeamName { get; set; }
        public string TeacherName { get; set; }
        public string SemesterName { get; set; }
        public DateTime? SemesterStart { get; set; }
        public DateTime? SemesterEnd { get; set; }
        public string Description { get; set; }
    }
}
