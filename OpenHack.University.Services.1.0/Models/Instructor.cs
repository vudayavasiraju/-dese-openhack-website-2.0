using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenHack.University.Services.Models
{
    public class Instructor
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + ", " + FirstMidName; }
          
        }

        public ICollection<CourseAssignment> CourseAssignments { get; set; }
        public OfficeAssignment OfficeAssignment { get; set; }

        public Contract.Instructor GetContract(bool child = true)
        {
            var contractInstructor = new Contract.Instructor()
            {
                FirstMidName = FirstMidName,
                FullName = FullName,
                HireDate = HireDate,
                ID = ID,
                LastName = LastName,
                CourseAssignments = new List<Contract.CourseAssignment>(),
                OfficeAssignment = OfficeAssignment != null ? OfficeAssignment.GetContract() : null
            };
            if (child)
            {
                if (CourseAssignments != null)
                {
                    foreach (var courseAssignment in CourseAssignments)
                    {
                        contractInstructor.CourseAssignments.Add(courseAssignment.GetContract());
                    }
                }
            }
            return contractInstructor;
        }
    }
}