using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerDAC.Models
{
    public class CourseLesson
    {
		public int Id { get; set; }
		public int CourseId { get; set; }
        public int LessonCount { get; set; }
        public LessonType LessonType { get; set; }
        public virtual Course Course { get; set; }
    }
}
