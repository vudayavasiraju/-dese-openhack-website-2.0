using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Management.Instrumentation;

namespace OpenHack.University.Services.Models
{
   
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public int Grade { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }

        public Contract.Enrollment GetContract()
        {
            var contractEnrollment = new Contract.Enrollment()
            {
                CourseID = CourseID,
                EnrollmentID = EnrollmentID,
                Grade = Grade.ToString(),
                StudentID = StudentID,
                Course = Course.GetContract()   
            };


            return contractEnrollment;
        }
    }
}
