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

        public List<ProjectListItemDTO> Search(DateTime? from, DateTime? to, string keyword)
            => _dal.SearchProjectsByTimeAndKeyword(from, to, keyword);
    }
}
