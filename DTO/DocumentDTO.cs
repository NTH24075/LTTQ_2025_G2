using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class DocumentDTO
    {
        public DocumentDTO() { }

        public DocumentDTO(long id, string name, string describe,
                           bool flag, long? teacherId)
        {
            DocumentId = id;
            DocumentName = name;
            DocumentDescribe = describe;
            Flag = flag;
            TeacherId = teacherId;
        }

        public long DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentDescribe { get; set; }
        public bool Flag { get; set; }
        public long? TeacherId { get; set; }
    }
}
