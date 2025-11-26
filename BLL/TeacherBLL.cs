using LTTQ_G2_2025.DAL;
using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.BLL
{
    public class TeacherBLL
    {
        private readonly TeacherDAL _teacherDAL = new TeacherDAL();
        private readonly AccountDAL _accountDAL = new AccountDAL();
        public TeacherViewDetailDTO ViewDetailDTO(long accountId)
        {
            return _teacherDAL.GetTeacherDTOByAccountId(accountId);
        }
        public TeacherViewDetailDTO GetTeacherById(long id) => _teacherDAL.GetById(id);
        public TeacherViewDetailDTO GetByAccountId(long accountId) => _teacherDAL.GetByAccountId(accountId);
        public List<TeacherViewDetailDTO> Search(string kw, int? fid, int? did) => _teacherDAL.Search(kw, fid, did);
        public List<TeacherViewDetailDTO> GetAll() => _teacherDAL.GetAll();
        public bool Insert(TeacherViewDetailDTO t) => _teacherDAL.Insert(t);
        public bool Update(TeacherViewDetailDTO t) => _teacherDAL.Update(t);
        public bool Delete(long id) => _teacherDAL.Delete(id);
        public bool AddTeacher(TeacherDTO dto)
        {
            return _teacherDAL.InsertTeacher(dto);
        }
        public bool UpdateBasicInfo(TeacherDTO dto) => _teacherDAL.UpdateBasicInfo(dto);
        public bool AddTeacherWithAutoAccount(TeacherDTO dto, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (_accountDAL.EmailExists(dto.Email))
            {
                errorMessage = "Email đã tồn tại.";
                return false;
            }

            // Lấy role teacher
            int roleId = _accountDAL.GetRoleIdByName("ROLE_TEACHER");
            if (roleId <= 0)
            {
                errorMessage = "ROLE_TEACHER không tồn tại.";
                return false;
            }

            // Tạo account với role_id
            long accountId = _accountDAL.CreateAccount(dto.Email, "12345678", roleId);
            if (accountId <= 0)
            {
                errorMessage = "Không tạo được tài khoản.";
                return false;
            }

            // Gán vào teacher
            dto.AccountId = accountId;
            dto.FlagDelete = false;

            bool ok = _teacherDAL.InsertTeacher(dto);
            if (!ok)
            {
                errorMessage = "Thêm giảng viên thất bại.";
                return false;
            }

            return true;
        }
    }
}
