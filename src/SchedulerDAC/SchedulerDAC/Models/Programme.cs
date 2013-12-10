using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerDAC.Models
{
    public class Programme
    {
		public Programme()
		{
			Semesters = new List<Semester>();
		}

        public int Id { get; set; }
        public string Name { get; set; }
        public int SemesterCount { get; set; }

        public virtual ICollection<Semester> Semesters { get; set; }
    }
}
