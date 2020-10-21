
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenHack.University.Models;
using OpenHack.University.Models.SchoolViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenHack.University.Pages.Instructors
{
    public class IndexModel : PageModel
    {


        private readonly string webserviceUrl;
        public IndexModel(IConfiguration configuration)
        {
            webserviceUrl = configuration.GetSection("WebServiceSettings:InstructorUrl").Value;
        }

        public InstructorIndexData InstructorData { get; set; }
        public int InstructorID { get; set; }
        public int CourseID { get; set; }

        public async Task OnGetAsync(int? id, int? courseID)
        {
            InstructorData = new InstructorIndexData();

            Services.Instructor.InstructorClient instructorClient = new OpenHack.University.Services.Instructor.InstructorClient();
            instructorClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(webserviceUrl);
            var instructorListContract = await instructorClient.SearchAsync();

            var instructors = new List<Instructor>();

            foreach (var instructorContract in instructorListContract)
            {
                var instructor = new Instructor()
                {
                    FirstMidName = instructorContract.FirstMidName,
                    HireDate = instructorContract.HireDate,
                    ID = instructorContract.ID,
                    LastName = instructorContract.LastName,
                    CourseAssignments = new List<CourseAssignment>(),
                    OfficeAssignment = instructorContract.OfficeAssignment != null ? new OfficeAssignment() { Location = instructorContract.OfficeAssignment.Location} : null
                };

                foreach( var courseAssignmentContract in instructorContract.CourseAssignments)
                {
                    var courseAssignment = new CourseAssignment()
                    {
                        CourseID = courseAssignmentContract.CourseID,
                        InstructorID = courseAssignmentContract.InstructorID
                    };

                    if(courseAssignmentContract.Course != null)
                    {
                        courseAssignment.Course = new Course()
                        {
                            CourseID = courseAssignmentContract.Course.CourseID,
                            Title = courseAssignmentContract.Course.Title
                        };
                    }

                    instructor.CourseAssignments.Add(courseAssignment);
                }
                instructors.Add(instructor);
            }

            InstructorData.Instructors = instructors;

            if (id != null)
            {
                InstructorID = id.Value;
                Instructor instructor = InstructorData.Instructors
                    .Where(i => i.ID == id.Value).Single();
                InstructorData.Courses = instructor.CourseAssignments.Select(s => s.Course);
            }

            if (courseID != null)
            {
                CourseID = courseID.Value;
                //var selectedCourse = InstructorData.Courses
                //    .Where(x => x.CourseID == courseID).Single();
                //await _context.Entry(selectedCourse).Collection(x => x.Enrollments).LoadAsync();
                //foreach (Enrollment enrollment in selectedCourse.Enrollments)
                //{
                //    await _context.Entry(enrollment).Reference(x => x.Student).LoadAsync();
                //}
                //InstructorData.Enrollments = selectedCourse.Enrollments;
            }
        }
    }
}
