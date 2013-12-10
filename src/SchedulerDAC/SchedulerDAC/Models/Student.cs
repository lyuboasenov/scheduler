using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerDAC.Models
{
    public class Student
    {
		public Student()
		{
			Enrollments = new List<Enrollment>();
		}

        public int Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
		public int StudentGroupId { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
		public virtual StudentGroup Group { get; set; }
    }
}
