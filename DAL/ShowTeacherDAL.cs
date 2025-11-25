using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DAL
{
    public class ShowTeacherDAL
    {
        public List<TeacherDTO> GetAllTeachers()
        {
            List<TeacherDTO> list = new List<TeacherDTO>();

            string query = "SELECT teacher_id, teacherName, email, phoneNumber, gender, address, img FROM Teacher WHERE flagDelete = 0";

            DataTable dt = DBHelper.Instance.ExecuteQuery(query);

            foreach (DataRow r in dt.Rows)
            {
                list.Add(new TeacherDTO
                {
                    TeacherId = Convert.ToInt64(r["teacher_id"]),
                    TeacherName = r["teacherName"].ToString(),
                    Email = r["email"].ToString(),
                    PhoneNumber = r["phoneNumber"].ToString(),
                    Gender = Convert.ToBoolean(r["gender"]),
                    Address = r["address"].ToString(),
                    Img = r["img"].ToString()
                });
            }

            return list;
        }
    }
}
