using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvelutionServiceReceiver.DTO
{
    public class SubmitExamRequestDto
    {
        public int ExamId { get; set; }
        public Guid StudentId { get; set; }
        public int SubjectId { get; set; } 
        public List<QuestionChioceSubmitDto> SelectedChoices { get; set; }
        public DateTime StartDateTime { get; set; } 
        public  DateTime EndDateTime { get; set; }
    }
}
