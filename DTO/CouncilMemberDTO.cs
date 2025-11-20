using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class CouncilMemberDTO
    {
        public CouncilMemberDTO() { }

        public CouncilMemberDTO(long id, long councilId,
                                long teacherId, string role)
        {
            CouncilMemberId = id;
            CouncilId = councilId;
            TeacherId = teacherId;
            MemberRole = role;
        }

        public long CouncilMemberId { get; set; }
        public long CouncilId { get; set; }
        public long TeacherId { get; set; }
        public string MemberRole { get; set; }
    }
}
