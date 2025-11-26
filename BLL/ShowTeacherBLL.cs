using LTTQ_G2_2025.DAL;
using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.BLL
{
    public class ShowTeacherBLL
    {
        private ShowTeacherDAL _dal = new ShowTeacherDAL();

        // Singleton
        private static ShowTeacherBLL _instance;
        public static ShowTeacherBLL Instance
        {
            get
            {
                if (_instance == null) _instance = new ShowTeacherBLL();
                return _instance;
            }
        }

        public List<TeacherDTO> GetAllTeachers()
        {
            return _dal.GetAllTeachers();
        }
    }

}
