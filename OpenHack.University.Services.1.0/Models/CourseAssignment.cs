namespace OpenHack.University.Services.Models
{
    public class CourseAssignment
    {
        public int InstructorID { get; set; }
        public int CourseID { get; set; }
        public Instructor Instructor { get; set; }
        public Course Course { get; set; }

        public Contract.CourseAssignment GetContract()
        {
            var contractCourseAssignment = new Contract.CourseAssignment()
            {
                CourseID = CourseID,
                InstructorID = InstructorID,
                Course = Course.GetContract(),
                Instructor = Instructor.GetContract(false)
            };

            return contractCourseAssignment;
        }
    }
}