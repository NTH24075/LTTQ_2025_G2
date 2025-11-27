using LTTQ_G2_2025.DAL;
using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTTQ_G2_2025.BLL
{
    public class StudentBLL
    {
        private readonly StudentDAL _studentDAL = new StudentDAL();
        private readonly AccountDAL _accountDAL = new AccountDAL();
        private readonly RoleDAL _roleDAL = new RoleDAL();
        private readonly ClassDAL _classDAL = new ClassDAL();
        public StudentDetailViewDTO GetStudentByAccountId(long accountId)
        {
            return _studentDAL.GetStudentDetailByAccountId(accountId);
        }
        public List<StudentViewDTO> GetAllStudents()
        => _studentDAL.GetAllStudents();

        public List<StudentViewDTO> SearchStudents(string keyword)
            => _studentDAL.SearchStudents(keyword);
        public bool UpdateStudentBasic(StudentDTO dto)
            => _studentDAL.UpdateStudentBasic(dto);
        public StudentDetailViewDTO GetStudentDetailByAccountId(long accountId)
            => _studentDAL.GetStudentDetailByAccountId(accountId);
        public long AddStudentWithAccount(
            string studentCode,
            string studentName,
            string dateOfBirth,
            bool gender,
            string email,
            string phone,
            string address,
            string imageRelativePath,
            string className 
        )
        {
            if (_studentDAL.IsStudentCodeExists(studentCode))
                throw new Exception("Mã sinh viên đã tồn tại.");
            if (_studentDAL.IsStudentEmailExists(email))
                throw new Exception("Email sinh viên đã tồn tại.");
            if (_studentDAL.IsPhoneExists(phone))
                throw new Exception("Số điện thoại đã tồn tại.");
            if (_accountDAL.EmailExists(email))
                throw new Exception("Email này đã tồn tại trong bảng Account.");

            int roleId = _roleDAL.GetRoleIdByName("ROLE_STUDENT");

            long accountId = _accountDAL.CreateAccount(email, "12345678", roleId);

            long classId = 0;
            if (!string.IsNullOrWhiteSpace(className))
                classId = _classDAL.GetClassIdByName(className);

            var dto = new StudentDTO
            {
                StudentCode = studentCode,
                StudentName = studentName,
                DateOfBirth = dateOfBirth,      
                StudentGender = gender,
                Email = email,
                PhoneNumber = phone,
                StudentAddress = address,
                Img = imageRelativePath,       
                AccountId = accountId,
                ClassId = classId == 0 ? (long?)null : classId
            };

            return _studentDAL.InsertStudent(dto);
        }
    }
}

