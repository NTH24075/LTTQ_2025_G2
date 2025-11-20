using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class StudentDTO
    {
        public long StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentCode { get; set; }
        public string DateOfBirth { get; set; }
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
