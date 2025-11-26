using LTTQ_G2_2025.DAL;
using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.BLL
{
    public class ProjectBLL
    {
        private readonly ProjectDAL _dal = new ProjectDAL();

        //public ProjectDTO GetProjectOfStudent(long accountId)
        //    => _dal.GetProjectByStudentAccount(accountId);

        public long? GetTeamIdOfStudent(long accountId)
            => _dal.GetTeamIdByStudentAccount(accountId);

        public bool CreateProjectForTeam(long teamId, string name, string content,
                                 string img, string description, bool status, int? semId, long teacherId)
    => _dal.CreateProjectAndAttachToTeam(teamId, name, content, img, description, status, semId, teacherId);
        public ProjectDetailDTO GetProjectDetail(long accountId)
            => _dal.GetProjectWithDetailByStudentAccount(accountId);
        

    }
}
