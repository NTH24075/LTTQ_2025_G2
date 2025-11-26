using LTTQ_G2_2025.DAL;
using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.BLL
{
    public class InforStageBLL
    {
        private StageDAL stageDAL = new StageDAL();

        // Thêm tham số projectId vào phương thức BLL
        public List<InforStage> GetStagesForDisplay(long projectId)
        {
            // Truyền projectId xuống DAL
            return stageDAL.GetStagesByProjectId(projectId);
        }
        public bool SaveNewStage(InforStage stageData)
        {
            // Thêm logic kiểm tra dữ liệu đầu vào (validation) tại đây
            if (string.IsNullOrEmpty(stageData.StageName) || string.IsNullOrEmpty(stageData.MilestoneName))
            {
                // Trả về false hoặc throw exception nếu dữ liệu không hợp lệ
                return false;
            }

            return stageDAL.InsertNewStageData(stageData);
        }
    }
}
