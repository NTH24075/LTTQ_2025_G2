using LTTQ_G2_2025.DAL;
using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace LTTQ_G2_2025.BLL
{
    /// <summary>
    /// BLL cho tab Tiến độ của giảng viên
    /// Làm việc với TienDoDAL + teacherId hiện tại
    /// </summary>
    internal class TienDoBLL
    {
        private readonly long teacherId;
        private readonly TienDoDAL tienDoDAL = new TienDoDAL();

        /// <summary>
        /// Bắt buộc truyền teacherId khi khởi tạo
        /// </summary>
        public TienDoBLL(long teacherId)
        {
            this.teacherId = teacherId;
        }

        #region 1. ĐỒ ÁN ĐÃ DUYỆT

        /// <summary>
        /// Lấy danh sách đồ án đã duyệt của giảng viên
        /// </summary>
        public DataTable GetApprovedProjects()
        {
            return tienDoDAL.GetApprovedProjectsByTeacher(teacherId);
        }

        /// <summary>
        /// Tìm kiếm đồ án đã duyệt theo tên (lọc trên DataTable)
        /// </summary>
        public DataTable SearchApprovedProjects(string keyword)
        {
            DataTable source = tienDoDAL.GetApprovedProjectsByTeacher(teacherId);

            keyword = (keyword ?? string.Empty).Trim();

            if (string.IsNullOrEmpty(keyword))
                return source;

            // Lọc trên DataView theo cột [Tên đồ án]
            DataView view = new DataView(source);
            // escape dấu ' trong keyword nếu có
            string safeKeyword = keyword.Replace("'", "''");
            view.RowFilter = $"[Tên đồ án] LIKE '%{safeKeyword}%'";

            return view.ToTable();
        }

        #endregion

        #region 2. NHÓM & THÀNH VIÊN

        /// <summary>
        /// Lấy danh sách sinh viên trong nhóm của 1 project
        /// </summary>
        public DataTable GetTeamMembersByProject(long projectId)
        {
            return tienDoDAL.GetTeamMembersByProject(projectId, teacherId);
        }

        /// <summary>
        /// Lấy thông tin team (nếu cần team_id, teamName...)
        /// </summary>
        public DataRow GetTeamByProject(long projectId)
        {
            return tienDoDAL.GetTeamByProject(projectId, teacherId);
        }

        #endregion

        #region 3. MILESTONE & TIẾN ĐỘ NỘP BÀI

        /// <summary>
        /// Lấy danh sách milestone của 1 đồ án
        /// </summary>
        public DataTable GetMilestonesByProject(long projectId)
        {
            return tienDoDAL.GetMilestonesByProject(projectId);
        }

        /// <summary>
        /// Lấy tiến độ nộp bài (đúng hạn / trễ / chưa nộp) theo từng mốc cho 1 project
        /// </summary>
        public DataTable GetMilestoneProgress(long projectId)
        {
            return tienDoDAL.GetMilestoneProgressByTeam(projectId, teacherId);
        }

        #endregion

        #region 4. CHẤM ĐIỂM (EVALUATION)

        /// <summary>
        /// Lấy bảng điểm theo từng milestone và từng sinh viên cho 1 project
        /// </summary>
        public DataTable GetEvaluationsByProject(long projectId)
        {
            return tienDoDAL.GetEvaluationsByProject(projectId, teacherId);
        }

        /// <summary>
        /// Lưu điểm (thêm mới hoặc cập nhật) cho 1 sinh viên ở 1 milestone
        /// </summary>

        #endregion
        public DataTable GetStageCompletionStatus(long projectId)
        {
            return tienDoDAL.GetStageCompletionStatus(projectId, teacherId);
        }
        public List<InforStage> GetStagesByProject(long projectId)
        {
            return tienDoDAL.GetStagesByProjectId(projectId);
        }

        public bool SaveEvaluation(long projectId, long milestoneId, decimal score, string comment)
        {
            return tienDoDAL.SaveEvaluationForMilestone(projectId, milestoneId, score, comment);
        }
        public DataRow GetProjectHeader(long projectId)
        {
            return tienDoDAL.GetProjectHeader(projectId, teacherId);
        }

        public DataRow GetMilestoneEvaluation(long projectId, long milestoneId)
        {
            return tienDoDAL.GetMilestoneEvaluation(projectId, milestoneId);
        }
        public bool UpdateMilestoneProgress(long milestoneId, int weightPercent, string milestoneDescription)
        {
            return tienDoDAL.UpdateMilestoneProgress(milestoneId, weightPercent, milestoneDescription);
        }
    }

}
