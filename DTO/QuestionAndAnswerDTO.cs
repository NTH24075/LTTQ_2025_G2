using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_G2_2025.DTO
{
    public class QuestionAndAnswerDTO
    {
        public QuestionAndAnswerDTO() { }

        public QuestionAndAnswerDTO(long id, string title, string content,
                                    string reply, long? studentId)
        {
            QuestionAndAnswerId = id;
            QuestionAndAnswerTitle = title;
            QuestionAndAnswerContent = content;
            QuestionAndAnswerReply = reply;
            StudentId = studentId;
        }

        public long QuestionAndAnswerId { get; set; }
        public string QuestionAndAnswerTitle { get; set; }
        public string QuestionAndAnswerContent { get; set; }
        public string QuestionAndAnswerReply { get; set; }
        public long? StudentId { get; set; }
    }
}
