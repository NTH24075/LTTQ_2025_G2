using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class TeamViewDTO
    {
        public long TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeacherName { get; set; }
        public string FacultyName { get; set; }
        public string ProjectName { get; set; }
        public string SemesterName { get; set; }

        public long? TeacherId { get; set; }
        public long? ProjectId { get; set; }
        public string ProjectDescription { get; set; }
    }
}
