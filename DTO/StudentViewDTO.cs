using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class StudentViewDTO:StudentDTO
    {
        public string facultyName { get; set; }
        public string className { get; set; }
    }
}
