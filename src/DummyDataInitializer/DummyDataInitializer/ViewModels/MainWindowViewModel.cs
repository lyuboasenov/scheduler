namespace DummyDataInitializer.ViewModels
{
	using Catel.IoC;
	using Catel.MVVM;
	using Catel.MVVM.Services;
	using SchedulerDAC.DAL;
	using SchedulerDAC.Models;
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.IO;
	using System.Text;
	using System.Text.RegularExpressions;
	using System.Collections.ObjectModel;
	using Catel.Data;
	using DummyDataInitializer.Models;
using Scheduler.Logic;

    /// <summary>
    /// MainWindow view model.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields
        private IServiceLocator serviceLocator;
		private Random rand;
		private int currentSemester;
		private ScheduleMaker scheduleMaker;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel(IServiceLocator serviceLocator)
            : base(serviceLocator)
        {
            this.serviceLocator = serviceLocator;
			InitializeCommand = new Command(OnInitializeCommandExecute);
			MakeScheduleCommand = new Command(OnMakeScheduleCommandExecute);

			ProgrammeFiles = new ObservableCollection<ProgrammeFile>();
			foreach(var filename in Directory.GetFiles(Environment.CurrentDirectory, "programme*.txt"))
			{
				ProgrammeFiles.Add(new ProgrammeFile(filename));
			}
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the title of the view model.
        /// </summary>
        /// <value>The title.</value>
        public override string Title { get { return "View model title"; } }

        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets

		/// <summary>
		/// Gets or sets the property value.
		/// </summary>
		public ObservableCollection<ProgrammeFile> ProgrammeFiles
		{
			get { return GetValue<ObservableCollection<ProgrammeFile>>(ProgrammeFilesProperty); }
			set { SetValue(ProgrammeFilesProperty, value); }
		}

		/// <summary>
		/// Register the ProgrammesFiles property so it is known in the class.
		/// </summary>
		public static readonly PropertyData ProgrammeFilesProperty = RegisterProperty("ProgrammeFiles", typeof(ObservableCollection<ProgrammeFile>), null);

        #endregion

        #region Commands
        /// <summary>
		/// Gets the InitializeCommand command.
		/// </summary>
		public Command InitializeCommand { get; private set; }

		/// <summary>
		/// Gets the MakeScheduleCommand command.
		/// </summary>
		public Command MakeScheduleCommand { get; private set; }
		
        #endregion

        #region Methods
		#region Dummy data initialization
		/// <summary>
		/// Method to invoke when the InitializeCommand command is executed.
		/// </summary>
		private void OnInitializeCommandExecute()
		{
			LoadDummyData();
		}

		private void LoadDummyData()
		{
			var context = new SchedulerContext();
			rand = new Random();
			currentSemester = rand.Next(0, 1);
			var start = DateTime.Now;
			foreach (var file in ProgrammeFiles)
			{
				if(file.IsSelected)
				{
					ImportProgrammeData(file.Filename, context);
				}
				
			}
			GenerateStudents(context);
			GenerateEnrollments(context);
			GenerateRooms(context);
			var end = DateTime.Now;
			var messageService = (IMessageService)serviceLocator.GetService(typeof(IMessageService));
			messageService.Show("Time taken to initialize:" + end.Subtract(start).ToString());
		}

		private void GenerateRooms(SchedulerContext context)
		{
			for(int i = 0; i < context.Programmes.Count(); i++)
			{
				var room = new Room() { Name = string.Format("LEC{0:00}", i), Capacity = rand.Next(200, 300) };
				room.HostedActivitie = LessonType.Lecture;
				context.Rooms.Add(room);				
			}

			for (int i = 0; i < context.Programmes.Count() * 5; i++)
			{
				var room = new Room() { Name = string.Format("EXE{0:00}", i), Capacity = rand.Next(30, 50) };
				room.HostedActivitie = LessonType.Exercise;
				context.Rooms.Add(room);
			}

			for (int i = 0; i < context.Programmes.Count() * 3; i++)
			{
				var room = new Room() { Name = string.Format("LAB{0:00}", i), Capacity = rand.Next(30, 50) };
				room.HostedActivitie = LessonType.LabExercise;
				context.Rooms.Add(room);
			}

			context.SaveChanges();
		}

		private void GenerateEnrollments(SchedulerContext context)
		{
			foreach(var semester in context.Semesters)
			{
				if((semester.SemesterNumber - 1) % 2 == currentSemester)
				{
					foreach(var course in semester.Courses)
					{
						foreach (var courseLesson in course.Lessons)
						{
							if (courseLesson.LessonType == LessonType.Exercise || courseLesson.LessonType == LessonType.LabExercise)
							{
								var groups = courseLesson.Course.Semester.StudentGroups;
								foreach (var group in groups)
								{
									var teacherId = GetRandomTeacherId(courseLesson.CourseId, context);
									for (int i = 0; i < courseLesson.LessonCount; i++)
									{
										var enrollment = new Enrollment()
										{
											CourseId = courseLesson.CourseId,
											LessonType = courseLesson.LessonType,
											TeacherId = teacherId
										};
										context.Enrollments.Add(enrollment);
										context.SaveChanges();

										group.Enrollments.Add(enrollment);
										context.SaveChanges();
										foreach (var student in group.Students)
										{
											student.Enrollments.Add(enrollment);
										}
										context.SaveChanges();
									}
								}
							}
							else
							{
								var t1TeacherId = GetRandomTeacherId(courseLesson.CourseId, context);
								var t2TeacherId = GetRandomTeacherId(courseLesson.CourseId, context);
								for (int i = 0; i < courseLesson.LessonCount; i++)
								{
									var torrent1 = courseLesson.Course.Semester.StudentGroups.Where(sg => sg.Number <= 4);
									var enrollment1 = new Enrollment()
									{
										CourseId = courseLesson.CourseId,
										LessonType = courseLesson.LessonType,
										TeacherId = t1TeacherId
									};
									context.Enrollments.Add(enrollment1);
									context.SaveChanges();

									foreach (var group in torrent1)
									{
										group.Enrollments.Add(enrollment1);
										foreach (var student in group.Students)
										{
											student.Enrollments.Add(enrollment1);
										}
										context.SaveChanges();
									}

									var torrent2 = courseLesson.Course.Semester.StudentGroups.Where(sg => sg.Number <= 4);
									var enrollment2 = new Enrollment()
									{
										CourseId = courseLesson.CourseId,
										LessonType = courseLesson.LessonType,
										TeacherId = t2TeacherId
									};
									context.Enrollments.Add(enrollment2);
									context.SaveChanges();

									foreach (var group in torrent2)
									{
										group.Enrollments.Add(enrollment2);
										foreach (var student in group.Students)
										{
											student.Enrollments.Add(enrollment2);
										}
										context.SaveChanges();
									}
								}
							}
						}
					}
				}
			}
		}

		private int GetRandomTeacherId(int courseId, SchedulerContext context)
		{
			var teachers = context.Teachers.Where(t => t.Courses.Count(c => c.Id == courseId) > 0);
			int index = rand.Next(0, teachers.Count() - 1);
			return teachers.AsEnumerable().ElementAt(index).Id;
		}

		private void GenerateStudents(SchedulerContext context)
		{
			foreach(var semester in context.Semesters)
			{
				if((semester.SemesterNumber - 1) % 2 == currentSemester)
				{
					int studentNumberCounter = 0;
					var numberPrefix = GetStudentNumberPrefix(semester.Programme.Name);
					for(int i = 0; i < rand.Next(4, 8); i++)
					{
						var group = new StudentGroup() { Number = i + 1, SemesterId = semester.Id };
						context.StudentGroups.Add(group);
						context.SaveChanges();
						for(int j = 0; j < rand.Next(25, 35); j++)
						{
							var student = new Student() { StudentGroupId = group.Id, Number = string.Format("{0}{1:00000}", numberPrefix, studentNumberCounter++) };
							context.Students.Add(student);
						}
						context.SaveChanges();
					}
				}
			}
		}

		private string GetStudentNumberPrefix(string programmeName)
		{
			StringBuilder prefix = new StringBuilder();
			string[] words = programmeName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
			foreach(var word in words)
			{
				if(word.Trim().Length > 1)
				{
					prefix.Append(word.Trim().Substring(0, 1));
				}
			}

			return prefix.ToString();			
		}
		
		private void ImportProgrammeData(string filename, SchedulerContext context)
		{
			using (FileStream stream = File.OpenRead(filename))
			{
				using (StreamReader reader = new StreamReader(stream))
				{
					//adds programme
					Programme programme = new Programme();
					programme.Name = reader.ReadLine().Replace("\"", string.Empty).Trim();
					context.Programmes.Add(programme);
					context.SaveChanges();
					
					string line;
					Regex regex = new Regex(@"^(?<code>([^\t]+\t){4})(?<name>[^\t]+)\t(?<semester>[^\t]+)\t(?<credits>[^\t]+)\t(?<lectures>[^\+]+)\+(?<exercises>[^\+]+)\+(?<labs>[^\+]+)\s*$");
					var semesters = new List<Semester>();
					int semesterNumber = 0;
					string lastSemesterId = string.Empty;
					bool skip = false;
					while((line = reader.ReadLine()) != null)
					{
						var match = regex.Match(line);
						if (match.Success)
						{
							var groups = match.Groups;
							//adds semester
							if (string.Compare(lastSemesterId, groups["semester"].Value.Trim()) != 0)
							{
								if(semesterNumber % 2 == currentSemester)
								{
									semesterNumber++;
									lastSemesterId = groups["semester"].Value.Trim();

									var semester = new Semester() { ProgrammeId = programme.Id, SemesterNumber = semesterNumber };
									context.Semesters.Add(semester);
									context.SaveChanges();
									semesters.Add(semester);
									skip = false;
								}
								else
								{
									skip = true;
								}
							}

							if (skip) continue;
													
							//Adds course
							var course = new Course()
							{
								Code = groups["code"].Value.Replace("	", string.Empty).Replace(" ", string.Empty),
								Name = groups["name"].Value.Trim(),
								SemesterId = semesters[semesterNumber - 1].Id,
								IsMandatory = true
							};
							context.Courses.Add(course);
							context.SaveChanges();

							//adds teachers
							for(int i = 0; i < rand.Next(2, 4); i++)
							{
								Teacher teacher = new Teacher() { Name = "Преподавател " + course.Name + "ов" };
								teacher.Courses.Add(course);
								context.Teachers.Add(teacher);
								context.SaveChanges();

								//adds teacher availability periods
								for(int j = 0; j < rand.Next(4, 7); j++)
								{
									int start = rand.Next(7, 14);
									int end = rand.Next(start, 21);
									DayOfWeek day = (DayOfWeek)(rand.Next() % 7);
									var period = new TeacherAvailabilityPeriod() { Start = start, End = end, DayOfWeek = day, TeacherId = teacher.Id };
									context.TeacherAvailabilityPeriods.Add(period);
								}
								context.SaveChanges();
							}


							//adds course lessons
							int lectures = Convert.ToInt32(groups["lectures"].Value.Trim());
							int exercises = Convert.ToInt32(groups["exercises"].Value.Trim());
							int labs = Convert.ToInt32(groups["labs"].Value.Trim());
							if (lectures > 0)
							{
								var courseLesson = new CourseLesson() { CourseId = course.Id, LessonType = LessonType.Lecture, LessonCount = lectures };
								context.CourseLessons.Add(courseLesson);
							}
							if (exercises > 0)
							{
								var courseLesson = new CourseLesson() { CourseId = course.Id, LessonType = LessonType.Exercise, LessonCount = exercises };
								context.CourseLessons.Add(courseLesson);
							}
							if (labs > 0)
							{
								var courseLesson = new CourseLesson() { CourseId = course.Id, LessonType = LessonType.LabExercise, LessonCount = labs };
								context.CourseLessons.Add(courseLesson);
							}
							context.SaveChanges();
						}
					}
				}
			}
		}
		#endregion
		
		/// <summary>
		/// Method to invoke when the MakeScheduleCommand command is executed.
		/// </summary>
		private void OnMakeScheduleCommandExecute()
		{
			scheduleMaker = new ScheduleMaker();
			scheduleMaker.MakeSchedule();
		}
        #endregion
    }
}
