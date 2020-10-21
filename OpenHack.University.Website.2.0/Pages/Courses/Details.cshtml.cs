
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OpenHack.University.Models;

using System.Threading.Tasks;

namespace OpenHack.University.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly string webserviceUrl;

        public DetailsModel(IConfiguration configuration)
        {
            webserviceUrl = configuration.GetSection("WebServiceSettings:CourseUrl").Value;
        }

        public Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Services.Course.CourseClient courseClient = new OpenHack.University.Services.Course.CourseClient();
            var courseContract = await courseClient.GetByIdAsync(id.Value);
            courseClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(webserviceUrl);

            Course = new Course() { CourseID = courseContract.CourseID, Credits = courseContract.Credits, Title = courseContract.Title };
            if (courseContract.Department != null)
            {
                Course.Department = new Department()
                {
                    Name = courseContract.Department.Name
                };
            }

            if (Course == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
