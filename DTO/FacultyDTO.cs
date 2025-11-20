using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class FacultyDTO
    {
        public FacultyDTO() { }

        public FacultyDTO(int facultyId, string facultyName)
        {
            FacultyId = facultyId;
            FacultyName = facultyName;
        }

        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
    }
}
