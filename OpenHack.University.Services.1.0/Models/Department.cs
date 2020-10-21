using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenHack.University.Services.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        public int? InstructorID { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public Instructor Administrator { get; set; }
        public ICollection<Course> Courses { get; set; }


        public Services.Contract.Department GetContract()
        {
            var contractDepartment = new Contract.Department()
            {
                Budget = Budget,
                DepartmentID = DepartmentID,
                InstructorID = InstructorID,
                Name = Name,
                StartDate = StartDate,
                Courses = new List<Contract.Course>(),
                Administrator = Administrator.GetContract()
            };

            foreach(var course in Courses)
            {
                contractDepartment.Courses.Add(course.GetContract());
            }

            return contractDepartment;
        }
    }


}