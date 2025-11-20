using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class ClassDTO
    {
        public ClassDTO() { }

        public ClassDTO(long clazzId, string clazzName,
                        int? facultyId, long? teacherId)
        {
            ClazzId = clazzId;
            ClazzName = clazzName;
            FacultyId = facultyId;
            TeacherId = teacherId;
        }

        public long ClazzId { get; set; }
        public string ClazzName { get; set; }
        public int? FacultyId { get; set; }
        public long? TeacherId { get; set; }
    }
}
