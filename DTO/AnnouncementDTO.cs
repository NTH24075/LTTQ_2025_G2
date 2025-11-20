using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class AnnouncementDTO
    {
        public AnnouncementDTO() { }

        public AnnouncementDTO(long id, string name, string content)
        {
            AnnouncementId = id;
            AnnouncementName = name;
            AnnouncementContent = content;
        }

        public long AnnouncementId { get; set; }
        public string AnnouncementName { get; set; }
        public string AnnouncementContent { get; set; }
    }
}
