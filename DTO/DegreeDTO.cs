using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class DegreeDTO
    {
        public DegreeDTO() { }

        public DegreeDTO(int degreeId, string degreeName)
        {
            DegreeId = degreeId;
            DegreeName = degreeName;
        }

        public int DegreeId { get; set; }
        public string DegreeName { get; set; }
    }
}
