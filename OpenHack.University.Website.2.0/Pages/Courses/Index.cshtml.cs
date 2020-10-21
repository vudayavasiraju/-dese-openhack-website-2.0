
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenHack.University.Models;
using Microsoft.Extensions.Configuration;

namespace OpenHack.University.Pages.Courses
{
    public class IndexModel : PageModel
    {

        private readonly string webserviceUrl;

        public IndexModel(IConfiguration configuration)
        {
            webserviceUrl = configuration.GetSection("WebServiceSettings:CourseUrl").Value;
        }


        public IList<Course> Courses { get; set; }

        public async Task OnGetAsync()
        {
            var courseClient = new Services.Course.CourseClient();
            courseClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(webserviceUrl);

            var courseListContract = await courseClient.SearchAsync();

            Courses = new List<Course>();

            foreach (var courseContract in courseListContract)
            {
                var course = new Course()
                {
                    CourseID = courseContract.CourseID,
                    Credits = courseContract.Credits,
                    DepartmentID = courseContract.DepartmentID,
                    Title = courseContract.Title
                };

                if (courseContract.Department != null)
                {
                    course.Department = new Department()
                    {
                        Name = courseContract.Department.Name
                    };
                }
                Courses.Add(course);
            }
        }

            
    }
}
