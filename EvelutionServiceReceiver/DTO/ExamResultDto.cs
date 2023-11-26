﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvelutionServiceReceiver.DTO
{
    public class ExamResultDto
    {
        public int Grade { get; set; }
        public DateTime endTime { get; set; } = DateTime.Now;
        public DateTime startTime { get; set; } 
        public int ExamId { get; set; }
        public string StudentId { get; set; }
        
    }
}
