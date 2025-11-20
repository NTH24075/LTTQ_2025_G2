using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class TeamDTO
    {
        public TeamDTO() { }

        public TeamDTO(long teamId, string teamName,
                       long? teacherId, long? projectId)
        {
            TeamId = teamId;
            TeamName = teamName;
            TeacherId = teacherId;
            ProjectId = projectId;
        }

        public long TeamId { get; set; }
        public string TeamName { get; set; }
        public long? TeacherId { get; set; }
        public long? ProjectId { get; set; }
    }

}
