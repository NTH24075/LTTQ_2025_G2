using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class EvaluationDTO
    {
        public EvaluationDTO() { }

        public EvaluationDTO(long evaluationId, long projectId, long milestoneId,
                             long councilId, decimal totalScore, string comment,
                             DateTime? evaluationDate)
        {
            EvaluationId = evaluationId;
            ProjectId = projectId;
            MilestoneId = milestoneId;
            CouncilId = councilId;
            TotalScore = totalScore;
            Comment = comment;
            EvaluationDate = evaluationDate;
        }

        public long EvaluationId { get; set; }
        public long ProjectId { get; set; }
        public long MilestoneId { get; set; }
        public long CouncilId { get; set; }
        public decimal TotalScore { get; set; }
        public string Comment { get; set; }
        public DateTime? EvaluationDate { get; set; }
    }
}
