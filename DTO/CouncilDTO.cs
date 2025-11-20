using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class CouncilDTO
    {
        public CouncilDTO() { }

        public CouncilDTO(long id, string name, DateTime? date,
                          string room, int? semesterId)
        {
            CouncilId = id;
            CouncilName = name;
            CouncilDate = date;
            Room = room;
            SemesterId = semesterId;
        }

        public long CouncilId { get; set; }
        public string CouncilName { get; set; }
        public DateTime? CouncilDate { get; set; }
        public string Room { get; set; }
        public int? SemesterId { get; set; }
    }
}
