using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvelutionServiceReceiver.DTO
{
    public class QuestionWithChoicesDto
    {
 
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<ChioceDto> Choices { get; set; }

    }
}
