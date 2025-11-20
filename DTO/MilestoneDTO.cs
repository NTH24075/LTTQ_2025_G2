using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class MilestoneDTO
    {
        public MilestoneDTO() { }

        public MilestoneDTO(long milestoneId, string milestoneName,
                            string milestoneDescription, int? weightPercent,
                            DateTime? dueDate, int? stageId,
                            long? projectId, int? semesterId)
        {
            MilestoneId = milestoneId;
            MilestoneName = milestoneName;
            MilestoneDescription = milestoneDescription;
            WeightPercent = weightPercent;
            DueDate = dueDate;
            StageId = stageId;
            ProjectId = projectId;
            SemesterId = semesterId;
        }

        public long MilestoneId { get; set; }
        public string MilestoneName { get; set; }
        public string MilestoneDescription { get; set; }
        public int? WeightPercent { get; set; }
        public DateTime? DueDate { get; set; }
        public int? StageId { get; set; }
        public long? ProjectId { get; set; }
        public int? SemesterId { get; set; }
    }
}
