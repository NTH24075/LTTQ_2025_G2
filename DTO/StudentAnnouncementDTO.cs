using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class StudentAnnouncementDTO
    {
        public StudentAnnouncementDTO() { }

        public StudentAnnouncementDTO(long id, long? studentId, long? announcementId)
        {
            StudentAnnouncementId = id;
            StudentId = studentId;
            AnnouncementId = announcementId;
        }

        public long StudentAnnouncementId { get; set; }
        public long? StudentId { get; set; }
        public long? AnnouncementId { get; set; }
    }
}
