using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class AdminDTO
    {
        public AdminDTO() { }

        public AdminDTO(int adminId, string name, string email,
                        string phoneNumber, string address,
                        string img, long? accountId)
        {
            AdminId = adminId;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Img = img;
            AccountId = accountId;
        }

        public int AdminId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Img { get; set; }
        public long? AccountId { get; set; }
    }
}
