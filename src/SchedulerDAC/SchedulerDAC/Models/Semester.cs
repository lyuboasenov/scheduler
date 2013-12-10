using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerDAC.Models
{
    public class Semester
    {
		public Semester()
		{
			Courses = new List<Course>();
			StudentGroups = new List<StudentGroup>();
		}

        public int Id { get; set; }
        public int SemesterNumber { get; set; }
        public int ProgrammeId { get; set; }

        public virtual Programme Programme { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
		public virtual ICollection<StudentGroup> StudentGroups { get; set; }
    }
}
