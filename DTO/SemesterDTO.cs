using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class SemesterDTO
    {
        public SemesterDTO() { }

        public SemesterDTO(int semesterId, string semesterName,
                           DateTime? startDate, DateTime? endDate)
        {
            SemesterId = semesterId;
            SemesterName = semesterName;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
