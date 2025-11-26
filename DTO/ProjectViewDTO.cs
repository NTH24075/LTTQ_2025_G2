using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class ProjectViewDTO
    {
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string TeacherName { get; set; }
        public string FacultyName { get; set; }
        public string SemesterName { get; set; }
    }

}
