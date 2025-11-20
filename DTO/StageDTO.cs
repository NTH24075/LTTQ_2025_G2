using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class StageDTO
    {
        public StageDTO() { }

        public StageDTO(int stageId, string stageName)
        {
            StageId = stageId;
            StageName = stageName;
        }

        public int StageId { get; set; }
        public string StageName { get; set; }
    }
}
