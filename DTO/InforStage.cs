using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class InforStage
    {
        public InforStage() { }
        public InforStage(long projectId,
            int stageId,
            string stageName,
            string progressReportFile,
            string milestoneName,
            string milestoneDescription,
            int? weightPercent,
            DateTime? dueDate,
            string progressReportName,
            string progressReportContent,
            int? progressReportPercent)
        {
            ProjectId = projectId;
            StageId = stageId;
            StageName = stageName;
            ProgressReportFile = progressReportFile;
            MilestoneName = milestoneName;
            MilestoneDescription = milestoneDescription;
            WeightPercent = weightPercent;
            DueDate = dueDate;
            ProgressReportName = progressReportName;
            ProgressReportContent = progressReportContent;
            ProgressReportPercent = progressReportPercent;
        }
        public long ProjectId { get; set; }
        public int StageId { get; set; }
        public string StageName { get; set; }

        public string ProgressReportFile { get; set; }
        public string MilestoneName { get; set; }
        public string MilestoneDescription { get; set; }
        public int? WeightPercent { get; set; }
        public DateTime? DueDate { get; set; }

        // 2 thuộc tính của giáo viên 
        public string ProgressReportName { get; set; }
        public string ProgressReportContent { get; set; }
        public int? ProgressReportPercent { get; set; }

        
    }
}
