using Catel.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyDataInitializer.Models
{
	/// <summary>
	/// ProgrammeFile model which fully supports serialization, property changed notifications,
	/// backwards compatibility and error checking.
	/// </summary>
	public class ProgrammeFile : ModelBase
	{
		#region Fields
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new object from scratch.
		/// </summary>
		public ProgrammeFile() { }

		public ProgrammeFile(string filename) 
		{
			Filename = filename;
			Name = filename.Substring(filename.LastIndexOf(Path.DirectorySeparatorChar) + 1);
		}

		#endregion

		#region Properties
		// TODO: Define your custom properties here using the modelprop code snippet
		/// <summary>
		/// Gets or sets the property value.
		/// </summary>
		public string Name
		{
			get { return GetValue<string>(NameProperty); }
			set { SetValue(NameProperty, value); }
		}

		/// <summary>
		/// Register the Name property so it is known in the class.
		/// </summary>
		public static readonly PropertyData NameProperty = RegisterProperty("Name", typeof(string), null);

		/// <summary>
		/// Gets or sets the property value.
		/// </summary>
		public string Filename
		{
			get { return GetValue<string>(FilenameProperty); }
			set { SetValue(FilenameProperty, value); }
		}

		/// <summary>
		/// Register the FullFilename property so it is known in the class.
		/// </summary>
		public static readonly PropertyData FilenameProperty = RegisterProperty("Filename", typeof(string), null);

		/// <summary>
		/// Gets or sets the property value.
		/// </summary>
		public bool IsSelected
		{
			get { return GetValue<bool>(IsSelectedProperty); }
			set { SetValue(IsSelectedProperty, value); }
		}

		/// <summary>
		/// Register the IsSelected property so it is known in the class.
		/// </summary>
		public static readonly PropertyData IsSelectedProperty = RegisterProperty("IsSelected", typeof(bool), null);

		#endregion

		#region Methods
		/// <summary>
		/// Validates the field values of this object. Override this method to enable
		/// validation of field values.
		/// </summary>
		/// <param name="validationResults">The validation results, add additional results to this list.</param>
		protected override void ValidateFields(List<IFieldValidationResult> validationResults)
		{
		}

		/// <summary>
		/// Validates the field values of this object. Override this method to enable
		/// validation of field values.
		/// </summary>
		/// <param name="validationResults">The validation results, add additional results to this list.</param>
		protected override void ValidateBusinessRules(List<IBusinessRuleValidationResult> validationResults)
		{
		}
		#endregion
	}
}
