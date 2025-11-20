using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class StudentDTO
    {
        public StudentDTO() { }

        public StudentDTO(long studentId, string studentName, string studentCode,
                          string dateOfBirth, string email, string phoneNumber,
                          bool studentGender, string studentAddress, string img,
                          bool flagDelete, long? accountId, long? clazzId, long? teamId)
        {
            StudentId = studentId;
            StudentName = studentName;
            StudentCode = studentCode;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
            StudentGender = studentGender;
            StudentAddress = studentAddress;
            Img = img;
            FlagDelete = flagDelete;
            AccountId = accountId;
            ClazzId = clazzId;
            TeamId = teamId;
        }

        public long StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentCode { get; set; }
        public string DateOfBirth { get; set; }   // hoặc DateTime nếu bạn đổi DB
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool StudentGender { get; set; }
        public string StudentAddress { get; set; }
        public string Img { get; set; }
        public bool FlagDelete { get; set; }
        public long? AccountId { get; set; }
        public long? ClazzId { get; set; }
        public long? TeamId { get; set; }
    }
}
