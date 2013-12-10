using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerDAC.Models
{
    public class Enrollment
    {
		public Enrollment()
		{
			Students = new List<Student>();
			StudentGroups = new List<StudentGroup>();
		}
        public int Id { get; set; }
		public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public LessonType LessonType { get; set; }

		public virtual Course Course { get; set; }
        public virtual Teacher Teacher { get; set; }
		public virtual ICollection<StudentGroup> StudentGroups { get; set; }
		public virtual ICollection<Student> Students { get; set; }
    }
}
