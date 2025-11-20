using LTTQ_G2_2025.DAL;
using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.BLL
{
    public class StudentBLL
    {
        private static StudentBLL instance;
        public static StudentBLL Instance
        {
            get
            {
                if (instance == null) instance = new StudentBLL();
                return instance;
            }
        }
        private StudentBLL() { }

        public List<StudentDTO> GetTeamMembersByAccountId(long accountId)
        {
            return StudentDAL.Instance.GetTeamMembersByAccountId(accountId);
        }
    }
}
