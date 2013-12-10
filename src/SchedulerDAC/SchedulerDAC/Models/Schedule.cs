using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerDAC.Models
{
    public class Schedule
    {
		public int Id { get; set; }
        public int RoomId { get; set; }
        public int EnrollmentId { get; set; }

        public virtual Enrollment Enrollment { get; set; }
        public virtual Room Room { get; set; }
    }
}
