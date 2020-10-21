using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;

namespace OpenHack.University.Services.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int CourseID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }

        public int DepartmentID { get; set; }

        public Department Department { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<CourseAssignment> CourseAssignments { get; set; }


        public Contract.Course GetContract()
        {
            var courseContract = new Contract.Course()
            {
                CourseID = CourseID,
                Credits = Credits,
                DepartmentID = DepartmentID,
                Title = Title,
                Enrollments = new List<Contract.Enrollment>()
            };

            //foreach(var enrollment in Enrollments)
            //{
            //    courseContract.Enrollments.Add(enrollment.GetContract());
            //}

            return courseContract;

        }
    }
}