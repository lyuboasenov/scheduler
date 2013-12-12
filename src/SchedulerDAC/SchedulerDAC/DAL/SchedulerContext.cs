using SchedulerDAC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerDAC.DAL
{
    public class SchedulerContext : DbContext
    {
        public SchedulerContext() : base("schedulerContext")
        {

        }

		public SchedulerContext(string nameOrConnectionString)
			: base(nameOrConnectionString)
		{

		}

		public DbSet<Schedule> Schedule { get; set; }
		public DbSet<Enrollment> Enrollments { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<StudentGroup> StudentGroups { get; set; }
		public DbSet<Semester> Semesters { get; set; }
		public DbSet<Programme> Programmes { get; set; }
		public DbSet<CourseLesson> CourseLessons { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<TeacherAvailabilityPeriod> TeacherAvailabilityPeriods { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<Room> Rooms { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>(); 
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>(); 

			base.OnModelCreating(modelBuilder);
        }
    }
}
