using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LTTQ_G2_2025.DAL
{
    public class StageDAL
    {
        public List<InforStage> GetStagesByProjectId(long projectId)
        {
            var stages = new List<InforStage>();

            string query = @"
                SELECT 
                    p.project_id,
                    p.name AS projectName,
                    s.stage_id,
                    s.stageName,

                    m.milestone_id,
                    m.milestoneName,
                    m.milestoneDescription,
                    m.weightPercent,
                    m.dueDate,
                         
                    pr.progress_report_id,
                    pr.progressReportName,
                    pr.progressReportContent,
                    pr.progressReportFile,
                    pr.progressReportPercent
                FROM Project p
                LEFT JOIN (
                    SELECT DISTINCT stage_id, project_id FROM Milestone
                    UNION
                    SELECT DISTINCT stage_id, project_id FROM ProgressReport
                ) sp
                       ON sp.project_id = p.project_id
                LEFT JOIN Stage s
                       ON s.stage_id = sp.stage_id
                LEFT JOIN Milestone m
                       ON m.stage_id = s.stage_id
                      AND m.project_id = p.project_id
                LEFT JOIN ProgressReport pr
                       ON pr.stage_id = s.stage_id
                      AND pr.project_id = p.project_id
                WHERE p.project_id = @projectId;   
                ";

            // Nếu ExecuteQuery ánh xạ theo vị trí, 1 tham số là đủ (SQL chỉ có 1 @projectId)
            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { projectId });

            foreach (DataRow row in dt.Rows)
            {
                var stage = new InforStage
                {
                    ProjectId = row["project_id"] is DBNull ? 0 : Convert.ToInt64(row["project_id"]),
                    StageId = row["stage_id"] is DBNull ? 0 : Convert.ToInt32(row["stage_id"]),
                    StageName = row["stageName"] is DBNull ? null : row["stageName"].ToString(),

                    MilestoneName = row["milestoneName"] is DBNull ? null : row["milestoneName"].ToString(),
                    MilestoneDescription = row["milestoneDescription"] is DBNull ? null : row["milestoneDescription"].ToString(),
                    WeightPercent = row["weightPercent"] is DBNull ? (int?)null : Convert.ToInt32(row["weightPercent"]),
                    DueDate = row["dueDate"] is DBNull ? (DateTime?)null : Convert.ToDateTime(row["dueDate"]),

                    ProgressReportContent = row["progressReportContent"] is DBNull ? null : row["progressReportContent"].ToString(),
                    ProgressReportPercent = row["progressReportPercent"] is DBNull ? (int?)null : Convert.ToInt32(row["progressReportPercent"]),
                    ProgressReportFile = row["progressReportFile"] is DBNull ? null : row["progressReportFile"].ToString()
                };
                stages.Add(stage);
            }

            return stages;
        }
        public bool InsertNewStageData(InforStage stageData)
        {
            // Các chuỗi SQL không cần thay đổi
            string sqlInsertStage = @"
                INSERT INTO Stage (stageName) 
                VALUES (@stageName);
                SELECT SCOPE_IDENTITY();"; // Thêm SELECT SCOPE_IDENTITY() để lấy ID mới

            string sqlInsertMilestone = @"
                INSERT INTO Milestone (project_id, stage_id, milestoneName, milestoneDescription, weightPercent, dueDate)
                VALUES (@projectId, @stageId, @milestoneName, @milestoneDescription, @weightPercent, @dueDate);";

            string sqlInsertPR = @"
                INSERT INTO ProgressReport (project_id, stage_id, progressReportFile, progressReportContent, progressReportPercent, progressReportName)
                VALUES (@projectId, @stageId, @progressReportFile, @progressReportContent, @progressReportPercent,@progressReportName);";

            try
            {
                // Bước 1 ĐÃ SỬA: 
                // Chỉ truyền MỘT đối tượng (@stageName) để khớp với MỘT tham số trong SQL.
                object stageIdObj = DataProvider.Instance.ExecuteScalar(sqlInsertStage, new object[]
                {
                    // Chỉ truyền stageName, không truyền ProjectId vào lệnh này
                    stageData.StageName
                });

                if (stageIdObj == null || stageIdObj is DBNull)
                {
                    MessageBox.Show("Không thể lấy ID giai đoạn mới sau khi chèn.", "Lỗi DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                int newStageId = Convert.ToInt32(stageIdObj);

                // Bước 2: Insert Milestone (logic này giữ nguyên, số tham số khớp)
                DataProvider.Instance.ExecuteNonQuery(sqlInsertMilestone, new object[]
                {
                    stageData.ProjectId,
                    newStageId,
                    stageData.MilestoneName,
                    stageData.MilestoneDescription,
                    stageData.WeightPercent,
                    stageData.DueDate
                    
                });

                // Bước 3: Insert Progress Report (logic này giữ nguyên, số tham số khớp)
                DataProvider.Instance.ExecuteNonQuery(sqlInsertPR, new object[]
               {
                    stageData.ProjectId,
                    newStageId,
                    stageData.ProgressReportFile,
                    stageData.ProgressReportContent,
                    stageData.ProgressReportPercent,
                    stageData.StageName
               });

                return true; // Thành công
            }
            catch (SqlException ex)
            {
                // Hiển thị lỗi DB chi tiết hơn để dễ gỡ lỗi
                MessageBox.Show("Lỗi DB chi tiết: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("SQL Error: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                // Bắt các lỗi chung khác
                MessageBox.Show("Lỗi chung khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
