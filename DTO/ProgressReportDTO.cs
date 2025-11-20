using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class ProgressReportDTO
    {
        public ProgressReportDTO() { }

        public ProgressReportDTO(long id, string name, string content,
                                 string file, int percent,
                                 int? stageId, long? projectId)
        {
            ProgressReportId = id;
            ProgressReportName = name;
            ProgressReportContent = content;
            ProgressReportFile = file;
            ProgressReportPercent = percent;
            StageId = stageId;
            ProjectId = projectId;
        }

        public long ProgressReportId { get; set; }
        public string ProgressReportName { get; set; }
        public string ProgressReportContent { get; set; }
        public string ProgressReportFile { get; set; }
        public int ProgressReportPercent { get; set; }
        public int? StageId { get; set; }
        public long? ProjectId { get; set; }
    }
}
