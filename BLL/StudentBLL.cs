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
        private readonly ClassDAL _clazzDAL = new ClassDAL();
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
            string clazzName // người dùng nhập tên lớp, sẽ lookup ra clazz_id
        )
        {
            // Validate trùng lặp
            if (_studentDAL.IsStudentCodeExists(studentCode))
                throw new Exception("Mã sinh viên đã tồn tại.");
            if (_studentDAL.IsStudentEmailExists(email))
                throw new Exception("Email sinh viên đã tồn tại.");
            if (_studentDAL.IsPhoneExists(phone))
                throw new Exception("Số điện thoại đã tồn tại.");
            if (_accountDAL.EmailExists(email))
                throw new Exception("Email này đã tồn tại trong bảng Account.");

            // Lấy role_id cho ROLE_STUDENT
            int roleId = _roleDAL.GetRoleIdByName("ROLE_STUDENT");

            // Tạo Account (mk mặc định 12345678)
            long accountId = _accountDAL.CreateAccount(email, "12345678", roleId);

            // Tìm clazz_id
            long clazzId = 0;
            if (!string.IsNullOrWhiteSpace(clazzName))
                clazzId = _clazzDAL.GetClassIdByName(clazzName);

            // Chuẩn bị DTO
            var dto = new StudentDTO
            {
                StudentCode = studentCode,
                StudentName = studentName,
                DateOfBirth = dateOfBirth,      // DB của bạn để VARCHAR(50)
                StudentGender = gender,
                Email = email,
                PhoneNumber = phone,
                StudentAddress = address,
                Img = imageRelativePath,        // ví dụ "Images\\Students\\abc.jpg"
                AccountId = accountId,
                ClazzId = clazzId == 0 ? (long?)null : clazzId
            };

            // Thêm Student
            return _studentDAL.InsertStudent(dto);
        }
    }
}

