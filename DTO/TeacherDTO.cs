using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class TeacherDTO
    {
        public TeacherDTO() { }

        public TeacherDTO(long teacherId, string teacherCode, string teacherName,
                          string dateOfBirth, string email, string phoneNumber,
                          bool teacherGender, string teacherAddress, string img,
                          bool flagDelete, int? facultyId, int? degreeId,
                          long? accountId)
        {
            TeacherId = teacherId;
            TeacherCode = teacherCode;
            TeacherName = teacherName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
            TeacherGender = teacherGender;
            TeacherAddress = teacherAddress;
            Img = img;
            FlagDelete = flagDelete;
            FacultyId = facultyId;
            DegreeId = degreeId;
            AccountId = accountId;
        }

        public long TeacherId { get; set; }
        public string TeacherCode { get; set; }
        public string TeacherName { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool TeacherGender { get; set; }
        public string TeacherAddress { get; set; }
        public string Img { get; set; }
        public bool FlagDelete { get; set; }
        public int? FacultyId { get; set; }
        public int? DegreeId { get; set; }
        public long? AccountId { get; set; }
    }
}
