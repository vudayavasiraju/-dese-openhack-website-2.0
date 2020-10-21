#region snippet_All
using OpenHack.University.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace OpenHack.University.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly string webserviceUrl;

        public DeleteModel(IConfiguration configuration)
        {
            webserviceUrl = configuration.GetSection("WebServiceSettings:StudentUrl").Value;
        }

        [BindProperty]
        public Student Student { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {

            var studentClient = new Services.Student.StudentClient();
            studentClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(webserviceUrl);

            if (id == null)
            {
                return NotFound();
            }


            var studentContract = await studentClient.GetByIdAsync(id.Value);
            Student = new Student()
            {
                EnrollmentDate = studentContract.EnrollmentDate,
                FirstMidName = studentContract.FirstMidName,
                ID = studentContract.ID,
                LastName = studentContract.LastName,
                Enrollments = new List<Enrollment>()
            };

            if (studentContract.Enrollments != null)
            {
                foreach (var enrollmentContract in studentContract.Enrollments)
                {
                    var enrollment = new Enrollment()
                    {
                        CourseID = enrollmentContract.CourseID,
                        Grade = enrollmentContract.Grade,
                        StudentID = enrollmentContract.StudentID,
                        EnrollmentID = enrollmentContract.EnrollmentID,
                        Course = new Course()
                    };

                    if (enrollmentContract.Course != null)
                    {
                        enrollment.Course = new Course()
                        {
                            CourseID = enrollmentContract.CourseID,
                            Title = enrollmentContract.Course.Title
                        };
                    }

                    Student.Enrollments.Add(enrollment);
                }
            }

            if (Student == null)
            {
                return NotFound();
            }

            return Page();

        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentClient = new OpenHack.University.Services.Student.StudentClient();
            studentClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(webserviceUrl);

            var studentToUpdateContract = await studentClient.GetByIdAsync(id.Value);


            if (studentToUpdateContract == null)
            {
                return NotFound();
            }

            var studentToUpdate = new Student()
            {
                ID = studentToUpdateContract.ID,
                LastName = studentToUpdateContract.LastName,
                FirstMidName = studentToUpdateContract.FirstMidName,
                EnrollmentDate = studentToUpdateContract.EnrollmentDate
            };

            await studentClient.DeleteAsync(studentToUpdate.ID);
            return RedirectToPage("./Index");
        }
    }
}
#endregion