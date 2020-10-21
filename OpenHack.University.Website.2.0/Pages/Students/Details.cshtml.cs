using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenHack.University.Models;

namespace OpenHack.University.Pages.Students
{
    public class DetailsModel : PageModel
    {

        private readonly string webserviceUrl;
       
        public DetailsModel(IConfiguration configuration)
        {
            webserviceUrl = configuration.GetSection("WebServiceSettings:StudentUrl").Value;
        }

        public Student Student { get; set; }


        #region snippet_OnGetAsync
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var studentClient = new Services.Student.StudentClient();
            studentClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(webserviceUrl);

            if (id == null)
            {
                return NotFound();
            }

            //getbyid()
            var studentContract = await studentClient.GetByIdAsync(id.Value);
            Student = new Student()
            {
                EnrollmentDate = studentContract.EnrollmentDate,
                FirstMidName = studentContract.FirstMidName,
                ID = studentContract.ID,
                LastName = studentContract.LastName,
                Enrollments = new List<Enrollment>()
            };

            if(studentContract.Enrollments != null)
            {
                foreach(var enrollmentContract in studentContract.Enrollments)
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
        #endregion
    }
}
