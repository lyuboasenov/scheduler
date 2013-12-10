using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulerDAC.Models
{
    public class Room
    {
		public Room()
		{

		}

        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public LessonType HostedActivitie { get; set; }
    }
}
