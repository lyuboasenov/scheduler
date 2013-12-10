using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerDAC.Models
{
	public class StudentGroup
	{
		public StudentGroup()
		{
			Students = new List<Student>();
			Enrollments = new List<Enrollment>();
		}

		public int Id { get; set; }
		public int Number { get; set; }
		public int SemesterId { get; set; }

		public virtual Semester Semester { get; set; }
		public virtual ICollection<Student> Students { get; set; }
		public virtual ICollection<Enrollment> Enrollments { get; set; }
	}
}
