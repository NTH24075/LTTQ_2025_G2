using LTTQ_G2_2025.DAL;
using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.BLL
{
    public class TeamBLL
    {
        private readonly TeamDAL _dal = new TeamDAL();

        public List<TeamViewDTO> Search(DateTime? from, DateTime? to, string keyword)
            => _dal.SearchTeams(keyword, from, to);
    }

}
