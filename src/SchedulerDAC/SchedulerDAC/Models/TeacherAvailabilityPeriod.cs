using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerDAC.Models
{
    public class TeacherAvailabilityPeriod
    {
		public int Id { get; set; }
        public int TeacherId { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
		public DayOfWeek DayOfWeek { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
