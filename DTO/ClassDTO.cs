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

        public ClassDTO(long classId, string className,
                        int? facultyId, long? teacherId)
        {
            ClassId = classId;
            ClassName = className;
            FacultyId = facultyId;
            TeacherId = teacherId;
        }

        public long ClassId { get; set; }
        public string ClassName { get; set; }
        public int? FacultyId { get; set; }
        public long? TeacherId { get; set; }
    }
}
