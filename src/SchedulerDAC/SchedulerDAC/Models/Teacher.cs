using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerDAC.Models
{
    public class Teacher
    {
		public Teacher()
		{
			Availability = new List<TeacherAvailabilityPeriod>();
			Courses = new List<Course>();
		}

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TeacherAvailabilityPeriod> Availability { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
