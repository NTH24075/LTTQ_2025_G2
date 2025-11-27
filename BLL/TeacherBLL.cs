using LTTQ_G2_2025.DAL;
using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.BLL
{
    internal class TeacherBLL   
    {
        private TeacherDAL teacherDAL = new TeacherDAL();
        private long? teacherId;
        public TeacherBLL()
        {
        }

        public TeacherBLL(long teacherId)
        {
            this.teacherId = teacherId;
        }
        public DataTable GetDanhSachDoAn()
        {
            if (teacherId.HasValue)
                return teacherDAL.GetProjectsByTeacher(teacherId.Value);

            // fallback: nếu không truyền teacherId (ví dụ Admin)
            return teacherDAL.GetAllProjects();
        }
        public DataTable GetProjectsByStatus(int status)
        {
            
                return teacherDAL.GetProjectsByTeacherAndStatus(teacherId.Value, status);

            
        }
        public DataRow GetProjectDetail(long id)
        {
            return teacherDAL.GetProjectDetail(id);
        }

        public bool UpdateProjectStatus(long id, int status)
        {
            return teacherDAL.UpdateProjectStatus(id, status) > 0;
        }

        public DataTable SearchProjects(string name, int status)
        {
            return teacherDAL.SearchProjectsByTeacher(name, status, teacherId.Value);
        }
        ///// tab 2

        public DataTable GetApprovedProjects()
        {
            return teacherDAL.GetApprovedProjectsByTeacher(teacherId.Value);
        }
        public long? GetTeacherIdByAccountId(long accountId)
        {
            return teacherDAL.GetTeacherIdByAccountId(accountId);
        }
    }
}
