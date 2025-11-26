using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class InforStage
    {
        public long ProjectId { get; set; }

        public int StageId { get; set; }
        public string StageName { get; set; }

        public long MilestoneId { get; set; }
        public string MilestoneName { get; set; }
        public string MilestoneDescription { get; set; }
        public int? WeightPercent { get; set; }
        public DateTime? DueDate { get; set; }

        public long ProgressReportId { get; set; }
        public string ProgressReportName { get; set; }
        public string ProgressReportContent { get; set; }
        public string ProgressReportFile { get; set; }
        public int? ProgressReportPercent { get; set; }

        // Nếu muốn hiển thị điểm đã chấm sau này thì thêm ở đây
        public decimal? EvaluationScore { get; set; }
        public string EvaluationComment { get; set; }
    }
}
