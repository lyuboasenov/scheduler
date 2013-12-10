using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerDAC.Models
{
    public class Course
    {
		public Course()
		{
			Teachers = new List<Teacher>();
			Lessons = new List<CourseLesson>();
		}

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMandatory { get; set; }
		public string Code { get; set; }
		public int SemesterId { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<CourseLesson> Lessons { get; set; }
		public virtual Semester Semester { get; set; }
    }
}
